using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Entities
{
    public class Mapping : DataEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
