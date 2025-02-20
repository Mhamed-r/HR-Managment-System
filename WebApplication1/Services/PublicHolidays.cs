using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public class PublicHolidays(ApplicationDbContext context) : IpublicHolidays
    {
        private readonly ApplicationDbContext _context = context ;

        public async Task AddPublicHolidaysAsync(PublicHoliday publicholiday)
        {
            await _context.publicHolidays.AddAsync(publicholiday);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PublicHoliday>> GetPublicHoliday()
        {
           return await _context.publicHolidays.ToListAsync();
        }

        public async Task UpdatePublicHolidaysAsync(PublicHoliday publicholiday)
        {
            _context.Update(publicholiday);
            await _context.SaveChangesAsync();
        }
    }
}
