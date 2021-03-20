using CRM.Common;
using CRM.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.store
{
    public partial class StoreController : RpcController
    {
        [Route(StoreRoute.FilterListAppUser), HttpPost]
        public async Task<List<Store_AppUserDTO>> FilterListAppUser([FromBody] Store_AppUserFilterDTO Store_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = Store_AppUserFilterDTO.Id;
            AppUserFilter.Username = Store_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Store_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Store_AppUserFilterDTO.Address;
            AppUserFilter.Email = Store_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Store_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Store_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Store_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Store_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Store_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Store_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Store_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Store_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Store_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Store_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Store_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Store_AppUserDTO> Store_AppUserDTOs = AppUsers
                .Select(x => new Store_AppUserDTO(x)).ToList();
            return Store_AppUserDTOs;
        }
        [Route(StoreRoute.FilterListDistrict), HttpPost]
        public async Task<List<Store_DistrictDTO>> FilterListDistrict([FromBody] Store_DistrictFilterDTO Store_DistrictFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = Store_DistrictFilterDTO.Id;
            DistrictFilter.Code = Store_DistrictFilterDTO.Code;
            DistrictFilter.Name = Store_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Store_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Store_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = Store_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Store_DistrictDTO> Store_DistrictDTOs = Districts
                .Select(x => new Store_DistrictDTO(x)).ToList();
            return Store_DistrictDTOs;
        }
        [Route(StoreRoute.FilterListOrganization), HttpPost]
        public async Task<List<Store_OrganizationDTO>> FilterListOrganization([FromBody] Store_OrganizationFilterDTO Store_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Store_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Store_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Store_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Store_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = Store_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = Store_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = Store_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = Store_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = Store_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = Store_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Store_OrganizationDTO> Store_OrganizationDTOs = Organizations
                .Select(x => new Store_OrganizationDTO(x)).ToList();
            return Store_OrganizationDTOs;
        }
        [Route(StoreRoute.FilterListStore), HttpPost]
        public async Task<List<Store_StoreDTO>> FilterListStore([FromBody] Store_StoreFilterDTO Store_StoreFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Skip = 0;
            StoreFilter.Take = 20;
            StoreFilter.OrderBy = StoreOrder.Id;
            StoreFilter.OrderType = OrderType.ASC;
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Id = Store_StoreFilterDTO.Id;
            StoreFilter.Code = Store_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Store_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Store_StoreFilterDTO.Name;
            StoreFilter.UnsignName = Store_StoreFilterDTO.UnsignName;
            StoreFilter.ParentStoreId = Store_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Store_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Store_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Store_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Store_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Store_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Store_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Store_StoreFilterDTO.WardId;
            StoreFilter.Address = Store_StoreFilterDTO.Address;
            StoreFilter.UnsignAddress = Store_StoreFilterDTO.UnsignAddress;
            StoreFilter.DeliveryAddress = Store_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Store_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Store_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Store_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Store_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Store_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Store_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Store_StoreFilterDTO.OwnerEmail;
            StoreFilter.TaxCode = Store_StoreFilterDTO.TaxCode;
            StoreFilter.LegalEntity = Store_StoreFilterDTO.LegalEntity;
            StoreFilter.AppUserId = Store_StoreFilterDTO.AppUserId;
            StoreFilter.StatusId = Store_StoreFilterDTO.StatusId;
            StoreFilter.StoreStatusId = Store_StoreFilterDTO.StoreStatusId;
            StoreFilter.CustomerId = Store_StoreFilterDTO.CustomerId;

            if (StoreFilter.Id == null) StoreFilter.Id = new IdFilter();
            StoreFilter.Id.In = await FilterStore(StoreService, OrganizationService, CurrentContext);

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Store_StoreDTO> Store_StoreDTOs = Stores
                .Select(x => new Store_StoreDTO(x)).ToList();
            return Store_StoreDTOs;
        }
        [Route(StoreRoute.FilterListProvince), HttpPost]
        public async Task<List<Store_ProvinceDTO>> FilterListProvince([FromBody] Store_ProvinceFilterDTO Store_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Id = Store_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Store_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Store_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Store_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = Store_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Store_ProvinceDTO> Store_ProvinceDTOs = Provinces
                .Select(x => new Store_ProvinceDTO(x)).ToList();
            return Store_ProvinceDTOs;
        }
        [Route(StoreRoute.FilterListStatus), HttpPost]
        public async Task<List<Store_StatusDTO>> FilterListStatus([FromBody] Store_StatusFilterDTO Store_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Store_StatusDTO> Store_StatusDTOs = Statuses
                .Select(x => new Store_StatusDTO(x)).ToList();
            return Store_StatusDTOs;
        }
        [Route(StoreRoute.FilterListStoreGrouping), HttpPost]
        public async Task<List<Store_StoreGroupingDTO>> FilterListStoreGrouping([FromBody] Store_StoreGroupingFilterDTO Store_StoreGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreGroupingFilter StoreGroupingFilter = new StoreGroupingFilter();
            StoreGroupingFilter.Skip = 0;
            StoreGroupingFilter.Take = int.MaxValue;
            StoreGroupingFilter.OrderBy = StoreGroupingOrder.Id;
            StoreGroupingFilter.OrderType = OrderType.ASC;
            StoreGroupingFilter.Selects = StoreGroupingSelect.ALL;
            StoreGroupingFilter.Id = Store_StoreGroupingFilterDTO.Id;
            StoreGroupingFilter.Code = Store_StoreGroupingFilterDTO.Code;
            StoreGroupingFilter.Name = Store_StoreGroupingFilterDTO.Name;
            StoreGroupingFilter.ParentId = Store_StoreGroupingFilterDTO.ParentId;
            StoreGroupingFilter.Path = Store_StoreGroupingFilterDTO.Path;
            StoreGroupingFilter.Level = Store_StoreGroupingFilterDTO.Level;
            StoreGroupingFilter.StatusId = Store_StoreGroupingFilterDTO.StatusId;

            if (StoreGroupingFilter.Id == null) StoreGroupingFilter.Id = new IdFilter();
            StoreGroupingFilter.Id.In = await FilterStoreGrouping(StoreGroupingService, CurrentContext);

            List<StoreGrouping> StoreGroupings = await StoreGroupingService.List(StoreGroupingFilter);
            List<Store_StoreGroupingDTO> Store_StoreGroupingDTOs = StoreGroupings
                .Select(x => new Store_StoreGroupingDTO(x)).ToList();
            return Store_StoreGroupingDTOs;
        }
        [Route(StoreRoute.FilterListStoreStatus), HttpPost]
        public async Task<List<Store_StoreStatusDTO>> FilterListStoreStatus([FromBody] Store_StoreStatusFilterDTO Store_StoreStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreStatusFilter StoreStatusFilter = new StoreStatusFilter();
            StoreStatusFilter.Skip = 0;
            StoreStatusFilter.Take = int.MaxValue;
            StoreStatusFilter.Take = 20;
            StoreStatusFilter.OrderBy = StoreStatusOrder.Id;
            StoreStatusFilter.OrderType = OrderType.ASC;
            StoreStatusFilter.Selects = StoreStatusSelect.ALL;

            List<StoreStatus> StoreStatuses = await StoreStatusService.List(StoreStatusFilter);
            List<Store_StoreStatusDTO> Store_StoreStatusDTOs = StoreStatuses
                .Select(x => new Store_StoreStatusDTO(x)).ToList();
            return Store_StoreStatusDTOs;
        }
        [Route(StoreRoute.FilterListStoreType), HttpPost]
        public async Task<List<Store_StoreTypeDTO>> FilterListStoreType([FromBody] Store_StoreTypeFilterDTO Store_StoreTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreTypeFilter StoreTypeFilter = new StoreTypeFilter();
            StoreTypeFilter.Skip = 0;
            StoreTypeFilter.Take = 20;
            StoreTypeFilter.OrderBy = StoreTypeOrder.Id;
            StoreTypeFilter.OrderType = OrderType.ASC;
            StoreTypeFilter.Selects = StoreTypeSelect.ALL;
            StoreTypeFilter.Id = Store_StoreTypeFilterDTO.Id;
            StoreTypeFilter.Code = Store_StoreTypeFilterDTO.Code;
            StoreTypeFilter.Name = Store_StoreTypeFilterDTO.Name;
            StoreTypeFilter.ColorId = Store_StoreTypeFilterDTO.ColorId;
            StoreTypeFilter.StatusId = Store_StoreTypeFilterDTO.StatusId;

            if (StoreTypeFilter.Id == null) StoreTypeFilter.Id = new IdFilter();
            StoreTypeFilter.Id.In = await FilterStoreType(StoreTypeService, CurrentContext);

            List<StoreType> StoreTypes = await StoreTypeService.List(StoreTypeFilter);
            List<Store_StoreTypeDTO> Store_StoreTypeDTOs = StoreTypes
                .Select(x => new Store_StoreTypeDTO(x)).ToList();
            return Store_StoreTypeDTOs;
        }
        [Route(StoreRoute.FilterListWard), HttpPost]
        public async Task<List<Store_WardDTO>> FilterListWard([FromBody] Store_WardFilterDTO Store_WardFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            WardFilter.Id = Store_WardFilterDTO.Id;
            WardFilter.Code = Store_WardFilterDTO.Code;
            WardFilter.Name = Store_WardFilterDTO.Name;
            WardFilter.Priority = Store_WardFilterDTO.Priority;
            WardFilter.DistrictId = Store_WardFilterDTO.DistrictId;
            WardFilter.StatusId = Store_WardFilterDTO.StatusId;

            List<Ward> Wards = await WardService.List(WardFilter);
            List<Store_WardDTO> Store_WardDTOs = Wards
                .Select(x => new Store_WardDTO(x)).ToList();
            return Store_WardDTOs;
        }

        [Route(StoreRoute.SingleListAppUser), HttpPost]
        public async Task<List<Store_AppUserDTO>> SingleListAppUser([FromBody] Store_AppUserFilterDTO Store_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = Store_AppUserFilterDTO.Id;
            AppUserFilter.Username = Store_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Store_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Store_AppUserFilterDTO.Address;
            AppUserFilter.Email = Store_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Store_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Store_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Store_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Store_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Store_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Store_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Store_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Store_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Store_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Store_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Store_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Store_AppUserDTO> Store_AppUserDTOs = AppUsers
                .Select(x => new Store_AppUserDTO(x)).ToList();
            return Store_AppUserDTOs;
        }

        [Route(StoreRoute.SingleListDistrict), HttpPost]
        public async Task<List<Store_DistrictDTO>> SingleListDistrict([FromBody] Store_DistrictFilterDTO Store_DistrictFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = Store_DistrictFilterDTO.Id;
            DistrictFilter.Code = Store_DistrictFilterDTO.Code;
            DistrictFilter.Name = Store_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Store_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Store_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = Store_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Store_DistrictDTO> Store_DistrictDTOs = Districts
                .Select(x => new Store_DistrictDTO(x)).ToList();
            return Store_DistrictDTOs;
        }
        [Route(StoreRoute.SingleListOrganization), HttpPost]
        public async Task<List<Store_OrganizationDTO>> SingleListOrganization([FromBody] Store_OrganizationFilterDTO Store_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Store_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Store_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Store_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Store_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = Store_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = Store_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = Store_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = Store_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = Store_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = Store_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Store_OrganizationDTO> Store_OrganizationDTOs = Organizations
                .Select(x => new Store_OrganizationDTO(x)).ToList();
            return Store_OrganizationDTOs;
        }
        [Route(StoreRoute.SingleListStore), HttpPost]
        public async Task<List<Store_StoreDTO>> SingleListStore([FromBody] Store_StoreFilterDTO Store_StoreFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Skip = 0;
            StoreFilter.Take = 20;
            StoreFilter.OrderBy = StoreOrder.Id;
            StoreFilter.OrderType = OrderType.ASC;
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Id = Store_StoreFilterDTO.Id;
            StoreFilter.Code = Store_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Store_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Store_StoreFilterDTO.Name;
            StoreFilter.UnsignName = Store_StoreFilterDTO.UnsignName;
            StoreFilter.ParentStoreId = Store_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Store_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Store_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Store_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Store_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Store_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Store_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Store_StoreFilterDTO.WardId;
            StoreFilter.Address = Store_StoreFilterDTO.Address;
            StoreFilter.UnsignAddress = Store_StoreFilterDTO.UnsignAddress;
            StoreFilter.DeliveryAddress = Store_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Store_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Store_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Store_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Store_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Store_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Store_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Store_StoreFilterDTO.OwnerEmail;
            StoreFilter.TaxCode = Store_StoreFilterDTO.TaxCode;
            StoreFilter.LegalEntity = Store_StoreFilterDTO.LegalEntity;
            StoreFilter.AppUserId = Store_StoreFilterDTO.AppUserId;
            StoreFilter.StatusId = Store_StoreFilterDTO.StatusId;
            StoreFilter.StoreStatusId = Store_StoreFilterDTO.StoreStatusId;
            StoreFilter.CustomerId = Store_StoreFilterDTO.CustomerId;

            if (StoreFilter.Id == null) StoreFilter.Id = new IdFilter();
            StoreFilter.Id.In = await FilterStore(StoreService, OrganizationService, CurrentContext);

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Store_StoreDTO> Store_StoreDTOs = Stores
                .Select(x => new Store_StoreDTO(x)).ToList();
            return Store_StoreDTOs;
        }
        [Route(StoreRoute.SingleListProvince), HttpPost]
        public async Task<List<Store_ProvinceDTO>> SingleListProvince([FromBody] Store_ProvinceFilterDTO Store_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Id = Store_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Store_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Store_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Store_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = Store_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Store_ProvinceDTO> Store_ProvinceDTOs = Provinces
                .Select(x => new Store_ProvinceDTO(x)).ToList();
            return Store_ProvinceDTOs;
        }
        [Route(StoreRoute.SingleListStatus), HttpPost]
        public async Task<List<Store_StatusDTO>> SingleListStatus([FromBody] Store_StatusFilterDTO Store_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Store_StatusDTO> Store_StatusDTOs = Statuses
                .Select(x => new Store_StatusDTO(x)).ToList();
            return Store_StatusDTOs;
        }
        [Route(StoreRoute.SingleListStoreGrouping), HttpPost]
        public async Task<List<Store_StoreGroupingDTO>> SingleListStoreGrouping([FromBody] Store_StoreGroupingFilterDTO Store_StoreGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreGroupingFilter StoreGroupingFilter = new StoreGroupingFilter();
            StoreGroupingFilter.Skip = 0;
            StoreGroupingFilter.Take = int.MaxValue;
            StoreGroupingFilter.OrderBy = StoreGroupingOrder.Id;
            StoreGroupingFilter.OrderType = OrderType.ASC;
            StoreGroupingFilter.Selects = StoreGroupingSelect.ALL;
            StoreGroupingFilter.Id = Store_StoreGroupingFilterDTO.Id;
            StoreGroupingFilter.Code = Store_StoreGroupingFilterDTO.Code;
            StoreGroupingFilter.Name = Store_StoreGroupingFilterDTO.Name;
            StoreGroupingFilter.ParentId = Store_StoreGroupingFilterDTO.ParentId;
            StoreGroupingFilter.Path = Store_StoreGroupingFilterDTO.Path;
            StoreGroupingFilter.Level = Store_StoreGroupingFilterDTO.Level;
            StoreGroupingFilter.StatusId = Store_StoreGroupingFilterDTO.StatusId;

            if (StoreGroupingFilter.Id == null) StoreGroupingFilter.Id = new IdFilter();
            StoreGroupingFilter.Id.In = await FilterStoreGrouping(StoreGroupingService, CurrentContext);

            List<StoreGrouping> StoreGroupings = await StoreGroupingService.List(StoreGroupingFilter);
            List<Store_StoreGroupingDTO> Store_StoreGroupingDTOs = StoreGroupings
                .Select(x => new Store_StoreGroupingDTO(x)).ToList();
            return Store_StoreGroupingDTOs;
        }
        [Route(StoreRoute.SingleListStoreStatus), HttpPost]
        public async Task<List<Store_StoreStatusDTO>> SingleListStoreStatus([FromBody] Store_StoreStatusFilterDTO Store_StoreStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreStatusFilter StoreStatusFilter = new StoreStatusFilter();
            StoreStatusFilter.Skip = 0;
            StoreStatusFilter.Take = int.MaxValue;
            StoreStatusFilter.Take = 20;
            StoreStatusFilter.OrderBy = StoreStatusOrder.Id;
            StoreStatusFilter.OrderType = OrderType.ASC;
            StoreStatusFilter.Selects = StoreStatusSelect.ALL;

            List<StoreStatus> StoreStatuses = await StoreStatusService.List(StoreStatusFilter);
            List<Store_StoreStatusDTO> Store_StoreStatusDTOs = StoreStatuses
                .Select(x => new Store_StoreStatusDTO(x)).ToList();
            return Store_StoreStatusDTOs;
        }
        [Route(StoreRoute.SingleListStoreType), HttpPost]
        public async Task<List<Store_StoreTypeDTO>> SingleListStoreType([FromBody] Store_StoreTypeFilterDTO Store_StoreTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreTypeFilter StoreTypeFilter = new StoreTypeFilter();
            StoreTypeFilter.Skip = 0;
            StoreTypeFilter.Take = 20;
            StoreTypeFilter.OrderBy = StoreTypeOrder.Id;
            StoreTypeFilter.OrderType = OrderType.ASC;
            StoreTypeFilter.Selects = StoreTypeSelect.ALL;
            StoreTypeFilter.Id = Store_StoreTypeFilterDTO.Id;
            StoreTypeFilter.Code = Store_StoreTypeFilterDTO.Code;
            StoreTypeFilter.Name = Store_StoreTypeFilterDTO.Name;
            StoreTypeFilter.ColorId = Store_StoreTypeFilterDTO.ColorId;
            StoreTypeFilter.StatusId = Store_StoreTypeFilterDTO.StatusId;

            List<StoreType> StoreTypes = await StoreTypeService.List(StoreTypeFilter);
            List<Store_StoreTypeDTO> Store_StoreTypeDTOs = StoreTypes
                .Select(x => new Store_StoreTypeDTO(x)).ToList();
            return Store_StoreTypeDTOs;
        }
        [Route(StoreRoute.SingleListWard), HttpPost]
        public async Task<List<Store_WardDTO>> SingleListWard([FromBody] Store_WardFilterDTO Store_WardFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            WardFilter.Id = Store_WardFilterDTO.Id;
            WardFilter.Code = Store_WardFilterDTO.Code;
            WardFilter.Name = Store_WardFilterDTO.Name;
            WardFilter.Priority = Store_WardFilterDTO.Priority;
            WardFilter.DistrictId = Store_WardFilterDTO.DistrictId;
            WardFilter.StatusId = Store_WardFilterDTO.StatusId;

            List<Ward> Wards = await WardService.List(WardFilter);
            List<Store_WardDTO> Store_WardDTOs = Wards
                .Select(x => new Store_WardDTO(x)).ToList();
            return Store_WardDTOs;
        }


        [Route(StoreRoute.SingleListBusinessType), HttpPost]
        public async Task<List<Store_BusinessTypeDTO>> SingleListBusinessType([FromBody] Store_BusinessTypeFilterDTO Store_BusinessTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            BusinessTypeFilter BusinessTypeFilter = new BusinessTypeFilter();
            BusinessTypeFilter.Skip = 0;
            BusinessTypeFilter.Take = int.MaxValue;
            BusinessTypeFilter.OrderBy = BusinessTypeOrder.Id;
            BusinessTypeFilter.OrderType = OrderType.ASC;
            BusinessTypeFilter.Selects = BusinessTypeSelect.ALL;
            BusinessTypeFilter.Id = Store_BusinessTypeFilterDTO.Id;
            BusinessTypeFilter.Code = Store_BusinessTypeFilterDTO.Code;
            BusinessTypeFilter.Name = Store_BusinessTypeFilterDTO.Name;

            List<BusinessType> BusinessTypes = await BusinessTypeService.List(BusinessTypeFilter);
            List<Store_BusinessTypeDTO> Store_BusinessTypeDTOs = BusinessTypes
                .Select(x => new Store_BusinessTypeDTO(x)).ToList();
            return Store_BusinessTypeDTOs;
        }
        [Route(StoreRoute.SingleListPosition), HttpPost]
        public async Task<List<Store_PositionDTO>> SingleListPosition([FromBody] Store_PositionFilterDTO Store_PositionFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Skip = 0;
            PositionFilter.Take = int.MaxValue;
            PositionFilter.OrderBy = PositionOrder.Id;
            PositionFilter.OrderType = OrderType.ASC;
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Id = Store_PositionFilterDTO.Id;
            PositionFilter.Code = Store_PositionFilterDTO.Code;
            PositionFilter.Name = Store_PositionFilterDTO.Name;

            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Store_PositionDTO> Store_PositionDTOs = Positions
                .Select(x => new Store_PositionDTO(x)).ToList();
            return Store_PositionDTOs;
        }
        [Route(StoreRoute.SingleListStoreDeliveryTime), HttpPost]
        public async Task<List<Store_StoreDeliveryTimeDTO>> SingleListStoreDeliveryTime([FromBody] Store_StoreDeliveryTimeFilterDTO Store_StoreDeliveryTimeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreDeliveryTimeFilter StoreDeliveryTimeFilter = new StoreDeliveryTimeFilter();
            StoreDeliveryTimeFilter.Skip = 0;
            StoreDeliveryTimeFilter.Take = int.MaxValue;
            StoreDeliveryTimeFilter.OrderBy = StoreDeliveryTimeOrder.Id;
            StoreDeliveryTimeFilter.OrderType = OrderType.ASC;
            StoreDeliveryTimeFilter.Selects = StoreDeliveryTimeSelect.ALL;
            StoreDeliveryTimeFilter.Id = Store_StoreDeliveryTimeFilterDTO.Id;
            StoreDeliveryTimeFilter.Code = Store_StoreDeliveryTimeFilterDTO.Code;
            StoreDeliveryTimeFilter.Name = Store_StoreDeliveryTimeFilterDTO.Name;

            List<StoreDeliveryTime> StoreDeliveryTimes = await StoreDeliveryTimeService.List(StoreDeliveryTimeFilter);
            List<Store_StoreDeliveryTimeDTO> Store_StoreDeliveryTimeDTOs = StoreDeliveryTimes
                .Select(x => new Store_StoreDeliveryTimeDTO(x)).ToList();
            return Store_StoreDeliveryTimeDTOs;
        }
        [Route(StoreRoute.SingleListRelationshipCustomerType), HttpPost]
        public async Task<List<Store_RelationshipCustomerTypeDTO>> SingleListRelationshipCustomerType([FromBody] Store_RelationshipCustomerTypeFilterDTO Store_RelationshipCustomerTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter = new RelationshipCustomerTypeFilter();
            RelationshipCustomerTypeFilter.Skip = 0;
            RelationshipCustomerTypeFilter.Take = int.MaxValue;
            RelationshipCustomerTypeFilter.OrderBy = RelationshipCustomerTypeOrder.Id;
            RelationshipCustomerTypeFilter.OrderType = OrderType.ASC;
            RelationshipCustomerTypeFilter.Selects = RelationshipCustomerTypeSelect.ALL;
            RelationshipCustomerTypeFilter.Id = Store_RelationshipCustomerTypeFilterDTO.Id;
            RelationshipCustomerTypeFilter.Name = Store_RelationshipCustomerTypeFilterDTO.Name;

            List<RelationshipCustomerType> RelationshipCustomerTypes = await RelationshipCustomerTypeService.List(RelationshipCustomerTypeFilter);
            List<Store_RelationshipCustomerTypeDTO> Store_RelationshipCustomerTypeDTOs = RelationshipCustomerTypes
                .Select(x => new Store_RelationshipCustomerTypeDTO(x)).ToList();
            return Store_RelationshipCustomerTypeDTOs;
        }
        [Route(StoreRoute.SingleListInfulenceLevelMarket), HttpPost]
        public async Task<List<Store_InfulenceLevelMarketDTO>> SingleListInfulenceLevelMarket([FromBody] Store_InfulenceLevelMarketFilterDTO Store_InfulenceLevelMarketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InfulenceLevelMarketFilter InfulenceLevelMarketFilter = new InfulenceLevelMarketFilter();
            InfulenceLevelMarketFilter.Skip = 0;
            InfulenceLevelMarketFilter.Take = int.MaxValue;
            InfulenceLevelMarketFilter.OrderBy = InfulenceLevelMarketOrder.Id;
            InfulenceLevelMarketFilter.OrderType = OrderType.ASC;
            InfulenceLevelMarketFilter.Selects = InfulenceLevelMarketSelect.ALL;
            InfulenceLevelMarketFilter.Id = Store_InfulenceLevelMarketFilterDTO.Id;
            InfulenceLevelMarketFilter.Name = Store_InfulenceLevelMarketFilterDTO.Name;

            List<InfulenceLevelMarket> InfulenceLevelMarkets = await InfulenceLevelMarketService.List(InfulenceLevelMarketFilter);
            List<Store_InfulenceLevelMarketDTO> Store_InfulenceLevelMarketDTOs = InfulenceLevelMarkets
                .Select(x => new Store_InfulenceLevelMarketDTO(x)).ToList();
            return Store_InfulenceLevelMarketDTOs;
        }
        [Route(StoreRoute.SingleListMarketPrice), HttpPost]
        public async Task<List<Store_MarketPriceDTO>> SingleListMarketPrice([FromBody] Store_MarketPriceFilterDTO Store_MarketPriceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MarketPriceFilter MarketPriceFilter = new MarketPriceFilter();
            MarketPriceFilter.Skip = 0;
            MarketPriceFilter.Take = int.MaxValue;
            MarketPriceFilter.OrderBy = MarketPriceOrder.Id;
            MarketPriceFilter.OrderType = OrderType.ASC;
            MarketPriceFilter.Selects = MarketPriceSelect.ALL;
            MarketPriceFilter.Id = Store_MarketPriceFilterDTO.Id;
            MarketPriceFilter.Name = Store_MarketPriceFilterDTO.Name;

            List<MarketPrice> MarketPrices = await MarketPriceService.List(MarketPriceFilter);
            List<Store_MarketPriceDTO> Store_MarketPriceDTOs = MarketPrices
                .Select(x => new Store_MarketPriceDTO(x)).ToList();
            return Store_MarketPriceDTOs;
        }
        [Route(StoreRoute.SingleListConsultingService), HttpPost]
        public async Task<List<Store_ConsultingServiceDTO>> SingleListConsultingService([FromBody] Store_ConsultingServiceFilterDTO Store_ConsultingServiceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ConsultingServiceFilter ConsultingServiceFilter = new ConsultingServiceFilter();
            ConsultingServiceFilter.Skip = 0;
            ConsultingServiceFilter.Take = int.MaxValue;
            ConsultingServiceFilter.OrderBy = ConsultingServiceOrder.Id;
            ConsultingServiceFilter.OrderType = OrderType.ASC;
            ConsultingServiceFilter.Selects = ConsultingServiceSelect.ALL;
            ConsultingServiceFilter.Id = Store_ConsultingServiceFilterDTO.Id;
            ConsultingServiceFilter.Name = Store_ConsultingServiceFilterDTO.Name;

            List<ConsultingService> ConsultingServices = await ConsultingServiceService.List(ConsultingServiceFilter);
            List<Store_ConsultingServiceDTO> Store_ConsultingServiceDTOs = ConsultingServices
                .Select(x => new Store_ConsultingServiceDTO(x)).ToList();
            return Store_ConsultingServiceDTOs;
        }
        [Route(StoreRoute.SingleListCooperativeAttitude), HttpPost]
        public async Task<List<Store_CooperativeAttitudeDTO>> SingleListCooperativeAttitude([FromBody] Store_CooperativeAttitudeFilterDTO Store_CooperativeAttitudeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CooperativeAttitudeFilter CooperativeAttitudeFilter = new CooperativeAttitudeFilter();
            CooperativeAttitudeFilter.Skip = 0;
            CooperativeAttitudeFilter.Take = int.MaxValue;
            CooperativeAttitudeFilter.OrderBy = CooperativeAttitudeOrder.Id;
            CooperativeAttitudeFilter.OrderType = OrderType.ASC;
            CooperativeAttitudeFilter.Selects = CooperativeAttitudeSelect.ALL;
            CooperativeAttitudeFilter.Id = Store_CooperativeAttitudeFilterDTO.Id;
            CooperativeAttitudeFilter.Name = Store_CooperativeAttitudeFilterDTO.Name;

            List<CooperativeAttitude> CooperativeAttitudes = await CooperativeAttitudeService.List(CooperativeAttitudeFilter);
            List<Store_CooperativeAttitudeDTO> Store_CooperativeAttitudeDTOs = CooperativeAttitudes
                .Select(x => new Store_CooperativeAttitudeDTO(x)).ToList();
            return Store_CooperativeAttitudeDTOs;
        }
        [Route(StoreRoute.SingleListCurrency), HttpPost]
        public async Task<List<Store_CurrencyDTO>> SingleListCurrency([FromBody] Store_CurrencyFilterDTO Store_CurrencyFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CurrencyFilter CurrencyFilter = new CurrencyFilter();
            CurrencyFilter.Skip = 0;
            CurrencyFilter.Take = 20;
            CurrencyFilter.OrderBy = CurrencyOrder.Id;
            CurrencyFilter.OrderType = OrderType.ASC;
            CurrencyFilter.Selects = CurrencySelect.ALL;
            CurrencyFilter.Id = Store_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Store_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Store_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Store_CurrencyDTO> Store_CurrencyDTOs = Currencies
                .Select(x => new Store_CurrencyDTO(x)).ToList();
            return Store_CurrencyDTOs;
        }


    }
}

