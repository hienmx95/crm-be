using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class CallCategoryEnum
    {
        public static GenericEnum CALL_AWAY = new GenericEnum { Id = 1, Code = "CALL AWAY", Name = "Cuộc gọi đi" };
        public static GenericEnum CALL_INCOMING = new GenericEnum { Id = 2, Code = "CALL INCOMING", Name = "Cuộc gọi đến" };
        public static List<GenericEnum> CallCategoryEnumList = new List<GenericEnum>
        {
            CALL_AWAY, CALL_INCOMING
        };
    }
}
