using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.workflow_direction
{
    public class WorkflowDirectionRoute : Root
    {
        public const string Master = Module + "/workflow-direction/workflow-direction-master";
        public const string Detail = Module + "/workflow-direction/workflow-direction-detail";
        private const string Default = Rpc + Module + "/workflow-direction";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Export = Default + "/export";

        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListWorkflowStep = Default + "/filter-list-workflow-step";
        public const string FilterListWorkflowDefinition = Default + "/filter-list-workflow-definition";

        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListWorkflowStep = Default + "/single-list-workflow-step";
        public const string SingleListWorkflowDefinition = Default + "/single-list-workflow-definition";
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Master, Count, List, Get,
                FilterListWorkflowStep, FilterListWorkflowDefinition, FilterListAppUser} },
            { "Thêm", new List<string> {
                Master, Count, List, Get,
                FilterListWorkflowStep, FilterListWorkflowDefinition,
                Detail, Create,
                SingleListWorkflowStep, SingleListWorkflowDefinition, SingleListAppUser } },
            { "Sửa", new List<string> {
                Master, Count, List, Get,
                FilterListWorkflowStep, FilterListWorkflowDefinition,
                Detail, Update,
                SingleListWorkflowStep, SingleListWorkflowDefinition, SingleListAppUser } },
            { "Xoá", new List<string> {
                Master, Count, List, Get,
                FilterListWorkflowStep, FilterListWorkflowDefinition,
                Delete, } },
        };
    }
}
