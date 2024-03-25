namespace OutHouse.Server.Domain.Exceptions
{
    public class ConflictException(string action, string item, Guid id, string reason)
       : OuthouseException($"Can't {action} {item} {id}; {reason}")
    {
        public string Action { get => action; }

        public string Item { get => item; }

        public Guid Id { get => id; }

        public string Reason { get => reason; }

    }
}
