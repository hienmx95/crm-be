using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MCustomerFeedback;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerFeedbackType;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_feedback
{
    public class CustomerFeedbackRoute : Root
    {
        public const string Parent = Module + "/customer-setting/customer-feedback";
        public const string Master = Module + "/customer-setting/customer-feedback/customer-feedback-master";
        public const string Detail = Module + "/customer-setting/customer-feedback/customer-feedback-detail";
        private const string Default = Rpc + Module + "/customer-feedback";
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
        
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListCustomerFeedbackType = Default + "/filter-list-customer-feedback-type";
        public const string FilterListStatus = Default + "/filter-list-status";

        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListCustomerFeedbackType = Default + "/single-list-customer-feedback-type";
        public const string SingleListStatus = Default + "/single-list-status";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerFeedbackFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(CustomerFeedbackFilter.CustomerId), FieldTypeEnum.ID.Id },
            { nameof(CustomerFeedbackFilter.FullName), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFeedbackFilter.Email), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFeedbackFilter.PhoneNumber), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFeedbackFilter.CustomerFeedbackTypeId), FieldTypeEnum.ID.Id },
            { nameof(CustomerFeedbackFilter.Title), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFeedbackFilter.SendDate), FieldTypeEnum.DATE.Id },
            { nameof(CustomerFeedbackFilter.Content), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFeedbackFilter.StatusId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                 FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus, } },
            { "Thêm", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                Detail, Create, 
                SingleListCustomer, SingleListCustomerFeedbackType, SingleListStatus, 
                 } },

            { "Sửa", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                Detail, Update, 
                SingleListCustomer, SingleListCustomerFeedbackType, SingleListStatus,  
                 } },

            { "Xoá", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                Delete, 
                SingleListCustomer, SingleListCustomerFeedbackType, SingleListStatus,  } },

            { "Xoá nhiều", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                Export } },

            { "Nhập excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomer, FilterListCustomerFeedbackType, FilterListStatus,  
                ExportTemplate, Import } },
        };
    }
}
