using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCoreWebApi.Dtos;
using netCoreWebApi.Models;
using netCoreWebApi.Services.Exceptions;
using netCoreWebApi.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // GET: api/Contact
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
    {
        return await _contactService.GetAllAsync();
    }

    // GET: api/Contact/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        return contact;
    }

    // PUT: api/Contact/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutContact(int id, ContactDto dto)
    {
        var contact = await _contactService.GetByIdAsync(id);

        if (contact == null)
            return NotFound();

        contact.Name = dto.Name;
        contact.Email = dto.Email;
        contact.Phone = dto.Phone;

        await _contactService.UpdateAsync(contact);

        return NoContent();
    }

    // POST: api/Contact
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Contact>> PostContact(ContactDto dto)
    {
        var contact = new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var createdContact = await _contactService.CreateAsync(contact);

        return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
    }

    // DELETE: api/Contact/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);

        if (contact == null)
            return NotFound();

        await _contactService.DeleteAsync(contact);
            return NoContent();
    }
}
