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
using CRM.Services.MCustomerGrouping;
using CRM.Services.MCustomerType;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_grouping
{
    public class CustomerGroupingRoute : Root
    {
        public const string Parent = Module + "/customer-setting/customer-grouping";
        public const string Master = Module + "/customer-setting/customer-grouping/customer-grouping-master";
        public const string Detail = Module + "/customer-setting/customer-grouping/customer-grouping-detail";
        private const string Default = Rpc + Module + "/customer-grouping";
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
        
        public const string FilterListCustomerType = Default + "/filter-list-customer-type";
        public const string FilterListCustomerGroupinging = Default + "/filter-list-customer-groupinging";
        public const string FilterListStatus = Default + "/filter-list-status";

        public const string SingleListCustomerType = Default + "/single-list-customer-type";
        public const string SingleListCustomerGroupinging = Default + "/single-list-customer-groupinging";
        public const string SingleListStatus = Default + "/single-list-status";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerGroupingFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(CustomerGroupingFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(CustomerGroupingFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(CustomerGroupingFilter.CustomerTypeId), FieldTypeEnum.ID.Id },
            { nameof(CustomerGroupingFilter.ParentId), FieldTypeEnum.ID.Id },
            { nameof(CustomerGroupingFilter.Path), FieldTypeEnum.STRING.Id },
            { nameof(CustomerGroupingFilter.Level), FieldTypeEnum.LONG.Id },
            { nameof(CustomerGroupingFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(CustomerGroupingFilter.Description), FieldTypeEnum.STRING.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                 FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus, } },
            { "Thêm", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                Detail, Create, 
                SingleListCustomerType, SingleListCustomerGroupinging, SingleListStatus, 
                 } },

            { "Sửa", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                Detail, Update, 
                SingleListCustomerType, SingleListCustomerGroupinging, SingleListStatus,  
                 } },

            { "Xoá", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                Delete, 
                SingleListCustomerType, SingleListCustomerGroupinging, SingleListStatus,  } },

            { "Xoá nhiều", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                Export } },

            { "Nhập excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCustomerType, FilterListCustomerGroupinging, FilterListStatus,  
                ExportTemplate, Import } },
        };
    }
}
