var gulp = require("gulp");
var fs = require("fs");
var args = require("yargs").argv;
var bump = require("gulp-bump");
var dotnet = require("gulp-dotnet-cli");

var shell = require("gulp-shell");

//optional, lets you do .pipe(debug()) to see whats going on
var debug = require("gulp-debug");

var project = JSON.parse(fs.readFileSync("./package.json"));

var config = {
    name: project.name,
    buildNumber: args.build || "000",
    version: project.version + "." + (args.build || "000"),
    mode: args.mode || "Debug",
    output: ".build/deploy",
    publish: ".build/publish",
    deployTarget: args.deployTarget,
    releasenotesfile: "ReleaseNotes.md"
}

var octopus = {
    apiKey: 'API-FZB0DV0BTJMZNA3QYABKW4KQOM',
    host: 'http://mtg-tfs-cc:8888',
    packages: '/nuget/packages'
}

gulp.task("default", ["compile", "test"]);
gulp.task('deploy', ["publish", "createRelease"]);

gulp.task("restore", () => {
    return gulp.src(config.name + ".sln", { read: false })
        .pipe(dotnet.restore({
            configfile: "nuget.config"
        }));
});

gulp.task("compile", function () {
    return gulp.src(config.name + ".sln", { read: false })
        .pipe(dotnet.clean({
            verbosity: 'quiet'
        }))
        .pipe(dotnet.build({
            configuration: config.mode,
            version: config.version,
            noIncremental: true
        }));
});

gulp.task("test", ["compile"], () => {
    return gulp.src("**/*.Tests*.csproj", { read: false })
        .pipe(dotnet.test({
            additionalArgs: "/p:CollectCoverage=true,CoverletOutputFormat=\"json,teamcity\"",
            noBuild: true,
            configuration: config.mode
        }));
});

gulp.task("webpub", ["test"], () => {
    return gulp.src(["**/*.csproj","!**/*.Tests*.csproj"], { read: false })
        .pipe(dotnet.publish({
            configuration: config.mode,
            version: config.version,
            verbosity: 'quiet',
            output: '../' + config.output
        }));
});

gulp.task("package", ["webpub"], shell.task([
    'dotnet octo pack' +
    ' --id=' + config.name +
    ' --outFolder=' + config.publish +
    ' --basePath=' + config.output +
    ' --version=' + config.version +
    ' --releasenotesfile=' + config.releasenotesfile +
    ' --overwrite'
]));

gulp.task('publish', ["package"], function () {

    var packageName = config.name + "." + config.version + ".nupkg";

    return gulp
        .src(config.publish + "/" + packageName, { read: false })
        .pipe(debug())
        .pipe(dotnet.push({
            source: octopus.host + octopus.packages,
            apiKey: octopus.apiKey
        }));

});

gulp.task('createRelease', ["publish"], shell.task([
    'dotnet octo create-release' +
    ' --server ' + octopus.host +
    ' --apikey ' + octopus.apiKey +
    ' --project ' + config.name +
    ' --version ' + config.version +
    ' --defaultpackageversion ' + config.version +
    ' --deployto ' + config.deployTarget +
    ' --releasenotesfile ' + config.releasenotesfile
]));

gulp.task('bump:patch', function () {
    return gulp
        .src("./package.json")
        .pipe(bump({ type: "patch" }))
        .pipe(gulp.dest('./'));
});

gulp.task('bump:minor', function () {
    return gulp
        .src("./package.json")
        .pipe(bump({ type: "minor" }))
        .pipe(gulp.dest('./'));
});

gulp.task('bump:major', function () {
    return gulp
        .src("./package.json")
        .pipe(bump({ type: "major" }))
        .pipe(gulp.dest('./'));
});
