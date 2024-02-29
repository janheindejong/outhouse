using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Base;

namespace OutHouse.Server.Users
{
    [Route("api/[controller]")]
    public class UserController : MyControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly UserContext _context;

        public UserController(ILogger<UserController> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation("Uh-oh");
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost(Name = "PostUser")]
        public ActionResult<User> Post(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
    }
}
