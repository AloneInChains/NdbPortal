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

        private NDBContext _ndbContext;
        private ICompanyRepository _company = default!;
        private IEmployeeRepository _employee = default!;
        private INormativeDocumentConfidentialityLevelRepository _normativeDocumentConfidentialityLevel = default!;
        private INormativeDocumentFileRepository _normativeDocumentFile = default!;
        private INormativeDocumentRelationRepository _normativeDocumentRelation = default!;
        private INormativeDocumentRelationTypeRepository _normativeDocumentRelationType = default!;
        private INormativeDocumentVisaRepository _normativeDocumentVisa = default!;
        private INormativeDocumentRepository _normativeDocument = default!;
        private INormativeDocumentTypeRepository _normativeDocumentType = default!;

        public RepositoryWrapper(NDBContext ndbContext)
        {
            _ndbContext = ndbContext;
        }

        public ICompanyRepository Company
        {
            get 
            { 
                if (_company == null)
                {
                    _company = new CompanyRepository(_ndbContext);
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
                    _employee = new EmployeeRepository(_ndbContext);
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
                    _normativeDocumentConfidentialityLevel = new NormativeDocumentConfidentialityLevelRepository(_ndbContext);
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
                    _normativeDocumentFile = new NormativeDocumentFileRepository(_ndbContext);
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
                    _normativeDocumentRelation = new NormativeDocumentRelationRepository(_ndbContext);
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
                    _normativeDocumentRelationType = new NormativeDocumentRelationTypeRepository(_ndbContext);
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
                    _normativeDocumentVisa = new NormativeDocumentVisaRepository(_ndbContext);
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
                    _normativeDocument = new NormativeDocumentRepository(_ndbContext);
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
                    _normativeDocumentType = new NormativeDocumentTypeRepository(_ndbContext);
                }

                return _normativeDocumentType;
            }
        }

        public async Task SaveAsync()
        {
            await _ndbContext.SaveChangesAsync();
        }
    }
}
