using CRM.Common;
using CRM.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.direct_sales_order
{
    public partial class DirectSalesOrderController : RpcController
    {

        [Route(DirectSalesOrderRoute.FilterListStore), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_StoreDTO>>> FilterListStore([FromBody] DirectSalesOrder_StoreFilterDTO DirectSalesOrder_StoreFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Skip = 0;
            StoreFilter.Take = 20;
            StoreFilter.OrderBy = StoreOrder.Id;
            StoreFilter.OrderType = OrderType.ASC;
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Id = DirectSalesOrder_StoreFilterDTO.Id;
            StoreFilter.Code = DirectSalesOrder_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = DirectSalesOrder_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = DirectSalesOrder_StoreFilterDTO.Name;
            StoreFilter.ParentStoreId = DirectSalesOrder_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = DirectSalesOrder_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = DirectSalesOrder_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = DirectSalesOrder_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = DirectSalesOrder_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = DirectSalesOrder_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = DirectSalesOrder_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = DirectSalesOrder_StoreFilterDTO.WardId;
            StoreFilter.Address = DirectSalesOrder_StoreFilterDTO.Address;
            StoreFilter.DeliveryAddress = DirectSalesOrder_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = DirectSalesOrder_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = DirectSalesOrder_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = DirectSalesOrder_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = DirectSalesOrder_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = DirectSalesOrder_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = DirectSalesOrder_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = DirectSalesOrder_StoreFilterDTO.OwnerEmail;
            StoreFilter.StatusId = DirectSalesOrder_StoreFilterDTO.StatusId;

            if (StoreFilter.Id == null) StoreFilter.Id = new IdFilter();
            StoreFilter.Id.In = await FilterStore(StoreService, OrganizationService, CurrentContext);

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<DirectSalesOrder_StoreDTO> DirectSalesOrder_StoreDTOs = Stores
                .Select(x => new DirectSalesOrder_StoreDTO(x)).ToList();
            return DirectSalesOrder_StoreDTOs;
        }

        [Route(DirectSalesOrderRoute.FilterListStoreStatus), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_StoreStatusDTO>>> FilterListStoreStatus([FromBody] DirectSalesOrder_StoreStatusFilterDTO DirectSalesOrder_StoreStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreStatusFilter StoreStatusFilter = new StoreStatusFilter();
            StoreStatusFilter.Skip = 0;
            StoreStatusFilter.Take = 20;
            StoreStatusFilter.OrderBy = StoreStatusOrder.Id;
            StoreStatusFilter.OrderType = OrderType.ASC;
            StoreStatusFilter.Selects = StoreStatusSelect.ALL;
            StoreStatusFilter.Id = DirectSalesOrder_StoreStatusFilterDTO.Id;
            StoreStatusFilter.Code = DirectSalesOrder_StoreStatusFilterDTO.Code;
            StoreStatusFilter.Name = DirectSalesOrder_StoreStatusFilterDTO.Name;

            List<StoreStatus> StoreStatuses = await StoreStatusService.List(StoreStatusFilter);
            List<DirectSalesOrder_StoreStatusDTO> DirectSalesOrder_StoreStatusDTOs = StoreStatuses
                .Select(x => new DirectSalesOrder_StoreStatusDTO(x)).ToList();
            return DirectSalesOrder_StoreStatusDTOs;
        }

        [Route(DirectSalesOrderRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_AppUserDTO>>> FilterListAppUser([FromBody] DirectSalesOrder_AppUserFilterDTO DirectSalesOrder_AppUserFilterDTO)
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
            AppUserFilter.Id = DirectSalesOrder_AppUserFilterDTO.Id;
            AppUserFilter.Username = DirectSalesOrder_AppUserFilterDTO.Username;
            AppUserFilter.Password = DirectSalesOrder_AppUserFilterDTO.Password;
            AppUserFilter.DisplayName = DirectSalesOrder_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = DirectSalesOrder_AppUserFilterDTO.Address;
            AppUserFilter.Email = DirectSalesOrder_AppUserFilterDTO.Email;
            AppUserFilter.Phone = DirectSalesOrder_AppUserFilterDTO.Phone;
            AppUserFilter.PositionId = DirectSalesOrder_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = DirectSalesOrder_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = DirectSalesOrder_AppUserFilterDTO.OrganizationId;
            AppUserFilter.SexId = DirectSalesOrder_AppUserFilterDTO.SexId;
            AppUserFilter.StatusId = DirectSalesOrder_AppUserFilterDTO.StatusId;
            AppUserFilter.Birthday = DirectSalesOrder_AppUserFilterDTO.Birthday;
            AppUserFilter.ProvinceId = DirectSalesOrder_AppUserFilterDTO.ProvinceId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<DirectSalesOrder_AppUserDTO> DirectSalesOrder_AppUserDTOs = AppUsers
                .Select(x => new DirectSalesOrder_AppUserDTO(x)).ToList();
            return DirectSalesOrder_AppUserDTOs;
        }

        [Route(DirectSalesOrderRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_OrganizationDTO>>> FilterListOrganization([FromBody] DirectSalesOrder_OrganizationFilterDTO DirectSalesOrder_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 99999;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = DirectSalesOrder_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = DirectSalesOrder_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = DirectSalesOrder_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = DirectSalesOrder_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = DirectSalesOrder_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = DirectSalesOrder_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = null;
            OrganizationFilter.Phone = DirectSalesOrder_OrganizationFilterDTO.Phone;
            OrganizationFilter.Address = DirectSalesOrder_OrganizationFilterDTO.Address;
            OrganizationFilter.Email = DirectSalesOrder_OrganizationFilterDTO.Email;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<DirectSalesOrder_OrganizationDTO> DirectSalesOrder_OrganizationDTOs = Organizations
                .Select(x => new DirectSalesOrder_OrganizationDTO(x)).ToList();
            return DirectSalesOrder_OrganizationDTOs;
        }

        [Route(DirectSalesOrderRoute.FilterListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_UnitOfMeasureDTO>>> FilterListUnitOfMeasure([FromBody] DirectSalesOrder_UnitOfMeasureFilterDTO DirectSalesOrder_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            UnitOfMeasureFilter UnitOfMeasureFilter = new UnitOfMeasureFilter();
            UnitOfMeasureFilter.Skip = 0;
            UnitOfMeasureFilter.Take = 20;
            UnitOfMeasureFilter.OrderBy = UnitOfMeasureOrder.Id;
            UnitOfMeasureFilter.OrderType = OrderType.ASC;
            UnitOfMeasureFilter.Selects = UnitOfMeasureSelect.ALL;
            UnitOfMeasureFilter.Id = DirectSalesOrder_UnitOfMeasureFilterDTO.Id;
            UnitOfMeasureFilter.Code = DirectSalesOrder_UnitOfMeasureFilterDTO.Code;
            UnitOfMeasureFilter.Name = DirectSalesOrder_UnitOfMeasureFilterDTO.Name;
            UnitOfMeasureFilter.Description = DirectSalesOrder_UnitOfMeasureFilterDTO.Description;
            UnitOfMeasureFilter.StatusId = DirectSalesOrder_UnitOfMeasureFilterDTO.StatusId;

            List<UnitOfMeasure> UnitOfMeasures = await UnitOfMeasureService.List(UnitOfMeasureFilter);
            List<DirectSalesOrder_UnitOfMeasureDTO> DirectSalesOrder_UnitOfMeasureDTOs = UnitOfMeasures
                .Select(x => new DirectSalesOrder_UnitOfMeasureDTO(x)).ToList();
            return DirectSalesOrder_UnitOfMeasureDTOs;
        }
        [Route(DirectSalesOrderRoute.FilterListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_EditedPriceStatusDTO>>> FilterListEditedPriceStatus([FromBody] DirectSalesOrder_EditedPriceStatusFilterDTO DirectSalesOrder_EditedPriceStatusFilterDTO)
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
            EditedPriceStatusFilter.Id = DirectSalesOrder_EditedPriceStatusFilterDTO.Id;
            EditedPriceStatusFilter.Code = DirectSalesOrder_EditedPriceStatusFilterDTO.Code;
            EditedPriceStatusFilter.Name = DirectSalesOrder_EditedPriceStatusFilterDTO.Name;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<DirectSalesOrder_EditedPriceStatusDTO> DirectSalesOrder_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new DirectSalesOrder_EditedPriceStatusDTO(x)).ToList();
            return DirectSalesOrder_EditedPriceStatusDTOs;
        }
        [Route(DirectSalesOrderRoute.FilterListRequestState), HttpPost]
        public async Task<ActionResult<List<DirectSalesOrder_RequestStateDTO>>> FilterListRequestState([FromBody] DirectSalesOrder_RequestStateFilterDTO DirectSalesOrder_RequestStateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RequestStateFilter RequestStateFilter = new RequestStateFilter();
            RequestStateFilter.Skip = 0;
            RequestStateFilter.Take = 20;
            RequestStateFilter.OrderBy = RequestStateOrder.Id;
            RequestStateFilter.OrderType = OrderType.ASC;
            RequestStateFilter.Selects = RequestStateSelect.ALL;
            RequestStateFilter.Id = DirectSalesOrder_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = DirectSalesOrder_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = DirectSalesOrder_RequestStateFilterDTO.Name;

            List<RequestState> RequestStatees = await RequestStateService.List(RequestStateFilter);
            List<DirectSalesOrder_RequestStateDTO> DirectSalesOrder_RequestStateDTOs = RequestStatees
                .Select(x => new DirectSalesOrder_RequestStateDTO(x)).ToList();
            return DirectSalesOrder_RequestStateDTOs;
        }
    }
}

