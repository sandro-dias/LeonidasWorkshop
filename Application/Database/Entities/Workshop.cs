using System.ComponentModel.DataAnnotations;

namespace Application.Database.Entities
{
    public class Workshop
    {
        public Workshop(int id, int cNPJ, int workload, int password)
        {
            Id = id;
            CNPJ = cNPJ;
            Workload = workload;
            Password = password;
        }

        [Key]
        public int Id { get; set; }
        public int CNPJ { get; set; }
        public int Workload { get; set; }
        public int Password { get; set; }
    }
}
