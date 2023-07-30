using API.Test.Jitsi.JWT.Data;
using API.Test.Jitsi.JWT.Models;
using API.Test.Jitsi.JWT.VievModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Test.Jitsi.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomNameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomNameController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RoomName
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomName>>> GetRoomNames()
        {
            return await _context.RoomNames.ToListAsync();
        }

        // GET: api/RoomName/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomName>> GetRoomName(Guid id)
        {
            var roomName = await _context.RoomNames.FindAsync(id);

            if (roomName == null)
            {
                return NotFound();
            }

            return roomName;
        }

        /*// GET: api/RoomName/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<RoomName>> GetRoomNameByName(string name)
        {
            var roomName = await _context.RoomNames.FindAsync(name);

            if (roomName == null)
            {
                return NotFound();
            }

            return roomName;
        }*/

        // Метод для генерации случайной строки
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST: api/RoomName
        [HttpPost]
        public async Task<ActionResult<RoomName>> PostRoomName(RoomName roomName)
        {
            roomName.Id = Guid.NewGuid(); // новый уникальный Guid в качестве идентификатора
            roomName.Roomname = GenerateRandomString(6); // Генерация случайной строки из 6 символов
            _context.RoomNames.Add(roomName);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoomName), new { id = roomName.Id }, roomName);
        }

        // DELETE: api/RoomName/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomName(Guid id)
        {
            var roomName = await _context.RoomNames.FindAsync(id);
            if (roomName == null)
            {
                return NotFound();
            }

            _context.RoomNames.Remove(roomName);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
