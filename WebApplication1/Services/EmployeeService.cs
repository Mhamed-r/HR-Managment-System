using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class EmployeeService(ApplicationDbContext _context) : IRepositoryService<ApplicationUser>
    {
        private readonly ApplicationDbContext _context = _context;

        public async Task AddEmployeeAsync(ApplicationUser entity)
        {
          await  _context.AddAsync(entity);
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            ApplicationUser Employee= await GetEmployeeByIdAsync(id);
            Employee.isDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetEmployeeByIdAsync(string id)
        => await _context.Employees.FindAsync(id);
        

        public async Task<IList<ApplicationUser>> GetEmployeeListAsync()
        => await _context.Employees.Where(E=>!E.isDeleted).ToListAsync();

        public async Task UpdateEmployeeAsync(ApplicationUser entity)
        {
            var SelectedEmplo = await GetEmployeeByIdAsync(entity.Id);
            SelectedEmplo.FullName = entity.FullName;
            SelectedEmplo.BirthDate = entity.BirthDate;
            SelectedEmplo.PhoneNumber = entity.PhoneNumber;
            SelectedEmplo.Address = entity.Address;
            SelectedEmplo.DepartmentID = entity.DepartmentID;
            SelectedEmplo.Nationality = entity.Nationality;
            SelectedEmplo.SSN = entity.SSN;
            SelectedEmplo.TimeIn = entity.TimeIn;
            SelectedEmplo.TimeOut = entity.TimeOut;
            SelectedEmplo.Salary = entity.Salary;
            
            await _context.SaveChangesAsync();
        }
    }
}
