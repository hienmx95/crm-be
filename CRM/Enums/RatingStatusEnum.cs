using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class RatingStatusEnum
    {
        public static GenericEnum GOOD = new GenericEnum { Id = 1, Code = "GOOD", Name = "Tuyệt vời" };

        public static List<GenericEnum> RatingStatusEnumList = new List<GenericEnum>()
        {
            GOOD
        };
    }
}
