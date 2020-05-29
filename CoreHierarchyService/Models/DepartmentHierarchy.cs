namespace CoreHierarchyService.Models
{
    public class DepartmentHierarchy
    {
        public string ChildId { get; set; }
        public string ParentId { get; set; }
        public string ChildName { get; set; }
        public string Level { get; set; }
        public string Path { get; set; }
    }
}
