using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class ActivityPriorityEnum
    {
        public static GenericEnum CAO = new GenericEnum { Id = 1, Code = "CAO", Name = "Cao" };
        public static GenericEnum BINH_THUONG = new GenericEnum { Id = 2, Code = "BINH_THUONG", Name = "Bình thường" };
        public static GenericEnum THAP = new GenericEnum { Id = 3, Code = "THAP", Name = "Thấp" };
        public static List<GenericEnum> ActivityPriorityEnumList = new List<GenericEnum>
        {
            CAO, BINH_THUONG, THAP
        };
    }

}
