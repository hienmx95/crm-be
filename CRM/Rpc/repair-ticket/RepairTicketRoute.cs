using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.repair_ticket
{
    public class RepairTicketRoute : Root
    {
        public const string Parent = Module + "/category";
        public const string Master = Module + "/category/repair-ticket/repair-ticket-master";
        public const string Detail = Module + "/category/repair-ticket/repair-ticket-detail";
        private const string Default = Rpc + Module + "/repair-ticket";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string GetPreview = Default + "/get-preview";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-template";
        public const string BulkDelete = Default + "/bulk-delete";
        
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListOrderCategory = Default + "/filter-list-order-category";
        public const string FilterListRepairStatus = Default + "/filter-list-repair-status";
        public const string FilterListPaymentStatus = Default + "/filter-list-payment-status";

        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListOrderCategory = Default + "/single-list-order-category";
        public const string SingleListRepairStatus = Default + "/single-list-repair-status";
        public const string SingleListCustomerSalesOrder = Default + "/single-list-customer-sales-order";
        public const string SingleListDirectSalesOrder = Default + "/single-list-direct-sales-order";
        public const string SingleListPaymentStatus = Default + "/single-list-payment-status";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(RepairTicketFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.OrderId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.OrderCategoryId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.RepairDueDate), FieldTypeEnum.DATE.Id },
            { nameof(RepairTicketFilter.ItemId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.RejectReason), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.DeviceState), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.RepairStatusId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.RepairAddess), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.ReceiveUser), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.ReceiveDate), FieldTypeEnum.DATE.Id },
            { nameof(RepairTicketFilter.RepairDate), FieldTypeEnum.DATE.Id },
            { nameof(RepairTicketFilter.ReturnDate), FieldTypeEnum.DATE.Id },
            { nameof(RepairTicketFilter.RepairSolution), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.Note), FieldTypeEnum.STRING.Id },
            { nameof(RepairTicketFilter.RepairCost), FieldTypeEnum.DECIMAL.Id },
            { nameof(RepairTicketFilter.PaymentStatusId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.CustomerId), FieldTypeEnum.ID.Id },
            { nameof(RepairTicketFilter.CreatorId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                 FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus, } },
            { "Thêm", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                Detail, Create, 
                SingleListAppUser, SingleListCustomer, SingleListItem, SingleListOrderCategory, SingleListPaymentStatus, SingleListRepairStatus,
                SingleListCustomerSalesOrder, SingleListDirectSalesOrder,
                 } },

            { "Sửa", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                Detail, Update, 
                SingleListAppUser, SingleListCustomer, SingleListItem, SingleListOrderCategory, SingleListPaymentStatus, SingleListRepairStatus,
                SingleListCustomerSalesOrder, SingleListDirectSalesOrder, 
                 } },

            { "Xoá", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                Delete, 
                SingleListAppUser, SingleListCustomer, SingleListItem, SingleListOrderCategory, SingleListPaymentStatus, SingleListRepairStatus,
                SingleListCustomerSalesOrder, SingleListDirectSalesOrder, } },

            { "Xoá nhiều", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                Export } },

            { "Nhập excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListOrderCategory, FilterListPaymentStatus, FilterListRepairStatus,  
                ExportTemplate, Import } },
        };
    }
}
