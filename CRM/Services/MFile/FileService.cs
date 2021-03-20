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
using RestSharp;
using CRM.Helpers;

namespace CRM.Services.MFile
{
    public interface IFileService : IServiceScoped
    {
        Task<int> Count(FileFilter FileFilter);
        Task<List<Entities.File>> List(FileFilter FileFilter);
        Task<Entities.File> Get(long Id);
        Task<Entities.File> Create(Entities.File File, string path);
    }

    public class FileService : BaseService, IFileService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IFileValidator FileValidator;

        public FileService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IFileValidator FileValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.FileValidator = FileValidator;
        }
        public async Task<int> Count(FileFilter FileFilter)
        {
            try
            {
                int result = await UOW.FileRepository.Count(FileFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(FileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(FileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Entities.File>> List(FileFilter FileFilter)
        {
            try
            {
                List<Entities.File> Files = await UOW.FileRepository.List(FileFilter);
                return Files;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(FileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(FileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Entities.File> Get(long Id)
        {
            Entities.File File = await UOW.FileRepository.Get(Id);
            if (File == null)
                return null;
            return File;
        }

        public async Task<Entities.File> Create(Entities.File File, string path)
        {
            RestClient restClient = new RestClient(InternalServices.UTILS);
            RestRequest restRequest = new RestRequest("/rpc/utils/file/upload");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.Method = Method.POST;
            restRequest.AddCookie("Token", CurrentContext.Token);
            restRequest.AddCookie("X-Language", CurrentContext.Language);
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFile("file", File.Content, File.Name);
            restRequest.AddParameter("path", path);
            try
            {
                var response = restClient.Execute<Entities.File>(restRequest);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    File.Id = response.Data.Id;
                    File.Url = "/rpc/utils/file/download" + response.Data.Path;
                    File.AppUserId = File.AppUserId;
                    File.RowId = response.Data.RowId;
                    await UOW.Begin(); 
                    await UOW.FileRepository.Create(File);
                    File = await UOW.FileRepository.Get(File.Id);
                    await UOW.Commit();
                    return File;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            } 
        }
    }
}
