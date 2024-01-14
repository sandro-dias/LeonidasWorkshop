using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Workshop : Entity
    {
        public Workshop() { }

        public static Workshop CreateWorkshop(string workShopName, int workload)
        {
            return new Workshop()
            {
                WorkShopName = workShopName,
                Workload = workload
            };
        }

        [Key]
        public long WorkShopId { get; private set; }
        public string WorkShopName { get; private set; }
        public int Workload { get; private set; }
    }
}
