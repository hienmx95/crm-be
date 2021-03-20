using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class ContractStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới tạo" };
        public static GenericEnum APPROVED = new GenericEnum { Id = 2, Code = "APPROVED", Name = "Đã duyệt" };
        public static GenericEnum DONE = new GenericEnum { Id = 3, Code = "DONE", Name = "Hoàn thành" };

        public static List<GenericEnum> ContractStatusEnumList = new List<GenericEnum>
        {
            NEW,APPROVED,DONE
        };
    }

}
