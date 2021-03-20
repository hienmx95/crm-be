using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CustomerContactTypeEnum
    {
        public static GenericEnum ZALO = new GenericEnum { Id = 1, Code = "ZALO", Name = "Zalo" };
        public static GenericEnum SKYPE = new GenericEnum { Id = 2, Code = "SKYPE", Name = "Skype" };

        public static List<GenericEnum> CustomerContactTypeEnumList = new List<GenericEnum>
        {
            ZALO, SKYPE
        };
    }

}
