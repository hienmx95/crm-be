using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CurrencyEnum
    {
        public static GenericEnum VND = new GenericEnum { Id = 1, Code = "VND", Name = "VND" };
        public static GenericEnum USD = new GenericEnum { Id = 2, Code = "USD", Name = "USD" };

        public static List<GenericEnum> CurrencyEnumList = new List<GenericEnum>
        {
            VND, USD
        };
    }

}
