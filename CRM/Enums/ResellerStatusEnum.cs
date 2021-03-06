using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class ResellerStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "New", Name = "Mới tạo" };
        public static GenericEnum PENDING = new GenericEnum { Id = 2, Code = "Pending", Name = "Chờ duyệt" };
        public static GenericEnum APPROVED = new GenericEnum { Id = 3, Code = "Approved", Name = "Đã duyệt" };
        public static GenericEnum REJECTED = new GenericEnum { Id = 4, Code = "Rejected", Name = "Từ chối" };
    }
}
