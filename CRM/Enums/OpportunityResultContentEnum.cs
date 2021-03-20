using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class OpportunityResultTypeEnum
    {
        public static GenericEnum THUA_DOI_THU => new GenericEnum { Id = 1, Name = "Thua đối thủ", Code = "THUA_DOI_THU" };
        public static GenericEnum KHONG_CO_KINH_PHI => new GenericEnum { Id = 2, Name = "Không có kinh phí", Code = "KHONG_CO_KINH_PHI" };
        public static GenericEnum KHONG_PHAN_HOI => new GenericEnum { Id = 3, Name = "Không phản hồi", Code = "KHONG_PHAN_HOI" };
        public static GenericEnum GIA => new GenericEnum { Id = 4, Name = "Giá", Code = "GIA" };
        public static GenericEnum KHAC => new GenericEnum { Id = 5, Name = "Khác", Code = "KHAC" };

        public static List<GenericEnum> OpportunityResultTypeEnumList = new List<GenericEnum>()
        {
            THUA_DOI_THU, KHONG_CO_KINH_PHI,KHONG_PHAN_HOI,GIA,KHAC
        };
    }
}
