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
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MStatus;
using CRM.Services.MTicketGroup;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevelRoute : Root
    {
        public const string Parent = Module + "/ticket-setting";
        public const string Master = Module + "/ticket-setting/ticket-issue-level/ticket-issue-level-master";
        public const string Detail = Module + "/ticket-setting/ticket-issue-level/ticket-issue-level-detail";
        private const string Default = Rpc + Module + "/ticket-issue-level";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string GetPreview = Default + "/get-preview";
        public const string Get = Default + "/get";
        public const string GetDraft = Default + "/get-draft";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-tempate";
        public const string BulkDelete = Default + "/bulk-delete";
        
        public const string FilterListStatus = Default + "/filter-list-status";
        
        public const string FilterListTicketGroup = Default + "/filter-list-ticket-group";

        public const string FilterListTicketType = Default + "/filter-list-ticket-type";

        public const string FilterListSLATimeUnit = Default + "/filter-list-s-l-a-time-unit";


        public const string SingleListSLATimeUnit = Default + "/single-list-s-l-a-time-unit";

        public const string SingleListStatus = Default + "/single-list-status";
        
        public const string SingleListTicketGroup = Default + "/single-list-ticket-group";

        public const string SingleListTicketType = Default + "/single-list-ticket-type";

        public const string SingleListSmsTemplate = Default + "/single-list-sms-template";

        public const string SingleListMailTemplate = Default + "/single-list-mail-template";

        public const string SingleListAppUser = Default + "/single-list-app-user";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketIssueLevelFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(TicketIssueLevelFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(TicketIssueLevelFilter.OrderNumber), FieldTypeEnum.LONG.Id },
            { nameof(TicketIssueLevelFilter.TicketGroupId), FieldTypeEnum.ID.Id },
            { nameof(TicketIssueLevelFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(TicketIssueLevelFilter.SLA), FieldTypeEnum.LONG.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, GetPreview, Parent, FilterListSLATimeUnit,
                 FilterListStatus, FilterListTicketGroup, FilterListTicketType} },
            { "Thêm", new List<string> { 
                Master, Count, List, Get, GetPreview, GetDraft, 
                SingleListSmsTemplate, SingleListMailTemplate, SingleListAppUser,
                FilterListStatus, FilterListTicketGroup, SingleListSLATimeUnit,
                Detail, Create, SingleListTicketType,
                SingleListStatus, SingleListTicketGroup, 
                Count, List, Count, List,  } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get, GetPreview, SingleListSLATimeUnit, 
                SingleListSmsTemplate, SingleListMailTemplate, SingleListAppUser,
                FilterListStatus, FilterListTicketGroup,  
                Detail, Update, SingleListTicketType,
                SingleListStatus, SingleListTicketGroup,  
                Count, List, Count, List,  } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketGroup,  
                Delete, 
                SingleListStatus, SingleListTicketGroup,  } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketGroup,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketGroup,
                Export, SingleListTicketType } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListStatus, FilterListTicketGroup,  
                ExportTemplate, Import } },
        };
    }
}
