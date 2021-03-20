using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_ImproveQualityServingDTO : DataDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long? StoreId { get; set; }

        public Store_ImproveQualityServingDTO()
        {

        }
        public Store_ImproveQualityServingDTO(ImproveQualityServing ImproveQualityServing)
        {
            this.Id = ImproveQualityServing.Id;
            this.Name = ImproveQualityServing.Name;
            this.Detail = ImproveQualityServing.Detail;
            this.StoreId = ImproveQualityServing.StoreId;
        }
    }

}
