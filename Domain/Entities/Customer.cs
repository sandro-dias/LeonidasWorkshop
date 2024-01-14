using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Customer : Entity
    {
        public Customer() { }

        public static Customer CreateCustomer (string name, string cpf)
        {
            return new Customer()
            {
                Name = name,
                CPF = cpf
            };
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; private set; }
        public string Name { get; private set; }
        public string CPF { get; private set; }
    }
}
