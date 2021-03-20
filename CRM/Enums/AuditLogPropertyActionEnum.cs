using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class AuditLogPropertyActionEnum
    {
        public static GenericEnum CREATE = new GenericEnum { Id = 1, Code = "CREATE", Name = "Tạo mới" };
        public static GenericEnum EDIT = new GenericEnum { Id = 2, Code = "EDIT", Name = "Chỉnh sửa" };
        public static GenericEnum DELETE = new GenericEnum { Id = 3, Code = "DELETE", Name = "Xóa" };
        public static GenericEnum SEND_MAIL = new GenericEnum { Id = 4, Code = "SEND_MAIL", Name = "Gửi Email" };
        public static GenericEnum SEND_SMS = new GenericEnum { Id = 5, Code = "SEND_SMS", Name = "Gửi SMS" };

        public static List<GenericEnum> AuditLogPropertyActionEnumList = new List<GenericEnum>
        {
            CREATE, EDIT, DELETE
        };
    }

}
