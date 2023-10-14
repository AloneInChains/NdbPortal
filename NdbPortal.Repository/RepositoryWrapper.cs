#nullable disable
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private readonly NDbContext _nDbContext;
        private ICompanyRepository _company = default!;
        private IEmployeeRepository _employee = default!;
        private INormativeDocumentConfidentialityLevelRepository _normativeDocumentConfidentialityLevel = default!;
        private INormativeDocumentFileRepository _normativeDocumentFile = default!;
        private INormativeDocumentRelationRepository _normativeDocumentRelation = default!;
        private INormativeDocumentRelationTypeRepository _normativeDocumentRelationType = default!;
        private INormativeDocumentVisaRepository _normativeDocumentVisa = default!;
        private INormativeDocumentRepository _normativeDocument = default!;
        private INormativeDocumentTypeRepository _normativeDocumentType = default!;

        public RepositoryWrapper(NDbContext nDbContext)
        {
            _nDbContext = nDbContext;
        }

        public ICompanyRepository Company
        {
            get { return _company ??= new CompanyRepository(_nDbContext); }
        }
        public IEmployeeRepository Employee => _employee ??= new EmployeeRepository(_nDbContext);

        public INormativeDocumentConfidentialityLevelRepository NormativeDocumentConfidentialityLevel =>
            _normativeDocumentConfidentialityLevel ??= new NormativeDocumentConfidentialityLevelRepository(_nDbContext);

        public INormativeDocumentFileRepository NormativeDocumentFile =>
            _normativeDocumentFile ??= new NormativeDocumentFileRepository(_nDbContext);

        public INormativeDocumentRelationRepository NormativeDocumentRelation =>
            _normativeDocumentRelation ??= new NormativeDocumentRelationRepository(_nDbContext);

        public INormativeDocumentRelationTypeRepository NormativeDocumentRelationType =>
            _normativeDocumentRelationType ??= new NormativeDocumentRelationTypeRepository(_nDbContext);

        public INormativeDocumentVisaRepository NormativeDocumentVisa =>
            _normativeDocumentVisa ??= new NormativeDocumentVisaRepository(_nDbContext);

        public INormativeDocumentRepository NormativeDocument => _normativeDocument ??= new NormativeDocumentRepository(_nDbContext);

        public INormativeDocumentTypeRepository NormativeDocumentType =>
            _normativeDocumentType ??= new NormativeDocumentTypeRepository(_nDbContext);

        public async Task SaveAsync()
        {
            await _nDbContext.SaveChangesAsync();
        }
    }
}
