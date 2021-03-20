using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class EditedPriceStatusEnum
    {
        public static GenericEnum ACTIVE = new GenericEnum { Id = 1, Code = "ACTIVE", Name = "Active" };
        public static GenericEnum INACTIVE = new GenericEnum { Id = 0, Code = "INACTIVE", Name = "Inactive" };

        public static List<GenericEnum> EditedPriceStatusEnumList = new List<GenericEnum>()
        {
            ACTIVE, INACTIVE
        };
    }
}
