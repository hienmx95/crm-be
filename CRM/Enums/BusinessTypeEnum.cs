using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class BusinessTypeEnum
    {
        public static GenericEnum CTCP => new GenericEnum { Id = 1, Name = "Công ty cổ phần", Code = "CTCP" };
        public static GenericEnum MTV => new GenericEnum { Id = 2, Name = "Công ty TNHH một thành viên", Code = "MTV" };
        public static GenericEnum HTV => new GenericEnum { Id = 3, Name = "Công ty THHH 2 thành viên trở lên", Code = "HTV" };
        public static GenericEnum HD => new GenericEnum { Id = 4, Name = "Công ty hợp danh", Code = "HD" };
        public static GenericEnum TN => new GenericEnum { Id = 5, Name = "Doanh nghiệp tư nhân", Code = "TN" };

        public static List<GenericEnum> BusinessTypeEnumList = new List<GenericEnum>()
        {
            CTCP, MTV, HTV, HD, TN
        };
    }
}
