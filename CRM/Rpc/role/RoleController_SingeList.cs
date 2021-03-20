using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.role
{
    public partial class RoleController : RpcController
    {

        [Route(RoleRoute.SingleListCurrentUser), HttpPost]
        public async Task<List<GenericEnum>> SingleListCurrentUser()
        {
            return CurrentUserEnum.CurrentUserEnumList;
        }
        [Route(RoleRoute.SingleListAppUser), HttpPost]
        public async Task<List<Role_AppUserDTO>> SingleListAppUser([FromBody] Role_AppUserFilterDTO Role_AppUserFilterDTO)
        {
            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.Id | AppUserSelect.Username | AppUserSelect.DisplayName | AppUserSelect.Email | AppUserSelect.Phone;
            AppUserFilter.Id = Role_AppUserFilterDTO.Id;
            AppUserFilter.Username = Role_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Role_AppUserFilterDTO.DisplayName;
            AppUserFilter.Email = Role_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Role_AppUserFilterDTO.Phone;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Role_AppUserDTO> Role_AppUserDTOs = AppUsers
                .Select(x => new Role_AppUserDTO(x)).ToList();
            return Role_AppUserDTOs;
        }
        [Route(RoleRoute.SingleListMenu), HttpPost]
        public async Task<List<Role_MenuDTO>> SingleListMenu([FromBody] Role_MenuFilterDTO Role_MenuFilterDTO)
        {
            MenuFilter MenuFilter = new MenuFilter();
            MenuFilter.Skip = 0;
            MenuFilter.Take = 20;
            MenuFilter.OrderBy = MenuOrder.Id;
            MenuFilter.OrderType = OrderType.ASC;
            MenuFilter.Selects = MenuSelect.ALL;
            MenuFilter.Id = Role_MenuFilterDTO.Id;
            MenuFilter.Code = Role_MenuFilterDTO.Code;
            MenuFilter.Name = Role_MenuFilterDTO.Name;
            MenuFilter.Path = Role_MenuFilterDTO.Path;

            List<Menu> Menus = await MenuService.List(MenuFilter);
            List<Role_MenuDTO> Role_MenuDTOs = Menus
                .Select(x => new Role_MenuDTO(x)).ToList();
            return Role_MenuDTOs;
        }

        [Route(RoleRoute.SingleListStatus), HttpPost]
        public async Task<List<Role_StatusDTO>> SingleListStatus([FromBody] Role_StatusFilterDTO Role_StatusFilterDTO)
        {
            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = Role_StatusFilterDTO.Id;
            StatusFilter.Code = Role_StatusFilterDTO.Code;
            StatusFilter.Name = Role_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Role_StatusDTO> Role_StatusDTOs = Statuses
                .Select(x => new Role_StatusDTO(x)).ToList();
            return Role_StatusDTOs;
        }

        [Route(RoleRoute.SingleListOrganization), HttpPost]
        public async Task<List<Role_OrganizationDTO>> SingleListOrganization([FromBody] Role_OrganizationFilterDTO Role_OrganizationFilterDTO)
        {
            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.Id | OrganizationSelect.Code | OrganizationSelect.Name | OrganizationSelect.Parent;
            OrganizationFilter.Id = Role_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Role_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Role_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Role_OrganizationFilterDTO.ParentId;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizationes = await OrganizationService.List(OrganizationFilter);
            List<Role_OrganizationDTO> Role_OrganizationDTOs = Organizationes
                .Select(x => new Role_OrganizationDTO(x)).ToList();
            return Role_OrganizationDTOs;
        }

        [Route(RoleRoute.SingleListField), HttpPost]
        public async Task<List<Role_FieldDTO>> SingleListField([FromBody] Role_FieldFilterDTO Role_FieldFilterDTO)
        {
            FieldFilter FieldFilter = new FieldFilter();
            FieldFilter.Skip = 0;
            FieldFilter.Take = 200;
            FieldFilter.OrderBy = FieldOrder.Id;
            FieldFilter.OrderType = OrderType.ASC;
            FieldFilter.Selects = FieldSelect.ALL;
            FieldFilter.Id = Role_FieldFilterDTO.Id;
            FieldFilter.MenuId = Role_FieldFilterDTO.MenuId;
            FieldFilter.Name = Role_FieldFilterDTO.Name;

            List<Field> Fieldes = await FieldService.List(FieldFilter);
            List<Role_FieldDTO> Role_FieldDTOs = Fieldes
                .Select(x => new Role_FieldDTO(x)).ToList();
            return Role_FieldDTOs;
        }

        [Route(RoleRoute.SingleListPermissionOperator), HttpPost]
        public async Task<List<Role_PermissionOperatorDTO>> SingleListPermissionOperator([FromBody] Role_PermissionOperatorFilterDTO Role_PermissionOperatorFilterDTO)
        {
            PermissionOperatorFilter PermissionOperatorFilter = new PermissionOperatorFilter();
            PermissionOperatorFilter.Skip = 0;
            PermissionOperatorFilter.Take = 200;
            PermissionOperatorFilter.Id = Role_PermissionOperatorFilterDTO.Id;
            PermissionOperatorFilter.Code = Role_PermissionOperatorFilterDTO.Code;
            PermissionOperatorFilter.Name = Role_PermissionOperatorFilterDTO.Name;
            PermissionOperatorFilter.FieldTypeId = Role_PermissionOperatorFilterDTO.FieldTypeId;

            List<PermissionOperator> PermissionOperatores = await PermissionOperatorService.List(PermissionOperatorFilter);
            List<Role_PermissionOperatorDTO> Role_PermissionOperatorDTOs = PermissionOperatores
                .Select(x => new Role_PermissionOperatorDTO(x)).ToList();
            return Role_PermissionOperatorDTOs;
        }

        [Route(RoleRoute.SingleListRequestState), HttpPost]
        public async Task<List<Role_RequestStateDTO>> SingleListRequestState()
        {
            RequestStateFilter RequestStateFilter = new RequestStateFilter();
            RequestStateFilter.Skip = 0;
            RequestStateFilter.Take = 20;
            RequestStateFilter.OrderBy = RequestStateOrder.Id;
            RequestStateFilter.OrderType = OrderType.ASC;
            RequestStateFilter.Selects = RequestStateSelect.ALL;

            List<RequestState> RequestStatees = await RequestStateService.List(RequestStateFilter);
            List<Role_RequestStateDTO> Role_RequestStateDTOs = RequestStatees
                .Select(x => new Role_RequestStateDTO(x)).ToList();
            return Role_RequestStateDTOs;
        }
        [Route(RoleRoute.SingleListCustomerGrouping), HttpPost]
        public async Task<List<Role_CustomerGroupingDTO>> SingleListCustomerGrouping([FromBody] Role_CustomerGroupingFilterDTO Role_CustomerGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = int.MaxValue;
            CustomerGroupingFilter.OrderBy = CustomerGroupingOrder.Id;
            CustomerGroupingFilter.OrderType = OrderType.ASC;
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            CustomerGroupingFilter.Id = Role_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Name = Role_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.Code = Role_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Path = Role_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.CustomerTypeId = Role_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.Level = Role_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = Role_CustomerGroupingFilterDTO.StatusId;
            CustomerGroupingFilter.Description = Role_CustomerGroupingFilterDTO.Description;
            CustomerGroupingFilter.ParentId = Role_CustomerGroupingFilterDTO.ParentId;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<Role_CustomerGroupingDTO> Role_CustomerGroupingDTOs = CustomerGroupings
                .Select(x => new Role_CustomerGroupingDTO(x)).ToList();
            return Role_CustomerGroupingDTOs;
        }

        [Route(RoleRoute.SingleListKnowledgeGroup), HttpPost]
        public async Task<List<Role_KnowledgeGroupDTO>> SingleListKnowledgeGroup([FromBody] Role_KnowledgeGroupFilterDTO Role_KnowledgeGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter.Skip = 0;
            KnowledgeGroupFilter.Take = 20;
            KnowledgeGroupFilter.OrderBy = KnowledgeGroupOrder.Id;
            KnowledgeGroupFilter.OrderType = OrderType.ASC;
            KnowledgeGroupFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
            KnowledgeGroupFilter.Id = Role_KnowledgeGroupFilterDTO.Id;
            KnowledgeGroupFilter.Name = Role_KnowledgeGroupFilterDTO.Name;
            KnowledgeGroupFilter.Code = Role_KnowledgeGroupFilterDTO.Code;
            KnowledgeGroupFilter.DisplayOrder = Role_KnowledgeGroupFilterDTO.DisplayOrder;
            KnowledgeGroupFilter.Description = Role_KnowledgeGroupFilterDTO.Description;

            List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);
            List<Role_KnowledgeGroupDTO> Role_KnowledgeGroupDTOs = KnowledgeGroups
                .Select(x => new Role_KnowledgeGroupDTO(x)).ToList();
            return Role_KnowledgeGroupDTOs;
        }
        [Route(RoleRoute.SingleListAppliedDepartment), HttpPost]
        public async Task<List<GenericEnum>> SingleListAppliedDepartment()
        {
            return CurrentDepartmentEnum.CurrentDepartmentEnumList;
        }
    }
}

