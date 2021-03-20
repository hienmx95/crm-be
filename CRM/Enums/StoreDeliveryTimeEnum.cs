using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class StoreDeliveryTimeEnum
    {
        public static GenericEnum SIX_HOURS = new GenericEnum { Id = 1, Code = "SIX_HOURS", Name = "Sau 6h" };
        public static GenericEnum TWELVE_HOURS = new GenericEnum { Id = 2, Code = "TWELVE_HOURS", Name = "Sau 12h" };
        public static GenericEnum TWENTY_FOUR_HOURS = new GenericEnum { Id = 3, Code = "TWENTY_FOUR_HOURS", Name = "Sau 24h" };
        public static GenericEnum FORTY_EIGHT_HOURS = new GenericEnum { Id = 4, Code = "FORTY_EIGHT_HOURS", Name = "Sau 48h" };

        public static List<GenericEnum> StoreDeliveryTimeEnumList = new List<GenericEnum>
        {
            SIX_HOURS, TWELVE_HOURS, TWENTY_FOUR_HOURS, FORTY_EIGHT_HOURS
        };
    }

}
