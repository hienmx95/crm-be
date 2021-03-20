using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ActionPageMapping : DataEntity,  IEquatable<ActionPageMapping>
    {
        public long ActionId { get; set; }
        public long PageId { get; set; }
        public Action Action { get; set; }
        public Page Page { get; set; }
        
        public bool Equals(ActionPageMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ActionPageMappingFilter : FilterEntity
    {
        public IdFilter ActionId { get; set; }
        public IdFilter PageId { get; set; }
        public List<ActionPageMappingFilter> OrFilter { get; set; }
        public ActionPageMappingOrder OrderBy {get; set;}
        public ActionPageMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActionPageMappingOrder
    {
        Action = 0,
        Page = 1,
    }

    [Flags]
    public enum ActionPageMappingSelect:long
    {
        ALL = E.ALL,
        Action = E._0,
        Page = E._1,
    }
}
