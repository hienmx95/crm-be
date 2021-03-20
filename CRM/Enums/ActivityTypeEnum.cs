using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class ActivityTypeEnum
    {
        public static GenericEnum WORK => new GenericEnum { Id = 1, Name = "Công việc", Code = "WORK" };
        public static GenericEnum SEND_MAIL => new GenericEnum { Id = 2, Name = "Email", Code = "SEND_MAIL" };
        public static GenericEnum SEND_SMS => new GenericEnum { Id = 3, Name = "Tin nhắn", Code = "SEND_SMS" };
        public static GenericEnum CALL => new GenericEnum { Id = 4, Name = "Cuộc gọi", Code = "CALL" };
        public static GenericEnum EVENT => new GenericEnum { Id = 5, Name = "Sự kiện", Code = "EVENT" };
        public static GenericEnum METTING => new GenericEnum { Id = 6, Name = "Gặp mặt", Code = "METTING" };

        public static List<GenericEnum> ActivityTypeEnumList = new List<GenericEnum>()
        {
            WORK, SEND_MAIL, SEND_SMS, CALL, EVENT, METTING
        };
    }
}
