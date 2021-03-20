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
using CRM.Services.MPhoneType;
using CRM.Services.MStatus;

namespace CRM.Rpc.phone_type
{
    public class PhoneTypeRoute : Root
    {
        public const string Parent = Module + "/phone-type";
        public const string Master = Module + "/phone-type/phone-type-master";
        public const string Detail = Module + "/phone-type/phone-type-detail";
        private const string Default = Rpc + Module + "/phone-type";
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
            { nameof(PhoneTypeFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(PhoneTypeFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(PhoneTypeFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(PhoneTypeFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(PhoneTypeFilter.RowId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                 FilterListStatus, } },
            { "Thêm", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Detail, Create, 
                SingleListStatus, 
                 } },

            { "Sửa", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Detail, Update, 
                SingleListStatus,  
                 } },

            { "Xoá", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Delete, 
                SingleListStatus,  } },

            { "Xoá nhiều", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Export } },

            { "Nhập excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                ExportTemplate, Import } },
        };
    }
}
