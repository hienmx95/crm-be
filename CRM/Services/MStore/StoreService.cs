using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;

namespace CRM.Services.MStore
{
    public interface IStoreService : IServiceScoped
    {
        Task<int> Count(StoreFilter StoreFilter);
        Task<List<Store>> List(StoreFilter StoreFilter);
        Task<Store> Get(long Id);
        Task<List<Store>> AddToCustomer(List<Store> Stores);
        Task<List<Store>> BulkRemove(List<Store> Stores);
        Task<Store> RemoveFromCustomer(Store Store);
        StoreFilter ToFilter(StoreFilter StoreFilter);
    }

    public class StoreService : BaseService, IStoreService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IStoreValidator StoreValidator;

        public StoreService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IStoreValidator StoreValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.StoreValidator = StoreValidator;
        }
        public async Task<int> Count(StoreFilter StoreFilter)
        {
            try
            {
                int result = await UOW.StoreRepository.Count(StoreFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Store>> List(StoreFilter StoreFilter)
        {
            try
            {
                List<Store> Stores = await UOW.StoreRepository.List(StoreFilter);
                return Stores;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Store> Get(long Id)
        {
            Store Store = await UOW.StoreRepository.Get(Id);
            if (Store == null)
                return null;
            return Store;
        }

        public async Task<List<Store>> AddToCustomer(List<Store> Stores)
        {
            if (!await StoreValidator.AddToCustomer(Stores))
                return Stores;

            try
            {
                await UOW.Begin();
                await UOW.StoreRepository.BulkMerge(Stores);
                await UOW.Commit();

                await Logging.CreateAuditLog(Stores, new { }, nameof(StoreService));
                return Stores;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Store> RemoveFromCustomer(Store Store)
        {
            try
            {
                var oldData = await UOW.StoreRepository.Get(Store.Id);
                Store.CustomerId = null;
                await UOW.Begin();
                await UOW.StoreRepository.BulkMerge(new List<Store> { Store });
                await UOW.Commit();

                Store = await UOW.StoreRepository.Get(Store.Id);
                await Logging.CreateAuditLog(Store, oldData, nameof(StoreService));
                return Store;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Store>> BulkRemove(List<Store> Stores)
        {
            try
            {
                Stores.ForEach(x => x.CustomerId = null); 
                await UOW.Begin();
                await UOW.StoreRepository.BulkMerge(Stores);
                await UOW.Commit();

                await Logging.CreateAuditLog(Stores, new { }, nameof(StoreService));
                return Stores;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public StoreFilter ToFilter(StoreFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<StoreFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                StoreFilter subFilter = new StoreFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.Code))






                        subFilter.Code = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.CodeDraft))






                        subFilter.CodeDraft = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))






                        subFilter.Name = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnsignName))






                        subFilter.UnsignName = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.ParentStoreId))
                        subFilter.ParentStoreId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                        subFilter.OrganizationId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.StoreTypeId))
                        subFilter.StoreTypeId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.StoreGroupingId))
                        subFilter.StoreGroupingId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.Telephone))






                        subFilter.Telephone = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.ProvinceId))
                        subFilter.ProvinceId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.DistrictId))
                        subFilter.DistrictId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.WardId))
                        subFilter.WardId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.Address))






                        subFilter.Address = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnsignAddress))






                        subFilter.UnsignAddress = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryAddress))






                        subFilter.DeliveryAddress = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.Latitude))


                        subFilter.Latitude = FilterPermissionDefinition.DecimalFilter;





                    if (FilterPermissionDefinition.Name == nameof(subFilter.Longitude))


                        subFilter.Longitude = FilterPermissionDefinition.DecimalFilter;





                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryLatitude))


                        subFilter.DeliveryLatitude = FilterPermissionDefinition.DecimalFilter;





                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryLongitude))


                        subFilter.DeliveryLongitude = FilterPermissionDefinition.DecimalFilter;





                    if (FilterPermissionDefinition.Name == nameof(subFilter.OwnerName))






                        subFilter.OwnerName = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.OwnerPhone))






                        subFilter.OwnerPhone = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.OwnerEmail))






                        subFilter.OwnerEmail = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.TaxCode))






                        subFilter.TaxCode = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.LegalEntity))






                        subFilter.LegalEntity = FilterPermissionDefinition.StringFilter;

                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter; if (FilterPermissionDefinition.Name == nameof(subFilter.StoreStatusId))
                        subFilter.StoreStatusId = FilterPermissionDefinition.IdFilter;
                }
            }
            return filter;
        }
    }
}
