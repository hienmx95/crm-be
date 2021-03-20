using System.Collections.Generic;
using CRM.Common;
using CRM.Entities;
using System.ComponentModel;

namespace CRM.Rpc.ticket
{
    [DisplayName("Quản lý Ticket")]
    public class TicketRoute : Root
    {
        public const string Parent = Module + "/customer-care";
        public const string Master = Module + "/customer-care/ticket/ticket-master";
        public const string Detail = Module + "/customer-care/ticket/ticket-detail";
        private const string Default = Rpc + Module + "/ticket";
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


        public const string SendSms = Default + "/send-sms";
        public const string SendEmail = Default + "/send-email";
        public const string Pause = Default + "/pause";
        public const string ChangeStatus = Default + "/change-status";
        public const string ReOpen = Default + "/re-open";
        public const string Close = Default + "/close";
        public const string Relation = Default + "/relation";
        public const string ViewCustomer = Default + "/view-customer";

        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListProduct = Default + "/filter-list-product";
        public const string FilterListCallLog = Default + "/filter-list-call-log";
        public const string FilterListTicket = Default + "/filter-list-ticket";
        public const string FilterListStatus = Default + "/filter-list-status";
        public const string FilterListTicketIssueLevel = Default + "/filter-list-ticket-issue-level";
        public const string FilterListTicketPriority = Default + "/filter-list-ticket-priority";
        public const string FilterListTicketSource = Default + "/filter-list-ticket-source";
        public const string FilterListTicketStatus = Default + "/filter-list-ticket-status";
        public const string FilterListTicketType = Default + "/filter-list-ticket-type";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomerType = Default + "/filter-list-customer-type";
        public const string FilterListTicketResolveType = Default + "/filter-list-ticket-resolve-type";

        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListProduct = Default + "/single-list-product";
        public const string SingleListCallLog = Default + "/single-list-call-log";
        public const string SingleListTicket = Default + "/single-list-ticket";
        public const string SingleListStatus = Default + "/single-list-status";
        public const string SingleListTicketIssueLevel = Default + "/single-list-ticket-issue-level";
        public const string SingleListTicketPriority = Default + "/single-list-ticket-priority";
        public const string SingleListTicketSource = Default + "/single-list-ticket-source";
        public const string SingleListTicketStatus = Default + "/single-list-ticket-status";
        public const string SingleListTicketGroup = Default + "/single-list-ticket-group";
        public const string SingleListTicketType = Default + "/single-list-ticket-type";
        public const string SingleListTicketResolveType = Default + "/single-list-ticket-resolve-type";
        public const string SingleListCustomerType = Default + "/single-list-customer-type";
        

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketFilter.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(TicketFilter.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(TicketFilter.UserId), FieldTypeEnum.ID.Id },
            { nameof(TicketFilter.TicketTypeId), FieldTypeEnum.ID.Id },
            { nameof(TicketFilter.DepartmentId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Parent, Count, List, Get,SingleListTicketGroup, FilterListCustomerType, SingleListCustomerType,
                SingleListTicketType,FilterListCustomer, FilterListOrganization, FilterListTicketResolveType,
                FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus,
                FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource,
                FilterListTicketStatus, FilterListAppUser, FilterListTicketType } },
            { "Thêm", new List<string> {
                Master, Count, List, Get,SingleListTicketType,SingleListTicketGroup, FilterListCustomerType, SingleListCustomerType,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                Detail, Create, SingleListTicketResolveType,
                SingleListCustomer, SingleListOrganization, SingleListProduct, SingleListCallLog, SingleListTicket, SingleListStatus, SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListAppUser,
                Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List,  } },

            { "Sửa", new List<string> {
                Master, Count, List, Get, GetPreview,SingleListTicketType,SingleListTicketGroup,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                Detail, Update,
                SingleListCustomer, SingleListOrganization, SingleListProduct, SingleListCallLog, SingleListTicket, SingleListStatus, SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListAppUser,
                Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List, Count, List,  } },

            { "Xoá", new List<string> {
                Master, Count, List, Get, GetPreview,SingleListTicketType,SingleListTicketGroup,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                Delete,
                SingleListCustomer, SingleListOrganization, SingleListProduct, SingleListCallLog, SingleListTicket, SingleListStatus, SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListAppUser,  } },

            { "Xoá nhiều", new List<string> {
                Master, Count, List, Get,SingleListTicketType,SingleListTicketGroup,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Master, Count, List, Get,SingleListTicketType,SingleListTicketGroup,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                Export } },

            { "Nhập excel", new List<string> {
                Master, Count, List, Get,SingleListTicketType,SingleListTicketGroup,
                FilterListCustomer, FilterListOrganization, FilterListProduct, FilterListCallLog, FilterListTicket, FilterListStatus, FilterListTicketIssueLevel, FilterListTicketPriority, FilterListTicketSource, FilterListTicketStatus, FilterListAppUser,
                ExportTemplate, Import } },

            { "Hiển thị", new List<string> {
                Master, Count, List, Get, GetPreview } },

            { "Gửi tin nhắn", new List<string> {
                GetPreview, SendSms } },

            { "Gửi email", new List<string> {
                GetPreview, SendEmail } },

            { "Tạm dừng", new List<string> {
                GetPreview, Pause, Update } },

            { "Thay đổi trạng thái", new List<string> {
                GetPreview, ChangeStatus, Update } },

            { "Mở lại", new List<string> {
                GetPreview, ReOpen, Update } },

            { "Đóng ticket", new List<string> {
                GetPreview, Close, Update } },

            { "Hiển thị ticket liên quan", new List<string> {
                GetPreview, Relation, Update } },

            { "Hiển thị Khách hàng", new List<string> {
                GetPreview, ViewCustomer } },
        };
    }
}
