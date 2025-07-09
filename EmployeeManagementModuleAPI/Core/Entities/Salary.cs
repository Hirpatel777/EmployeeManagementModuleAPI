namespace EmployeeManagementModuleAPI.Core.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Employee Employee { get; set; }

       
    }
}
