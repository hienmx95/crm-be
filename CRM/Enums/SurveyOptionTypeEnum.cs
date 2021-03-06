using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class SurveyOptionTypeEnum
    {
        public static GenericEnum SINGLE = new GenericEnum { Id = 1, Code = "SINGLE", Name = "Lựa chọn đơn" };
        public static GenericEnum ROW = new GenericEnum { Id = 2, Code = "ROW", Name = "Hàng" };
        public static GenericEnum COLUMN = new GenericEnum { Id = 3, Code = "COLUMN", Name = "Cột" };
    }
}
