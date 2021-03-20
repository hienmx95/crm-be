using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class SLAStatusEnum
    {
        public static GenericEnum Success => new GenericEnum { Id = 1, Name = "Hoàn thành", Code = "Success", Value = "#008b02" };
        public static GenericEnum Fail => new GenericEnum { Id = 2, Name = "Trễ hạn", Code = "Fail", Value = "#db3e00" };

        public static List<GenericEnum> SLAStatusEnumList = new List<GenericEnum>()
        {
            Success, Fail
        };
    }
}
