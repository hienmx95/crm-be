using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class TicketResolveTypeEnum
    {
        public static GenericEnum ANSWERED = new GenericEnum { Id = 1, Code = "ANSWERED", Name = "Gửi thông tin phản hồi" };
        public static GenericEnum DUPLICATED = new GenericEnum { Id = 2, Code = "DUPLICATED", Name = "Trùng thông tin" };
        public static GenericEnum SPAM = new GenericEnum { Id = 3, Code = "SPAM", Name = "Spam" };
        public static GenericEnum OTHER = new GenericEnum { Id = 4, Code = "OTHER", Name = "Khác" };

        public static List<GenericEnum> TicketResolveTypeEnumList = new List<GenericEnum>()
        {
            ANSWERED, DUPLICATED, SPAM, OTHER
        };
    }
}
