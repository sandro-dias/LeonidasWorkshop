using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Workshop : Entity
    {
        public Workshop() { }

        public static Workshop CreateWorkshop(string name, int workload, string cNPJ, string password)
        {
            return new Workshop()
            {
                Name = name,
                Workload = workload,
                CNPJ = cNPJ,
                Password = password
            };
        }

        [Key]
        public long WorkShopId { get; private set; }
        public string Name { get; private set; }
        public int Workload { get; private set; }
        public string CNPJ { get; private set; }
        public string Password { get; private set; }
    }
}
