using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class OpportunityStatusEnum
    {
        public static GenericEnum MOI = new GenericEnum { Id = 1, Code = "MOI", Name = "Mới" };
        public static GenericEnum PTNC = new GenericEnum { Id = 2, Code = "PTNC", Name = "Phân tích nhu cầu" };
        public static GenericEnum TKG = new GenericEnum { Id = 3, Code = "TKG", Name = "Tham khảo giá" };
        public static GenericEnum NRQD = new GenericEnum { Id = 4, Code = "NRQD", Name = "Người ra quyết định" };
        public static GenericEnum PTHV = new GenericEnum { Id = 5, Code = "PTHV", Name = "Phân tích hành vi" };
        public static GenericEnum BG = new GenericEnum { Id = 6, Code = "BG", Name = "Báo giá" };
        public static GenericEnum DP = new GenericEnum { Id = 7, Code = "DP", Name = "Đàm phán" };
        public static GenericEnum TC = new GenericEnum { Id = 8, Code = "TC", Name = "Thành công" };
    }

}
