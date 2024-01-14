using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Customer : Entity
    {
        public Customer (string name)
        {
            Name = name;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; private set; }
        public string Name { get; private set; }
    }
}
