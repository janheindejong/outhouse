namespace OutHouse.Server.Service.Mappers
{
    public record class MemberDto(Guid Id, string Email, string Name, string Role) : IEntity;
}
