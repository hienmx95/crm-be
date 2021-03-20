using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contact
{

    public class Contact_EditedPriceStatusDTO : DataDTO
    {

        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


        public Contact_EditedPriceStatusDTO() { }
        public Contact_EditedPriceStatusDTO(EditedPriceStatus EditedPriceStatus)
        {

            this.Id = EditedPriceStatus.Id;

            this.Code = EditedPriceStatus.Code;

            this.Name = EditedPriceStatus.Name;

            this.Errors = EditedPriceStatus.Errors;
        }
    }

    public class Contact_EditedPriceStatusFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public EditedPriceStatusOrder OrderBy { get; set; }
    }
}
