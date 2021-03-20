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
using CRM.Enums;

namespace CRM.Rpc.reports.opportunity_report
{
    public partial class OpportunityReportController : RpcController
    {
        [Route(OpportunityReportRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<OpportunityReport_CompanyDTO>>> FilterListCompany([FromBody] OpportunityReport_CompanyFilterDTO OpportunityReport_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = 20;
            CompanyFilter.OrderBy = CompanyOrder.Id;
            CompanyFilter.OrderType = OrderType.ASC;
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Id = OpportunityReport_CompanyFilterDTO.Id;
            CompanyFilter.Name = OpportunityReport_CompanyFilterDTO.Name;
            CompanyFilter.Phone = OpportunityReport_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = OpportunityReport_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = OpportunityReport_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = OpportunityReport_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = OpportunityReport_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<OpportunityReport_CompanyDTO> OpportunityReport_CompanyDTOs = Companys
                .Select(x => new OpportunityReport_CompanyDTO(x)).ToList();
            return OpportunityReport_CompanyDTOs;
        }
        [Route(OpportunityReportRoute.FilterListSaleStage), HttpPost]
        public async Task<ActionResult<List<OpportunityReport_SaleStageDTO>>> FilterListSaleStage([FromBody] OpportunityReport_SaleStageFilterDTO OpportunityReport_SaleStageFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SaleStageFilter SaleStageFilter = new SaleStageFilter();
            SaleStageFilter.Skip = 0;
            SaleStageFilter.Take = 20;
            SaleStageFilter.OrderBy = SaleStageOrder.Id;
            SaleStageFilter.OrderType = OrderType.ASC;
            SaleStageFilter.Selects = SaleStageSelect.ALL;
            SaleStageFilter.Id = OpportunityReport_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = OpportunityReport_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = OpportunityReport_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<OpportunityReport_SaleStageDTO> OpportunityReport_SaleStageDTOs = SaleStages
                .Select(x => new OpportunityReport_SaleStageDTO(x)).ToList();
            return OpportunityReport_SaleStageDTOs;
        }
        [Route(OpportunityReportRoute.FilterListProbability), HttpPost]
        public async Task<ActionResult<List<OpportunityReport_ProbabilityDTO>>> FilterListProbability([FromBody] OpportunityReport_ProbabilityFilterDTO OpportunityReport_ProbabilityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProbabilityFilter ProbabilityFilter = new ProbabilityFilter();
            ProbabilityFilter.Skip = 0;
            ProbabilityFilter.Take = 20;
            ProbabilityFilter.OrderBy = ProbabilityOrder.Id;
            ProbabilityFilter.OrderType = OrderType.ASC;
            ProbabilityFilter.Selects = ProbabilitySelect.ALL;
            ProbabilityFilter.Id = OpportunityReport_ProbabilityFilterDTO.Id;
            ProbabilityFilter.Code = OpportunityReport_ProbabilityFilterDTO.Code;
            ProbabilityFilter.Name = OpportunityReport_ProbabilityFilterDTO.Name;

            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            List<OpportunityReport_ProbabilityDTO> OpportunityReport_ProbabilityDTOs = Probabilities
                .Select(x => new OpportunityReport_ProbabilityDTO(x)).ToList();
            return OpportunityReport_ProbabilityDTOs;
        }
        [Route(OpportunityReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<OpportunityReport_AppUserDTO>>> FilterListAppUser([FromBody] OpportunityReport_AppUserFilterDTO OpportunityReport_AppUserFilterDTO)
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
            AppUserFilter.Id = OpportunityReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = OpportunityReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = OpportunityReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = OpportunityReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = OpportunityReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = OpportunityReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = OpportunityReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = OpportunityReport_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = OpportunityReport_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = OpportunityReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = OpportunityReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = OpportunityReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = OpportunityReport_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = OpportunityReport_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = OpportunityReport_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = OpportunityReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<OpportunityReport_AppUserDTO> OpportunityReport_AppUserDTOs = AppUsers
                .Select(x => new OpportunityReport_AppUserDTO(x)).ToList();
            return OpportunityReport_AppUserDTOs;
        }
        [Route(OpportunityReportRoute.FilterListItem), HttpPost]
        public async Task<ActionResult<List<OpportunityReport_ItemDTO>>> FilterListItem([FromBody] OpportunityReport_ItemFilterDTO OpportunityReport_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = OpportunityReport_ItemFilterDTO.Id;
            ItemFilter.ProductId = OpportunityReport_ItemFilterDTO.ProductId;
            ItemFilter.Code = OpportunityReport_ItemFilterDTO.Code;
            ItemFilter.Name = OpportunityReport_ItemFilterDTO.Name;
            ItemFilter.ScanCode = OpportunityReport_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = OpportunityReport_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = OpportunityReport_ItemFilterDTO.RetailPrice;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<OpportunityReport_ItemDTO> OpportunityReport_ItemDTOs = Items
                .Select(x => new OpportunityReport_ItemDTO(x)).ToList();
            return OpportunityReport_ItemDTOs;
        }
        [Route(OpportunityReportRoute.ListItem), HttpPost]
        public async Task<object> ListItem([FromBody] OpportunityReport_ItemFilterDTO OpportunityReport_ItemFilterDTO)
        {
            if (OpportunityReport_ItemFilterDTO.OpportunityReportId != null)
            {
                var query = (from t1 in DataContext.Item
                             join t2 in DataContext.OpportunityItemMapping on t1.Id equals t2.ItemId
                             where t2.OpportunityId == OpportunityReport_ItemFilterDTO.OpportunityReportId.Equal
                             select new
                             {
                                 Name = t1.Name,
                                 UnitOfMeasureName = t2.UnitOfMeasure == null ? "" : t2.UnitOfMeasure.Name,
                                 RequestQuantity = t2.RequestQuantity,
                                 SalePrice = t2.SalePrice,
                                 RevenueOfItem = t2.RequestQuantity * t2.SalePrice,
                             }).ToList();
                return query.Skip(OpportunityReport_ItemFilterDTO.Skip).Take(OpportunityReport_ItemFilterDTO.Take);
            }

            return null;
        }
        [Route(OpportunityReportRoute.CountItem), HttpPost]
        public async Task<long> CountItem([FromBody] OpportunityReport_ItemFilterDTO OpportunityReport_ItemFilterDTO)
        {
            if (OpportunityReport_ItemFilterDTO.OpportunityReportId != null)
            {
                var query = (from t1 in DataContext.Item
                             join t2 in DataContext.OpportunityItemMapping on t1.Id equals t2.ItemId
                             where t2.OpportunityId == OpportunityReport_ItemFilterDTO.OpportunityReportId.Equal
                             select new
                             {
                                 Name = t1.Name,
                                 UnitOfMeasureName = t2.UnitOfMeasure == null ? "" : t2.UnitOfMeasure.Name,
                                 RequestQuantity = t2.RequestQuantity,
                                 SalePrice = t2.SalePrice,
                                 RevenueOfItem = t2.RequestQuantity * t2.SalePrice,
                             }).ToList();
                return query.Count();
            }
            return 0;
        }



    }
}

