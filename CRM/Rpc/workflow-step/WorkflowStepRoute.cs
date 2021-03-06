using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.workflow_step
{
    public class WorkflowStepRoute : Root
    {
        public const string Master = Module + "/workflow-step/workflow-step-master";
        public const string Detail = Module + "/workflow-step/workflow-step-detail";
        private const string Default = Rpc + Module + "/workflow-step";
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
        public const string FilterListRole = Default + "/filter-list-role";
        public const string FilterListWorkflowDefinition = Default + "/filter-list-workflow-definition";

        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListRole = Default + "/single-list-role";
        public const string SingleListWorkflowDefinition = Default + "/single-list-workflow-definition";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition, FilterListAppUser} },
            { "Thêm", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition,
                Detail, Create,
                SingleListRole, SingleListWorkflowDefinition,  SingleListAppUser} },
            { "Sửa", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition,
                Detail, Update,
                SingleListRole, SingleListWorkflowDefinition,  SingleListAppUser} },
            { "Xoá", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition,
                Delete, } },
            { "Xoá nhiều", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition,
                BulkDelete } },
            { "Nhập excel", new List<string> {
                Master, Count, List, Get,
                FilterListRole, FilterListWorkflowDefinition,
                ExportTemplate, } },
        };
    }
}
