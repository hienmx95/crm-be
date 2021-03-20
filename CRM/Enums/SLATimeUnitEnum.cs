using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class SLATimeUnitEnum
    {
        public static GenericEnum MINUTES    = new GenericEnum { Id = 1, Code = "MINUTES", Name = "Phút" };
        public static GenericEnum HOURS = new GenericEnum { Id = 2, Code = "HOURS", Name = "Giờ" };
        public static GenericEnum DAY = new GenericEnum { Id = 3, Code = "DAY", Name = "Ngày" };

        public static List<GenericEnum> SLATimeUnitEnumList = new List<GenericEnum>()
        {
            MINUTES, HOURS, DAY
        };
    }

}
