using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Common.PermissionResult;
using CRM.Common.UserProfile;
using CRM.Models;
using CRM.Services.MRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CRM.Rpc
{
    public class PermissionController : SimpleController
    {
        private IPermissionService PermissionService;
        private ICurrentContext CurrentContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DataContext DataContext;
        public IConfiguration Configuration { get; }
        public PermissionController(IPermissionService PermissionService, ICurrentContext CurrentContext, IHttpContextAccessor httpContextAccessor, DataContext DataContext)
        {
            this.PermissionService = PermissionService;
            this.CurrentContext = CurrentContext;
            this.httpContextAccessor = httpContextAccessor;
            this.DataContext = DataContext;
        }

        [HttpPost, Route("api/v1/crm/permission/list-path")]
        public async Task<List<string>> ListPath()
        {
            var HttpContext = httpContextAccessor.HttpContext;
            string authorization = FworkEnum.PrefixCookie + HttpContext.Request.Cookies["fwork-token"];

            string URI = FworkEnum.URI_AUTHORIZATION;

            WebClient client = new WebClient();
            client.Headers.Add("authorization", authorization);
            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            PermissionOfUser UserProfile = JsonConvert.DeserializeObject<PermissionOfUser>(s);
            // Phân tích ra lấy danh sách Resource và Action
            var resourcePage = new List<ResourceActionPageResponse>();
            resourcePage = getResourcePage(UserProfile);

            var query = DataContext.F1_ResourceActionPageMapping.ToList();
            var path = (from t1 in query
                        join t2 in resourcePage on t1.ResourceCode equals t2.ResourceCode
                        where t1.ActionCode == t2.ActionCode
                        select t1.PageCode).ToList();
            return path;
        }

        [HttpPost, Route("api/v1/crm/permission/list-permission")]
        public async Task<List<ResourceActionPageResponse>> ListPermission()
        {
            var HttpContext = httpContextAccessor.HttpContext;
            string authorization = FworkEnum.PrefixCookie + HttpContext.Request.Cookies["fwork-token"];

            string URI = "https://dev.fpt.work/api/v1/iam/authorization/crm-143a1ca7";

            WebClient client = new WebClient();
            client.Headers.Add("authorization", authorization);
            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            PermissionOfUser UserProfile = JsonConvert.DeserializeObject<PermissionOfUser>(s);
            return getResourcePage(UserProfile); ;
        }

        [HttpPost, Route("api/v1/crm/permission/check-action-in-resource")]
        public async Task<List<string>> CheckActionInResource([FromBody] ResourceActionPageResponse ResourceActionPageResponse)
        {
            var HttpContext = httpContextAccessor.HttpContext;
            string authorization = FworkEnum.PrefixCookie + HttpContext.Request.Cookies["fwork-token"];
            string URI = FworkEnum.URI_AUTHORIZATION;

            WebClient client = new WebClient();
            client.Headers.Add("authorization", authorization);
            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            PermissionOfUser UserProfile = JsonConvert.DeserializeObject<PermissionOfUser>(s);
            // Phân tích ra lấy danh sách Resource và Action
            return getResourcePage(UserProfile)
                    .Where(p => p.ResourceCode == ResourceActionPageResponse.ResourceCode)
                    .Select(p => p.ActionCode).ToList();
        }

        private List<ResourceActionPageResponse> getResourcePage(PermissionOfUser UserProfile)
        {
            // Phân tích ra lấy danh sách Resource và Action 
            var resourcePage = new List<ResourceActionPageResponse>();
            if (UserProfile != null)
            {
                if (UserProfile.success)
                {
                    if (UserProfile.result != null)
                    {
                        if (UserProfile.result.permissions != null || UserProfile.result.permissions.Any())
                        {
                            foreach (var per in UserProfile.result.permissions.Where(p => p.resourceStatus == true))
                            {
                                if (per.action != null || per.action.Any())
                                {
                                    foreach (var action in per.action.Where(p => p.status == true))
                                    {
                                        resourcePage.Add(new ResourceActionPageResponse { ResourceCode = per.resourceCode, ActionCode = action.code });
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return resourcePage;
        }


    }
}
