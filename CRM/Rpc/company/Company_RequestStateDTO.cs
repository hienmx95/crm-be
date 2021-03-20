using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_RequestStateDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Company_RequestStateDTO() { }
        public Company_RequestStateDTO(RequestState RequestState)
        {
            this.Id = RequestState.Id;
            this.Code = RequestState.Code;
            this.Name = RequestState.Name;
            this.Errors = RequestState.Errors;
        }
    }

    public class Company_RequestStateFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public RequestStateOrder OrderBy { get; set; }
    }
}
