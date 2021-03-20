using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CustomerLeadSourceEnum
    {
        public static GenericEnum CALL = new GenericEnum { Id = 1, Code = "CALL", Name = "Gọi điện" };
        public static GenericEnum FB = new GenericEnum { Id = 2, Code = "FB", Name = "Facebook" };
        public static GenericEnum WEB = new GenericEnum { Id = 3, Code = "WEB", Name = "Website" };
        public static GenericEnum PARTNER = new GenericEnum { Id = 4, Code = "PARTNER", Name = "Đối tác" };
        public static GenericEnum EMAIL = new GenericEnum { Id = 5, Code = "EMAIL", Name = "Email" };
        public static GenericEnum CUSTOMER_OLD = new GenericEnum { Id = 6, Code = "CUSTOMER_OLD", Name = "Khách hàng cũ" };
        public static GenericEnum EMPLOYEE = new GenericEnum { Id = 7, Code = "EMPLOYEE", Name = "Từ nhân viên" };
        public static GenericEnum LEAD = new GenericEnum { Id = 8, Code = "LEAD", Name = "Chuyển đổi từ đầu mối" };
        public static List<GenericEnum> CustomerLeadSourceEnumList = new List<GenericEnum>
        {
            CALL, FB, WEB, PARTNER, EMAIL, CUSTOMER_OLD, EMPLOYEE, LEAD
        };
    }

}
