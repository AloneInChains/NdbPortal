using AutoMapper;
using NdbPortal.Entities.Dtos.Company;
using NdbPortal.Entities.Dtos.Employee;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel;
using NdbPortal.Entities.Dtos.NormativeDocumentFile;
using NdbPortal.Entities.Dtos.NormativeDocumentRelation;
using NdbPortal.Entities.Dtos.NormativeDocumentRelationType;
using NdbPortal.Entities.Dtos.NormativeDocumentType;
using NdbPortal.Entities.Dtos.NormativeDocumentVisa;
using NdbPortal.Entities.Models;

namespace NdbPortal.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyGetDto>();
            CreateMap<Company, CompanyAddDto>();
            CreateMap<Company, CompanyUpdateDto>();
            CreateMap<CompanyAddDto, Company>();

            CreateMap<Employee, EmployeeGetDto>();
            CreateMap<Employee, EmployeeAddDto>();
            CreateMap<Employee, EmployeeUpdateDto>();
            CreateMap<EmployeeAddDto, Employee>();

            CreateMap<NormativeDocumentConfidentialityLevel, NormativeDocumentConfidentialityLevelGetDto>();
            CreateMap<NormativeDocumentConfidentialityLevel, NormativeDocumentConfidentialityLevelAddDto>();
            CreateMap<NormativeDocumentConfidentialityLevel, NormativeDocumentConfidentialityLevelUpdateDto>();
            CreateMap<NormativeDocumentConfidentialityLevelAddDto, NormativeDocumentConfidentialityLevel>();
            CreateMap<NormativeDocumentConfidentialityLevelUpdateDto, NormativeDocumentConfidentialityLevel>();

            CreateMap<NormativeDocumentFile, NormativeDocumentFileGetDto>();
            CreateMap<NormativeDocumentFile, NormativeDocumentFileAddDto>();
            CreateMap<NormativeDocumentFile, NormativeDocumentFileUpdateDto>();
            CreateMap<NormativeDocumentFileAddDto, NormativeDocumentFile>();
            CreateMap<NormativeDocumentFileUpdateDto, NormativeDocumentFile>();


            CreateMap<NormativeDocumentRelation, NormativeDocumentRelationGetDto>();
            CreateMap<NormativeDocumentRelation, NormativeDocumentRelationAddDto>();
            CreateMap<NormativeDocumentRelation, NormativeDocumentRelationUpdateDto>();
            CreateMap<NormativeDocumentRelationAddDto, NormativeDocumentRelation>();
            CreateMap<NormativeDocumentRelationUpdateDto, NormativeDocumentRelation>();

            CreateMap<NormativeDocumentRelationType, NormativeDocumentRelationTypeGetDto>();
            CreateMap<NormativeDocumentRelationType, NormativeDocumentRelationTypeAddDto>();
            CreateMap<NormativeDocumentRelationType, NormativeDocumentRelationTypeUpdateDto>();
            CreateMap<NormativeDocumentRelationTypeAddDto, NormativeDocumentRelationType>();
            CreateMap<NormativeDocumentRelationTypeUpdateDto, NormativeDocumentRelationType>();

            CreateMap<NormativeDocument, NormativeDocumentGetDto>();
            CreateMap<NormativeDocument, NormativeDocumentAddDto>();
            CreateMap<NormativeDocument, NormativeDocumentUpdateDto>();
            CreateMap<NormativeDocumentAddDto, NormativeDocument>();
            CreateMap<NormativeDocumentUpdateDto, NormativeDocument>();

            CreateMap<NormativeDocumentType, NormativeDocumentTypeGetDto>();
            CreateMap<NormativeDocumentType, NormativeDocumentTypeAddDto>();
            CreateMap<NormativeDocumentType, NormativeDocumentTypeUpdateDto>();
            CreateMap<NormativeDocumentTypeAddDto, NormativeDocumentType>();
            CreateMap<NormativeDocumentTypeUpdateDto, NormativeDocumentType>();

            CreateMap<NormativeDocumentVisa, NormativeDocumentVisaGetDto>();
            CreateMap<NormativeDocumentVisa, NormativeDocumentVisaAddDto>();
            CreateMap<NormativeDocumentVisa, NormativeDocumentVisaUpdateDto>();
            CreateMap<NormativeDocumentVisaAddDto, NormativeDocumentVisa>();
            CreateMap<NormativeDocumentVisaUpdateDto, NormativeDocumentVisa>();

            CreateMap<NormativeDocumentGetWithDetailsDto, NormativeDocument>();
            CreateMap<NormativeDocument, NormativeDocumentGetWithDetailsDto>();
            
            CreateMap<EmployeeGetWithDetailsDto, Employee>();

        }
    }
}
