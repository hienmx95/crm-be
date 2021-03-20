using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class KpiCriteriaItemEnum
    {
        public static GenericEnum ORDER_OUTPUT = new GenericEnum { Id = 1, Code = "OrderOutput", Name = "Sản lượng đơn hàng" };
        public static GenericEnum SALES = new GenericEnum { Id = 2, Code = "Sales", Name = "Doanh số" };
        public static GenericEnum ORDER_NUMBER = new GenericEnum { Id = 3, Code = "OrderNumber", Name = "Số đơn hàng" };
        public static GenericEnum NUMBER_OF_CUSTOMER = new GenericEnum { Id = 4, Code = "NumberOfCustomer", Name = "Số khách hàng" };
        public static GenericEnum COUNT_ITEM_IN_CONTRACT = new GenericEnum { Id = 5, Code = "COUNT_ITEM_IN_CONTRACT", Name = "Sản lượng theo hợp đồng" };
        public static GenericEnum COUNT_CONTRACT = new GenericEnum { Id = 6, Code = "COUNT_CONTRACT", Name = "Số lượng hợp đồng" };
        public static GenericEnum REVEUNE_CONTRACT = new GenericEnum { Id = 7, Code = "REVEUNE_CONTRACT", Name = "Doanh thu hợp đồng" };

        public static List<GenericEnum> KpiCriteriaItemEnumList = new List<GenericEnum>()
        {
            ORDER_OUTPUT,
            SALES,
            ORDER_NUMBER,
            NUMBER_OF_CUSTOMER ,
            COUNT_ITEM_IN_CONTRACT,
            COUNT_CONTRACT,
            REVEUNE_CONTRACT
        };
    }
}
