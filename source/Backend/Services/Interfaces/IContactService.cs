using netCoreWebApi.Models;

namespace netCoreWebApi.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Contact id);
    }
}
