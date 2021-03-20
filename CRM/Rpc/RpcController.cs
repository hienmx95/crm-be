using CRM.Common;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Services.MAppUser;
using CRM.Services.MOrganization;
using CRM.Services.MStoreGrouping;
using CRM.Services.MStoreType;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Entities;
using CRM.Enums;
using CRM.Services.MStore;
using System;
using CRM.Helpers;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using CRM.Common.PermissionResult;
using Microsoft.Extensions.Configuration;

namespace CRM.Rpc
{
    public class RpcController : ControllerBase
    {
        private DataContext DataContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public bool UnAuthorization { get; set; }
        protected RpcController(IHttpContextAccessor httpContextAccessor, DataContext DataContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.DataContext = DataContext;
            CheckPermission();
        }
        public void CheckPermission()
        {
            UnAuthorization = false;
            var HttpContext = httpContextAccessor.HttpContext;
            #region check permission
            string page = HttpContext.Request.Path.Value.ToLower().Substring(1);
            string authorization = FworkEnum.PrefixCookie + HttpContext.Request.Cookies["fwork-token"];

            var ResourceActionResponse = DataContext.F1_ResourceActionPageMapping
                .Where(p => p.PageCode == page).Select(p => new ResourceActionPageResponse
                {
                    ResourceCode = p.ResourceCode,
                    ActionCode = p.ActionCode,
                }).FirstOrDefault();
            if (ResourceActionResponse == null
                || subscribeForPushAPICheckPermission(ResourceActionResponse.ResourceCode, ResourceActionResponse.ActionCode, authorization) == false)
            {
                UnAuthorization = true;
            }
            #endregion
        }
        protected bool subscribeForPushAPICheckPermission(string resource, string action, string authorization)
        {
            string URI = FworkEnum.URI_CHECK_PERMISION;
            string myParameters = "resource=" + resource + "&action=" + action;

            WebClient client = new WebClient();
            client.Headers.Add("authorization", authorization);
            Stream data = client.OpenRead(URI + "?" + myParameters);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            PermissionResult permissionResult = JsonConvert.DeserializeObject<PermissionResult>(s);
            return permissionResult.data.access;
        }



        protected async Task<List<long>> FilterOrganization(IOrganizationService OrganizationService, ICurrentContext CurrentContext)
        {
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return new List<long>();
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });

            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "OrganizationId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            if (In != null)
            {
                List<string> InPaths = Organizations.Where(o => In.Count == 0 || In.Contains(o.Id)).Select(o => o.Path).ToList();
                Organizations = Organizations.Where(o => InPaths.Any(p => o.Path.StartsWith(p))).ToList();
            }
            if (NotIn != null)
            {
                List<string> NotInPaths = Organizations.Where(o => NotIn.Count == 0 || NotIn.Contains(o.Id)).Select(o => o.Path).ToList();
                Organizations = Organizations.Where(o => !NotInPaths.Any(p => o.Path.StartsWith(p))).ToList();
            }

            List<long> organizationIds = Organizations.Select(o => o.Id).ToList();

