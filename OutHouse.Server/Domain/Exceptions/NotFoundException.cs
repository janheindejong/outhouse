namespace OutHouse.Server.Domain.Exceptions
{
    public class NotFoundException(string item, Guid id) 
        : Exception($"{item} {id} does not exist")
    {
        public string Item { get => item; }

        public Guid Id { get => id; }
    }
}
