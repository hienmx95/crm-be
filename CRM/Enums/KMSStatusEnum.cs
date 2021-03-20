using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class KMSStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới" };
        public static GenericEnum DOING = new GenericEnum { Id = 2, Code = "DOING", Name = "Đang áp dụng" };
        public static GenericEnum EXPIRED = new GenericEnum { Id = 3, Code = "EXPIRED", Name = "Hết hạn" };

        public static List<GenericEnum> KMSStatusEnumList = new List<GenericEnum>()
        {
            NEW, DOING, EXPIRED
        };
    }

}
