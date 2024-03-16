using OutHouse.Server.Models;

namespace OutHouse.Server.Service.Mappers
{
    public static class Mapper
    {
        public static OuthouseDto ToDto(this Outhouse outhouse)
        {
            return new(outhouse.Id, outhouse.Name);
        }

        public static MemberDto ToDto(this Member member)
        {
            return new(member.Id, member.Email, member.Name);
        }
    }
}
