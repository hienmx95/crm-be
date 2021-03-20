using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class CallStatusEnum
    {
        public static GenericEnum ANSWERED = new GenericEnum { Id = 1, Code = "ANSWERED", Name = "Đã trả lời" };
        public static GenericEnum NO_ANSWER = new GenericEnum { Id = 2, Code = "NO ANSWER", Name = "Không trả lời" };
        public static GenericEnum MISSED_CALL = new GenericEnum { Id = 3, Code = "MISSED CALL", Name = "Cuộc gọi nhỡ" };
        public static List<GenericEnum> CallStatusEnumList = new List<GenericEnum>
        {
            ANSWERED, NO_ANSWER, MISSED_CALL
        };
    }
}
