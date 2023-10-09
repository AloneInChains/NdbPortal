using NdbPortal.Contracts;
using NdbPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get 
            { 
                if (_company == null)
                {
                    _company = new CompanyRepository(_nDbContext);
                }

                return _company;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_nDbContext);
                }

                return _employee;
            }
        }

        public INormativeDocumentConfidentialityLevelRepository NormativeDocumentConfidentialityLevel
        {
            get
            {
                if (_normativeDocumentConfidentialityLevel == null)
                {
                    _normativeDocumentConfidentialityLevel = new NormativeDocumentConfidentialityLevelRepository(_nDbContext);
                }

                return _normativeDocumentConfidentialityLevel;
            }
        }

        public INormativeDocumentFileRepository NormativeDocumentFile
        {
            get
            {
                if (_normativeDocumentFile == null)
                {
                    _normativeDocumentFile = new NormativeDocumentFileRepository(_nDbContext);
                }

                return _normativeDocumentFile;
            }
        }

        public INormativeDocumentRelationRepository NormativeDocumentRelation 
        { 
            get
            {
                if (_normativeDocumentRelation == null)
                {
                    _normativeDocumentRelation = new NormativeDocumentRelationRepository(_nDbContext);
                }

                return _normativeDocumentRelation;
            }        
        }

        public INormativeDocumentRelationTypeRepository NormativeDocumentRelationType 
        { 
            get
            {
                if (_normativeDocumentRelationType == null)
                {
                    _normativeDocumentRelationType = new NormativeDocumentRelationTypeRepository(_nDbContext);
                }

                return _normativeDocumentRelationType;
            }
        }

        public INormativeDocumentVisaRepository NormativeDocumentVisa 
        { 
            get
            {
                if (_normativeDocumentVisa == null)
                {
                    _normativeDocumentVisa = new NormativeDocumentVisaRepository(_nDbContext);
                }

                return _normativeDocumentVisa;

            }
        }

        public INormativeDocumentRepository NormativeDocument
        {
            get
            {
                if (_normativeDocument == null)
                {
                    _normativeDocument = new NormativeDocumentRepository(_nDbContext);
                }

                return _normativeDocument;
            }
        }

        public INormativeDocumentTypeRepository NormativeDocumentType
        {
            get
            {
                if (_normativeDocumentType == null)
                {
                    _normativeDocumentType = new NormativeDocumentTypeRepository(_nDbContext);
                }

                return _normativeDocumentType;
            }
        }

        public async Task SaveAsync()
        {
            await _nDbContext.SaveChangesAsync();
        }
    }
}
