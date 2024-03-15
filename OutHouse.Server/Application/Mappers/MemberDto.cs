namespace OutHouse.Server.Application.Mappers
{
    public record class MemberDto(Guid Id, string Email, string Name) : IEntity;
}
