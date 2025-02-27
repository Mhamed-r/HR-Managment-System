using HR.ManagmentSystem.Services;
using HR.ManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace HR.ManagmentSystem.Controllers
{
    public class EmployeeSalaryController(EmployeeService employeeService, DepartmentService departmentService, GeneralSettingsService generalSettingsService, PublicHolidays publicHolidaysService) : Controller
    {

        private readonly EmployeeService _employeeService = employeeService;
        private readonly DepartmentService _departmentService = departmentService;
        private readonly GeneralSettingsService _generalSettingsService;
        private readonly PublicHolidays _publicHolidaysService;


        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}
