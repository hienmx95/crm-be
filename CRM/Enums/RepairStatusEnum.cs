using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class RepairStatusEnum
    {
        public static GenericEnum DTN = new GenericEnum { Id = 1, Code = "DTN", Name = "Đã tiếp nhận", Value = "#000000" };
        public static GenericEnum DSC = new GenericEnum { Id = 2, Code = "DSC", Name = "Đang sửa chữa", Value = "#000000" };
        public static GenericEnum DSX = new GenericEnum { Id = 3, Code = "DSX", Name = "Đã sử xong", Value = "#000000" };
        public static GenericEnum DBG = new GenericEnum { Id = 4, Code = "DBG", Name = "Đã bàn giao", Value = "#000000" };

        public static List<GenericEnum> RepairStatusEnumList = new List<GenericEnum>()
        {
            DTN, DSC, DSX, DBG
        };
    }
}
