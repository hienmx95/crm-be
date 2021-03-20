using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Rpc.omni_config
{
    public class OmniConfigController : RpcController
    {
        private ICurrentContext CurrentContext;
        public OmniConfigController(
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CurrentContext = CurrentContext;
        }
    }
}