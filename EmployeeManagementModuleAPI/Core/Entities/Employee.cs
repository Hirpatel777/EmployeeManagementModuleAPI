namespace EmployeeManagementModuleAPI.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public int DeptId { get; set; }
        public int CityId { get; set; }

        public Department Department { get; set; }
       public City City { get; set; }
    }
}
