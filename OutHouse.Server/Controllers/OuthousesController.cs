using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.DataAccess;
using OutHouse.Server.Identity;
using OutHouse.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace OutHouse.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OuthousesController(
        ILogger<OuthousesController> logger,
        ApplicationDbContext context)
        : ControllerBase
    {

        private readonly ILogger<OuthousesController> _logger = logger;
        private readonly OuthouseRepository _outhouseRepository = new(context);

        private UserContext UserContext => new(HttpContext);

        [HttpPost("")]
        public async Task<ActionResult<OuthouseDto>> CreateNew(string name)
        {
            Outhouse outhouse = Outhouse.CreateNew(name, UserContext);
            _outhouseRepository.Add(outhouse);
            await _outhouseRepository.UnitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = outhouse.Id }, outhouse.ToDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OuthouseDto>> Get(Guid id)
        {
            Outhouse? outhouse = await _outhouseRepository.GetById(id);
            if (outhouse == null)
            {
                return NotFound();
            }

            if (!outhouse.IsMember(UserContext))
            {
                return Forbid();
            }

            return Ok(outhouse.ToDto());
        }

        [HttpGet("{id}/members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers(Guid id)
        {
            Outhouse? outhouse = await _outhouseRepository.GetById(id);
            if (outhouse == null)
            {
                return NotFound();
            }

            if (!outhouse.IsMember(UserContext))
            {
                return Forbid();
            }

            return Ok(outhouse.Members.Select(x => x.ToDto()));
        }

        [HttpPost("{id}/members")]
        public async Task<ActionResult<MemberDto>> PostMember(Guid id, AddMemberRequest request)
        {
            Outhouse? outhouse = await _outhouseRepository.GetById(id);
            if (outhouse == null)
            {
                return NotFound();
            }

            Result<Member> result = outhouse.AddMember(request.User, request.Role, UserContext);
            if (result.IsFailure)
            {
                return result.Error.Code switch
                {
                    nameof(OuthouseErrors.InsufficientPrivileges) => Forbid(),
                    nameof(OuthouseErrors.MemberAlreadyExists) => UnprocessableEntity(),
                    _ => throw new Exception("This should not happen"),
                };
            }

            await _outhouseRepository.UnitOfWork.SaveChangesAsync();
            return Ok(result.Data?.ToDto());
        }
    }

    public struct AddMemberRequest
    {
        [Required]
        public IUser User { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
