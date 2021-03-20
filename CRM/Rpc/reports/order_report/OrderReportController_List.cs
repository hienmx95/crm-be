using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using Microsoft.AspNetCore.Mvc;
using CRM.Entities;

namespace CRM.Rpc.reports.order_report
{
    public partial class OrderReportController : RpcController
    {
        [Route(OrderReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<OrderReport_AppUserDTO>>> FilterListAppUser([FromBody] OrderReport_AppUserFilterDTO OrderReport_AppUserFilterDTO)
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
            AppUserFilter.Id = OrderReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = OrderReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = OrderReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = OrderReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = OrderReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = OrderReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = OrderReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = OrderReport_AppUserFilterDTO.Birthday;
            AppUserFilter.PositionId = OrderReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = OrderReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = OrderReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = OrderReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<OrderReport_AppUserDTO> OrderReport_AppUserDTOs = AppUsers
                .Select(x => new OrderReport_AppUserDTO(x)).ToList();
            return OrderReport_AppUserDTOs;
        }
        [Route(OrderReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<OrderReport_OrganizationDTO>>> FilterListOrganization([FromBody] OrderReport_OrganizationFilterDTO OrderReport_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = OrderReport_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = OrderReport_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = OrderReport_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = OrderReport_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = OrderReport_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = OrderReport_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = OrderReport_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = OrderReport_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = OrderReport_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = OrderReport_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<OrderReport_OrganizationDTO> OrderReport_OrganizationDTOs = Organizations
                .Select(x => new OrderReport_OrganizationDTO(x)).ToList();
            return OrderReport_OrganizationDTOs;
        }

        [Route(OrderReportRoute.FilterListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<OrderReport_PaymentStatusDTO>>> FilterListPaymentStatus([FromBody] OrderReport_PaymentStatusFilterDTO OrderReport_PaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter();
            PaymentStatusFilter.Skip = 0;
            PaymentStatusFilter.Take = 20;
            PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
            PaymentStatusFilter.OrderType = OrderType.ASC;
            PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;
            PaymentStatusFilter.Id = OrderReport_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = OrderReport_PaymentStatusFilterDTO.Name;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<OrderReport_PaymentStatusDTO> OrderReport_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new OrderReport_PaymentStatusDTO(x)).ToList();
            return OrderReport_PaymentStatusDTOs;
        }

        [Route(OrderReportRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<OrderReport_CompanyDTO>>> FilterListCompany([FromBody] OrderReport_CompanyFilterDTO OrderReport_CompanyFilterDTO)
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
            CompanyFilter.Id = OrderReport_CompanyFilterDTO.Id;
            CompanyFilter.Name = OrderReport_CompanyFilterDTO.Name;
            CompanyFilter.Phone = OrderReport_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = OrderReport_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = OrderReport_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = OrderReport_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = OrderReport_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<OrderReport_CompanyDTO> OrderReport_CompanyDTOs = Companys
                .Select(x => new OrderReport_CompanyDTO(x)).ToList();
            return OrderReport_CompanyDTOs;
        }

        [Route(OrderReportRoute.FilterListOpportunity), HttpPost]
        public async Task<ActionResult<List<OrderReport_OpportunityDTO>>> FilterListOpportunity([FromBody] OrderReport_OpportunityFilterDTO OrderReport_OpportunityFilterDTO)
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
            OpportunityFilter.Id = OrderReport_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = OrderReport_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = OrderReport_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.ClosingDate = OrderReport_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = OrderReport_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = OrderReport_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = OrderReport_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = OrderReport_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.CreatorId = OrderReport_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.Amount = OrderReport_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = OrderReport_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = OrderReport_OpportunityFilterDTO.Description;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<OrderReport_OpportunityDTO> OrderReport_OpportunityDTOs = Opportunities
                .Select(x => new OrderReport_OpportunityDTO(x)).ToList();
            return OrderReport_OpportunityDTOs;
        }

        [Route(OrderReportRoute.FilterListOrderCategory), HttpPost]
        public async Task<ActionResult<List<OrderReport_OrderCategoryDTO>>> FilterListOrderCategory([FromBody] OrderReport_OrderCategoryFilterDTO OrderReport_OrderCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter();
            OrderCategoryFilter.Skip = 0;
            OrderCategoryFilter.Take = int.MaxValue;
            OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
            OrderCategoryFilter.OrderType = OrderType.ASC;
            OrderCategoryFilter.Selects = OrderCategorySelect.ALL;
            OrderCategoryFilter.Id = OrderReport_OrderCategoryFilterDTO.Id;
            OrderCategoryFilter.Name = OrderReport_OrderCategoryFilterDTO.Name;
            OrderCategoryFilter.Code = OrderReport_OrderCategoryFilterDTO.Code;

            List<OrderCategory> OrderCategories = await OrderCategoryService.List(OrderCategoryFilter);
            List<OrderReport_OrderCategoryDTO> OrderReport_OrderCategoryDTOs = OrderCategories
                .Select(x => new OrderReport_OrderCategoryDTO(x)).ToList();
            return OrderReport_OrderCategoryDTOs;
        }
    }
}
