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
using CRM.Services.MTicketGroup;
using CRM.Services.MStatus;
using CRM.Services.MTicketType;

namespace CRM.Rpc.ticket_group
{
    public class TicketGroupRoute : Root
    {
        public const string Parent = Module + "/ticket-setting";
        public const string Master = Module + "/ticket-setting/ticket-group/ticket-group-master";
        public const string Detail = Module + "/ticket-setting/ticket-group/ticket-group-detail";
        private const string Default = Rpc + Module + "/ticket-group";
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
        
        public const string FilterListTicketType = Default + "/filter-list-ticket-type";
        

        
        public const string SingleListStatus = Default + "/single-list-status";
        
        public const string SingleListTicketType = Default + "/single-list-ticket-type";
        
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketGroupFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(TicketGroupFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(TicketGroupFilter.OrderNumber), FieldTypeEnum.LONG.Id },
            { nameof(TicketGroupFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(TicketGroupFilter.TicketTypeId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, GetPreview, Parent,
                 FilterListStatus, FilterListTicketType, } },
            { "Thêm", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                Detail, Create, 
                SingleListStatus, SingleListTicketType, 
                Count, List, Count, List,  } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                Detail, Update, 
                SingleListStatus, SingleListTicketType,  
                Count, List, Count, List,  } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                Delete, 
                SingleListStatus, SingleListTicketType,  } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketType,  
                ExportTemplate, Import } },
        };
    }
}
