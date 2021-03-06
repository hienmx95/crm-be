using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class RoutingKeyEnum
    {
        public static GenericEnum AppUserSync = new GenericEnum { Id = 1, Code = "AppUser.Sync", Name = "Đồng bộ AppUser" };
        public static GenericEnum OrganizationSync = new GenericEnum { Id = 2, Code = "Organization.Sync", Name = "Đồng bộ Organization" };
        public static GenericEnum StorenSync = new GenericEnum { Id = 3, Code = "Store.Sync", Name = "Đồng bộ Store" };
        public static GenericEnum MailSend = new GenericEnum { Id = 4, Code = "Mail.Send", Name = "Gửi Mail" };
        public static GenericEnum AuditLogSend = new GenericEnum { Id = 5, Code = "AuditLog.Send", Name = "Audit Log" };
        public static GenericEnum SystemLogSend = new GenericEnum { Id = 6, Code = "SystemLog.Send", Name = "System Log" };
        public static GenericEnum UserNotificationSend = new GenericEnum { Id = 7, Code = "UserNotification.Send", Name = "UserNotification" };

        public static List<GenericEnum> RoutingKeyEnumList = new List<GenericEnum>()
        {
            AppUserSync, OrganizationSync, StorenSync, MailSend, AuditLogSend, SystemLogSend, UserNotificationSend
        };

        public static GenericEnum AppUserUsed = new GenericEnum { Id = 1001, Code = "AppUser.Used", Name = "AppUser Used" };
        public static GenericEnum OrganizationUsed = new GenericEnum { Id = 1002, Code = "Organization.Used", Name = "Organization Used" };

        public static GenericEnum AlbumUsed = new GenericEnum { Id = 101, Code = "Album.Used", Name = "Album Used" };
        public static GenericEnum BrandUsed = new GenericEnum { Id = 102, Code = "Brand.Used", Name = "Brand Used" };
        public static GenericEnum ItemUsed = new GenericEnum { Id = 103, Code = "Item.Used", Name = "Item Used" };
        public static GenericEnum ProductUsed = new GenericEnum { Id = 104, Code = "Product.Used", Name = "Product Used" };
        public static GenericEnum ProductTypeUsed = new GenericEnum { Id = 105, Code = "ProductType.Used", Name = "ProductType Used" };
        public static GenericEnum StoreUsed = new GenericEnum { Id = 106, Code = "Store.Used", Name = "Store Used" };
        public static GenericEnum StoreTypeUsed = new GenericEnum { Id = 107, Code = "StoreType.Used", Name = "StoreType Used" };
        public static GenericEnum SupplierUsed = new GenericEnum { Id = 108, Code = "Supplier.Used", Name = "Supplier Used" };
        public static GenericEnum SurveyUsed = new GenericEnum { Id = 109, Code = "Survey.Used", Name = "Survey Used" };
        public static GenericEnum TaxTypeUsed = new GenericEnum { Id = 110, Code = "TaxType.Used", Name = "TaxType Used" };
        public static GenericEnum UnitOfMeasureUsed = new GenericEnum { Id = 111, Code = "UnitOfMeasure.Used", Name = "UnitOfMeasure Used" };
        public static GenericEnum CustomerExportUsed = new GenericEnum { Id = 112, Code = "CustomerExport.Used", Name = "CustomerExport Used" };
        public static GenericEnum CustomerProjectUsed = new GenericEnum { Id = 113, Code = "CustomerProject.Used", Name = "CustomerProject Used" };
        public static GenericEnum CustomerRetailUsed = new GenericEnum { Id = 114, Code = "CustomerRetail.Used", Name = "CustomerRetail Used" };
        public static GenericEnum CustomerAgentUsed = new GenericEnum { Id = 115, Code = "CustomerAgent.Used", Name = "CustomerAgent Used" };
        public static GenericEnum CustomerUsed = new GenericEnum { Id = 116, Code = "Customer.Used", Name = "Customer Used" };
    }
}
