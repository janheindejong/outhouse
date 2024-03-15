namespace OutHouse.Server.Domain.Exceptions
{

    public class SeeOtherException(string action, string item, Guid id)
        : Exception($"{action} is already represented by {item} {id}")
    {
        public string Action { get => action; }

        public string Item { get => item; }

        public Guid Id { get => id; }
    }
}
