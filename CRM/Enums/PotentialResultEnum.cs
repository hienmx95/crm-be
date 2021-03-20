using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class PotentialResultEnum
    {
        public static GenericEnum SUCCESS = new GenericEnum { Id = 1, Code = "SUCCESS", Name = "Thành công" };
        public static GenericEnum FAILED = new GenericEnum { Id = 2, Code = "FAILED", Name = "Thất bại" };

        public static List<GenericEnum> PotentialResultEnumList = new List<GenericEnum>()
        {
            SUCCESS, FAILED
        };
    }
}
