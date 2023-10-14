namespace NdbPortal.Entities.Models
{
    public sealed class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
        public ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
