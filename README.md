# CoreHierarchyService

## Intro..
The principle behind this project was to create a service based application, that also exposes a web API in Net Core 3.1.
This should be a consumer app at present, but could be extended to support a customer UI.

The service, will present a User hierarchy structure in a consumable JSON output. The details of tree level/depth and attributes/properties that will be exposed are open for discussion.
It should also take leaf node members (lowest level of detail) and feed back to source.
Validation needs to be considered along with field types and definitions...

I want to include a API UI, so have chosen Swashbuckle Swagger. 
Built my own Serilog helper package to support custom logging (so I don’t have to rely on the default logger), and included this in the build.


## TODO:- 
- Add authority provider, to protect the API extensions.
- Add ORM, either EF6 or dapper. Initial thoughts are EF6 as the migration and apply can be handled through my CI/CD once configured.
- Add structured CQRS for my logic handlers.
- Add Models & Types – May build a separate package to house these, as consumer applications may require integration. Depends on direction I want to go.
- Service to multithread a schedule – To control run times (maybe – for the time being I want to keep this as light as possible)

## Review:
Build mechanism – Do I still want to use Gulp as my build config. It’s tricky to support in Core vs Framework.
Do I want to build a custom front end for this in ASP/React, as opposed to using API documenter, Swagger.
Dependency Injection overuse could be a issue here especially as bit service and API rely on this for start-up. No need for Structure Map (yay), but will require a registry for core service and API components.


## Reasources: 
Great walkthrough of setting up a service.
- https://www.youtube.com/watch?v=PzrTiz_NRKA
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio
- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-3.1&tabs=visual-studio

Issue investigation.
- https://stackoverflow.com/questions/58097568/ihostbuilder-does-not-contain-a-definition-for-configurewebhostdefaults
- https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/host-and-deploy/windows-service/samples/3.x/WebAppServiceSample/Program.cs
