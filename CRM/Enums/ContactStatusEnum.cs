using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class ContactStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới" };
        public static GenericEnum NORMAL = new GenericEnum { Id = 2, Code = "NORMAL", Name = "Bình thường" };
        public static GenericEnum GOOD = new GenericEnum { Id = 3, Code = "GOOD", Name = "Hài lòng" };
        public static GenericEnum FRIENDLY = new GenericEnum { Id = 4, Code = "FRIENDLY", Name = "Thân thiết" };

        public static List<GenericEnum> ContactStatusEnumList = new List<GenericEnum>()
        {
            NEW, NORMAL, GOOD, FRIENDLY
        };
    }

}
