using Microsoft.EntityFrameworkCore;
using netCoreWebApi.Models;
using netCoreWebApi.Services.Exceptions;
using netCoreWebApi.Services.Interfaces;
using System.Data;

namespace netCoreWebApi.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contact.FindAsync(id);
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task UpdateAsync(Contact contact)
        {
            bool hasAny = await _context.Contact.AnyAsync(x => x.Id == contact.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task DeleteAsync(Contact contact)
        {
            try
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
