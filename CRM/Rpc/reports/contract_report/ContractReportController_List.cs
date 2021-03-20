using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
namespace CRM.Rpc.reports.contract_report
{
    public partial class ContractReportController : RpcController
    {
        [Route(ContractReportRoute.FilterListContractType), HttpPost]
        public async Task<ActionResult<List<ContractReport_ContractTypeDTO>>> FilterListContractType([FromBody] ContractReport_ContractTypeFilterDTO ContractReport_ContractTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractTypeFilter ContractTypeFilter = new ContractTypeFilter();
            ContractTypeFilter.Skip = 0;
            ContractTypeFilter.Take = 20;
            ContractTypeFilter.OrderBy = ContractTypeOrder.Id;
            ContractTypeFilter.OrderType = OrderType.ASC;
            ContractTypeFilter.Selects = ContractTypeSelect.ALL;
            ContractTypeFilter.Id = ContractReport_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = ContractReport_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = ContractReport_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<ContractReport_ContractTypeDTO> ContractReport_ContractTypeDTOs = ContractTypes
                .Select(x => new ContractReport_ContractTypeDTO(x)).ToList();
            return ContractReport_ContractTypeDTOs;
        }
        [Route(ContractReportRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<ContractReport_CompanyDTO>>> FilterListCompany([FromBody] ContractReport_CompanyFilterDTO ContractReport_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = int.MaxValue;
            CompanyFilter.OrderBy = CompanyOrder.Id;
            CompanyFilter.OrderType = OrderType.ASC;
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Id = ContractReport_CompanyFilterDTO.Id;
            CompanyFilter.Name = ContractReport_CompanyFilterDTO.Name;
            CompanyFilter.Phone = ContractReport_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = ContractReport_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = ContractReport_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = ContractReport_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = ContractReport_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<ContractReport_CompanyDTO> ContractReport_CompanyDTOs = Companys
                .Select(x => new ContractReport_CompanyDTO(x)).ToList();
            return ContractReport_CompanyDTOs;
        }
        [Route(ContractReportRoute.FilterListOpportunity), HttpPost]
        public async Task<ActionResult<List<ContractReport_OpportunityDTO>>> FilterListOpportunity([FromBody] ContractReport_OpportunityFilterDTO ContractReport_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Skip = 0;
            OpportunityFilter.Take = 20;
            OpportunityFilter.OrderBy = OpportunityOrder.Id;
            OpportunityFilter.OrderType = OrderType.ASC;
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Id = ContractReport_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = ContractReport_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = ContractReport_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = ContractReport_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = ContractReport_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = ContractReport_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = ContractReport_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = ContractReport_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = ContractReport_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = ContractReport_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.Amount = ContractReport_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = ContractReport_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = ContractReport_OpportunityFilterDTO.Description;
            OpportunityFilter.CreatorId = ContractReport_OpportunityFilterDTO.CreatorId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<ContractReport_OpportunityDTO> ContractReport_OpportunityDTOs = Opportunities
                .Select(x => new ContractReport_OpportunityDTO(x)).ToList();
            return ContractReport_OpportunityDTOs;
        }
        [Route(ContractReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<ContractReport_OrganizationDTO>>> FilterListOrganization([FromBody] ContractReport_OrganizationFilterDTO ContractReport_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = ContractReport_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = ContractReport_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = ContractReport_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = ContractReport_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = ContractReport_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = ContractReport_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = ContractReport_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = ContractReport_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = ContractReport_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = ContractReport_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<ContractReport_OrganizationDTO> ContractReport_OrganizationDTOs = Organizations
                .Select(x => new ContractReport_OrganizationDTO(x)).ToList();
            return ContractReport_OrganizationDTOs;
        }
        [Route(ContractReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<ContractReport_AppUserDTO>>> FilterListAppUser([FromBody] ContractReport_AppUserFilterDTO ContractReport_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = ContractReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = ContractReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = ContractReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = ContractReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = ContractReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = ContractReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = ContractReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = ContractReport_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = ContractReport_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = ContractReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = ContractReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = ContractReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = ContractReport_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = ContractReport_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = ContractReport_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = ContractReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<ContractReport_AppUserDTO> ContractReport_AppUserDTOs = AppUsers
                .Select(x => new ContractReport_AppUserDTO(x)).ToList();
            return ContractReport_AppUserDTOs;
        }


    }
}

