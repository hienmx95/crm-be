using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class PaymentStatusEnum
    {
        public static GenericEnum CTT = new GenericEnum { Id = 1, Name = "Chưa thanh toán", Value = "#000000" };
        public static GenericEnum TTMP = new GenericEnum { Id = 2, Name = "Thanh toán một phần", Value = "#000000" };
        public static GenericEnum DTT = new GenericEnum { Id = 3, Name = "Đã thanh toán", Value = "#000000" };

        public static List<GenericEnum> PaymentStatusEnumList = new List<GenericEnum>()
        {
            CTT, TTMP, DTT
        };
    }
}
