namespace OutHouse.Server.Domain.Exceptions
{
    public class ForbiddenException(string action, string item, Guid id)
       : OuthouseException($"Forbidden to {action} {item} {id}")
    {
        public string Action { get => action; }

        public string Item { get => item; }

        public Guid Id { get => id; }
    }
}
