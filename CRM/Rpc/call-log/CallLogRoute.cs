using System.Collections.Generic;
using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLogRoute : Root
    {
        public const string Parent = Module + "/customer-care";
        public const string Master = Module + "/customer-care/call-log/call-log-master";
        public const string Detail = Module + "/customer-care/call-log/call-log-detail";
        private const string Default = Rpc + Module + "/call-log";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string GetPreview = Default + "/get-preview";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-tempate";
        public const string BulkDelete = Default + "/bulk-delete";

        public const string FilterListCallType = Default + "/filter-list-call-type";
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListCallEmotion = Default + "/filter-list-call-emotion";
        public const string FilterListCallCategory = Default + "/filter-list-call-category";
        public const string FilterListCallStatus = Default + "/filter-list-call-status";

        public const string SingleListCallType = Default + "/single-list-call-type";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListCallEmotion = Default + "/single-list-call-emotion";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListEntityReference = Default + "/single-list-call-log-reference";
        public const string SingleListCallCategory = Default + "/single-list-call-category";
        public const string SingleListCallStatus = Default + "/single-list-call-status";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CallLogFilter.AppUserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus } },
            { "Thêm", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                Detail, Create,
                SingleListCallType, SingleListCustomer, SingleListCallEmotion, SingleListAppUser,SingleListEntityReference, 
                SingleListCallCategory, SingleListCallStatus } },

            { "Sửa", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                Detail, Update,
                SingleListCallType, SingleListCustomer, SingleListCallEmotion, SingleListAppUser,SingleListEntityReference,
                SingleListCallCategory, SingleListCallStatus  } },

            { "Xoá", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                Delete,
                SingleListCallType, SingleListCustomer, SingleListCallEmotion, SingleListAppUser,SingleListEntityReference,
                SingleListCallCategory, SingleListCallStatus  } },

            { "Xoá nhiều", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                Export } },

            { "Nhập excel", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCallType, FilterListCustomer, FilterListCallEmotion, FilterListCallCategory, FilterListCallStatus,
                ExportTemplate, Import } },
        };
    }
}
