using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CustomerLeadLevelEnum
    {
        public static GenericEnum KTN = new GenericEnum { Id = 1, Code = "KTN", Name = "Không tiềm năng" };
        public static GenericEnum TNT = new GenericEnum { Id = 2, Code = "TNT", Name = "Tiềm năng thấp" };
        public static GenericEnum TN = new GenericEnum { Id = 3, Code = "TN", Name = "Tiềm năng" };
        public static GenericEnum TNC = new GenericEnum { Id = 4, Code = "TNC", Name = "Tiềm năng cao" };

        public static List<GenericEnum> CustomerLeadLevelEnumList = new List<GenericEnum>
        {
            KTN, TNT, TN, TNC
        };
    }

}
