using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Workshop : Entity
    {
        public Workshop() { }

        public static Workshop CreateWorkshop(string name, int workload)
        {
            return new Workshop()
            {
                Name = name,
                Workload = workload
            };
        }

        [Key]
        public long WorkShopId { get; private set; }
        public string Name { get; private set; }
        public int Workload { get; private set; }
    }
}
