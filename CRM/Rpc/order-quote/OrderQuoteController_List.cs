using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common; 
using Microsoft.AspNetCore.Mvc; 
using CRM.Entities; 
using CRM.Enums;

namespace CRM.Rpc.order_quote
{
    public partial class OrderQuoteController : RpcController
    {
        [Route(OrderQuoteRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<OrderQuote_CompanyDTO>>> FilterListCompany([FromBody] OrderQuote_CompanyFilterDTO OrderQuote_CompanyFilterDTO)
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
            CompanyFilter.Id = OrderQuote_CompanyFilterDTO.Id;
            CompanyFilter.Name = OrderQuote_CompanyFilterDTO.Name;
            CompanyFilter.Phone = OrderQuote_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = OrderQuote_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = OrderQuote_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = OrderQuote_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = OrderQuote_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<OrderQuote_CompanyDTO> OrderQuote_CompanyDTOs = Companys
                .Select(x => new OrderQuote_CompanyDTO(x)).ToList();
            return OrderQuote_CompanyDTOs;
        }
        [Route(OrderQuoteRoute.FilterListContact), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ContactDTO>>> FilterListContact([FromBody] OrderQuote_ContactFilterDTO OrderQuote_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Skip = 0;
            ContactFilter.Take = 20;
            ContactFilter.OrderBy = ContactOrder.Id;
            ContactFilter.OrderType = OrderType.ASC;
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Id = OrderQuote_ContactFilterDTO.Id;
            ContactFilter.Name = OrderQuote_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = OrderQuote_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = OrderQuote_ContactFilterDTO.CompanyId;
            ContactFilter.ZIPCode = OrderQuote_ContactFilterDTO.ZIPCode;
            ContactFilter.PositionId = OrderQuote_ContactFilterDTO.PositionId;
            ContactFilter.SexId = OrderQuote_ContactFilterDTO.SexId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<OrderQuote_ContactDTO> OrderQuote_ContactDTOs = Contacts
                .Select(x => new OrderQuote_ContactDTO(x)).ToList();
            return OrderQuote_ContactDTOs;
        }
        [Route(OrderQuoteRoute.FilterListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<OrderQuote_OrderQuoteStatusDTO>>> FilterListOrderQuoteStatus([FromBody] OrderQuote_OrderQuoteStatusFilterDTO OrderQuote_OrderQuoteStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter();
            OrderQuoteStatusFilter.Skip = 0;
            OrderQuoteStatusFilter.Take = 20;
            OrderQuoteStatusFilter.OrderBy = OrderQuoteStatusOrder.Id;
            OrderQuoteStatusFilter.OrderType = OrderType.ASC;
            OrderQuoteStatusFilter.Selects = OrderQuoteStatusSelect.ALL;
            OrderQuoteStatusFilter.Id = OrderQuote_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = OrderQuote_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = OrderQuote_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<OrderQuote_OrderQuoteStatusDTO> OrderQuote_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new OrderQuote_OrderQuoteStatusDTO(x)).ToList();
            return OrderQuote_OrderQuoteStatusDTOs;
        }
        [Route(OrderQuoteRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<OrderQuote_AppUserDTO>>> FilterListAppUser([FromBody] OrderQuote_AppUserFilterDTO OrderQuote_AppUserFilterDTO)
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
            AppUserFilter.Id = OrderQuote_AppUserFilterDTO.Id;
            AppUserFilter.Username = OrderQuote_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = OrderQuote_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = OrderQuote_AppUserFilterDTO.Address;
            AppUserFilter.Email = OrderQuote_AppUserFilterDTO.Email;
            AppUserFilter.Phone = OrderQuote_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = OrderQuote_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = OrderQuote_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = OrderQuote_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = OrderQuote_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = OrderQuote_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = OrderQuote_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = OrderQuote_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = OrderQuote_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = OrderQuote_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = OrderQuote_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<OrderQuote_AppUserDTO> OrderQuote_AppUserDTOs = AppUsers
                .Select(x => new OrderQuote_AppUserDTO(x)).ToList();
            return OrderQuote_AppUserDTOs;
        }

        [Route(OrderQuoteRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<OrderQuote_CompanyDTO>>> SingleListCompany([FromBody] OrderQuote_CompanyFilterDTO OrderQuote_CompanyFilterDTO)
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
            CompanyFilter.Id = OrderQuote_CompanyFilterDTO.Id;
            CompanyFilter.Name = OrderQuote_CompanyFilterDTO.Name;
            CompanyFilter.Phone = OrderQuote_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = OrderQuote_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = OrderQuote_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = OrderQuote_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = OrderQuote_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<OrderQuote_CompanyDTO> OrderQuote_CompanyDTOs = Companys
                .Select(x => new OrderQuote_CompanyDTO(x)).ToList();
            return OrderQuote_CompanyDTOs;
        }
        [Route(OrderQuoteRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ContactDTO>>> SingleListContact([FromBody] OrderQuote_ContactFilterDTO OrderQuote_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Skip = 0;
            ContactFilter.Take = 20;
            ContactFilter.OrderBy = ContactOrder.Id;
            ContactFilter.OrderType = OrderType.ASC;
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Id = OrderQuote_ContactFilterDTO.Id;
            ContactFilter.Name = OrderQuote_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = OrderQuote_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = OrderQuote_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<OrderQuote_ContactDTO> OrderQuote_ContactDTOs = Contacts
                .Select(x => new OrderQuote_ContactDTO(x)).ToList();
            return OrderQuote_ContactDTOs;
        }
        [Route(OrderQuoteRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<OrderQuote_DistrictDTO>>> SingleListDistrict([FromBody] OrderQuote_DistrictFilterDTO OrderQuote_DistrictFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = OrderQuote_DistrictFilterDTO.Id;
            DistrictFilter.Code = OrderQuote_DistrictFilterDTO.Code;
            DistrictFilter.Name = OrderQuote_DistrictFilterDTO.Name;
            DistrictFilter.Priority = OrderQuote_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = OrderQuote_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = OrderQuote_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<OrderQuote_DistrictDTO> OrderQuote_DistrictDTOs = Districts
                .Select(x => new OrderQuote_DistrictDTO(x)).ToList();
            return OrderQuote_DistrictDTOs;
        }
        [Route(OrderQuoteRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<OrderQuote_NationDTO>>> SingleListNation([FromBody] OrderQuote_NationFilterDTO OrderQuote_NationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            NationFilter NationFilter = new NationFilter();
            NationFilter.Skip = 0;
            NationFilter.Take = 20;
            NationFilter.OrderBy = NationOrder.Id;
            NationFilter.OrderType = OrderType.ASC;
            NationFilter.Selects = NationSelect.ALL;
            NationFilter.Id = OrderQuote_NationFilterDTO.Id;
            NationFilter.Code = OrderQuote_NationFilterDTO.Code;
            NationFilter.Name = OrderQuote_NationFilterDTO.Name;
            NationFilter.Priority = OrderQuote_NationFilterDTO.DisplayOrder;
            NationFilter.StatusId = OrderQuote_NationFilterDTO.StatusId;

            List<Nation> Nations = await NationService.List(NationFilter);
            List<OrderQuote_NationDTO> OrderQuote_NationDTOs = Nations
                .Select(x => new OrderQuote_NationDTO(x)).ToList();
            return OrderQuote_NationDTOs;
        }
        [Route(OrderQuoteRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ProvinceDTO>>> SingleListProvince([FromBody] OrderQuote_ProvinceFilterDTO OrderQuote_ProvinceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Id = OrderQuote_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = OrderQuote_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = OrderQuote_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = OrderQuote_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = OrderQuote_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<OrderQuote_ProvinceDTO> OrderQuote_ProvinceDTOs = Provinces
                .Select(x => new OrderQuote_ProvinceDTO(x)).ToList();
            return OrderQuote_ProvinceDTOs;
        }
        [Route(OrderQuoteRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<OrderQuote_OpportunityDTO>>> SingleListOpportunity([FromBody] OrderQuote_OpportunityFilterDTO OrderQuote_OpportunityFilterDTO)
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
            OpportunityFilter.Id = OrderQuote_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = OrderQuote_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = OrderQuote_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.ClosingDate = OrderQuote_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = OrderQuote_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = OrderQuote_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = OrderQuote_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = OrderQuote_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.CreatorId = OrderQuote_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.Amount = OrderQuote_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = OrderQuote_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = OrderQuote_OpportunityFilterDTO.Description;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<OrderQuote_OpportunityDTO> OrderQuote_OpportunityDTOs = Opportunities
                .Select(x => new OrderQuote_OpportunityDTO(x)).ToList();
            return OrderQuote_OpportunityDTOs;
        }
        [Route(OrderQuoteRoute.SingleListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<OrderQuote_OrderQuoteStatusDTO>>> SingleListOrderQuoteStatus([FromBody] OrderQuote_OrderQuoteStatusFilterDTO OrderQuote_OrderQuoteStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter();
            OrderQuoteStatusFilter.Skip = 0;
            OrderQuoteStatusFilter.Take = 20;
            OrderQuoteStatusFilter.OrderBy = OrderQuoteStatusOrder.Id;
            OrderQuoteStatusFilter.OrderType = OrderType.ASC;
            OrderQuoteStatusFilter.Selects = OrderQuoteStatusSelect.ALL;
            OrderQuoteStatusFilter.Id = OrderQuote_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = OrderQuote_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = OrderQuote_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<OrderQuote_OrderQuoteStatusDTO> OrderQuote_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new OrderQuote_OrderQuoteStatusDTO(x)).ToList();
            return OrderQuote_OrderQuoteStatusDTOs;
        }
        [Route(OrderQuoteRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<OrderQuote_AppUserDTO>>> SingleListAppUser([FromBody] OrderQuote_AppUserFilterDTO OrderQuote_AppUserFilterDTO)
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
            AppUserFilter.Id = OrderQuote_AppUserFilterDTO.Id;
            AppUserFilter.Username = OrderQuote_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = OrderQuote_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = OrderQuote_AppUserFilterDTO.Address;
            AppUserFilter.Email = OrderQuote_AppUserFilterDTO.Email;
            AppUserFilter.Phone = OrderQuote_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = OrderQuote_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = OrderQuote_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = OrderQuote_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = OrderQuote_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = OrderQuote_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = OrderQuote_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = OrderQuote_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = OrderQuote_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = OrderQuote_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = OrderQuote_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<OrderQuote_AppUserDTO> OrderQuote_AppUserDTOs = AppUsers
                .Select(x => new OrderQuote_AppUserDTO(x)).ToList();
            return OrderQuote_AppUserDTOs;
        }
        [Route(OrderQuoteRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] OrderQuote_ProductGroupingFilterDTO OrderQuote_ProductGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductGroupingFilter ProductGroupingFilter = new ProductGroupingFilter();
            ProductGroupingFilter.Skip = 0;
            ProductGroupingFilter.Take = 20;
            ProductGroupingFilter.OrderBy = ProductGroupingOrder.Id;
            ProductGroupingFilter.OrderType = OrderType.ASC;
            ProductGroupingFilter.Selects = ProductGroupingSelect.Id | ProductGroupingSelect.Code
                | ProductGroupingSelect.Name | ProductGroupingSelect.Parent;

            if (ProductGroupingFilter.Id == null) ProductGroupingFilter.Id = new IdFilter();
            ProductGroupingFilter.Id.In = await FilterProductGrouping(ProductGroupingService, CurrentContext);

            List<ProductGrouping> OrderQuoteGroupings = await ProductGroupingService.List(ProductGroupingFilter);
            List<OrderQuote_ProductGroupingDTO> OrderQuote_ProductGroupingDTOs = OrderQuoteGroupings
                .Select(x => new OrderQuote_ProductGroupingDTO(x)).ToList();
            return OrderQuote_ProductGroupingDTOs;
        }
        [Route(OrderQuoteRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ProductTypeDTO>>> SingleListProductType([FromBody] OrderQuote_ProductTypeFilterDTO IndirectSalesOrder_ProductTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter();
            ProductTypeFilter.Skip = 0;
            ProductTypeFilter.Take = 20;
            ProductTypeFilter.OrderBy = ProductTypeOrder.Id;
            ProductTypeFilter.OrderType = OrderType.ASC;
            ProductTypeFilter.Selects = ProductTypeSelect.ALL;
            ProductTypeFilter.Id = IndirectSalesOrder_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = IndirectSalesOrder_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = IndirectSalesOrder_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = IndirectSalesOrder_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<OrderQuote_ProductTypeDTO> OrderQuote_ProductTypeDTOs = ProductTypes
                .Select(x => new OrderQuote_ProductTypeDTO(x)).ToList();
            return OrderQuote_ProductTypeDTOs;
        }
        [Route(OrderQuoteRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<OrderQuote_SupplierDTO>>> SingleListSupplier([FromBody] OrderQuote_SupplierFilterDTO Product_SupplierFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            SupplierFilter.Id = Product_SupplierFilterDTO.Id;
            SupplierFilter.Code = Product_SupplierFilterDTO.Code;
            SupplierFilter.Name = Product_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Product_SupplierFilterDTO.TaxCode;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<OrderQuote_SupplierDTO> OrderQuote_SupplierDTOs = Suppliers
                .Select(x => new OrderQuote_SupplierDTO(x)).ToList();
            return OrderQuote_SupplierDTOs;
        }
        [Route(OrderQuoteRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<OrderQuote_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] OrderQuote_EditedPriceStatusFilterDTO OrderQuote_EditedPriceStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EditedPriceStatusFilter EditedPriceStatusFilter = new EditedPriceStatusFilter();
            EditedPriceStatusFilter.Skip = 0;
            EditedPriceStatusFilter.Take = 20;
            EditedPriceStatusFilter.OrderBy = EditedPriceStatusOrder.Id;
            EditedPriceStatusFilter.OrderType = OrderType.ASC;
            EditedPriceStatusFilter.Selects = EditedPriceStatusSelect.ALL;
            EditedPriceStatusFilter.Id = OrderQuote_EditedPriceStatusFilterDTO.Id;
            EditedPriceStatusFilter.Code = OrderQuote_EditedPriceStatusFilterDTO.Code;
            EditedPriceStatusFilter.Name = OrderQuote_EditedPriceStatusFilterDTO.Name;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<OrderQuote_EditedPriceStatusDTO> OrderQuote_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new OrderQuote_EditedPriceStatusDTO(x)).ToList();
            return OrderQuote_EditedPriceStatusDTOs;
        }

        [Route(OrderQuoteRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<OrderQuote_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] OrderQuote_UnitOfMeasureFilterDTO OrderQuote_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            //TODO cần optimize lại phần này, sử dụng itemId thay vì productId

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = OrderQuote_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<OrderQuote_UnitOfMeasureDTO> OrderQuote_UnitOfMeasureDTOs = new List<OrderQuote_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                OrderQuote_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new OrderQuote_UnitOfMeasureDTO(x)).ToList();
            }
            OrderQuote_UnitOfMeasureDTO OrderQuote_UnitOfMeasureDTO = new OrderQuote_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            OrderQuote_UnitOfMeasureDTOs.Add(OrderQuote_UnitOfMeasureDTO);
            OrderQuote_UnitOfMeasureDTOs = OrderQuote_UnitOfMeasureDTOs.Distinct().ToList();
            return OrderQuote_UnitOfMeasureDTOs;
        }

        [Route(OrderQuoteRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<OrderQuote_TaxTypeDTO>>> SingleListTaxType([FromBody] OrderQuote_TaxTypeFilterDTO InOrderQuote_TaxTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TaxTypeFilter TaxTypeFilter = new TaxTypeFilter();
            TaxTypeFilter.Skip = 0;
            TaxTypeFilter.Take = 20;
            TaxTypeFilter.OrderBy = TaxTypeOrder.Id;
            TaxTypeFilter.OrderType = OrderType.ASC;
            TaxTypeFilter.Selects = TaxTypeSelect.ALL;
            TaxTypeFilter.Id = InOrderQuote_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = InOrderQuote_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = InOrderQuote_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<OrderQuote_TaxTypeDTO> OrderQuote_TaxTypeDTOs = TaxTypes
                .Select(x => new OrderQuote_TaxTypeDTO(x)).ToList();
            return OrderQuote_TaxTypeDTOs;
        }

        [Route(OrderQuoteRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<OrderQuote_ItemDTO>>> ListItem([FromBody] OrderQuote_ItemFilterDTO OrderQuote_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = OrderQuote_ItemFilterDTO.Skip;
            ItemFilter.Take = OrderQuote_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = OrderQuote_ItemFilterDTO.Id;
            ItemFilter.Code = OrderQuote_ItemFilterDTO.Code;
            ItemFilter.Name = OrderQuote_ItemFilterDTO.Name;
            ItemFilter.ProductTypeId = OrderQuote_ItemFilterDTO.ProductTypeId;
            ItemFilter.SupplierId = OrderQuote_ItemFilterDTO.SupplierId;
            ItemFilter.ProductGroupingId = OrderQuote_ItemFilterDTO.ProductGroupingId;
            ItemFilter.ProductId = OrderQuote_ItemFilterDTO.ProductId;
            ItemFilter.RetailPrice = OrderQuote_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = OrderQuote_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = OrderQuote_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            ItemFilter = ItemService.ToFilter(ItemFilter);

            List<Item> Items = await ItemService.List(ItemFilter);
            List<OrderQuote_ItemDTO> OrderQuote_ItemDTOs = Items
                .Select(x => new OrderQuote_ItemDTO(x)).ToList();
            return OrderQuote_ItemDTOs;
        }
        [Route(OrderQuoteRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] OrderQuote_ItemFilterDTO OrderQuote_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = OrderQuote_ItemFilterDTO.Id;
            ItemFilter.Code = OrderQuote_ItemFilterDTO.Code;
            ItemFilter.Name = OrderQuote_ItemFilterDTO.Name;
            ItemFilter.ProductId = OrderQuote_ItemFilterDTO.ProductId;
            ItemFilter.RetailPrice = OrderQuote_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = OrderQuote_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = OrderQuote_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            ItemFilter = ItemService.ToFilter(ItemFilter);

            return await ItemService.Count(ItemFilter);
        }

    }
}

