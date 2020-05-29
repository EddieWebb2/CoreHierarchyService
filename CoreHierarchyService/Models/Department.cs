namespace CoreHierarchyService.Models
{
    public class Department
    {
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string ChildId { get; set; }
        public string ChildName { get; set; }
    }
}