            return organizationIds;
        }
        protected async Task<List<long>> FilterAppUser(IAppUserService AppUserService, IOrganizationService OrganizationService, ICurrentContext CurrentContext)
        {
            List<long> organizationIds = await FilterOrganization(OrganizationService, CurrentContext);

            List<long> In = null;
            List<long> NotIn = null;
            IdFilter CurrentUserId = new IdFilter();
            //foreach (var currentFilter in CurrentContext.Filters)
            //{
            //    List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
            //    foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
            //    {
            //        if (FilterPermissionDefinition.Name == "AppUserId")
            //        {
            //            if (FilterPermissionDefinition.IdFilter.Equal != null)
            //            {
            //                if (In == null) In = new List<long>();
            //                In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
            //            }
            //            if (FilterPermissionDefinition.IdFilter.In != null)
            //            {
            //                if (In == null) In = new List<long>();
            //                In.AddRange(FilterPermissionDefinition.IdFilter.In);
            //            }

            //            if (FilterPermissionDefinition.IdFilter.NotEqual != null)
            //            {
            //                if (NotIn == null) NotIn = new List<long>();
            //                NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
            //            }
            //            if (FilterPermissionDefinition.IdFilter.NotIn != null)
            //            {
            //                if (NotIn == null) NotIn = new List<long>();
            //                NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
            //            }
            //        }
            //        if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
            //        {
            //            if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
            //            {
            //                CurrentUserId.Equal = CurrentContext.UserId;
            //            }
            //            if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
            //            {
            //                CurrentUserId.NotEqual = CurrentContext.UserId;
            //            }
            //        }
            //    }
            //}


            AppUserFilter AppUserFilter = new AppUserFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn, Equal = CurrentUserId.Equal, NotEqual = CurrentUserId.NotEqual },
                OrganizationId = new IdFilter { In = organizationIds },
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.Id,
                OrderBy = AppUserOrder.DisplayName,
                OrderType = OrderType.ASC,
            };

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<long> AppUserIds = AppUsers.Select(a => a.Id).ToList();

            return AppUserIds;
        }

        protected async Task<List<long>> FilterStore(IStoreService StoreService, IOrganizationService OrganizationService, ICurrentContext CurrentContext)
        {
            List<long> organizationIds = await FilterOrganization(OrganizationService, CurrentContext);

            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "StoreId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            List<Store> Stores = await StoreService.List(new StoreFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn },
                OrganizationId = new IdFilter { In = organizationIds },
                Skip = 0,
                Take = int.MaxValue,
                Selects = StoreSelect.Id,
            });
            List<long> StoreIds = Stores.Select(a => a.Id).ToList();

            return StoreIds;
        }

        protected async Task<List<long>> FilterStoreGrouping(IStoreGroupingService StoreGroupingService, ICurrentContext CurrentContext)
        {
            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "StoreGroupingId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            List<StoreGrouping> StoreGroupings = await StoreGroupingService.List(new StoreGroupingFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn },
                Skip = 0,
                Take = int.MaxValue,
                Selects = StoreGroupingSelect.Id,
            });
            List<long> StoreGroupingIds = StoreGroupings.Select(a => a.Id).ToList();

            return StoreGroupingIds;
        }

        protected async Task<List<long>> FilterStoreType(IStoreTypeService StoreTypeService, ICurrentContext CurrentContext)
        {
            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "StoreTypeId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            List<StoreType> StoreTypes = await StoreTypeService.List(new StoreTypeFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn },
                Skip = 0,
                Take = int.MaxValue,
                Selects = StoreTypeSelect.Id,
            });
            List<long> StoreTypeIds = StoreTypes.Select(a => a.Id).ToList();

            return StoreTypeIds;
        }

        protected async Task<List<long>> FilterProductGrouping(IProductGroupingService ProductGroupingService, ICurrentContext CurrentContext)
        {
            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "ProductGroupingId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            List<ProductGrouping> ProductGroupings = await ProductGroupingService.List(new ProductGroupingFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn },
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProductGroupingSelect.Id,
            });
            List<long> ProductGroupingIds = ProductGroupings.Select(a => a.Id).ToList();

            return ProductGroupingIds;
        }

        protected async Task<List<long>> FilterProductType(IProductTypeService ProductTypeService, ICurrentContext CurrentContext)
        {
            List<long> In = null;
            List<long> NotIn = null;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == "ProductTypeId")
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal != null)
                        {
                            if (In == null) In = new List<long>();
                            In.Add(FilterPermissionDefinition.IdFilter.Equal.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.In != null)
                        {
                            if (In == null) In = new List<long>();
                            In.AddRange(FilterPermissionDefinition.IdFilter.In);
                        }

                        if (FilterPermissionDefinition.IdFilter.NotEqual != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.Add(FilterPermissionDefinition.IdFilter.NotEqual.Value);
                        }
                        if (FilterPermissionDefinition.IdFilter.NotIn != null)
                        {
                            if (NotIn == null) NotIn = new List<long>();
                            NotIn.AddRange(FilterPermissionDefinition.IdFilter.NotIn);
                        }
                    }
                }
            }

            List<ProductType> ProductTypes = await ProductTypeService.List(new ProductTypeFilter
            {
                Id = new IdFilter { In = In, NotIn = NotIn },
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProductTypeSelect.Id,
            });
            List<long> ProductTypeIds = ProductTypes.Select(a => a.Id).ToList();

            return ProductTypeIds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="AppUserService"></param>
        /// <param name="OrganizationService"></param>
        /// <param name="CurrentContext"></param>
        /// <param name="DataContext"></param>
        /// <returns> AppUserIds, OrganizationIds </returns>
        protected async Task<(List<long>, List<long>)> FilterOrganizationAndUser(IdFilter OrganizationId, IAppUserService AppUserService, IOrganizationService OrganizationService, ICurrentContext CurrentContext, DataContext DataContext)
        {
            List<long> OrganizationIds = await FilterOrganization(OrganizationService, CurrentContext);
            List<OrganizationDAO> OrganizationDAOs = await DataContext.Organization.Where(o => o.DeletedAt == null && (OrganizationIds.Count == 0 || OrganizationIds.Contains(o.Id))).ToListAsync();
            OrganizationDAO OrganizationDAO = null;
            if (OrganizationId?.Equal != null)
            {
                OrganizationDAO = await DataContext.Organization.Where(o => o.Id == OrganizationId.Equal.Value).FirstOrDefaultAsync();
                OrganizationDAOs = OrganizationDAOs.Where(o => o.Path.StartsWith(OrganizationDAO.Path)).ToList();
            }
            OrganizationIds = OrganizationDAOs.Select(o => o.Id).ToList();
            List<long> AppUserIds = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);
            return (AppUserIds, OrganizationIds);
        }

        protected DateTime LocalStartDay(ICurrentContext CurrentContext)
        {
            DateTime Start = StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Date.AddHours(0 - CurrentContext.TimeZone);
            return Start;
        }

        protected DateTime LocalEndDay(ICurrentContext CurrentContext)
        {
            DateTime End = StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Date.AddHours(0 - CurrentContext.TimeZone).AddDays(1).AddSeconds(-1);
            return End;
        }
    }

    //[Authorize]
    //[Authorize(Policy = "Simple")]
    public class SimpleController : ControllerBase
    {
    }

    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {
        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PermissionHandler(ICurrentContext CurrentContext, DataContext DataContext, IHttpContextAccessor httpContextAccessor)
        {
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            context.Succeed(requirement);
        }
    }

    public class SimpleRequirement : IAuthorizationRequirement
    {
        public SimpleRequirement()
        {
        }
    }
    public class SimpleHandler : AuthorizationHandler<SimpleRequirement>
    {
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SimpleHandler(ICurrentContext CurrentContext, DataContext DataContext, IHttpContextAccessor httpContextAccessor)
        {
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SimpleRequirement requirement)
        {

        }
    }


}
