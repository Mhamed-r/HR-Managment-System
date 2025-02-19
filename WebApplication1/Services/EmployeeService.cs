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
            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetEmployeeByIdAsync(string id)
        => await _context.Employees.FindAsync(id);
        

        public async Task<IList<ApplicationUser>> GetEmployeeListAsync()
        => await _context.Employees.ToListAsync();

        public async Task UpdateEmployeeAsync(ApplicationUser entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
