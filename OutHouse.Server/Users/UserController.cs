using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Base;

namespace OutHouse.Server.Users
{
    [Route("api/[controller]")]
    public class UserController : MyControllerBase
    {

        private static readonly List<User> users = [new User { UserId = 1, Name = "Marie" }];

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            _logger.LogInformation("Uh-oh");
            return users;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IEnumerable<User> GetUser(int id)
        {
            return users.Where(x => x.UserId == id);
        }

        [HttpPost(Name = "PostUser")]
        public ActionResult<User> Post(User user)
        {
            int newId = users.Select(x => x.UserId).Max() + 1;
            user.UserId = newId;
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId}, user);
        }
    }
}
