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
using CRM.Services.MScheduleMaster;
using CRM.Services.MAppUser;
using CRM.Services.MStatus;

namespace CRM.Rpc.schedule_master
{
    public class ScheduleMasterRoute : Root
    {
        public const string Master = Module + "/schedule-master/schedule-master-master";
        public const string Detail = Module + "/schedule-master/schedule-master-detail";
        private const string Default = Rpc + Module + "/schedule-master";
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
        
        
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        
        public const string FilterListStatus = Default + "/filter-list-status";
        

        
        public const string SingleListAppUser = Default + "/single-list-app-user";
        
        public const string SingleListStatus = Default + "/single-list-status";
        
        public static Dictionary<string, FieldType> Filters = new Dictionary<string, FieldType>
        {
            //{ nameof(ScheduleMasterFilter.Id), FieldType.ID },
            //{ nameof(ScheduleMasterFilter.ManagerId), FieldType.ID },
            //{ nameof(ScheduleMasterFilter.SalerId), FieldType.ID },
            //{ nameof(ScheduleMasterFilter.Name), FieldType.STRING },
            //{ nameof(ScheduleMasterFilter.Code), FieldType.STRING },
            //{ nameof(ScheduleMasterFilter.StatusId), FieldType.ID },
            //{ nameof(ScheduleMasterFilter.RecurDays), FieldType.DATE },
            //{ nameof(ScheduleMasterFilter.StartDate), FieldType.DATE },
            //{ nameof(ScheduleMasterFilter.EndDate), FieldType.DATE },
            //{ nameof(ScheduleMasterFilter.StartDayOfWeek), FieldType.DATE },
            //{ nameof(ScheduleMasterFilter.DisplayOrder), FieldType.LONG },
            //{ nameof(ScheduleMasterFilter.Description), FieldType.STRING },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, FilterListAppUser, FilterListStatus, } },

            { "Thêm", new List<string> { 
                Master, Count, List, Get,  FilterListAppUser, FilterListStatus, 
                Detail, Create, 
                 SingleListAppUser, SingleListStatus, } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get,  FilterListAppUser, FilterListStatus, 
                Detail, Update, 
                 SingleListAppUser, SingleListStatus, } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get,  FilterListAppUser, FilterListStatus, 
                Detail, Delete, 
                 SingleListAppUser, SingleListStatus, } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, FilterListAppUser, FilterListStatus, 
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, FilterListAppUser, FilterListStatus, 
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, FilterListAppUser, FilterListStatus, 
                ExportTemplate, Import } },
        };
    }
}
