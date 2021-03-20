using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class SaleStageEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới" };
        public static GenericEnum ANALYSIS = new GenericEnum { Id = 2, Code = "ANALYSIS", Name = "Cần phân tích" };
        public static GenericEnum PROPOSE = new GenericEnum { Id = 3, Code = "PROPOSE", Name = "Gửi đề xuất/Báo giá" };
        public static GenericEnum NEGOTIATION = new GenericEnum { Id = 4, Code = "NEGOTIATION", Name = "Thương lượng" };
        public static GenericEnum CLOSED = new GenericEnum { Id = 5, Code = "CLOSED", Name = "Đóng" };

        public static List<GenericEnum> SaleStageEnumList = new List<GenericEnum>
        {
            NEW, ANALYSIS, PROPOSE, NEGOTIATION, CLOSED
        };
    }

}
