namespace Application.UseCases.CreateWorkshop.Input
{
    public class CreateWorkshopInput
    {
        public string Name { get; init; }
        public int Workload { get; init; }
        public string CNPJ { get; init; }
        public string Password { get; init; }
    }
}
