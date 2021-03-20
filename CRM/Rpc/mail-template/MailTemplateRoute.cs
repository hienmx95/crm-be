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
using CRM.Services.MMailTemplate;
using CRM.Services.MStatus;

namespace CRM.Rpc.mail_template
{
    public class MailTemplateRoute : Root
    {
        public const string Parent = Module + "/mail";
        public const string Master = Module + "/mail/mail-template/mail-template-master";
        public const string Detail = Module + "/mail/mail-template/mail-template-detail";
        private const string Default = Rpc + Module + "/mail-template";
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
        
        
        public const string FilterListStatus = Default + "/filter-list-status";
        
        public const string SingleListStatus = Default + "/single-list-status";

        public const string SingleListPattern = Default + "/single-list-pattern";
        
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(MailTemplateFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(MailTemplateFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(MailTemplateFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(MailTemplateFilter.Content), FieldTypeEnum.STRING.Id },
            { nameof(MailTemplateFilter.StatusId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, GetPreview, Parent,
                 FilterListStatus, } },
            { "Thêm", new List<string> { 
                Master, Count, List, Get, GetPreview, Parent,
                FilterListStatus, SingleListPattern,
                Detail, Create, 
                SingleListStatus, 
                Count, List,  } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get, GetPreview, Parent,
                FilterListStatus, SingleListPattern,
                Detail, Update, 
                SingleListStatus,  
                Count, List,  } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Delete, 
                SingleListStatus,  } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus,  
                ExportTemplate, Import } },
        };
    }
}
