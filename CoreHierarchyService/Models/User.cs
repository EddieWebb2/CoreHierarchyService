using CoreHierarchyService.Types;

namespace CoreHierarchyService.Models
{
    public class User
    {
        public Salutation Title { get; set; }
        public string EmployeeFirstNames { get; set; }
        public string EmployeeSurname { get; set; }
    }
}
