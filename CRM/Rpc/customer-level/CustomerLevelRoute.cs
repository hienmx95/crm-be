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
using CRM.Services.MCustomerLevel;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_level
{
    public class CustomerLevelRoute : Root
    {
        public const string Parent = Module + "/customer-setting/customer-level";
        public const string Master = Module + "/customer-setting/customer-level/customer-level-master";
        public const string Detail = Module + "/customer-setting/customer-level/customer-level-detail";
        private const string Default = Rpc + Module + "/customer-level";
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
        
        public const string FilterListStatus = Default + "/filter-list-status";

        public const string SingleListStatus = Default + "/single-list-status";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerLevelFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(CustomerLevelFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(CustomerLevelFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(CustomerLevelFilter.Color), FieldTypeEnum.STRING.Id },
            { nameof(CustomerLevelFilter.PointFrom), FieldTypeEnum.LONG.Id },
            { nameof(CustomerLevelFilter.PointTo), FieldTypeEnum.LONG.Id },
            { nameof(CustomerLevelFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(CustomerLevelFilter.Description), FieldTypeEnum.STRING.Id },
            { nameof(CustomerLevelFilter.RowId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "T??m ki???m", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                 FilterListStatus, } },
            { "Th??m", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Detail, Create, 
                SingleListStatus, 
                 } },

            { "S???a", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Detail, Update, 
                SingleListStatus,  
                 } },

            { "Xo??", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Delete, 
                SingleListStatus,  } },

            { "Xo?? nhi???u", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                BulkDelete } },

            { "Xu???t excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Export } },

            { "Nh???p excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                ExportTemplate, Import } },
        };
    }
}
