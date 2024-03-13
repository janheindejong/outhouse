using System.Data;

namespace OutHouse.Server.Models
{
    public class Outhouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Member> Members { get; } = [];

        public Result<Member> AddMember(IUser user, Role role, IUser userContext)
        {
            if (!IsAdmin(userContext))
            {
                return Result<Member>.Failure(OuthouseErrors.InsufficientPrivileges);
            }

            if (IsMember(user))
            {
                return Result<Member>.Failure(OuthouseErrors.MemberAlreadyExists(user));
            }

            Member newMember = new()
            {
                UserId = user.Id,
                OuthouseId = Id,
                Role = role
            };
            Members.Add(newMember);
            return Result<Member>.Success(newMember);
        }

        public Result DeleteMember(IUser user, IUser userContext)
        {
            if (!(IsAdmin(userContext) || userContext.Id == user.Id))
            {
                return Result.Failure(OuthouseErrors.InsufficientPrivileges);
            }

            Member? membership = Members.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (membership == null)
            {
                return Result.Failure(OuthouseErrors.MemberNotFound(user));
            }

            if (IsLastAdmin(user))
            {
                return Result.Failure(OuthouseErrors.CantModifyLastAdmin);
            }

            Members.Remove(membership);
            return Result.Success();
        }

        public Result ModifyMemberRole(IUser user, Role newRole, IUser userContext)
        {
            if (!IsAdmin(userContext))
            {
                return Result.Failure(OuthouseErrors.InsufficientPrivileges);
            }

            Member? membership = Members.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (membership == null)
            {
                return Result.Failure(OuthouseErrors.MemberNotFound(user));
            }

            if (IsLastAdmin(user))
            {
                return Result.Failure(OuthouseErrors.CantModifyLastAdmin);
            }

            membership.Role = newRole;
            return Result.Success();
        }

        public bool IsAdmin(IUser userContext)
        {
            return Members.Any(x => x.UserId == userContext.Id && x.Role == Role.Admin);
        }

        public static Outhouse CreateNew(string name, IUser userContext)
        {
            Outhouse outhouse = new()
            {
                Name = name
            };

            Member owner = new()
            {
                UserId = userContext.Id,
                OuthouseId = outhouse.Id,
                Role = Role.Admin
            };

            outhouse.Members.Add(owner);
            return outhouse;
        }

        public bool IsMember(IUser user)
        {
            return Members.Any(x => x.UserId == user.Id);
        }

        private bool IsLastAdmin(IUser user)
        {
            return !Members.Where(x => x.Role == Role.Admin && !(x.UserId == user.Id)).Any();
        }
    }

    public interface IUser
    {
        Guid Id { get; }
    }
}
