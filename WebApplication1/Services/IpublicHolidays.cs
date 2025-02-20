using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public interface IpublicHolidays
    {
        Task AddPublicHolidaysAsync(PublicHoliday publicholiday);
        Task UpdatePublicHolidaysAsync(PublicHoliday publicholiday);
        Task<List<PublicHoliday>> GetPublicHoliday();
    }
}
