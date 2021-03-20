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
using CRM.Services.MTicketType;
using CRM.Services.MStatus;

namespace CRM.Rpc.ticket_type
{
    public class TicketTypeRoute : Root
    {
        public const string Parent = Module + "/ticket-setting";
        public const string Master = Module + "/ticket-setting/ticket-type/ticket-type-master";
        public const string Detail = Module + "/ticket-setting/ticket-type/ticket-type-detail";
        private const string Default = Rpc + Module + "/ticket-type";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
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
        
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketTypeFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(TicketTypeFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(TicketTypeFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(TicketTypeFilter.ColorCode), FieldTypeEnum.STRING.Id },
            { nameof(TicketTypeFilter.StatusId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, FilterListStatus, SingleListStatus} },

            { "Thêm", new List<string> { 
                Master, Count, List, Get,  FilterListStatus, Parent,
                Detail, Create, 
                 SingleListStatus, } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get,  FilterListStatus, 
                Detail, Update, 
                 SingleListStatus, } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get,  FilterListStatus, 
                Detail, Delete, 
                 SingleListStatus, } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, FilterListStatus, 
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, FilterListStatus, 
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, FilterListStatus, 
                ExportTemplate, Import } },
        };
    }
}
