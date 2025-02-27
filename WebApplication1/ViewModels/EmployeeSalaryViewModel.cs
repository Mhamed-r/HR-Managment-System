namespace HR.ManagmentSystem.ViewModels
{
    public class EmployeeSalaryViewModel
    {
        public string Emp_Name { get; set; }
        public string Dep_Name { get; set; }
        public decimal BasicSalary { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsenceDays { get; set; }
        public int OvertimeHours { get; set; }
        public decimal OvertimeAmount { get; set; }
        public int DeductionHours { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
    }
}
