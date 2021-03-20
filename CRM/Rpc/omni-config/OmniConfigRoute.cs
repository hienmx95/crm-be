using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.omni_config
{
    public class OmniConfigRoute : Root
    {
        public const string Parent = Module + "/omni-setting";
        public const string Master = Module + "/omni-setting/omni-config/omni-config-master";
        public const string ChooseMaster = Module + "/omni-setting/omni-config/omni-config-choose/omni-config-choose-master";
        public const string ChooseZalo = Module + "/omni-setting/omni-config/omni-config-choose/omni-config-choose-zalo/*";
        public const string ChooseFacebook = Module + "/omni-setting/omni-config/omni-config-choose/omni-config-choose-facebook/*";
        //private const string Default = Rpc + Module + "/dashboards/user";

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Hiển thị", new List<string> {
                Parent,
                Master,
                ChooseMaster,
                ChooseZalo,
                ChooseFacebook
            } },
        };
    }
}
