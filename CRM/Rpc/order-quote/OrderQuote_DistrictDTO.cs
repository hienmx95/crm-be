using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? Priority { get; set; }
        
        public long ProvinceId { get; set; }
        
        public long StatusId { get; set; }
        

        public OrderQuote_DistrictDTO() {}
        public OrderQuote_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            
            this.Code = District.Code;
            
            this.Name = District.Name;
            
            this.Priority = District.Priority;
            
            this.ProvinceId = District.ProvinceId;
            
            this.StatusId = District.StatusId;
            
            this.Errors = District.Errors;
        }
    }

    public class OrderQuote_DistrictFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter Priority { get; set; }
        
        public IdFilter ProvinceId { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public DistrictOrder OrderBy { get; set; }
    }
}