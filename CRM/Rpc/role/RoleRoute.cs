using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.role
{
    public class RoleRoute : Root
    {
        public const string Parent = Module + "/account";
        public const string Master = Module + "/account/role/role-master";
        public const string Detail = Module + "/account/role/role-detail/*";
        private const string Default = Rpc + Module + "/role";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Clone = Default + "/clone";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string AssignAppUser = Default + "/assign-app-user";
        public const string GetMenu = Default + "/get-menu";

        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCurrentUser = Default + "/single-list-current-user";

        public const string SingleListStatus = Default + "/single-list-status";
        public const string SingleListMenu = Default + "/single-list-menu";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListField = Default + "/single-list-field";
        public const string SingleListPermissionOperator = Default + "/single-list-permission-operator";
        public const string SingleListRequestState = Default + "/single-list-request-state";
        public const string SingleListCustomerGrouping = Default + "/single-list-customer-grouping";
        public const string SingleListKnowledgeGroup = Default + "/single-list-knowledge-group";
        public const string SingleListAppliedDepartment = Default + "/single-list-applied-department";


        public const string CountAppUser = Default + "/count-app-user";
        public const string ListAppUser = Default + "/list-app-user";

        public const string CountPermission = Default + "/count-permission";
        public const string ListPermission = Default + "/list-permission";
        public const string GetPermission = Default + "/get-permission";
        public const string CreatePermission = Default + "/create-permission";
        public const string UpdatePermission = Default + "/update-permission";
        public const string DeletePermission = Default + "/delete-permission";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(RoleFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(RoleFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(RoleFilter.StatusId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Count, List, Get, Clone, Parent,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator, SingleListCurrentUser,SingleListCustomerGrouping,SingleListKnowledgeGroup } },
            { "Thêm", new List<string> {
                Master, Count, List, Get, Clone, CountPermission, ListPermission, GetPermission, CreatePermission, UpdatePermission, DeletePermission,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator, SingleListRequestState,SingleListAppliedDepartment,
                Detail, Create, GetMenu,
                SingleListStatus ,SingleListCurrentUser,SingleListCustomerGrouping,SingleListKnowledgeGroup} },
            { "Sửa", new List<string> {
                Master, Count, List, Get, Clone, CountPermission, ListPermission, GetPermission, CreatePermission, UpdatePermission, DeletePermission,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator,
                SingleListRequestState, SingleListCurrentUser,SingleListAppliedDepartment,
                Detail, Update, GetMenu, SingleListCustomerGrouping,
                 } },
             { "Gán người dùng", new List<string> {
                Master, Count, List, Get, Clone,
                CountAppUser, ListAppUser,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator, SingleListCurrentUser,
                Detail, AssignAppUser,SingleListCustomerGrouping,SingleListKnowledgeGroup,SingleListAppliedDepartment
                } },
             { "Tạo nhanh quyền", new List<string> {
                Master, Count, List, Get, Clone,
                Detail, CreatePermission, GetMenu, Master, Count, List, Get, CountPermission, ListPermission, GetPermission, CreatePermission, UpdatePermission, DeletePermission,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator, SingleListCurrentUser,SingleListCustomerGrouping,SingleListKnowledgeGroup,SingleListAppliedDepartment} },
            { "Xoá", new List<string> {
                Master, Count, List, Get, Clone,
                SingleListAppUser, SingleListStatus, SingleListMenu, SingleListOrganization,
                SingleListField, SingleListPermissionOperator,SingleListCurrentUser,SingleListCustomerGrouping,SingleListKnowledgeGroup,
                SingleListAppliedDepartment,
                Detail, Delete,
                 } },
        };
    }
}
