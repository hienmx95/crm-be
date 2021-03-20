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
using CRM.Enums;
using CRM.Services.MOrganization;
using CRM.Services.MFile;

namespace CRM.Services.MContact
{
    public interface IContactService :  IServiceScoped
    {
        Task<int> Count(ContactFilter ContactFilter);
        Task<List<Contact>> List(ContactFilter ContactFilter);
        Task<Contact> Get(long Id);
        Task<Contact> Create(Contact Contact);
        Task<Contact> Update(Contact Contact);
        Task<Contact> Delete(Contact Contact);
        Task<List<Contact>> BulkDelete(List<Contact> Contacts);
        Task<List<Contact>> Import(List<Contact> Contacts);
        Task<Entities.File> UploadFile(Entities.File File);
        Task<ContactFilter> ToFilter(ContactFilter ContactFilter);
    }

    public class ContactService : BaseService, IContactService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IContactValidator ContactValidator;
        private IFileService FileService;
        private IOrganizationService OrganizationService;

        public ContactService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            IFileService FileService,
            IContactValidator ContactValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ContactValidator = ContactValidator;
            this.FileService = FileService;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(ContactFilter ContactFilter)
        {
            try
            {
                int result = await UOW.ContactRepository.Count(ContactFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Contact>> List(ContactFilter ContactFilter)
        {
            try
            {
                List<Contact> Contacts = await UOW.ContactRepository.List(ContactFilter);
                return Contacts;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Contact> Get(long Id)
        {
            Contact Contact = await UOW.ContactRepository.Get(Id);
            if (Contact == null)
                return null;
            return Contact;
        }
       
        public async Task<Contact> Create(Contact Contact)
        {
            if (!await ContactValidator.Create(Contact))
                return Contact;

            try
            {
                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                Contact.CreatorId = Creator.Id;
                Contact.OrganizationId = Creator.OrganizationId;
                Contact.ContactStatusId = ContactStatusEnum.NEW.Id;
                await UOW.ContactRepository.Create(Contact);
                await UOW.ContactRepository.Update(Contact);
                await UOW.Commit();
                Contact = await UOW.ContactRepository.Get(Contact.Id);
                await Logging.CreateAuditLog(Contact, new { }, nameof(ContactService));
                return Contact;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Contact> Update(Contact Contact)
        {
            if (!await ContactValidator.Update(Contact))
                return Contact;
            try
            {
                var oldData = await UOW.ContactRepository.Get(Contact.Id);

                await UOW.Begin();
                await UOW.ContactRepository.Update(Contact);
                await UOW.Commit();

                Contact = await UOW.ContactRepository.Get(Contact.Id);
                await Logging.CreateAuditLog(Contact, oldData, nameof(ContactService));
                return Contact;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Contact> Delete(Contact Contact)
        {
            if (!await ContactValidator.Delete(Contact))
                return Contact;

            try
            {
                await UOW.Begin();
                await UOW.ContactRepository.Delete(Contact);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Contact, nameof(ContactService));
                return Contact;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Contact>> BulkDelete(List<Contact> Contacts)
        {
            if (!await ContactValidator.BulkDelete(Contacts))
                return Contacts;

            try
            {
                await UOW.Begin();
                await UOW.ContactRepository.BulkDelete(Contacts);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Contacts, nameof(ContactService));
                return Contacts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<Contact>> Import(List<Contact> Contacts)
        {
            if (!await ContactValidator.Import(Contacts))
                return Contacts;
            try
            {
                await UOW.Begin();
                await UOW.ContactRepository.BulkMerge(Contacts);
                await UOW.Commit();

                await Logging.CreateAuditLog(Contacts, new { }, nameof(ContactService));
                return Contacts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ContactService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ContactService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Entities.File> UploadFile(Entities.File File)
        {
            FileInfo fileInfo = new FileInfo(File.Name);
            string path = $"/contact/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            string thumbnailPath = $"/contact/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            File = await FileService.Create(File, path);
            return File;
        }

        public async Task<ContactFilter> ToFilter(ContactFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ContactFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ContactFilter subFilter = new ContactFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        //if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        //{
                        //    if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                        //    subFilter.UserId.Equal = CurrentContext.UserId;
                        //}
                        //if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        //{
                        //    if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                        //    subFilter.UserId.NotEqual = CurrentContext.UserId;
                        //}
                    } 
                }
            }
            return filter;
        }
    }
}
