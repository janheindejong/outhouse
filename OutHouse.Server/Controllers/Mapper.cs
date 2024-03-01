using OutHouse.Server.Models;

namespace OutHouse.Server.Controllers
{
    public static class Mapper
    {
        public static OuthouseDto ToDto(this Outhouse outhouse)
        {
            return new(outhouse.Id, outhouse.Name);
        }

        public static MemberDto ToDto(this Member member)
        {
            return new(member.Id, member.UserId);
        }
    }

    public record class OuthouseDto(Guid Id, string Name);
    public record class MemberDto(Guid Id, Guid UserId);
}
