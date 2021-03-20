using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class UsedVariationEnum
    {
        public static GenericEnum USED = new GenericEnum { Id = 1, Code = "USED", Name = "Sử dụng phiên bản" };
        public static GenericEnum NOTUSED = new GenericEnum { Id = 0, Code = "NOTUSED", Name = "Không sử dụng phiên bản" };

        public static List<GenericEnum> UsedVariationEnumList = new List<GenericEnum>
        {
            USED, NOTUSED
        };
    }
}
