using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Contracts
{
    public interface IRepositoryWrapper
    {
        ICompanyRepository Company { get; }

        IEmployeeRepository Employee { get; }

        INormativeDocumentRepository NormativeDocument { get; }

        INormativeDocumentConfidentialityLevelRepository NormativeDocumentConfidentialityLevel { get; }

        INormativeDocumentFileRepository NormativeDocumentFile { get; }

        INormativeDocumentRelationRepository NormativeDocumentRelation { get; }

        INormativeDocumentRelationTypeRepository NormativeDocumentRelationType { get; }

        INormativeDocumentTypeRepository NormativeDocumentType { get; }

        INormativeDocumentVisaRepository NormativeDocumentVisa { get; }

        Task SaveAsync();
    }
}
