using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contact
{

    public class Contact_OrderQuoteStatusDTO : DataDTO
    {

        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


        public Contact_OrderQuoteStatusDTO() { }
        public Contact_OrderQuoteStatusDTO(OrderQuoteStatus OrderQuoteStatus)
        {

            this.Id = OrderQuoteStatus.Id;

            this.Code = OrderQuoteStatus.Code;

            this.Name = OrderQuoteStatus.Name;

            this.Errors = OrderQuoteStatus.Errors;
        }
    }

    public class Contact_OrderQuoteStatusFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public OrderQuoteStatusOrder OrderBy { get; set; }
    }
}
