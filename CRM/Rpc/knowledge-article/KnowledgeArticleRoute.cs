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
using CRM.Services.MKnowledgeArticle;
using CRM.Services.MAppUser;
using CRM.Services.MKnowledgeGroup;

namespace CRM.Rpc.knowledge_article
{
    public class KnowledgeArticleRoute : Root
    {
        public const string Parent = Module + "/knowledge";
        public const string Master = Module + "/knowledge/knowledge-article/knowledge-article-master";
        public const string Detail = Module + "/knowledge/knowledge-article/knowledge-article-detail";
        private const string Default = Rpc + Module + "/knowledge-article";
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


        public const string FilterListAppUser = Default + "/filter-list-app-user";

        public const string FilterListKnowledgeGroup = Default + "/filter-list-knowledge-group";
        public const string FilterListStatus = Default + "/filter-list-status";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListItem = Default + "/filter-list-item";
        public const string FilterListKMSStatus = Default + "/filter-list-k-m-s-status";


        public const string SingleListAppUser = Default + "/single-list-app-user";

        public const string SingleListKnowledgeGroup = Default + "/single-list-knowledge-group";
        public const string SingleListStatus = Default + "/single-list-status";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListKMSStatus = Default + "/single-list-k-m-s-status";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
           { nameof(KnowledgeArticleFilter.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(KnowledgeArticleFilter.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(KnowledgeArticleFilter.UserId), FieldTypeEnum.ID.Id },
            { nameof(KnowledgeArticleFilter.KnowledgeGroupId), FieldTypeEnum.ID.Id },
            { nameof(KnowledgeArticleFilter.AppliedDepartmentId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Parent, Count, List, Get, GetPreview, SingleListKnowledgeGroup,
                FilterListAppUser, FilterListKnowledgeGroup, SingleListStatus, FilterListStatus ,FilterListOrganization,FilterListItem,FilterListKMSStatus
            } },
            { "Thêm", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                Detail, Create, SingleListStatus,
                SingleListAppUser, SingleListKnowledgeGroup,
                Count, List, Count, List,SingleListOrganization,SingleListItem ,SingleListKMSStatus } },

            { "Sửa", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                Detail, Update, SingleListStatus,
                SingleListAppUser, SingleListKnowledgeGroup,
                Count, List, Count, List,SingleListOrganization,SingleListItem ,SingleListKMSStatus } },

            { "Xoá", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                Delete,
                SingleListAppUser, SingleListKnowledgeGroup,  } },

            { "Xoá nhiều", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                Export, SingleListStatus } },

            { "Nhập excel", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListKnowledgeGroup,
                ExportTemplate, Import } },
        };
    }
}
