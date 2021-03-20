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
using CRM.Services.MTicketOfUser;
using CRM.Services.MTicket;
using CRM.Services.MTicketStatus;
using CRM.Services.MAppUser;

namespace CRM.Rpc.ticket_of_user
{
    public class TicketOfUserRoute : Root
    {
        public const string Master = Module + "/ticket-of-user/ticket-of-user-master";
        public const string Detail = Module + "/ticket-of-user/ticket-of-user-detail";
        private const string Default = Rpc + Module + "/ticket-of-user";
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
        
        
        public const string FilterListTicket = Default + "/filter-list-ticket";
        
        public const string FilterListTicketStatus = Default + "/filter-list-ticket-status";
        
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        

        
        public const string SingleListTicket = Default + "/single-list-ticket";
        
        public const string SingleListTicketStatus = Default + "/single-list-ticket-status";
        
        public const string SingleListAppUser = Default + "/single-list-app-user";
        
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketOfUserFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(TicketOfUserFilter.Notes), FieldTypeEnum.STRING.Id },
            { nameof(TicketOfUserFilter.UserId), FieldTypeEnum.ID.Id },
            { nameof(TicketOfUserFilter.TicketId), FieldTypeEnum.ID.Id },
            { nameof(TicketOfUserFilter.TicketStatusId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, GetPreview,
                 FilterListTicket, FilterListTicketStatus, FilterListAppUser, } },
            { "Thêm", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                Detail, Create, 
                SingleListTicket, SingleListTicketStatus, SingleListAppUser, 
                Count, List, Count, List, Count, List,  } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                Detail, Update, 
                SingleListTicket, SingleListTicketStatus, SingleListAppUser,  
                Count, List, Count, List, Count, List,  } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                Delete, 
                SingleListTicket, SingleListTicketStatus, SingleListAppUser,  } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListTicket, FilterListTicketStatus, FilterListAppUser,  
                ExportTemplate, Import } },
        };
    }
}
