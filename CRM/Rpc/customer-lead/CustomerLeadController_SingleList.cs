using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common; 
using Microsoft.AspNetCore.Mvc; 
using CRM.Entities; 
using CRM.Enums;

namespace CRM.Rpc.customer_lead
{
    public partial class CustomerLeadController : RpcController
    {
        [Route(CustomerLeadRoute.FilterListActivityStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ActivityStatusDTO>>> FilterListActivityStatus([FromBody] CustomerLead_ActivityStatusFilterDTO CustomerLead_ActivityStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityStatusFilter ActivityStatusFilter = new ActivityStatusFilter();
            ActivityStatusFilter.Skip = 0;
            ActivityStatusFilter.Take = 20;
            ActivityStatusFilter.OrderBy = ActivityStatusOrder.Id;
            ActivityStatusFilter.OrderType = OrderType.ASC;
            ActivityStatusFilter.Selects = ActivityStatusSelect.ALL;
            ActivityStatusFilter.Id = CustomerLead_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = CustomerLead_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = CustomerLead_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<CustomerLead_ActivityStatusDTO> CustomerLead_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new CustomerLead_ActivityStatusDTO(x)).ToList();
            return CustomerLead_ActivityStatusDTOs;
        }
        [Route(CustomerLeadRoute.FilterListActivityType), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ActivityTypeDTO>>> FilterListActivityType([FromBody] CustomerLead_ActivityTypeFilterDTO CustomerLead_ActivityTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityTypeFilter ActivityTypeFilter = new ActivityTypeFilter();
            ActivityTypeFilter.Skip = 0;
            ActivityTypeFilter.Take = 20;
            ActivityTypeFilter.OrderBy = ActivityTypeOrder.Id;
            ActivityTypeFilter.OrderType = OrderType.ASC;
            ActivityTypeFilter.Selects = ActivityTypeSelect.ALL;
            ActivityTypeFilter.Id = CustomerLead_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = CustomerLead_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = CustomerLead_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<CustomerLead_ActivityTypeDTO> CustomerLead_ActivityTypeDTOs = ActivityTypes
                .Select(x => new CustomerLead_ActivityTypeDTO(x)).ToList();
            return CustomerLead_ActivityTypeDTOs;
        }
        [Route(CustomerLeadRoute.FilterListCustomerLeadLevel), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadLevelDTO>>> FilterListCustomerLeadLevel([FromBody] CustomerLead_CustomerLeadLevelFilterDTO CustomerLead_CustomerLeadLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadLevelFilter CustomerLeadLevelFilter = new CustomerLeadLevelFilter();
            CustomerLeadLevelFilter.Skip = 0;
            CustomerLeadLevelFilter.Take = 20;
            CustomerLeadLevelFilter.OrderBy = CustomerLeadLevelOrder.Id;
            CustomerLeadLevelFilter.OrderType = OrderType.ASC;
            CustomerLeadLevelFilter.Selects = CustomerLeadLevelSelect.ALL;
            CustomerLeadLevelFilter.Id = CustomerLead_CustomerLeadLevelFilterDTO.Id;
            CustomerLeadLevelFilter.Code = CustomerLead_CustomerLeadLevelFilterDTO.Code;
            CustomerLeadLevelFilter.Name = CustomerLead_CustomerLeadLevelFilterDTO.Name;

            List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);
            List<CustomerLead_CustomerLeadLevelDTO> CustomerLead_CustomerLeadLevelDTOs = CustomerLeadLevels
                .Select(x => new CustomerLead_CustomerLeadLevelDTO(x)).ToList();
            return CustomerLead_CustomerLeadLevelDTOs;
        }
        [Route(CustomerLeadRoute.FilterListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadSourceDTO>>> FilterListCustomerLeadSource([FromBody] CustomerLead_CustomerLeadSourceFilterDTO CustomerLead_CustomerLeadSourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadSourceFilter CustomerLeadSourceFilter = new CustomerLeadSourceFilter();
            CustomerLeadSourceFilter.Skip = 0;
            CustomerLeadSourceFilter.Take = 20;
            CustomerLeadSourceFilter.OrderBy = CustomerLeadSourceOrder.Id;
            CustomerLeadSourceFilter.OrderType = OrderType.ASC;
            CustomerLeadSourceFilter.Selects = CustomerLeadSourceSelect.ALL;
            CustomerLeadSourceFilter.Id = CustomerLead_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = CustomerLead_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = CustomerLead_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<CustomerLead_CustomerLeadSourceDTO> CustomerLead_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new CustomerLead_CustomerLeadSourceDTO(x)).ToList();
            return CustomerLead_CustomerLeadSourceDTOs;
        }
        [Route(CustomerLeadRoute.FilterListCustomerLeadStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadStatusDTO>>> FilterListCustomerLeadStatus([FromBody] CustomerLead_CustomerLeadStatusFilterDTO CustomerLead_CustomerLeadStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
            CustomerLeadStatusFilter.Skip = 0;
            CustomerLeadStatusFilter.Take = 20;
            CustomerLeadStatusFilter.OrderBy = CustomerLeadStatusOrder.Id;
            CustomerLeadStatusFilter.OrderType = OrderType.ASC;
            CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
            CustomerLeadStatusFilter.Id = CustomerLead_CustomerLeadStatusFilterDTO.Id;
            CustomerLeadStatusFilter.Code = CustomerLead_CustomerLeadStatusFilterDTO.Code;
            CustomerLeadStatusFilter.Name = CustomerLead_CustomerLeadStatusFilterDTO.Name;

            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            List<CustomerLead_CustomerLeadStatusDTO> CustomerLead_CustomerLeadStatusDTOs = CustomerLeadStatuses
                .Select(x => new CustomerLead_CustomerLeadStatusDTO(x)).ToList();
            return CustomerLead_CustomerLeadStatusDTOs;
        }
        [Route(CustomerLeadRoute.FilterListDistrict), HttpPost]
        public async Task<ActionResult<List<CustomerLead_DistrictDTO>>> FilterListDistrict([FromBody] CustomerLead_DistrictFilterDTO CustomerLead_DistrictFilterDTO)
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
            DistrictFilter.Id = CustomerLead_DistrictFilterDTO.Id;
            DistrictFilter.Code = CustomerLead_DistrictFilterDTO.Code;
            DistrictFilter.Name = CustomerLead_DistrictFilterDTO.Name;
            DistrictFilter.Priority = CustomerLead_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = CustomerLead_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = CustomerLead_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<CustomerLead_DistrictDTO> CustomerLead_DistrictDTOs = Districts
                .Select(x => new CustomerLead_DistrictDTO(x)).ToList();
            return CustomerLead_DistrictDTOs;
        }
        [Route(CustomerLeadRoute.FilterListProfession), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProfessionDTO>>> FilterListProfession([FromBody] CustomerLead_ProfessionFilterDTO CustomerLead_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter.Skip = 0;
            ProfessionFilter.Take = 20;
            ProfessionFilter.OrderBy = ProfessionOrder.Id;
            ProfessionFilter.OrderType = OrderType.ASC;
            ProfessionFilter.Selects = ProfessionSelect.ALL;
            ProfessionFilter.Id = CustomerLead_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = CustomerLead_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = CustomerLead_ProfessionFilterDTO.Name;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<CustomerLead_ProfessionDTO> CustomerLead_ProfessionDTOs = Professions
                .Select(x => new CustomerLead_ProfessionDTO(x)).ToList();
            return CustomerLead_ProfessionDTOs;
        }
        [Route(CustomerLeadRoute.FilterListProvince), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProvinceDTO>>> FilterListProvince([FromBody] CustomerLead_ProvinceFilterDTO CustomerLead_ProvinceFilterDTO)
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
            ProvinceFilter.Id = CustomerLead_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = CustomerLead_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = CustomerLead_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = CustomerLead_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = CustomerLead_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<CustomerLead_ProvinceDTO> CustomerLead_ProvinceDTOs = Provinces
                .Select(x => new CustomerLead_ProvinceDTO(x)).ToList();
            return CustomerLead_ProvinceDTOs;
        }
        [Route(CustomerLeadRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<CustomerLead_AppUserDTO>>> FilterListAppUser([FromBody] CustomerLead_AppUserFilterDTO CustomerLead_AppUserFilterDTO)
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
            AppUserFilter.Id = CustomerLead_AppUserFilterDTO.Id;
            AppUserFilter.Username = CustomerLead_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CustomerLead_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CustomerLead_AppUserFilterDTO.Address;
            AppUserFilter.Email = CustomerLead_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CustomerLead_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CustomerLead_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CustomerLead_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = CustomerLead_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = CustomerLead_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = CustomerLead_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CustomerLead_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = CustomerLead_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = CustomerLead_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = CustomerLead_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = CustomerLead_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            ////AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CustomerLead_AppUserDTO> CustomerLead_AppUserDTOs = AppUsers
                .Select(x => new CustomerLead_AppUserDTO(x)).ToList();
            return CustomerLead_AppUserDTOs;
        }

        [Route(CustomerLeadRoute.FilterListProductGrouping), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProductGroupingDTO>>> FilterListProductGrouping([FromBody] CustomerLead_ProductGroupingFilterDTO CustomerLead_ProductGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ProductGroupingFilter ProductGroupingFilter = new ProductGroupingFilter();
            ProductGroupingFilter.Skip = 0;
            ProductGroupingFilter.Take = int.MaxValue;
            ProductGroupingFilter.OrderBy = ProductGroupingOrder.Id;
            ProductGroupingFilter.OrderType = OrderType.ASC;
            ProductGroupingFilter.Selects = ProductGroupingSelect.Id | ProductGroupingSelect.Code
                | ProductGroupingSelect.Name | ProductGroupingSelect.Parent;

            if (ProductGroupingFilter.Id == null) ProductGroupingFilter.Id = new IdFilter();
            ProductGroupingFilter.Id.In = await FilterProductGrouping(ProductGroupingService, CurrentContext);
            List<ProductGrouping> ProductGroupings = await ProductGroupingService.List(ProductGroupingFilter);
            List<CustomerLead_ProductGroupingDTO> CustomerLead_ProductGroupingDTOs = ProductGroupings
                .Select(x => new CustomerLead_ProductGroupingDTO(x)).ToList();
            return CustomerLead_ProductGroupingDTOs;
        }

        [Route(CustomerLeadRoute.FilterListProductType), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProductTypeDTO>>> FilterListProductType([FromBody] CustomerLead_ProductTypeFilterDTO CustomerLead_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = CustomerLead_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = CustomerLead_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = CustomerLead_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = CustomerLead_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = CustomerLead_ProductTypeFilterDTO.StatusId;

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<CustomerLead_ProductTypeDTO> CustomerLead_ProductTypeDTOs = ProductTypes
                .Select(x => new CustomerLead_ProductTypeDTO(x)).ToList();
            return CustomerLead_ProductTypeDTOs;
        }
        [Route(CustomerLeadRoute.FilterListSupplier), HttpPost]
        public async Task<ActionResult<List<CustomerLead_SupplierDTO>>> FilterListSupplier([FromBody] CustomerLead_SupplierFilterDTO CustomerLead_SupplierFilterDTO)
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
            SupplierFilter.Id = CustomerLead_SupplierFilterDTO.Id;
            SupplierFilter.Code = CustomerLead_SupplierFilterDTO.Code;
            SupplierFilter.Name = CustomerLead_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = CustomerLead_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = CustomerLead_SupplierFilterDTO.Phone;
            SupplierFilter.Email = CustomerLead_SupplierFilterDTO.Email;
            SupplierFilter.Address = CustomerLead_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = CustomerLead_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = CustomerLead_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = CustomerLead_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = CustomerLead_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = CustomerLead_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = CustomerLead_SupplierFilterDTO.StatusId;
            SupplierFilter.Description = CustomerLead_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<CustomerLead_SupplierDTO> CustomerLead_SupplierDTOs = Suppliers
                .Select(x => new CustomerLead_SupplierDTO(x)).ToList();
            return CustomerLead_SupplierDTOs;
        }

        [Route(CustomerLeadRoute.FilterListItem), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ItemDTO>>> FilterListItem([FromBody] CustomerLead_ItemFilterDTO CustomerLead_ItemFilterDTO)
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
            ItemFilter.Id = CustomerLead_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerLead_ItemFilterDTO.ProductId;
            ItemFilter.Code = CustomerLead_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerLead_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerLead_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = CustomerLead_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerLead_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = CustomerLead_ItemFilterDTO.StatusId;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<CustomerLead_ItemDTO> CustomerLead_ItemDTOs = Items
                .Select(x => new CustomerLead_ItemDTO(x)).ToList();
            return CustomerLead_ItemDTOs;
        }

        [Route(CustomerLeadRoute.SingleListCustomerLeadLevel), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadLevelDTO>>> SingleListCustomerLeadLevel([FromBody] CustomerLead_CustomerLeadLevelFilterDTO CustomerLead_CustomerLeadLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadLevelFilter CustomerLeadLevelFilter = new CustomerLeadLevelFilter();
            CustomerLeadLevelFilter.Skip = 0;
            CustomerLeadLevelFilter.Take = 20;
            CustomerLeadLevelFilter.OrderBy = CustomerLeadLevelOrder.Id;
            CustomerLeadLevelFilter.OrderType = OrderType.ASC;
            CustomerLeadLevelFilter.Selects = CustomerLeadLevelSelect.ALL;
            CustomerLeadLevelFilter.Id = CustomerLead_CustomerLeadLevelFilterDTO.Id;
            CustomerLeadLevelFilter.Code = CustomerLead_CustomerLeadLevelFilterDTO.Code;
            CustomerLeadLevelFilter.Name = CustomerLead_CustomerLeadLevelFilterDTO.Name;

            List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);
            List<CustomerLead_CustomerLeadLevelDTO> CustomerLead_CustomerLeadLevelDTOs = CustomerLeadLevels
                .Select(x => new CustomerLead_CustomerLeadLevelDTO(x)).ToList();
            return CustomerLead_CustomerLeadLevelDTOs;
        }
        [Route(CustomerLeadRoute.SingleListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadSourceDTO>>> SingleListCustomerLeadSource([FromBody] CustomerLead_CustomerLeadSourceFilterDTO CustomerLead_CustomerLeadSourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadSourceFilter CustomerLeadSourceFilter = new CustomerLeadSourceFilter();
            CustomerLeadSourceFilter.Skip = 0;
            CustomerLeadSourceFilter.Take = 20;
            CustomerLeadSourceFilter.OrderBy = CustomerLeadSourceOrder.Id;
            CustomerLeadSourceFilter.OrderType = OrderType.ASC;
            CustomerLeadSourceFilter.Selects = CustomerLeadSourceSelect.ALL;
            CustomerLeadSourceFilter.Id = CustomerLead_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = CustomerLead_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = CustomerLead_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<CustomerLead_CustomerLeadSourceDTO> CustomerLead_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new CustomerLead_CustomerLeadSourceDTO(x)).ToList();
            return CustomerLead_CustomerLeadSourceDTOs;
        }
        [Route(CustomerLeadRoute.SingleListCustomerLeadStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadStatusDTO>>> SingleListCustomerLeadStatus([FromBody] CustomerLead_CustomerLeadStatusFilterDTO CustomerLead_CustomerLeadStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
            CustomerLeadStatusFilter.Skip = 0;
            CustomerLeadStatusFilter.Take = 20;
            CustomerLeadStatusFilter.OrderBy = CustomerLeadStatusOrder.Id;
            CustomerLeadStatusFilter.OrderType = OrderType.ASC;
            CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
            CustomerLeadStatusFilter.Id = CustomerLead_CustomerLeadStatusFilterDTO.Id;
            CustomerLeadStatusFilter.Code = CustomerLead_CustomerLeadStatusFilterDTO.Code;
            CustomerLeadStatusFilter.Name = CustomerLead_CustomerLeadStatusFilterDTO.Name;

            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            List<CustomerLead_CustomerLeadStatusDTO> CustomerLead_CustomerLeadStatusDTOs = CustomerLeadStatuses
                .Select(x => new CustomerLead_CustomerLeadStatusDTO(x)).ToList();
            return CustomerLead_CustomerLeadStatusDTOs;
        }

        [Route(CustomerLeadRoute.SingleListActivityStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ActivityStatusDTO>>> SingleListActivityStatus([FromBody] CustomerLead_ActivityStatusFilterDTO CustomerLead_ActivityStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityStatusFilter ActivityStatusFilter = new ActivityStatusFilter();
            ActivityStatusFilter.Skip = 0;
            ActivityStatusFilter.Take = 20;
            ActivityStatusFilter.OrderBy = ActivityStatusOrder.Id;
            ActivityStatusFilter.OrderType = OrderType.ASC;
            ActivityStatusFilter.Selects = ActivityStatusSelect.ALL;
            ActivityStatusFilter.Id = CustomerLead_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = CustomerLead_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = CustomerLead_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<CustomerLead_ActivityStatusDTO> CustomerLead_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new CustomerLead_ActivityStatusDTO(x)).ToList();
            return CustomerLead_ActivityStatusDTOs;
        }
        [Route(CustomerLeadRoute.SingleListActivityType), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ActivityTypeDTO>>> SingleListActivityType([FromBody] CustomerLead_ActivityTypeFilterDTO CustomerLead_ActivityTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityTypeFilter ActivityTypeFilter = new ActivityTypeFilter();
            ActivityTypeFilter.Skip = 0;
            ActivityTypeFilter.Take = 20;
            ActivityTypeFilter.OrderBy = ActivityTypeOrder.Id;
            ActivityTypeFilter.OrderType = OrderType.ASC;
            ActivityTypeFilter.Selects = ActivityTypeSelect.ALL;
            ActivityTypeFilter.Id = CustomerLead_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = CustomerLead_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = CustomerLead_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<CustomerLead_ActivityTypeDTO> CustomerLead_ActivityTypeDTOs = ActivityTypes
                .Select(x => new CustomerLead_ActivityTypeDTO(x)).ToList();
            return CustomerLead_ActivityTypeDTOs;
        }

        [Route(CustomerLeadRoute.SingleListActivityPriority), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ActivityPriorityDTO>>> SingleListActivityPriority([FromBody] CustomerLead_ActivityPriorityFilterDTO CustomerLead_ActivityPriorityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityPriorityFilter ActivityPriorityFilter = new ActivityPriorityFilter();
            ActivityPriorityFilter.Skip = 0;
            ActivityPriorityFilter.Take = 20;
            ActivityPriorityFilter.OrderBy = ActivityPriorityOrder.Id;
            ActivityPriorityFilter.OrderType = OrderType.ASC;
            ActivityPriorityFilter.Selects = ActivityPrioritySelect.ALL;
            ActivityPriorityFilter.Id = CustomerLead_ActivityPriorityFilterDTO.Id;
            ActivityPriorityFilter.Code = CustomerLead_ActivityPriorityFilterDTO.Code;
            ActivityPriorityFilter.Name = CustomerLead_ActivityPriorityFilterDTO.Name;

            List<ActivityPriority> ActivityPriorities = await ActivityPriorityService.List(ActivityPriorityFilter);
            List<CustomerLead_ActivityPriorityDTO> CustomerLead_ActivityPriorityDTOs = ActivityPriorities
                .Select(x => new CustomerLead_ActivityPriorityDTO(x)).ToList();
            return CustomerLead_ActivityPriorityDTOs;
        }

        [Route(CustomerLeadRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<CustomerLead_DistrictDTO>>> SingleListDistrict([FromBody] CustomerLead_DistrictFilterDTO CustomerLead_DistrictFilterDTO)
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
            DistrictFilter.Id = CustomerLead_DistrictFilterDTO.Id;
            DistrictFilter.Code = CustomerLead_DistrictFilterDTO.Code;
            DistrictFilter.Name = CustomerLead_DistrictFilterDTO.Name;
            DistrictFilter.Priority = CustomerLead_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = CustomerLead_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<CustomerLead_DistrictDTO> CustomerLead_DistrictDTOs = Districts
                .Select(x => new CustomerLead_DistrictDTO(x)).ToList();
            return CustomerLead_DistrictDTOs;
        }
        [Route(CustomerLeadRoute.SingleListProfession), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProfessionDTO>>> SingleListProfession([FromBody] CustomerLead_ProfessionFilterDTO CustomerLead_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter.Skip = 0;
            ProfessionFilter.Take = 20;
            ProfessionFilter.OrderBy = ProfessionOrder.Id;
            ProfessionFilter.OrderType = OrderType.ASC;
            ProfessionFilter.Selects = ProfessionSelect.ALL;
            ProfessionFilter.Id = CustomerLead_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = CustomerLead_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = CustomerLead_ProfessionFilterDTO.Name;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<CustomerLead_ProfessionDTO> CustomerLead_ProfessionDTOs = Professions
                .Select(x => new CustomerLead_ProfessionDTO(x)).ToList();
            return CustomerLead_ProfessionDTOs;
        }
        [Route(CustomerLeadRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProvinceDTO>>> SingleListProvince([FromBody] CustomerLead_ProvinceFilterDTO CustomerLead_ProvinceFilterDTO)
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
            ProvinceFilter.Id = CustomerLead_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = CustomerLead_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = CustomerLead_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = CustomerLead_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<CustomerLead_ProvinceDTO> CustomerLead_ProvinceDTOs = Provinces
                .Select(x => new CustomerLead_ProvinceDTO(x)).ToList();
            return CustomerLead_ProvinceDTOs;
        }
        [Route(CustomerLeadRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<CustomerLead_AppUserDTO>>> SingleListAppUser([FromBody] CustomerLead_AppUserFilterDTO CustomerLead_AppUserFilterDTO)
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
            AppUserFilter.Id = CustomerLead_AppUserFilterDTO.Id;
            AppUserFilter.Username = CustomerLead_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CustomerLead_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CustomerLead_AppUserFilterDTO.Address;
            AppUserFilter.Email = CustomerLead_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CustomerLead_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CustomerLead_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CustomerLead_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = CustomerLead_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = CustomerLead_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = CustomerLead_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CustomerLead_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = CustomerLead_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = CustomerLead_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = CustomerLead_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CustomerLead_AppUserDTO> CustomerLead_AppUserDTOs = AppUsers
                .Select(x => new CustomerLead_AppUserDTO(x)).ToList();
            return CustomerLead_AppUserDTOs;
        }
        [Route(CustomerLeadRoute.SingleListProduct), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProductDTO>>> SingleListProduct([FromBody] CustomerLead_ProductFilterDTO CustomerLead_ProductFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            ProductFilter.Id = CustomerLead_ProductFilterDTO.Id;
            ProductFilter.Name = CustomerLead_ProductFilterDTO.Name;
            ProductFilter.Code = CustomerLead_ProductFilterDTO.Code;
            ProductFilter.SupplierId = CustomerLead_ProductFilterDTO.SupplierId;
            ProductFilter.ProductTypeId = CustomerLead_ProductFilterDTO.ProductTypeId;

            List<Product> Products = await ProductService.List(ProductFilter);
            List<CustomerLead_ProductDTO> CustomerLead_ProductDTOs = Products
                .Select(x => new CustomerLead_ProductDTO(x)).ToList();
            return CustomerLead_ProductDTOs;
        }

        [Route(CustomerLeadRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] CustomerLead_ProductGroupingFilterDTO CustomerLead_ProductGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ProductGroupingFilter ProductGroupingFilter = new ProductGroupingFilter();
            ProductGroupingFilter.Skip = 0;
            ProductGroupingFilter.Take = int.MaxValue;
            ProductGroupingFilter.OrderBy = ProductGroupingOrder.Id;
            ProductGroupingFilter.OrderType = OrderType.ASC;
            ProductGroupingFilter.Selects = ProductGroupingSelect.Id | ProductGroupingSelect.Code
                | ProductGroupingSelect.Name | ProductGroupingSelect.Parent;

            if (ProductGroupingFilter.Id == null) ProductGroupingFilter.Id = new IdFilter();
            ProductGroupingFilter.Id.In = await FilterProductGrouping(ProductGroupingService, CurrentContext);
            List<ProductGrouping> ProductGroupings = await ProductGroupingService.List(ProductGroupingFilter);
            List<CustomerLead_ProductGroupingDTO> CustomerLead_ProductGroupingDTOs = ProductGroupings
                .Select(x => new CustomerLead_ProductGroupingDTO(x)).ToList();
            return CustomerLead_ProductGroupingDTOs;
        }

        [Route(CustomerLeadRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProductTypeDTO>>> SingleListProductType([FromBody] CustomerLead_ProductTypeFilterDTO CustomerLead_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = CustomerLead_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = CustomerLead_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = CustomerLead_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = CustomerLead_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<CustomerLead_ProductTypeDTO> CustomerLead_ProductTypeDTOs = ProductTypes
                .Select(x => new CustomerLead_ProductTypeDTO(x)).ToList();
            return CustomerLead_ProductTypeDTOs;
        }
        [Route(CustomerLeadRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<CustomerLead_SupplierDTO>>> SingleListSupplier([FromBody] CustomerLead_SupplierFilterDTO CustomerLead_SupplierFilterDTO)
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
            SupplierFilter.Id = CustomerLead_SupplierFilterDTO.Id;
            SupplierFilter.Code = CustomerLead_SupplierFilterDTO.Code;
            SupplierFilter.Name = CustomerLead_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = CustomerLead_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = CustomerLead_SupplierFilterDTO.Phone;
            SupplierFilter.Email = CustomerLead_SupplierFilterDTO.Email;
            SupplierFilter.Address = CustomerLead_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = CustomerLead_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = CustomerLead_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = CustomerLead_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = CustomerLead_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = CustomerLead_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = CustomerLead_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<CustomerLead_SupplierDTO> CustomerLead_SupplierDTOs = Suppliers
                .Select(x => new CustomerLead_SupplierDTO(x)).ToList();
            return CustomerLead_SupplierDTOs;
        }
        [Route(CustomerLeadRoute.FilterListSex), HttpPost]
        public async Task<ActionResult<List<CustomerLead_SexDTO>>> FilterListSex([FromBody] CustomerLead_SexFilterDTO CustomerLead_SexFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SexFilter SexFilter = new SexFilter();
            SexFilter.Skip = 0;
            SexFilter.Take = 20;
            SexFilter.OrderBy = SexOrder.Id;
            SexFilter.OrderType = OrderType.ASC;
            SexFilter.Selects = SexSelect.ALL;
            SexFilter.Id = CustomerLead_SexFilterDTO.Id;
            SexFilter.Code = CustomerLead_SexFilterDTO.Code;
            SexFilter.Name = CustomerLead_SexFilterDTO.Name;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<CustomerLead_SexDTO> CustomerLead_SexDTOs = Sexes
                .Select(x => new CustomerLead_SexDTO(x)).ToList();
            return CustomerLead_SexDTOs;
        }
        [Route(CustomerLeadRoute.SingleListSex), HttpPost]
        public async Task<ActionResult<List<CustomerLead_SexDTO>>> SingleListSex([FromBody] CustomerLead_SexFilterDTO CustomerLead_SexFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SexFilter SexFilter = new SexFilter();
            SexFilter.Skip = 0;
            SexFilter.Take = 20;
            SexFilter.OrderBy = SexOrder.Id;
            SexFilter.OrderType = OrderType.ASC;
            SexFilter.Selects = SexSelect.ALL;
            SexFilter.Id = CustomerLead_SexFilterDTO.Id;
            SexFilter.Code = CustomerLead_SexFilterDTO.Code;
            SexFilter.Name = CustomerLead_SexFilterDTO.Name;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<CustomerLead_SexDTO> CustomerLead_SexDTOs = Sexes
                .Select(x => new CustomerLead_SexDTO(x)).ToList();
            return CustomerLead_SexDTOs;
        }
        [Route(CustomerLeadRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<CustomerLead_NationDTO>>> SingleListNation([FromBody] CustomerLead_NationFilterDTO CustomerLead_NationFilterDTO)
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
            NationFilter.Id = CustomerLead_NationFilterDTO.Id;
            NationFilter.Code = CustomerLead_NationFilterDTO.Code;
            NationFilter.Name = CustomerLead_NationFilterDTO.Name;
            NationFilter.Priority = CustomerLead_NationFilterDTO.DisplayOrder;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<CustomerLead_NationDTO> CustomerLead_NationDTOs = Nations
                .Select(x => new CustomerLead_NationDTO(x)).ToList();
            return CustomerLead_NationDTOs;
        }

        [Route(CustomerLeadRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ContactDTO>>> SingleListContact([FromBody] CustomerLead_ContactFilterDTO CustomerLead_ContactFilterDTO)
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
            ContactFilter.Id = CustomerLead_ContactFilterDTO.Id;
            ContactFilter.Name = CustomerLead_ContactFilterDTO.Name;
            ContactFilter.Phone = CustomerLead_ContactFilterDTO.Phone;
            ContactFilter.ProfessionId = CustomerLead_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = CustomerLead_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<CustomerLead_ContactDTO> CustomerLead_ContactDTOs = Contacts
                .Select(x => new CustomerLead_ContactDTO(x)).ToList();
            return CustomerLead_ContactDTOs;
        }

        [Route(CustomerLeadRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CompanyDTO>>> SingleListCompany([FromBody] CustomerLead_CompanyFilterDTO CustomerLead_CompanyFilterDTO)
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
            CompanyFilter.Id = CustomerLead_CompanyFilterDTO.Id;
            CompanyFilter.Name = CustomerLead_CompanyFilterDTO.Name;
            CompanyFilter.Phone = CustomerLead_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = CustomerLead_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = CustomerLead_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = CustomerLead_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = CustomerLead_CompanyFilterDTO.EmailOther;
            CompanyFilter.Description = CustomerLead_CompanyFilterDTO.Description;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<CustomerLead_CompanyDTO> CustomerLead_CompanyDTOs = Companys
                .Select(x => new CustomerLead_CompanyDTO(x)).ToList();
            return CustomerLead_CompanyDTOs;
        }
        [Route(CustomerLeadRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<CustomerLead_OpportunityDTO>>> SingleListOpportunity([FromBody] CustomerLead_OpportunityFilterDTO CustomerLead_OpportunityFilterDTO)
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
            OpportunityFilter.Id = CustomerLead_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = CustomerLead_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = CustomerLead_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = CustomerLead_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = CustomerLead_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = CustomerLead_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = CustomerLead_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = CustomerLead_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = CustomerLead_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = CustomerLead_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.Amount = CustomerLead_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = CustomerLead_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = CustomerLead_OpportunityFilterDTO.Description;
            OpportunityFilter.CreatorId = CustomerLead_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.CustomerLeadId = CustomerLead_OpportunityFilterDTO.CustomerLeadId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<CustomerLead_OpportunityDTO> CustomerLead_OpportunityDTOs = Opportunities
                .Select(x => new CustomerLead_OpportunityDTO(x)).ToList();
            return CustomerLead_OpportunityDTOs;
        }
        [Route(CustomerLeadRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<CustomerLead_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] CustomerLead_UnitOfMeasureFilterDTO CustomerLead_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            //TODO cần optimize lại phần này, sử dụng itemId thay vì productId

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = CustomerLead_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<CustomerLead_UnitOfMeasureDTO> CustomerLead_UnitOfMeasureDTOs = new List<CustomerLead_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                CustomerLead_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new CustomerLead_UnitOfMeasureDTO(x)).ToList();
            }
            CustomerLead_UnitOfMeasureDTO CustomerLead_UnitOfMeasureDTO = new CustomerLead_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            CustomerLead_UnitOfMeasureDTOs.Add(CustomerLead_UnitOfMeasureDTO);
            CustomerLead_UnitOfMeasureDTOs = CustomerLead_UnitOfMeasureDTOs.Distinct().ToList();
            return CustomerLead_UnitOfMeasureDTOs;
        }
        [Route(CustomerLeadRoute.SingleListItem), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ItemDTO>>> SingleListItem([FromBody] CustomerLead_ItemFilterDTO CustomerLead_ItemFilterDTO)
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
            ItemFilter.Id = CustomerLead_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerLead_ItemFilterDTO.ProductId;
            ItemFilter.Code = CustomerLead_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerLead_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerLead_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = CustomerLead_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerLead_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<CustomerLead_ItemDTO> CustomerLead_ItemDTOs = Items
                .Select(x => new CustomerLead_ItemDTO(x)).ToList();
            return CustomerLead_ItemDTOs;
        }

        [Route(CustomerLeadRoute.SingleListProbability), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ProbabilityDTO>>> SingleListProbability([FromBody] CustomerLead_ProbabilityFilterDTO CustomerLead_ProbabilityFilterDTO)
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
            ProbabilityFilter.Id = CustomerLead_ProbabilityFilterDTO.Id;
            ProbabilityFilter.Code = CustomerLead_ProbabilityFilterDTO.Code;
            ProbabilityFilter.Name = CustomerLead_ProbabilityFilterDTO.Name;

            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            List<CustomerLead_ProbabilityDTO> CustomerLead_ProbabilityDTOs = Probabilities
                .Select(x => new CustomerLead_ProbabilityDTO(x)).ToList();
            return CustomerLead_ProbabilityDTOs;
        }

        [Route(CustomerLeadRoute.SingleListEmailTemplate), HttpPost]
        public async Task<ActionResult<List<CustomerLead_MailTemplateDTO>>> SingleListMailTemplate([FromBody] CustomerLead_MailTemplateFilterDTO CustomerLead_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter.Skip = 0;
            MailTemplateFilter.Take = 20;
            MailTemplateFilter.OrderBy = MailTemplateOrder.Id;
            MailTemplateFilter.OrderType = OrderType.ASC;
            MailTemplateFilter.Selects = MailTemplateSelect.ALL;
            MailTemplateFilter.Id = CustomerLead_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = CustomerLead_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = CustomerLead_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = CustomerLead_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<CustomerLead_MailTemplateDTO> CustomerLead_MailTemplateDTOs = MailTemplates
                .Select(x => new CustomerLead_MailTemplateDTO(x)).ToList();
            return CustomerLead_MailTemplateDTOs;
        }

        [Route(CustomerLeadRoute.SingleListFileType), HttpPost]
        public async Task<ActionResult<List<CustomerLead_FileTypeDTO>>> SingleListFileType([FromBody] CustomerLead_FileTypeFilterDTO CustomerLead_FileTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            FileTypeFilter FileTypeFilter = new FileTypeFilter();
            FileTypeFilter.Skip = 0;
            FileTypeFilter.Take = 20;
            FileTypeFilter.OrderBy = FileTypeOrder.Id;
            FileTypeFilter.OrderType = OrderType.ASC;
            FileTypeFilter.Selects = FileTypeSelect.ALL;
            FileTypeFilter.Id = CustomerLead_FileTypeFilterDTO.Id;
            FileTypeFilter.Code = CustomerLead_FileTypeFilterDTO.Code;
            FileTypeFilter.Name = CustomerLead_FileTypeFilterDTO.Name;

            List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);
            List<CustomerLead_FileTypeDTO> CustomerLead_FileTypeDTOs = FileTypes
                .Select(x => new CustomerLead_FileTypeDTO(x)).ToList();
            return CustomerLead_FileTypeDTOs;
        }
        [Route(CustomerLeadRoute.SingleListCurrency), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CurrencyDTO>>> SingleListCurrency([FromBody] CustomerLead_CurrencyFilterDTO CustomerLead_CurrencyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CurrencyFilter CurrencyFilter = new CurrencyFilter();
            CurrencyFilter.Skip = 0;
            CurrencyFilter.Take = 20;
            CurrencyFilter.OrderBy = CurrencyOrder.Id;
            CurrencyFilter.OrderType = OrderType.ASC;
            CurrencyFilter.Selects = CurrencySelect.ALL;
            CurrencyFilter.Id = CustomerLead_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = CustomerLead_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = CustomerLead_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<CustomerLead_CurrencyDTO> CustomerLead_CurrencyDTOs = Currencies
                .Select(x => new CustomerLead_CurrencyDTO(x)).ToList();
            return CustomerLead_CurrencyDTOs;
        }


        [Route(CustomerLeadRoute.SingleListSmsTemplate), HttpPost]
        public async Task<ActionResult<List<CustomerLead_SmsTemplateDTO>>> SingleListSmsTemplate([FromBody] CustomerLead_SmsTemplateFilterDTO CustomerLead_SmsTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter.Skip = 0;
            SmsTemplateFilter.Take = 20;
            SmsTemplateFilter.OrderBy = SmsTemplateOrder.Id;
            SmsTemplateFilter.OrderType = OrderType.ASC;
            SmsTemplateFilter.Selects = SmsTemplateSelect.ALL;
            SmsTemplateFilter.Id = CustomerLead_SmsTemplateFilterDTO.Id;
            SmsTemplateFilter.Code = CustomerLead_SmsTemplateFilterDTO.Code;
            SmsTemplateFilter.Name = CustomerLead_SmsTemplateFilterDTO.Name;
            SmsTemplateFilter.Content = CustomerLead_SmsTemplateFilterDTO.Content;

            List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);
            List<CustomerLead_SmsTemplateDTO> CustomerLead_SmsTemplateDTOs = SmsTemplates
                .Select(x => new CustomerLead_SmsTemplateDTO(x)).ToList();
            return CustomerLead_SmsTemplateDTOs;
        }

        [Route(CustomerLeadRoute.FilterListEmailStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLead_EmailStatusDTO>>> FilterListEmailStatus([FromBody] CustomerLead_EmailStatusFilterDTO CustomerLead_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = CustomerLead_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = CustomerLead_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = CustomerLead_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<CustomerLead_EmailStatusDTO> CustomerLead_EmailStatusDTOs = Statuses
                .Select(x => new CustomerLead_EmailStatusDTO(x)).ToList();
            return CustomerLead_EmailStatusDTOs;
        }
    }
}


