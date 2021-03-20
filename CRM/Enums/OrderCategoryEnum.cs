using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class OrderCategoryEnum
    {
        public static GenericEnum ORDER_CUSTOMER = new GenericEnum { Id = 1, Code = "ORDER_CUSTOMER", Name = "Đơn hàng khách hàng" };
        public static GenericEnum ORDER_DIRECT = new GenericEnum { Id = 2, Code = "ORDER_DIRECT", Name = "Đơn hàng đại lý" };

        public static List<GenericEnum> OrderCategoryEnumList = new List<GenericEnum>()
        {
            ORDER_CUSTOMER, ORDER_DIRECT
        };
    }
}
