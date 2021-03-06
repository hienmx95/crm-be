using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace CRM.Rpc.app_user
{
    [DisplayName("Quản lý người dùng")]
    public class AppUserRoute : Root
    {
        public const string Parent = Module + "/account";
        public const string Master = Module + "/account/app-user/app-user-master";
        public const string Detail = Module + "/account/app-user/app-user-detail";
        private const string Default = Rpc + Module + "/app-user";
        public const string Mobile = Default + "/master-data.profile";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Update = Default + "/update";
        public const string Export = Default + "/export";

        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListPosition = Default + "/filter-list-position";
        public const string FilterListStatus = Default + "/filter-list-status";

        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListPosition = Default + "/single-list-position";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListStatus = Default + "/single-list-status";
        public const string SingleListRole = Default + "/single-list-role";
        public const string CountRole = Default + "/count-role";
        public const string ListRole = Default + "/list-role";
        public const string UpdateRole = Default + "/update-role";
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(AppUserFilter.OrganizationId), FieldTypeEnum.ID.Id},
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master,Detail,Count,List,Get,
                FilterListPosition, FilterListOrganization, FilterListStatus,
                SingleListOrganization, SingleListPosition, SingleListSex, SingleListStatus, SingleListRole, CountRole, ListRole}},
            { "Sửa", new List<string> {
                Master, Count, List,Get,
                FilterListPosition,
                Detail,Get,Update, 
                SingleListOrganization, SingleListPosition, SingleListSex, SingleListStatus, SingleListRole,
                CountRole, ListRole}},
            { "Thiết lập vai trò", new List<string> {
                Parent,
                Master, Count, List, Get,
                FilterListPosition,
                Detail, Get, UpdateRole
            }},
        };
    }
}
