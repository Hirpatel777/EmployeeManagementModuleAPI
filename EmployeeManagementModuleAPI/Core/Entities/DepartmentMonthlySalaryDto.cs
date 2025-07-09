namespace EmployeeManagementModuleAPI.Core.Entities
{
    public class DepartmentMonthlySalaryDto
    {
        public string DepartmentName { get; set; }
        public int Month { get; set; } // 1 = Jan, 12 = Dec
        public decimal TotalSalary { get; set; }
    }
}
