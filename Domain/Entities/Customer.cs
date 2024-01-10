namespace Domain.Entities
{
    public class Customer
    {
        public Customer (string name)
        {
            Name = name;
        }

        public long CustomerId { get; private set; }
        public string Name { get; private set; }
    }
}
