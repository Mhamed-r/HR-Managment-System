using AutoMapper;
using HR.ManagmentSystem.Helpers;
using HR.ManagmentSystem.Services;
using HR.ManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebApplication1.Models;
using WebApplication1.Services;

namespace HR.ManagmentSystem.Controllers
{
    public class EmployeeSalaryController(EmployeeService employeeService, IDepartmentService departmentService,
        IGeneralSettingsService generalSettingsService, IpublicHolidays  publicHolidaysService, IAttendanceService attendanceService, IMapper mapper) : Controller
    {
        private readonly IMapper _mapper=mapper;

        private readonly EmployeeService _employeeService = employeeService;
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly IGeneralSettingsService _generalSettingsService = generalSettingsService;
        private readonly IpublicHolidays _publicHolidaysService = publicHolidaysService;
        private readonly IAttendanceService _attendanceService = attendanceService;

        public async Task<IActionResult> Index()
        {

            var Employees =await _employeeService.GetEmployeeListAsync();
            List<EmployeeSalaryViewModel> EmployeeViewModel = _mapper.Map<List<EmployeeSalaryViewModel>>(Employees);

            return View(EmployeeViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeReport(string employeeId, int? month, int? year)
        {
            int selectedYear = year ?? DateTime.Now.Year;
            int selectedMonth = month ?? DateTime.Now.Month;
            var generalSettings = await _generalSettingsService.GetGeneralSettings();
            var publicHolidays = await _publicHolidaysService.GetPublicHoliday();
            var publicHolidaysInMonth = publicHolidays
               .Where(ph => ph.Date.Year == selectedYear && ph.Date.Month == selectedMonth)
               .ToList();
            int totalWorkingDays = WorkingDaysCalculate.CalculateWorkingDays(selectedYear, selectedMonth, generalSettings, publicHolidaysInMonth);
            var employees = await _employeeService.GetEmployeeListAsync();
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            var reportData = new List<EmployeeSalaryViewModel>();
            var attendanceRecords = _attendanceService.GetAttendanceForEmployee(employeeId, selectedYear, selectedMonth);
            int totalPresentDays = attendanceRecords.Count();
            int totalAbsentDays = totalWorkingDays - totalPresentDays;


            // Calculate OverTimeperHour
            decimal OverHourRate = generalSettings.OvertimeRatePerHour;
            decimal Salary = employee.Salary;
            decimal SalaryperDay = Salary / totalWorkingDays;
            TimeSpan TotalOverHour = employee.TimeOut - employee.TimeIn;
            decimal OverHour = TotalOverHour.Hours + (TotalOverHour.Minutes / 60);
            decimal AmountPerHour = SalaryperDay / OverHour;
            decimal TotalOverHourSalary = AmountPerHour * OverHourRate;
            // Calculate Deduction
            decimal DeductionHourRata = generalSettings.DeductionRatePerHour;
            decimal TotalDeductionSalary = DeductionHourRata * AmountPerHour;
            // Calculate Absant Days
            decimal AbsantDaySalary = SalaryperDay * totalAbsentDays;

            // Calculate Total Salary
            decimal TotalSalary = Salary + TotalOverHourSalary - TotalDeductionSalary - AbsantDaySalary;
            //Map to ViewModel
            EmployeeSalaryViewModel salaryViewModel = _mapper.Map<EmployeeSalaryViewModel>(employee);

            salaryViewModel.PresentDays = totalPresentDays;
            salaryViewModel.AbsentDays = totalAbsentDays;
            salaryViewModel.OvertimeHours = OverHour;
            salaryViewModel.DeductionHours = DeductionHourRata;
            salaryViewModel.TotalOvertimePay = TotalOverHourSalary;
            salaryViewModel.TotalDeduction = TotalDeductionSalary;
            salaryViewModel.NetSalary = TotalSalary;

            return View(salaryViewModel);
        }
    }
}
