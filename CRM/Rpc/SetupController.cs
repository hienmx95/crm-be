using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Helpers;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CRM.Rpc
{
    public class SetupController : ControllerBase
    {
        private DataContext DataContext;
        public SetupController(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        //Khởi tạo ràng  buộc giữa các API và các action phục vụ cho authorization
        [HttpGet, Route("api/v1/crm/setup/init-action-page-mapping-enum")]
        public ActionResult InitActionPageMappingEnum()
        {
            List<F1_ResourceActionPageMappingDAO> ResourceActionPageList = ResourceActionPageMappingEnum.ResourceActionPageMappingEnumList.Select(item => new F1_ResourceActionPageMappingDAO
            {
                ResourceCode = item.ResourceCode,
                ActionCode = item.ActionCode,
                PageCode = item.PageCode,
            }).ToList();
            DataContext.F1_ResourceActionPageMapping.BulkSynchronize(ResourceActionPageList);
            return Ok();
        }
        



        [HttpGet, Route("rpc/crm/setup/year/{year}")]
        public bool ChangeYear(int year)
        {
            StaticParams.ChangeYear = year;
            return true;
        }

        [HttpGet, Route("rpc/crm/setup/init")]
        public ActionResult Init()
        {
            InitEnum();
            //InitRoute();
            //InitAdmin();
            return Ok();
        }

        #region sync
        [HttpGet, Route("rpc/crm/setup/init-data")]
        public ActionResult InitData()
        {
            RestClient RestClient = new RestClient(InternalServices.ES);
            InitImage(RestClient);
            InitProvince(RestClient);
            InitDistrict(RestClient);
            InitWard(RestClient);
            InitOrganization(RestClient);
            InitAppUser(RestClient);

            InitProductType(RestClient);
            InitProductGrouping(RestClient);
            InitCategory(RestClient);
            InitSupplier(RestClient);
            InitBrand(RestClient);
            InitUnitOfMeasure(RestClient);
            InitUnitOfMeasureGrouping(RestClient);
            InitTaxType(RestClient);
            InitStoreType(RestClient);
            InitStoreGrouping(RestClient);
            InitStore(RestClient);
            InitProduct(RestClient);
            InitDirectSalesOrder(RestClient);
            return Ok();
        }

        private void InitImage(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/image/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/image/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    ImageFilter ImageFilter = new ImageFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(ImageFilter);
                    IRestResponse<List<Image>> RestResponse = RestClient.Post<List<Image>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<ImageDAO> ImageInDB = DataContext.Image.AsNoTracking().ToList();
                        List<Image> Images = RestResponse.Data;
                        foreach (Image Image in Images)
                        {
                            ImageDAO ImageDAO = ImageInDB.Where(x => x.Id == Image.Id).FirstOrDefault();
                            if (ImageDAO == null)
                            {
                                ImageDAO = new ImageDAO
                                {
                                    Id = Image.Id,
                                };
                                ImageInDB.Add(ImageDAO);
                            }
                            ImageDAO.Url = Image.Url;
                            ImageDAO.Name = Image.Name;
                            ImageDAO.ThumbnailUrl = Image.ThumbnailUrl;
                            ImageDAO.CreatedAt = Image.CreatedAt;
                            ImageDAO.UpdatedAt = Image.UpdatedAt;
                            ImageDAO.DeletedAt = Image.DeletedAt;
                            ImageDAO.RowId = Image.RowId;
                        }
                        DataContext.Image.BulkMerge(ImageInDB);
                    }
                }
            }
        }

        private void InitOrganization(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/organization/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/organization/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    OrganizationFilter OrganizationFilter = new OrganizationFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(OrganizationFilter);
                    IRestResponse<List<Organization>> RestResponse = RestClient.Post<List<Organization>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<OrganizationDAO> OrganizationInDB = DataContext.Organization.AsNoTracking().ToList();
                        List<Organization> Organizations = RestResponse.Data;
                        foreach (Organization Organization in Organizations)
                        {
                            OrganizationDAO OrganizationDAO = OrganizationInDB.Where(x => x.Id == Organization.Id).FirstOrDefault();
                            if (OrganizationDAO == null)
                            {
                                OrganizationDAO = new OrganizationDAO
                                {
                                    Id = Organization.Id,
                                };
                                OrganizationInDB.Add(OrganizationDAO);
                            }
                            OrganizationDAO.Code = Organization.Code;
                            OrganizationDAO.Name = Organization.Name;
                            OrganizationDAO.Path = Organization.Path;
                            OrganizationDAO.Level = Organization.Level;
                            OrganizationDAO.Address = Organization.Address;
                            OrganizationDAO.Email = Organization.Email;
                            OrganizationDAO.ParentId = Organization.ParentId;
                            OrganizationDAO.StatusId = Organization.StatusId;
                            OrganizationDAO.CreatedAt = Organization.CreatedAt;
                            OrganizationDAO.UpdatedAt = Organization.UpdatedAt;
                            OrganizationDAO.DeletedAt = Organization.DeletedAt;
                            OrganizationDAO.RowId = Organization.RowId;
                        }
                        DataContext.Organization.BulkMerge(OrganizationInDB);
                    }
                }
            }
        }

        private void InitAppUser(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/app-user/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/app-user/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    AppUserFilter AppUserFilter = new AppUserFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(AppUserFilter);
                    IRestResponse<List<AppUser>> RestResponse = RestClient.Post<List<AppUser>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<AppUserDAO> AppUserInDB = DataContext.AppUser.AsNoTracking().ToList();
                        List<AppUser> AppUsers = RestResponse.Data;
                        foreach (AppUser AppUser in AppUsers)
                        {
                            AppUserDAO AppUserDAO = AppUserInDB.Where(x => x.Id == AppUser.Id).FirstOrDefault();
                            if (AppUserDAO == null)
                            {
                                AppUserDAO = new AppUserDAO
                                {
                                    Id = AppUser.Id,

                                };
                                AppUserInDB.Add(AppUserDAO);
                            }
                            AppUserDAO.Username = AppUser.Username;
                            AppUserDAO.DisplayName = AppUser.DisplayName;
                            AppUserDAO.Address = AppUser.Address;
                            AppUserDAO.Email = AppUser.Email;
                            AppUserDAO.Phone = AppUser.Phone;
                            AppUserDAO.Department = AppUser.Department;
                            AppUserDAO.OrganizationId = AppUser.OrganizationId;
                            AppUserDAO.SexId = AppUser.SexId;
                            AppUserDAO.StatusId = AppUser.StatusId;
                            AppUserDAO.CreatedAt = AppUser.CreatedAt;
                            AppUserDAO.UpdatedAt = AppUser.UpdatedAt;
                            AppUserDAO.DeletedAt = AppUser.DeletedAt;
                            AppUserDAO.Avatar = AppUser.Avatar;
                            AppUserDAO.Birthday = AppUser.Birthday;
                            AppUserDAO.RowId = AppUser.RowId;
                        }
                        DataContext.AppUser.BulkMerge(AppUserInDB);
                    }
                }
            }
        }

        private void InitNation(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/nation/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/nation/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    NationFilter NationFilter = new NationFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(NationFilter);
                    IRestResponse<List<Nation>> RestResponse = RestClient.Post<List<Nation>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<NationDAO> NationInDB = DataContext.Nation.AsNoTracking().ToList();
                        List<Nation> Nations = RestResponse.Data;
                        foreach (Nation Nation in Nations)
                        {
                            NationDAO NationDAO = NationInDB.Where(x => x.Id == Nation.Id).FirstOrDefault();
                            if (NationDAO == null)
                            {
                                NationDAO = new NationDAO
                                {
                                    Id = Nation.Id,
                                };
                                NationInDB.Add(NationDAO);
                            }
                            NationDAO.Code = Nation.Code;
                            NationDAO.Name = Nation.Name;
                            NationDAO.StatusId = Nation.StatusId;
                            NationDAO.CreatedAt = Nation.CreatedAt;
                            NationDAO.UpdatedAt = Nation.UpdatedAt;
                            NationDAO.DeletedAt = Nation.DeletedAt;
                            NationDAO.RowId = Nation.RowId;
                            NationDAO.Used = Nation.Used;
                        }
                        DataContext.Nation.BulkMerge(NationInDB);
                    }
                }
            }
        }

        private void InitProvince(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/province/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/province/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    ProvinceFilter ProvinceFilter = new ProvinceFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(ProvinceFilter);
                    IRestResponse<List<Province>> RestResponse = RestClient.Post<List<Province>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<ProvinceDAO> ProvinceInDB = DataContext.Province.AsNoTracking().ToList();
                        List<Province> Provinces = RestResponse.Data;
                        foreach (Province Province in Provinces)
                        {
                            ProvinceDAO ProvinceDAO = ProvinceInDB.Where(x => x.Id == Province.Id).FirstOrDefault();
                            if (ProvinceDAO == null)
                            {
                                ProvinceDAO = new ProvinceDAO
                                {
                                    Id = Province.Id,
                                };
                                ProvinceInDB.Add(ProvinceDAO);
                            }
                            ProvinceDAO.Code = Province.Code;
                            ProvinceDAO.Name = Province.Name;
                            ProvinceDAO.StatusId = Province.StatusId;
                            ProvinceDAO.CreatedAt = Province.CreatedAt;
                            ProvinceDAO.UpdatedAt = Province.UpdatedAt;
                            ProvinceDAO.DeletedAt = Province.DeletedAt;
                            ProvinceDAO.RowId = Province.RowId;
                        }
                        DataContext.Province.BulkMerge(ProvinceInDB);
                    }
                }
            }
        }

        private void InitDistrict(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/district/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/district/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    DistrictFilter DistrictFilter = new DistrictFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(DistrictFilter);
                    IRestResponse<List<District>> RestResponse = RestClient.Post<List<District>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<DistrictDAO> DistrictInDB = DataContext.District.AsNoTracking().ToList();
                        List<District> Districts = RestResponse.Data;
                        foreach (District District in Districts)
                        {
                            DistrictDAO DistrictDAO = DistrictInDB.Where(x => x.Id == District.Id).FirstOrDefault();
                            if (DistrictDAO == null)
                            {
                                DistrictDAO = new DistrictDAO
                                {
                                    Id = District.Id,
                                };
                                DistrictInDB.Add(DistrictDAO);
                            }
                            DistrictDAO.Code = District.Code;
                            DistrictDAO.Name = District.Name;
                            DistrictDAO.ProvinceId = District.ProvinceId;
                            DistrictDAO.StatusId = District.StatusId;
                            DistrictDAO.CreatedAt = District.CreatedAt;
                            DistrictDAO.UpdatedAt = District.UpdatedAt;
                            DistrictDAO.DeletedAt = District.DeletedAt;
                            DistrictDAO.RowId = District.RowId;
                        }
                        DataContext.District.BulkMerge(DistrictInDB);
                    }
                }
            }
        }

        private void InitWard(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/ward/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/ward/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    WardFilter WardFilter = new WardFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(WardFilter);
                    IRestResponse<List<Ward>> RestResponse = RestClient.Post<List<Ward>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<WardDAO> WardInDB = DataContext.Ward.AsNoTracking().ToList();
                        List<Ward> Wards = RestResponse.Data;
                        foreach (Ward Ward in Wards)
                        {
                            WardDAO WardDAO = WardInDB.Where(x => x.Id == Ward.Id).FirstOrDefault();
                            if (WardDAO == null)
                            {
                                WardDAO = new WardDAO
                                {
                                    Id = Ward.Id,
                                };
                                WardInDB.Add(WardDAO);
                            }
                            WardDAO.Code = Ward.Code;
                            WardDAO.Name = Ward.Name;
                            WardDAO.DistrictId = Ward.DistrictId;
                            WardDAO.StatusId = Ward.StatusId;
                            WardDAO.CreatedAt = Ward.CreatedAt;
                            WardDAO.UpdatedAt = Ward.UpdatedAt;
                            WardDAO.DeletedAt = Ward.DeletedAt;
                            WardDAO.RowId = Ward.RowId;
                        }
                        DataContext.Ward.BulkMerge(WardInDB);
                    }
                }
            }
        }

        private void InitProductType(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/product-type/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/product-type/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    ProductTypeFilter ProductTypeFilter = new ProductTypeFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(ProductTypeFilter);
                    IRestResponse<List<ProductType>> RestResponse = RestClient.Post<List<ProductType>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<ProductTypeDAO> ProductTypeInDB = DataContext.ProductType.AsNoTracking().ToList();
                        List<ProductType> ProductTypes = RestResponse.Data;
                        foreach (ProductType ProductType in ProductTypes)
                        {
                            ProductTypeDAO ProductTypeDAO = ProductTypeInDB.Where(x => x.Id == ProductType.Id).FirstOrDefault();
                            if (ProductTypeDAO == null)
                            {
                                ProductTypeDAO = new ProductTypeDAO
                                {
                                    Id = ProductType.Id,
                                };
                                ProductTypeInDB.Add(ProductTypeDAO);
                            }
                            ProductTypeDAO.Code = ProductType.Code;
                            ProductTypeDAO.Name = ProductType.Name;
                            ProductTypeDAO.Description = ProductType.Description;
                            ProductTypeDAO.StatusId = ProductType.StatusId;
                            ProductTypeDAO.CreatedAt = ProductType.CreatedAt;
                            ProductTypeDAO.UpdatedAt = ProductType.UpdatedAt;
                            ProductTypeDAO.DeletedAt = ProductType.DeletedAt;
                            ProductTypeDAO.Used = ProductType.Used;
                            ProductTypeDAO.RowId = ProductType.RowId;
                        }
                        DataContext.ProductType.BulkMerge(ProductTypeInDB);
                    }
                }
            }
        }

        private void InitProductGrouping(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/product-grouping/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/product-grouping/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    ProductGroupingFilter ProductGroupingFilter = new ProductGroupingFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(ProductGroupingFilter);
                    IRestResponse<List<ProductGrouping>> RestResponse = RestClient.Post<List<ProductGrouping>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<ProductGroupingDAO> ProductGroupingInDB = DataContext.ProductGrouping.AsNoTracking().ToList();
                        List<ProductGrouping> ProductGroupings = RestResponse.Data;
                        foreach (ProductGrouping ProductGrouping in ProductGroupings)
                        {
                            ProductGroupingDAO ProductGroupingDAO = ProductGroupingInDB.Where(x => x.Id == ProductGrouping.Id).FirstOrDefault();
                            if (ProductGroupingDAO == null)
                            {
                                ProductGroupingDAO = new ProductGroupingDAO
                                {
                                    Id = ProductGrouping.Id,
                                };
                                ProductGroupingInDB.Add(ProductGroupingDAO);
                            }
                            ProductGroupingDAO.Code = ProductGrouping.Code;
                            ProductGroupingDAO.Name = ProductGrouping.Name;
                            ProductGroupingDAO.Description = ProductGrouping.Description;
                            ProductGroupingDAO.Level = ProductGrouping.Level;
                            ProductGroupingDAO.ParentId = ProductGrouping.ParentId;
                            ProductGroupingDAO.Path = ProductGrouping.Path;
                            ProductGroupingDAO.CreatedAt = ProductGrouping.CreatedAt;
                            ProductGroupingDAO.UpdatedAt = ProductGrouping.UpdatedAt;
                            ProductGroupingDAO.DeletedAt = ProductGrouping.DeletedAt;
                            ProductGroupingDAO.RowId = ProductGrouping.RowId;
                        }
                        DataContext.ProductGrouping.BulkMerge(ProductGroupingInDB);
                    }
                }
            }
        }

        private void InitCategory(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/category/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/category/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    CategoryFilter CategoryFilter = new CategoryFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(CategoryFilter);
                    IRestResponse<List<Category>> RestResponse = RestClient.Post<List<Category>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<CategoryDAO> CategoryInDB = DataContext.Category.AsNoTracking().ToList();
                        List<Category> Categorys = RestResponse.Data;
                        foreach (Category Category in Categorys)
                        {
                            CategoryDAO CategoryDAO = CategoryInDB.Where(x => x.Id == Category.Id).FirstOrDefault();
                            if (CategoryDAO == null)
                            {
                                CategoryDAO = new CategoryDAO
                                {
                                    Id = Category.Id,
                                };
                                CategoryInDB.Add(CategoryDAO);
                            }
                            CategoryDAO.Code = Category.Code;
                            CategoryDAO.Name = Category.Name;
                            CategoryDAO.StatusId = Category.StatusId;
                            CategoryDAO.Level = Category.Level;
                            CategoryDAO.ParentId = Category.ParentId;
                            CategoryDAO.ImageId = Category.ImageId;
                            CategoryDAO.Path = Category.Path;
                            CategoryDAO.CreatedAt = Category.CreatedAt;
                            CategoryDAO.UpdatedAt = Category.UpdatedAt;
                            CategoryDAO.DeletedAt = Category.DeletedAt;
                            CategoryDAO.RowId = Category.RowId;
                            CategoryDAO.Used = Category.Used;
                        }
                        DataContext.Category.BulkMerge(CategoryInDB);
                    }
                }
            }
        }

        private void InitSupplier(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/supplier/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/supplier/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    SupplierFilter SupplierFilter = new SupplierFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(SupplierFilter);
                    IRestResponse<List<Supplier>> RestResponse = RestClient.Post<List<Supplier>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<SupplierDAO> SupplierInDB = DataContext.Supplier.AsNoTracking().ToList();
                        List<Supplier> Suppliers = RestResponse.Data;
                        foreach (Supplier Supplier in Suppliers)
                        {
                            SupplierDAO SupplierDAO = SupplierInDB.Where(x => x.Id == Supplier.Id).FirstOrDefault();
                            if (SupplierDAO == null)
                            {
                                SupplierDAO = new SupplierDAO
                                {
                                    Id = Supplier.Id,
                                };
                                SupplierInDB.Add(SupplierDAO);
                            }
                            SupplierDAO.Code = Supplier.Code;
                            SupplierDAO.Name = Supplier.Name;
                            SupplierDAO.Description = Supplier.Description;
                            SupplierDAO.TaxCode = Supplier.TaxCode;
                            SupplierDAO.Phone = Supplier.Phone;
                            SupplierDAO.Email = Supplier.Email;
                            SupplierDAO.Address = Supplier.Address;
                            SupplierDAO.NationId = Supplier.NationId;
                            SupplierDAO.ProvinceId = Supplier.ProvinceId;
                            SupplierDAO.DistrictId = Supplier.DistrictId;
                            SupplierDAO.WardId = Supplier.WardId;
                            SupplierDAO.OwnerName = Supplier.OwnerName;
                            SupplierDAO.PersonInChargeId = Supplier.PersonInChargeId;
                            SupplierDAO.StatusId = Supplier.StatusId;
                            SupplierDAO.CreatedAt = Supplier.CreatedAt;
                            SupplierDAO.UpdatedAt = Supplier.UpdatedAt;
                            SupplierDAO.DeletedAt = Supplier.DeletedAt;
                            SupplierDAO.Used = Supplier.Used;
                            SupplierDAO.RowId = Supplier.RowId;
                        }
                        DataContext.Supplier.BulkMerge(SupplierInDB);
                    }
                }
            }
        }

        private void InitBrand(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/brand/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/brand/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    BrandFilter BrandFilter = new BrandFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(BrandFilter);
                    IRestResponse<List<Brand>> RestResponse = RestClient.Post<List<Brand>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<BrandDAO> BrandInDB = DataContext.Brand.AsNoTracking().ToList();
                        List<Brand> Brands = RestResponse.Data;
                        foreach (Brand Brand in Brands)
                        {
                            BrandDAO BrandDAO = BrandInDB.Where(x => x.Id == Brand.Id).FirstOrDefault();
                            if (BrandDAO == null)
                            {
                                BrandDAO = new BrandDAO
                                {
                                    Id = Brand.Id,
                                };
                                BrandInDB.Add(BrandDAO);
                            }
                            BrandDAO.Code = Brand.Code;
                            BrandDAO.Name = Brand.Name;
                            BrandDAO.Description = Brand.Description;
                            BrandDAO.StatusId = Brand.StatusId;
                            BrandDAO.CreatedAt = Brand.CreatedAt;
                            BrandDAO.UpdatedAt = Brand.UpdatedAt;
                            BrandDAO.DeletedAt = Brand.DeletedAt;
                            BrandDAO.RowId = Brand.RowId;
                            BrandDAO.Used = Brand.Used;
                        }
                        DataContext.Brand.BulkMerge(BrandInDB);
                    }
                }
            }
        }

        private void InitUnitOfMeasure(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/unit-of-measure/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/unit-of-measure/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    UnitOfMeasureFilter UnitOfMeasureFilter = new UnitOfMeasureFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(UnitOfMeasureFilter);
                    IRestResponse<List<UnitOfMeasure>> RestResponse = RestClient.Post<List<UnitOfMeasure>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<UnitOfMeasureDAO> UnitOfMeasureInDB = DataContext.UnitOfMeasure.AsNoTracking().ToList();
                        List<UnitOfMeasure> UnitOfMeasures = RestResponse.Data;
                        foreach (UnitOfMeasure UnitOfMeasure in UnitOfMeasures)
                        {
                            UnitOfMeasureDAO UnitOfMeasureDAO = UnitOfMeasureInDB.Where(x => x.Id == UnitOfMeasure.Id).FirstOrDefault();
                            if (UnitOfMeasureDAO == null)
                            {
                                UnitOfMeasureDAO = new UnitOfMeasureDAO
                                {
                                    Id = UnitOfMeasure.Id,
                                };
                                UnitOfMeasureInDB.Add(UnitOfMeasureDAO);
                            }
                            UnitOfMeasureDAO.Code = UnitOfMeasure.Code;
                            UnitOfMeasureDAO.Name = UnitOfMeasure.Name;
                            UnitOfMeasureDAO.Description = UnitOfMeasure.Description;
                            UnitOfMeasureDAO.StatusId = UnitOfMeasure.StatusId;
                            UnitOfMeasureDAO.CreatedAt = UnitOfMeasure.CreatedAt;
                            UnitOfMeasureDAO.UpdatedAt = UnitOfMeasure.UpdatedAt;
                            UnitOfMeasureDAO.DeletedAt = UnitOfMeasure.DeletedAt;
                            UnitOfMeasureDAO.RowId = UnitOfMeasure.RowId;
                        }
                        DataContext.UnitOfMeasure.BulkMerge(UnitOfMeasureInDB);
                    }
                }
            }
        }

        private void InitUnitOfMeasureGrouping(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/unit-of-measure-grouping/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/unit-of-measure-grouping/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter = new UnitOfMeasureGroupingFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(UnitOfMeasureGroupingFilter);
                    IRestResponse<List<UnitOfMeasureGrouping>> RestResponse = RestClient.Post<List<UnitOfMeasureGrouping>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<UnitOfMeasureGroupingDAO> UnitOfMeasureGroupingInDB = DataContext.UnitOfMeasureGrouping.AsNoTracking().ToList();
                        List<UnitOfMeasureGrouping> UnitOfMeasureGroupings = RestResponse.Data;
                        foreach (UnitOfMeasureGrouping UnitOfMeasureGrouping in UnitOfMeasureGroupings)
                        {
                            UnitOfMeasureGroupingDAO UnitOfMeasureGroupingDAO = UnitOfMeasureGroupingInDB.Where(x => x.Id == UnitOfMeasureGrouping.Id).FirstOrDefault();
                            if (UnitOfMeasureGroupingDAO == null)
                            {
                                UnitOfMeasureGroupingDAO = new UnitOfMeasureGroupingDAO
                                {
                                    Id = UnitOfMeasureGrouping.Id,
                                };
                                UnitOfMeasureGroupingInDB.Add(UnitOfMeasureGroupingDAO);
                            }
                            UnitOfMeasureGroupingDAO.Code = UnitOfMeasureGrouping.Code;
                            UnitOfMeasureGroupingDAO.Name = UnitOfMeasureGrouping.Name;
                            UnitOfMeasureGroupingDAO.Description = UnitOfMeasureGrouping.Description;
                            UnitOfMeasureGroupingDAO.StatusId = UnitOfMeasureGrouping.StatusId;
                            UnitOfMeasureGroupingDAO.UnitOfMeasureId = UnitOfMeasureGrouping.UnitOfMeasureId;
                            UnitOfMeasureGroupingDAO.CreatedAt = UnitOfMeasureGrouping.CreatedAt;
                            UnitOfMeasureGroupingDAO.UpdatedAt = UnitOfMeasureGrouping.UpdatedAt;
                            UnitOfMeasureGroupingDAO.DeletedAt = UnitOfMeasureGrouping.DeletedAt;
                            UnitOfMeasureGroupingDAO.RowId = UnitOfMeasureGrouping.RowId;
                        }
                        DataContext.UnitOfMeasureGrouping.BulkMerge(UnitOfMeasureGroupingInDB);

                        List<UnitOfMeasureGroupingContentDAO> UnitOfMeasureGroupingContentInDB = DataContext.UnitOfMeasureGroupingContent.AsNoTracking().ToList();
                        List<UnitOfMeasureGroupingContent> UnitOfMeasureGroupingContents = UnitOfMeasureGroupings.Where(x => x.UnitOfMeasureGroupingContents != null).SelectMany(x => x.UnitOfMeasureGroupingContents).ToList();
                        foreach (UnitOfMeasureGroupingContent UnitOfMeasureGroupingContent in UnitOfMeasureGroupingContents)
                        {
                            UnitOfMeasureGroupingContentDAO UnitOfMeasureGroupingContentDAO = UnitOfMeasureGroupingContentInDB.Where(x => x.Id == UnitOfMeasureGroupingContent.Id).FirstOrDefault();
                            if (UnitOfMeasureGroupingContentDAO == null)
                            {
                                UnitOfMeasureGroupingContentDAO = new UnitOfMeasureGroupingContentDAO()
                                {
                                    Id = UnitOfMeasureGroupingContent.Id
                                };
                                UnitOfMeasureGroupingContentInDB.Add(UnitOfMeasureGroupingContentDAO);
                            }
                            UnitOfMeasureGroupingContentDAO.Factor = UnitOfMeasureGroupingContent.Factor;
                            UnitOfMeasureGroupingContentDAO.UnitOfMeasureGroupingId = UnitOfMeasureGroupingContent.UnitOfMeasureGroupingId;
                            UnitOfMeasureGroupingContentDAO.UnitOfMeasureId = UnitOfMeasureGroupingContent.UnitOfMeasureId;
                        }
                        DataContext.UnitOfMeasureGroupingContent.BulkMerge(UnitOfMeasureGroupingContentInDB);
                    }
                }
            }
        }

        private void InitTaxType(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/tax-type/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/tax-type/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    TaxTypeFilter TaxTypeFilter = new TaxTypeFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(TaxTypeFilter);
                    IRestResponse<List<TaxType>> RestResponse = RestClient.Post<List<TaxType>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<TaxTypeDAO> TaxTypeInDB = DataContext.TaxType.AsNoTracking().ToList();
                        List<TaxType> TaxTypes = RestResponse.Data;
                        foreach (TaxType TaxType in TaxTypes)
                        {
                            TaxTypeDAO TaxTypeDAO = TaxTypeInDB.Where(x => x.Id == TaxType.Id).FirstOrDefault();
                            if (TaxTypeDAO == null)
                            {
                                TaxTypeDAO = new TaxTypeDAO
                                {
                                    Id = TaxType.Id,
                                };
                                TaxTypeInDB.Add(TaxTypeDAO);
                            }
                            TaxTypeDAO.Code = TaxType.Code;
                            TaxTypeDAO.Name = TaxType.Name;
                            TaxTypeDAO.Percentage = TaxType.Percentage;
                            TaxTypeDAO.StatusId = TaxType.StatusId;
                            TaxTypeDAO.CreatedAt = TaxType.CreatedAt;
                            TaxTypeDAO.UpdatedAt = TaxType.UpdatedAt;
                            TaxTypeDAO.DeletedAt = TaxType.DeletedAt;
                            TaxTypeDAO.RowId = TaxType.RowId;
                        }
                        DataContext.TaxType.BulkMerge(TaxTypeInDB);
                    }
                }
            }
        }

        private void InitStoreType(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/store-type/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/store-type/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    StoreTypeFilter StoreTypeFilter = new StoreTypeFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(StoreTypeFilter);
                    IRestResponse<List<StoreType>> RestResponse = RestClient.Post<List<StoreType>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<StoreTypeDAO> StoreTypeInDB = DataContext.StoreType.AsNoTracking().ToList();
                        List<StoreType> StoreTypes = RestResponse.Data;
                        foreach (StoreType StoreType in StoreTypes)
                        {
                            StoreTypeDAO StoreTypeDAO = StoreTypeInDB.Where(x => x.Id == StoreType.Id).FirstOrDefault();
                            if (StoreTypeDAO == null)
                            {
                                StoreTypeDAO = new StoreTypeDAO
                                {
                                    Id = StoreType.Id,
                                };
                                StoreTypeInDB.Add(StoreTypeDAO);
                            }
                            StoreTypeDAO.Code = StoreType.Code;
                            StoreTypeDAO.Name = StoreType.Name;
                            StoreTypeDAO.ColorId = StoreType.ColorId;
                            StoreTypeDAO.StatusId = StoreType.StatusId;
                            StoreTypeDAO.CreatedAt = StoreType.CreatedAt;
                            StoreTypeDAO.UpdatedAt = StoreType.UpdatedAt;
                            StoreTypeDAO.DeletedAt = StoreType.DeletedAt;
                            StoreTypeDAO.Used = StoreType.Used;
                            StoreTypeDAO.RowId = StoreType.RowId;
                        }
                        DataContext.StoreType.BulkMerge(StoreTypeInDB);
                    }
                }
            }
        }

        private void InitStoreGrouping(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/store-grouping/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/store-grouping/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    StoreGroupingFilter StoreGroupingFilter = new StoreGroupingFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(StoreGroupingFilter);
                    IRestResponse<List<StoreGrouping>> RestResponse = RestClient.Post<List<StoreGrouping>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<StoreGroupingDAO> StoreGroupingInDB = DataContext.StoreGrouping.AsNoTracking().ToList();
                        List<StoreGrouping> StoreGroupings = RestResponse.Data;
                        foreach (StoreGrouping StoreGrouping in StoreGroupings)
                        {
                            StoreGroupingDAO StoreGroupingDAO = StoreGroupingInDB.Where(x => x.Id == StoreGrouping.Id).FirstOrDefault();
                            if (StoreGroupingDAO == null)
                            {
                                StoreGroupingDAO = new StoreGroupingDAO
                                {
                                    Id = StoreGrouping.Id,
                                };
                                StoreGroupingInDB.Add(StoreGroupingDAO);
                            }
                            StoreGroupingDAO.Code = StoreGrouping.Code;
                            StoreGroupingDAO.Name = StoreGrouping.Name;
                            StoreGroupingDAO.Level = StoreGrouping.Level;
                            StoreGroupingDAO.ParentId = StoreGrouping.ParentId;
                            StoreGroupingDAO.Path = StoreGrouping.Path;
                            StoreGroupingDAO.CreatedAt = StoreGrouping.CreatedAt;
                            StoreGroupingDAO.UpdatedAt = StoreGrouping.UpdatedAt;
                            StoreGroupingDAO.DeletedAt = StoreGrouping.DeletedAt;
                            StoreGroupingDAO.RowId = StoreGrouping.RowId;
                            StoreGroupingDAO.Used = StoreGrouping.Used;
                        }
                        DataContext.StoreGrouping.BulkMerge(StoreGroupingInDB);
                    }
                }
            }
        }

        private void InitStore(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/store/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/store/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    StoreFilter StoreFilter = new StoreFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(StoreFilter);
                    IRestResponse<List<Store>> RestResponse = RestClient.Post<List<Store>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<Store> Stores = RestResponse.Data;
                        List<StoreImageMapping> StoreImageMappings = Stores.Where(x => x.StoreImageMappings != null).SelectMany(x => x.StoreImageMappings).ToList();
                        List<StoreDAO> StoreDAOs = new List<StoreDAO>();
                        List<StoreImageMappingDAO> StoreImageMappingDAOs = new List<StoreImageMappingDAO>();
                        foreach (Store Store in Stores)
                        {
                            StoreDAO StoreDAO = new StoreDAO();
                            StoreDAO.Id = Store.Id;
                            StoreDAO.Code = Store.Code;
                            StoreDAO.CodeDraft = Store.CodeDraft;
                            StoreDAO.Name = Store.Name;
                            StoreDAO.UnsignName = Store.UnsignName;
                            StoreDAO.ParentStoreId = Store.ParentStoreId;
                            StoreDAO.OrganizationId = Store.OrganizationId;
                            StoreDAO.StoreStatusId = Store.StoreStatusId;
                            StoreDAO.StoreTypeId = Store.StoreTypeId;
                            StoreDAO.StoreGroupingId = Store.StoreGroupingId;
                            StoreDAO.Telephone = Store.Telephone;
                            StoreDAO.ProvinceId = Store.ProvinceId;
                            StoreDAO.DistrictId = Store.DistrictId;
                            StoreDAO.WardId = Store.WardId;
                            StoreDAO.Address = Store.Address;
                            StoreDAO.UnsignAddress = Store.UnsignAddress;
                            StoreDAO.DeliveryAddress = Store.DeliveryAddress;
                            StoreDAO.Latitude = Store.Latitude;
                            StoreDAO.Longitude = Store.Longitude;
                            StoreDAO.DeliveryLatitude = Store.DeliveryLatitude;
                            StoreDAO.DeliveryLongitude = Store.DeliveryLongitude;
                            StoreDAO.OwnerName = Store.OwnerName;
                            StoreDAO.OwnerPhone = Store.OwnerPhone;
                            StoreDAO.OwnerEmail = Store.OwnerEmail;
                            StoreDAO.TaxCode = Store.TaxCode;
                            StoreDAO.LegalEntity = Store.LegalEntity;
                            StoreDAO.AppUserId = Store.AppUserId;
                            StoreDAO.StatusId = Store.StatusId;
                            StoreDAO.CreatedAt = Store.CreatedAt;
                            StoreDAO.UpdatedAt = Store.UpdatedAt;
                            StoreDAO.DeletedAt = Store.DeletedAt;
                            StoreDAO.RowId = Store.RowId;
                            StoreDAO.Used = Store.Used;
                            StoreDAOs.Add(StoreDAO);
                        }
                        DataContext.Store.BulkMerge(StoreDAOs);

                        foreach (StoreImageMapping StoreImageMapping in StoreImageMappings)
                        {
                            StoreImageMappingDAO StoreImageMappingDAO = new StoreImageMappingDAO
                            {
                                StoreId = StoreImageMapping.StoreId,
                                ImageId = StoreImageMapping.ImageId
                            };
                            StoreImageMappingDAOs.Add(StoreImageMappingDAO);
                        }
                        DataContext.StoreImageMapping.BulkMerge(StoreImageMappingDAOs);
                    }
                }
            }
        }

        private void InitProduct(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/product/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/product/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    ProductFilter ProductFilter = new ProductFilter
                    {
                        Skip = i * 10000,
                        Take = 10000
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(ProductFilter);
                    IRestResponse<List<Product>> RestResponse = RestClient.Post<List<Product>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<ProductDAO> ProductDAOs = new List<ProductDAO>();
                        List<ProductProductGroupingMappingDAO> ProductProductGroupingMappingDAOs = new List<ProductProductGroupingMappingDAO>();
                        List<ProductImageMappingDAO> ProductImageMappingDAOs = new List<ProductImageMappingDAO>();
                        List<VariationGroupingDAO> VariationGroupingDAOs = new List<VariationGroupingDAO>();
                        List<VariationDAO> VariationDAOs = new List<VariationDAO>();
                        List<ItemDAO> ItemDAOs = new List<ItemDAO>();
                        List<ItemImageMappingDAO> ItemImageMappingDAOs = new List<ItemImageMappingDAO>();

                        List<Product> Products = RestResponse.Data;
                        List<ProductProductGroupingMapping> ProductProductGroupingMappings = Products.Where(x => x.ProductProductGroupingMappings != null).SelectMany(x => x.ProductProductGroupingMappings).ToList();
                        List<ProductImageMapping> ProductImageMappings = Products.Where(x => x.ProductImageMappings != null).SelectMany(x => x.ProductImageMappings).ToList();
                        List<VariationGrouping> VariationGroupings = Products.Where(x => x.VariationGroupings != null).SelectMany(x => x.VariationGroupings).ToList();
                        List<Variation> Variations = VariationGroupings.Where(x => x.Variations != null).SelectMany(x => x.Variations).ToList();
                        List<Item> Items = Products.Where(x => x.Items != null).SelectMany(x => x.Items).ToList();
                        List<ItemImageMapping> ItemImageMappings = Items.Where(x => x.ItemImageMappings != null).SelectMany(x => x.ItemImageMappings).ToList();

                        foreach (Product Product in Products)
                        {
                            ProductDAO ProductDAO = new ProductDAO();
                            ProductDAO.Id = Product.Id;
                            ProductDAO.Code = Product.Code;
                            ProductDAO.Name = Product.Name;
                            ProductDAO.Description = Product.Description;
                            ProductDAO.ScanCode = Product.ScanCode;
                            ProductDAO.ERPCode = Product.ERPCode;
                            ProductDAO.CategoryId = Product.CategoryId;
                            ProductDAO.ProductTypeId = Product.ProductTypeId;
                            ProductDAO.BrandId = Product.BrandId;
                            ProductDAO.UnitOfMeasureId = Product.UnitOfMeasureId;
                            ProductDAO.UnitOfMeasureGroupingId = Product.UnitOfMeasureGroupingId;
                            ProductDAO.SalePrice = Product.SalePrice;
                            ProductDAO.RetailPrice = Product.RetailPrice;
                            ProductDAO.TaxTypeId = Product.TaxTypeId;
                            ProductDAO.StatusId = Product.StatusId;
                            ProductDAO.OtherName = Product.OtherName;
                            ProductDAO.TechnicalName = Product.TechnicalName;
                            ProductDAO.Note = Product.Note;
                            ProductDAO.IsNew = Product.IsNew;
                            ProductDAO.UsedVariationId = Product.UsedVariationId;
                            ProductDAO.CreatedAt = Product.CreatedAt;
                            ProductDAO.UpdatedAt = Product.UpdatedAt;
                            ProductDAO.DeletedAt = Product.DeletedAt;
                            ProductDAO.RowId = Product.RowId;
                            ProductDAO.Used = Product.Used;
                            ProductDAOs.Add(ProductDAO);
                        }
                        DataContext.Product.BulkMerge(ProductDAOs);

                        foreach (ProductProductGroupingMapping ProductProductGroupingMapping in ProductProductGroupingMappings)
                        {
                            ProductProductGroupingMappingDAO ProductProductGroupingMappingDAO = new ProductProductGroupingMappingDAO();
                            ProductProductGroupingMappingDAO.ProductId = ProductProductGroupingMapping.ProductId;
                            ProductProductGroupingMappingDAO.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
                            ProductProductGroupingMappingDAOs.Add(ProductProductGroupingMappingDAO);
                        }
                        DataContext.ProductProductGroupingMapping.BulkMerge(ProductProductGroupingMappingDAOs);

                        foreach (ProductImageMapping ProductImageMapping in ProductImageMappings)
                        {
                            ProductImageMappingDAO ProductImageMappingDAO = new ProductImageMappingDAO
                            {
                                ProductId = ProductImageMapping.ProductId,
                                ImageId = ProductImageMapping.ImageId
                            };
                            ProductImageMappingDAOs.Add(ProductImageMappingDAO);
                        }
                        DataContext.ProductImageMapping.BulkMerge(ProductImageMappingDAOs);

                        foreach (VariationGrouping VariationGrouping in VariationGroupings)
                        {
                            VariationGroupingDAO VariationGroupingDAO = new VariationGroupingDAO();
                            VariationGroupingDAO.Id = VariationGrouping.Id;
                            VariationGroupingDAO.Name = VariationGrouping.Name;
                            VariationGroupingDAO.ProductId = VariationGrouping.ProductId;
                            VariationGroupingDAO.CreatedAt = VariationGrouping.CreatedAt;
                            VariationGroupingDAO.UpdatedAt = VariationGrouping.UpdatedAt;
                            VariationGroupingDAO.DeletedAt = VariationGrouping.DeletedAt;
                            VariationGroupingDAO.RowId = VariationGrouping.RowId;
                            VariationGroupingDAO.Used = VariationGrouping.Used;
                            VariationGroupingDAOs.Add(VariationGroupingDAO);
                        }
                        DataContext.VariationGrouping.BulkMerge(VariationGroupingDAOs);

                        foreach (Variation Variation in Variations)
                        {
                            VariationDAO VariationDAO = new VariationDAO();
                            VariationDAO.Id = Variation.Id;
                            VariationDAO.Code = Variation.Code;
                            VariationDAO.Name = Variation.Name;
                            VariationDAO.VariationGroupingId = Variation.VariationGroupingId;
                            VariationDAO.CreatedAt = Variation.CreatedAt;
                            VariationDAO.UpdatedAt = Variation.UpdatedAt;
                            VariationDAO.DeletedAt = Variation.DeletedAt;
                            VariationDAO.RowId = Variation.RowId;
                            VariationDAO.Used = Variation.Used;
                            VariationDAOs.Add(VariationDAO);
                        }
                        DataContext.Variation.BulkMerge(VariationDAOs);

                        foreach (Item Item in Items)
                        {
                            ItemDAO ItemDAO = new ItemDAO();
                            ItemDAO.Id = Item.Id;
                            ItemDAO.Code = Item.Code;
                            ItemDAO.Name = Item.Name;
                            ItemDAO.ProductId = Item.ProductId;
                            ItemDAO.ScanCode = Item.ScanCode;
                            ItemDAO.SalePrice = Item.SalePrice;
                            ItemDAO.RetailPrice = Item.RetailPrice;
                            ItemDAO.StatusId = Item.StatusId;
                            ItemDAO.CreatedAt = Item.CreatedAt;
                            ItemDAO.UpdatedAt = Item.UpdatedAt;
                            ItemDAO.DeletedAt = Item.DeletedAt;
                            ItemDAO.RowId = Item.RowId;
                            ItemDAO.Used = Item.Used;
                            ItemDAOs.Add(ItemDAO);
                        }
                        DataContext.Item.BulkMerge(ItemDAOs);

                        foreach (ItemImageMapping ItemImageMapping in ItemImageMappings)
                        {
                            ItemImageMappingDAO ItemImageMappingDAO = new ItemImageMappingDAO
                            {
                                ItemId = ItemImageMapping.ItemId,
                                ImageId = ItemImageMapping.ImageId
                            };
                            ItemImageMappingDAOs.Add(ItemImageMappingDAO);
                        }
                        DataContext.ItemImageMapping.BulkMerge(ItemImageMappingDAOs);
                    }
                }
            }
        }

        private void InitDirectSalesOrder(RestClient RestClient)
        {
            IRestRequest CountRequest = new RestRequest("rpc/es/direct-sales-order/count");
            CountRequest.RequestFormat = DataFormat.Json;
            IRestResponse<long> CountResponse = RestClient.Post<long>(CountRequest);
            if (CountResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long count = CountResponse.Data;
                var BatchCounter = (count / 10000) + 1;

                for (int i = 0; i < BatchCounter; i++)
                {
                    IRestRequest ListRequest = new RestRequest("rpc/es/direct-sales-order/list");
                    ListRequest.RequestFormat = DataFormat.Json;
                    DirectSalesOrderFilter DirectSalesOrderFilter = new DirectSalesOrderFilter
                    {
                        Skip = i * 10000,
                        Take = 10000,
                    };
                    ListRequest.Method = Method.POST;
                    ListRequest.AddJsonBody(DirectSalesOrderFilter);
                    IRestResponse<List<DirectSalesOrder>> RestResponse = RestClient.Post<List<DirectSalesOrder>>(ListRequest);
                    if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<DirectSalesOrder> DirectSalesOrders = RestResponse.Data.Where(x => x.RequestStateId == RequestStateEnum.APPROVED.Id).ToList();
                        List<DirectSalesOrderContent> DirectSalesOrderContents = DirectSalesOrders.Where(x => x.DirectSalesOrderContents != null).SelectMany(x => x.DirectSalesOrderContents).ToList();
                        List<DirectSalesOrderPromotion> DirectSalesOrderPromotions = DirectSalesOrders.Where(x => x.DirectSalesOrderPromotions != null).SelectMany(x => x.DirectSalesOrderPromotions).ToList();
                        List<DirectSalesOrderDAO> DirectSalesOrderDAOs = new List<DirectSalesOrderDAO>();
                        List<DirectSalesOrderContentDAO> DirectSalesOrderContentDAOs = new List<DirectSalesOrderContentDAO>();
                        List<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotionDAOs = new List<DirectSalesOrderPromotionDAO>();
                        foreach (DirectSalesOrder DirectSalesOrder in DirectSalesOrders)
                        {
                            DirectSalesOrderDAO DirectSalesOrderDAO = new DirectSalesOrderDAO()
                            {
                                Id = DirectSalesOrder.Id,
                                Code = DirectSalesOrder.Code,
                                BuyerStoreId = DirectSalesOrder.BuyerStoreId,
                                DeliveryDate = DirectSalesOrder.DeliveryDate,
                                EditedPriceStatusId = DirectSalesOrder.EditedPriceStatusId,
                                GeneralDiscountAmount = DirectSalesOrder.GeneralDiscountAmount,
                                GeneralDiscountPercentage = DirectSalesOrder.GeneralDiscountPercentage,
                                Note = DirectSalesOrder.Note,
                                OrderDate = DirectSalesOrder.OrderDate,
                                PhoneNumber = DirectSalesOrder.PhoneNumber,
                                PromotionCode = DirectSalesOrder.PromotionCode,
                                PromotionValue = DirectSalesOrder.PromotionValue,
                                RequestStateId = DirectSalesOrder.RequestStateId,
                                SaleEmployeeId = DirectSalesOrder.SaleEmployeeId,
                                CreatorId = DirectSalesOrder.CreatorId,
                                StoreAddress = DirectSalesOrder.StoreAddress,
                                SubTotal = DirectSalesOrder.SubTotal,
                                Total = DirectSalesOrder.Total,
                                TotalAfterTax = DirectSalesOrder.TotalAfterTax,
                                TotalTaxAmount = DirectSalesOrder.TotalTaxAmount,
                                OrganizationId = DirectSalesOrder.OrganizationId,
                                DeliveryAddress = DirectSalesOrder.DeliveryAddress,
                                CreatedAt = DirectSalesOrder.CreatedAt,
                                UpdatedAt = DirectSalesOrder.UpdatedAt,
                                DeletedAt = DirectSalesOrder.DeletedAt,
                                RowId = DirectSalesOrder.RowId
                            };
                            DirectSalesOrderDAOs.Add(DirectSalesOrderDAO);
                        }
                        DataContext.DirectSalesOrder.BulkMerge(DirectSalesOrderDAOs);

                        foreach (DirectSalesOrderContent DirectSalesOrderContent in DirectSalesOrderContents)
                        {
                            DirectSalesOrderContentDAO DirectSalesOrderContentDAO = new DirectSalesOrderContentDAO
                            {
                                DirectSalesOrderId = DirectSalesOrderContent.DirectSalesOrderId,
                                Id = DirectSalesOrderContent.Id,
                                ItemId = DirectSalesOrderContent.ItemId,
                                UnitOfMeasureId = DirectSalesOrderContent.UnitOfMeasureId,
                                PrimaryUnitOfMeasureId = DirectSalesOrderContent.PrimaryUnitOfMeasureId,
                                Quantity = DirectSalesOrderContent.Quantity,
                                RequestedQuantity = DirectSalesOrderContent.RequestedQuantity,
                                Amount = DirectSalesOrderContent.Amount,
                                DiscountAmount = DirectSalesOrderContent.DiscountAmount,
                                DiscountPercentage = DirectSalesOrderContent.DiscountPercentage,
                                GeneralDiscountAmount = DirectSalesOrderContent.GeneralDiscountAmount,
                                GeneralDiscountPercentage = DirectSalesOrderContent.GeneralDiscountPercentage,
                                EditedPriceStatusId = DirectSalesOrderContent.EditedPriceStatusId,
                                Factor = DirectSalesOrderContent.Factor,
                                PrimaryPrice = DirectSalesOrderContent.PrimaryPrice,
                                SalePrice = DirectSalesOrderContent.SalePrice,
                                TaxAmount = DirectSalesOrderContent.TaxAmount,
                                TaxPercentage = DirectSalesOrderContent.TaxPercentage,
                            };
                            DirectSalesOrderContentDAOs.Add(DirectSalesOrderContentDAO);
                        }
                        DataContext.DirectSalesOrderContent.BulkMerge(DirectSalesOrderContentDAOs);

                        foreach (DirectSalesOrderPromotion DirectSalesOrderPromotion in DirectSalesOrderPromotions)
                        {
                            DirectSalesOrderPromotionDAO DirectSalesOrderPromotionDAO = new DirectSalesOrderPromotionDAO
                            {
                                DirectSalesOrderId = DirectSalesOrderPromotion.DirectSalesOrderId,
                                Id = DirectSalesOrderPromotion.Id,
                                ItemId = DirectSalesOrderPromotion.ItemId,
                                UnitOfMeasureId = DirectSalesOrderPromotion.UnitOfMeasureId,
                                PrimaryUnitOfMeasureId = DirectSalesOrderPromotion.PrimaryUnitOfMeasureId,
                                Quantity = DirectSalesOrderPromotion.Quantity,
                                RequestedQuantity = DirectSalesOrderPromotion.RequestedQuantity,
                                Factor = DirectSalesOrderPromotion.Factor,
                                Note = DirectSalesOrderPromotion.Note,
                            };
                            DirectSalesOrderPromotionDAOs.Add(DirectSalesOrderPromotionDAO);
                        }
                        DataContext.DirectSalesOrderPromotion.BulkMerge(DirectSalesOrderPromotionDAOs);
                    }
                }
            }
        }
        #endregion

        #region permission
        private ActionResult InitRoute()
        {
            List<Type> routeTypes = typeof(SetupController).Assembly.GetTypes()
               .Where(x => typeof(Root).IsAssignableFrom(x) && x.IsClass && x.Name != "Root")
               .ToList();


            InitMenu(routeTypes);
            InitPage(routeTypes);
            InitField(routeTypes);
            InitAction(routeTypes);

            DataContext.ActionPageMapping.Where(ap => ap.Action.IsDeleted || ap.Page.IsDeleted).DeleteFromQuery();
            DataContext.PermissionActionMapping.Where(ap => ap.Action.IsDeleted).DeleteFromQuery();
            DataContext.Action.Where(p => p.IsDeleted || p.Menu.IsDeleted).DeleteFromQuery();
            DataContext.Page.Where(p => p.IsDeleted).DeleteFromQuery();
            DataContext.PermissionContent.Where(f => f.Field.IsDeleted == true || f.Field.Menu.IsDeleted).DeleteFromQuery();
            DataContext.Field.Where(pf => pf.IsDeleted || pf.Menu.IsDeleted).DeleteFromQuery();
            DataContext.Permission.Where(p => p.Menu.IsDeleted).DeleteFromQuery();
            DataContext.Menu.Where(v => v.IsDeleted).DeleteFromQuery();
            return Ok();
        }

        private ActionResult InitAdmin()
        {
            RoleDAO Admin = DataContext.Role
               .Where(r => r.Name == "ADMIN")
               .FirstOrDefault();
            if (Admin == null)
            {
                Admin = new RoleDAO
                {
                    Name = "ADMIN",
                    Code = "ADMIN",
                    StatusId = StatusEnum.ACTIVE.Id,
                };
                DataContext.Role.Add(Admin);
                DataContext.SaveChanges();
            }

            AppUserDAO AppUser = DataContext.AppUser
                .Where(au => au.Username.ToLower() == "Administrator".ToLower())
                .FirstOrDefault();
            if (AppUser == null)
            {
                return Ok();
            }

            AppUserRoleMappingDAO AppUserRoleMappingDAO = DataContext.AppUserRoleMapping
                .Where(ur => ur.RoleId == Admin.Id && ur.AppUserId == AppUser.Id)
                .FirstOrDefault();
            if (AppUserRoleMappingDAO == null)
            {
                AppUserRoleMappingDAO = new AppUserRoleMappingDAO
                {
                    AppUserId = AppUser.Id,
                    RoleId = Admin.Id,
                };
                DataContext.AppUserRoleMapping.Add(AppUserRoleMappingDAO);
                DataContext.SaveChanges();
            }

            List<MenuDAO> Menus = DataContext.Menu.AsNoTracking()
                .Include(v => v.Actions)
                .ToList();
            List<PermissionDAO> permissions = DataContext.Permission.AsNoTracking()
                .Include(p => p.PermissionActionMappings)
                .ToList();
            foreach (MenuDAO Menu in Menus)
            {
                PermissionDAO permission = permissions
                    .Where(p => p.MenuId == Menu.Id && p.RoleId == Admin.Id)
                    .FirstOrDefault();
                if (permission == null)
                {
                    permission = new PermissionDAO
                    {
                        Code = Admin + "_" + Menu.Name,
                        Name = Admin + "_" + Menu.Name,
                        MenuId = Menu.Id,
                        RoleId = Admin.Id,
                        StatusId = StatusEnum.ACTIVE.Id,
                        PermissionActionMappings = new List<PermissionActionMappingDAO>(),
                    };
                    permissions.Add(permission);
                }
                else
                {
                    permission.StatusId = StatusEnum.ACTIVE.Id;
                    if (permission.PermissionActionMappings == null)
                        permission.PermissionActionMappings = new List<PermissionActionMappingDAO>();
                }
                foreach (ActionDAO action in Menu.Actions)
                {
                    PermissionActionMappingDAO PermissionActionMappingDAO = permission.PermissionActionMappings
                        .Where(ppm => ppm.ActionId == action.Id).FirstOrDefault();
                    if (PermissionActionMappingDAO == null)
                    {
                        PermissionActionMappingDAO = new PermissionActionMappingDAO
                        {
                            ActionId = action.Id
                        };
                        permission.PermissionActionMappings.Add(PermissionActionMappingDAO);
                    }
                }

            }
            DataContext.Permission.BulkMerge(permissions);
            permissions.ForEach(p =>
            {
                foreach (var action in p.PermissionActionMappings)
                {
                    action.PermissionId = p.Id;
                }
            });

            List<PermissionActionMappingDAO> PermissionActionMappingDAOs = permissions
                .SelectMany(p => p.PermissionActionMappings).ToList();
            DataContext.PermissionContent.Where(pf => pf.Permission.RoleId == Admin.Id).DeleteFromQuery();
            DataContext.PermissionActionMapping.Where(pf => pf.Permission.RoleId == Admin.Id).DeleteFromQuery();
            DataContext.PermissionActionMapping.BulkMerge(PermissionActionMappingDAOs);
            return Ok();
        }

        private void InitMenu(List<Type> routeTypes)
        {
            List<MenuDAO> Menus = DataContext.Menu.AsNoTracking().ToList();
            Menus.ForEach(m => m.IsDeleted = true);
            foreach (Type type in routeTypes)
            {
                MenuDAO Menu = Menus.Where(m => m.Code == type.Name && type.Name != "Root").FirstOrDefault();

                var DisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Select(x => ((DisplayNameAttribute)x).DisplayName)
                .DefaultIfEmpty(type.Name)
                .First();

                if (Menu == null)
                {
                    Menu = new MenuDAO
                    {
                        Code = type.Name,
                        Name = DisplayName ?? type.Name,
                        IsDeleted = false,
                    };
                    Menus.Add(Menu);
                }
                else
                {
                    Menu.IsDeleted = false;
                }
            }
            DataContext.BulkMerge(Menus);
        }

        private void InitPage(List<Type> routeTypes)
        {
            List<PageDAO> pages = DataContext.Page.AsNoTracking().OrderBy(p => p.Path).ToList();
            pages.ForEach(p => p.IsDeleted = true);
            foreach (Type type in routeTypes)
            {
                var values = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(x => (string)x.GetRawConstantValue())
                .ToList();
                foreach (string value in values)
                {
                    PageDAO page = pages.Where(p => p.Path == value).FirstOrDefault();
                    if (page == null)
                    {
                        page = new PageDAO
                        {
                            Name = value,
                            Path = value,
                            IsDeleted = false,
                        };
                        pages.Add(page);
                    }
                    else
                    {
                        page.IsDeleted = false;
                    }
                }
            }
            DataContext.BulkMerge(pages);
        }

        private void InitField(List<Type> routeTypes)
        {
            List<MenuDAO> Menus = DataContext.Menu.AsNoTracking().ToList();
            List<FieldDAO> fields = DataContext.Field.AsNoTracking().ToList();
            fields.ForEach(p => p.IsDeleted = true);
            foreach (Type type in routeTypes)
            {
                MenuDAO Menu = Menus.Where(m => m.Code == type.Name).FirstOrDefault();
                var value = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => !fi.IsInitOnly && fi.FieldType == typeof(Dictionary<string, long>))
                .Select(x => (Dictionary<string, long>)x.GetValue(x))
                .FirstOrDefault();
                if (value == null)
                    continue;
                foreach (var pair in value)
                {
                    FieldDAO field = fields
                        .Where(p => p.MenuId == Menu.Id && p.Name == pair.Key)
                        .FirstOrDefault();
                    if (field == null)
                    {
                        field = new FieldDAO
                        {
                            MenuId = Menu.Id,
                            Name = pair.Key,
                            FieldTypeId = pair.Value,
                            IsDeleted = false,
                        };
                        fields.Add(field);
                    }
                    else
                    {
                        field.FieldTypeId = pair.Value;
                        field.IsDeleted = false;
                    }
                }
            }
            DataContext.BulkMerge(fields);
        }

        private void InitAction(List<Type> routeTypes)
        {
            List<MenuDAO> Menus = DataContext.Menu.AsNoTracking().ToList();
            List<ActionDAO> actions = DataContext.Action.AsNoTracking().ToList();
            actions.ForEach(p => p.IsDeleted = true);
            foreach (Type type in routeTypes)
            {
                MenuDAO Menu = Menus.Where(m => m.Code == type.Name).FirstOrDefault();
                var value = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
               .Where(fi => !fi.IsInitOnly && fi.FieldType == typeof(Dictionary<string, List<string>>))
               .Select(x => (Dictionary<string, List<string>>)x.GetValue(x))
               .FirstOrDefault();
                if (value == null)
                    continue;
                foreach (var pair in value)
                {
                    ActionDAO action = actions
                        .Where(p => p.MenuId == Menu.Id && p.Name == pair.Key)
                        .FirstOrDefault();
                    if (action == null)
                    {
                        action = new ActionDAO
                        {
                            MenuId = Menu.Id,
                            Name = pair.Key,
                            IsDeleted = false,
                        };
                        actions.Add(action);
                    }
                    else
                    {
                        action.IsDeleted = false;
                    }
                }
            }
            DataContext.BulkMerge(actions);

            actions = DataContext.Action.Where(a => a.IsDeleted == false).AsNoTracking().ToList();
            List<PageDAO> PageDAOs = DataContext.Page.AsNoTracking().ToList();
            List<ActionPageMappingDAO> ActionPageMappingDAOs = new List<ActionPageMappingDAO>();
            foreach (Type type in routeTypes)
            {
                MenuDAO Menu = Menus.Where(m => m.Code == type.Name).FirstOrDefault();
                var value = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
               .Where(fi => !fi.IsInitOnly && fi.FieldType == typeof(Dictionary<string, List<string>>))
               .Select(x => (Dictionary<string, List<string>>)x.GetValue(x))
               .FirstOrDefault();
                if (value == null)
                    continue;

                foreach (var pair in value)
                {
                    ActionDAO action = actions
                        .Where(p => p.MenuId == Menu.Id && p.Name == pair.Key)
                        .FirstOrDefault();
                    if (action == null)
                        continue;
                    List<string> pages = pair.Value;
                    foreach (string page in pages)
                    {
                        PageDAO PageDAO = PageDAOs.Where(p => p.Path == page).FirstOrDefault();
                        if (PageDAO != null)
                        {
                            if (!ActionPageMappingDAOs.Any(ap => ap.ActionId == action.Id && ap.PageId == PageDAO.Id))
                            {
                                ActionPageMappingDAOs.Add(new ActionPageMappingDAO
                                {
                                    ActionId = action.Id,
                                    PageId = PageDAO.Id
                                });
                            }
                        }
                    }
                }
            }
            ActionPageMappingDAOs = ActionPageMappingDAOs
                .GroupBy(x => new { x.ActionId, x.PageId })
                .Select(x => new ActionPageMappingDAO
                {
                    ActionId = x.Key.ActionId,
                    PageId = x.Key.PageId
                }).ToList();
            DataContext.ActionPageMapping.DeleteFromQuery();
            DataContext.BulkInsert(ActionPageMappingDAOs);
        }
        #endregion

        #region enum
        private ActionResult InitEnum()
        {
            InitEditedPriceStatusEnum();
            InitNotificationStatusEnum();
            InitWorkflowStateEnum();
            InitWorkflowTypeEnum();
            InitPermissionEnum();
            InitSLAStatusEnum();
            InitCompanyStatusEnum();
            InitActivityPriorityEnum();
            InitActivityStatusEnum();
            InitActivityTypeEnum();
            InitBusinessTypeEnum();
            InitEntityReferenceEnum();
            InitContactStatusEnum();
            InitContractStatusEnum();
            InitContractTypeEnum();
            InitCurrencyEnum();
            InitCustomerLeadLevelEnum();
            InitCustomerLeadStatusEnum();
            InitCustomerLeadSourceEnum();
            InitEmailStatusEnum();
            InitEmailTypeEnum();
            InitKMSStatusEnum();
            InitKpiEnum();
            InitOrderPaymentStatusEnum();
            InitOrderCategoryEnum();
            InitOrderQuoteStatusEnum();
            InitPaymentStatusEnum();
            InitPotentialResultEnum();
            InitProbabilityEnum();
            InitRatingStatusEnum();
            InitRepairStatusEnum();
            InitSaleStageEnum();
            InitSLATimeUnitEnum();
            InitStoreDeliveryTimeEnum();
            InitTicketResolveTypeEnum();
            InitOpportunityResultTypeEnum();
            InitFileTypeEnum();
            InitCallStatusEnum();
            InitCallCategoryEnum();
            InitCustomerTypeEnum();
            return Ok();
        }

        private void InitEditedPriceStatusEnum()
        {
            List<EditedPriceStatusDAO> EditedPriceStatusEnumList = EditedPriceStatusEnum.EditedPriceStatusEnumList.Select(item => new EditedPriceStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.EditedPriceStatus.BulkSynchronize(EditedPriceStatusEnumList);
        }

        private void InitCustomerTypeEnum()
        {
            List<CustomerTypeDAO> CustomerTypeEnumList = CustomerTypeEnum.CustomerTypeEnumList.Select(item => new CustomerTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CustomerType.BulkSynchronize(CustomerTypeEnumList);
        }

        private void InitCallStatusEnum()
        {
            List<CallStatusDAO> CallStatusEnumList = CallStatusEnum.CallStatusEnumList.Select(item => new CallStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CallStatus.BulkSynchronize(CallStatusEnumList);
        }

        private void InitCallCategoryEnum()
        {
            List<CallCategoryDAO> CallCategoryEnumList = CallCategoryEnum.CallCategoryEnumList.Select(item => new CallCategoryDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CallCategory.BulkSynchronize(CallCategoryEnumList);
        }

        private void InitNotificationStatusEnum()
        {
            List<NotificationStatusDAO> NotificationStatusEnumList = NotificationStatusEnum.NotificationStatusEnumList.Select(item => new NotificationStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.NotificationStatus.BulkSynchronize(NotificationStatusEnumList);
        }

        private void InitWorkflowStateEnum()
        {
            List<WorkflowStateDAO> WorkflowStateEnumList = WorkflowStateEnum.WorkflowStateEnumList.Select(item => new WorkflowStateDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.WorkflowState.BulkSynchronize(WorkflowStateEnumList);
        }

        private void InitWorkflowTypeEnum()
        {
            List<WorkflowTypeDAO> WorkflowTypeEnumList = WorkflowTypeEnum.WorkflowTypeEnumList.Select(item => new WorkflowTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.WorkflowType.BulkSynchronize(WorkflowTypeEnumList);
        }

        private void InitPermissionEnum()
        {
            List<FieldTypeDAO> FieldTypeDAOs = FieldTypeEnum.List.Select(item => new FieldTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.FieldType.BulkSynchronize(FieldTypeDAOs);
            List<PermissionOperatorDAO> PermissionOperatorDAOs = new List<PermissionOperatorDAO>();
            List<PermissionOperatorDAO> ID = PermissionOperatorEnum.PermissionOperatorEnumForID.Select(item => new PermissionOperatorDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                FieldTypeId = FieldTypeEnum.ID.Id,
            }).ToList();
            PermissionOperatorDAOs.AddRange(ID);
            List<PermissionOperatorDAO> STRING = PermissionOperatorEnum.PermissionOperatorEnumForSTRING.Select(item => new PermissionOperatorDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                FieldTypeId = FieldTypeEnum.STRING.Id,
            }).ToList();
            PermissionOperatorDAOs.AddRange(STRING);

            List<PermissionOperatorDAO> LONG = PermissionOperatorEnum.PermissionOperatorEnumForLONG.Select(item => new PermissionOperatorDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                FieldTypeId = FieldTypeEnum.LONG.Id,
            }).ToList();
            PermissionOperatorDAOs.AddRange(LONG);

            List<PermissionOperatorDAO> DECIMAL = PermissionOperatorEnum.PermissionOperatorEnumForDECIMAL.Select(item => new PermissionOperatorDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                FieldTypeId = FieldTypeEnum.DECIMAL.Id,
            }).ToList();
            PermissionOperatorDAOs.AddRange(DECIMAL);

            List<PermissionOperatorDAO> DATE = PermissionOperatorEnum.PermissionOperatorEnumForDATE.Select(item => new PermissionOperatorDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                FieldTypeId = FieldTypeEnum.DATE.Id,
            }).ToList();
            PermissionOperatorDAOs.AddRange(DATE);

            DataContext.PermissionOperator.BulkSynchronize(PermissionOperatorDAOs);
        }

        private void InitSLAStatusEnum()
        {
            List<SLAStatusDAO> SLAStatusDAOs = SLAStatusEnum.SLAStatusEnumList.Select(item => new SLAStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                ColorCode = item.Value,
            }).ToList();
            DataContext.SLAStatus.BulkSynchronize(SLAStatusDAOs);
        }

        private void InitCompanyStatusEnum()
        {
            List<CompanyStatusDAO> CompanyStatusDAOs = CompanyStatusEnum.CompanyStatusEnumList.Select(item => new CompanyStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CompanyStatus.BulkSynchronize(CompanyStatusDAOs);
        }

        private void InitActivityPriorityEnum()
        {
            List<ActivityPriorityDAO> ActivityPriorityDAOs = ActivityPriorityEnum.ActivityPriorityEnumList.Select(item => new ActivityPriorityDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ActivityPriority.BulkSynchronize(ActivityPriorityDAOs);
        }

        private void InitEntityReferenceEnum()
        {
            List<EntityReferenceDAO> EntityReferenceDAOs = EntityReferenceEnum.EntityReferenceEnumList.Select(item => new EntityReferenceDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.EntityReference.BulkSynchronize(EntityReferenceDAOs);
        }

        private void InitActivityStatusEnum()
        {
            List<ActivityStatusDAO> ActivityStatusDAOs = ActivityStatusEnum.ActivityStatusEnumList.Select(item => new ActivityStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ActivityStatus.BulkSynchronize(ActivityStatusDAOs);
        }

        private void InitActivityTypeEnum()
        {
            List<ActivityTypeDAO> ActivityTypeDAOs = ActivityTypeEnum.ActivityTypeEnumList.Select(item => new ActivityTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ActivityType.BulkSynchronize(ActivityTypeDAOs);
        }

        private void InitBusinessTypeEnum()
        {
            List<BusinessTypeDAO> BusinessTypeDAOs = BusinessTypeEnum.BusinessTypeEnumList.Select(item => new BusinessTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.BusinessType.BulkSynchronize(BusinessTypeDAOs);
        }

        private void InitContactStatusEnum()
        {
            List<ContactStatusDAO> ContactStatusDAOs = ContactStatusEnum.ContactStatusEnumList.Select(item => new ContactStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ContactStatus.BulkSynchronize(ContactStatusDAOs);
        }

        private void InitContractStatusEnum()
        {
            List<ContractStatusDAO> ContractStatusDAOs = ContractStatusEnum.ContractStatusEnumList.Select(item => new ContractStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ContractStatus.BulkSynchronize(ContractStatusDAOs);
        }

        private void InitContractTypeEnum()
        {
            List<ContractTypeDAO> ContractTypeDAOs = ContractTypeEnum.ContractTypeEnumList.Select(item => new ContractTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.ContractType.BulkSynchronize(ContractTypeDAOs);
        }

        private void InitCurrencyEnum()
        {
            List<CurrencyDAO> CurrencyDAOs = CurrencyEnum.CurrencyEnumList.Select(item => new CurrencyDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.Currency.BulkSynchronize(CurrencyDAOs);
        }

        private void InitCustomerLeadLevelEnum()
        {
            List<CustomerLeadLevelDAO> CustomerLeadLevelDAOs = CustomerLeadLevelEnum.CustomerLeadLevelEnumList.Select(item => new CustomerLeadLevelDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CustomerLeadLevel.BulkSynchronize(CustomerLeadLevelDAOs);
        }

        private void InitCustomerLeadStatusEnum()
        {
            List<CustomerLeadStatusDAO> CustomerLeadStatusDAOs = CustomerLeadStatusEnum.CustomerLeadStatusEnumList.Select(item => new CustomerLeadStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CustomerLeadStatus.BulkSynchronize(CustomerLeadStatusDAOs);
        }

        private void InitCustomerLeadSourceEnum()
        {
            List<CustomerLeadSourceDAO> CustomerLeadSourceDAOs = CustomerLeadSourceEnum.CustomerLeadSourceEnumList.Select(item => new CustomerLeadSourceDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.CustomerLeadSource.BulkSynchronize(CustomerLeadSourceDAOs);
        }

        private void InitEmailStatusEnum()
        {
            List<EmailStatusDAO> EmailStatusDAOs = EmailStatusEnum.EmailStatusEnumList.Select(item => new EmailStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name
            }).ToList();
            DataContext.EmailStatus.BulkSynchronize(EmailStatusDAOs);
        }

        private void InitEmailTypeEnum()
        {
            List<EmailTypeDAO> EmailTypeDAOs = EmailTypeEnum.EmailTypeEnumList.Select(item => new EmailTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name
            }).ToList();
            DataContext.EmailType.BulkSynchronize(EmailTypeDAOs);
        }

        private void InitKMSStatusEnum()
        {
            List<KMSStatusDAO> KMSStatusDAOs = KMSStatusEnum.KMSStatusEnumList.Select(item => new KMSStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.KMSStatus.BulkSynchronize(KMSStatusDAOs);
        }

        private void InitKpiEnum()
        {
            List<KpiCriteriaGeneralDAO> KpiCriteriaGeneralDAOs = KpiCriteriaGeneralEnum.KpiCriteriaGeneralEnumList.Select(item => new KpiCriteriaGeneralDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.KpiCriteriaGeneral.BulkSynchronize(KpiCriteriaGeneralDAOs);

            List<KpiCriteriaItemDAO> KpiCriteriaItemDAOs = KpiCriteriaItemEnum.KpiCriteriaItemEnumList.Select(item => new KpiCriteriaItemDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.KpiCriteriaItem.BulkSynchronize(KpiCriteriaItemDAOs);

            List<KpiPeriodDAO> KpiPeriodDAOs = KpiPeriodEnum.KpiPeriodEnumList.Select(item => new KpiPeriodDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.KpiPeriod.BulkSynchronize(KpiPeriodDAOs);

            List<KpiYearDAO> KpiYearDAOs = KpiYearEnum.KpiYearEnumList.Select(item => new KpiYearDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.KpiYear.BulkSynchronize(KpiYearDAOs);
        }

        private void InitOrderPaymentStatusEnum()
        {
            List<OrderPaymentStatusDAO> OrderPaymentStatusDAOs = OrderPaymentStatusEnum.OrderPaymentStatusEnumList.Select(item => new OrderPaymentStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.OrderPaymentStatus.BulkSynchronize(OrderPaymentStatusDAOs);
        }

        private void InitOrderCategoryEnum()
        {
            List<OrderCategoryDAO> OrderCategoryDAOs = OrderCategoryEnum.OrderCategoryEnumList.Select(item => new OrderCategoryDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.OrderCategory.BulkSynchronize(OrderCategoryDAOs);
        }

        private void InitOrderQuoteStatusEnum()
        {
            List<OrderQuoteStatusDAO> OrderQuoteStatusDAOs = OrderQuoteStatusEnum.OrderQuoteStatusEnumList.Select(item => new OrderQuoteStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.OrderQuoteStatus.BulkSynchronize(OrderQuoteStatusDAOs);
        }

        private void InitPaymentStatusEnum()
        {
            List<PaymentStatusDAO> PaymentStatusDAOs = PaymentStatusEnum.PaymentStatusEnumList.Select(item => new PaymentStatusDAO
            {
                Id = item.Id,
                Name = item.Name,
                Code = item.Value,
            }).ToList();
            DataContext.PaymentStatus.BulkSynchronize(PaymentStatusDAOs);
        }

        private void InitPotentialResultEnum()
        {
            List<PotentialResultDAO> PotentialResultDAOs = PotentialResultEnum.PotentialResultEnumList.Select(item => new PotentialResultDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.PotentialResult.BulkSynchronize(PotentialResultDAOs);
        }

        private void InitProbabilityEnum()
        {
            List<ProbabilityDAO> ProbabilityDAOs = ProbabilityEnum.ProbabilityEnumList.Select(item => new ProbabilityDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.Probability.BulkSynchronize(ProbabilityDAOs);
        }

        private void InitRatingStatusEnum()
        {
            List<RatingStatusDAO> RatingStatusDAOs = RatingStatusEnum.RatingStatusEnumList.Select(item => new RatingStatusDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.RatingStatus.BulkSynchronize(RatingStatusDAOs);
        }

        private void InitRepairStatusEnum()
        {
            List<RepairStatusDAO> RepairStatusDAOs = RepairStatusEnum.RepairStatusEnumList.Select(item => new RepairStatusDAO
            {
                Id = item.Id,
                Code = item.Value,
                Name = item.Name,
            }).ToList();
            DataContext.RepairStatus.BulkSynchronize(RepairStatusDAOs);
        }

        private void InitSaleStageEnum()
        {
            List<SaleStageDAO> SaleStageDAOs = SaleStageEnum.SaleStageEnumList.Select(item => new SaleStageDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.SaleStage.BulkSynchronize(SaleStageDAOs);
        }

        private void InitSLATimeUnitEnum()
        {
            List<SLATimeUnitDAO> SLATimeUnitDAOs = SLATimeUnitEnum.SLATimeUnitEnumList.Select(item => new SLATimeUnitDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.SLATimeUnit.BulkSynchronize(SLATimeUnitDAOs);
        }

        private void InitStoreDeliveryTimeEnum()
        {
            List<StoreDeliveryTimeDAO> StoreDeliveryTimeDAOs = StoreDeliveryTimeEnum.StoreDeliveryTimeEnumList.Select(item => new StoreDeliveryTimeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.StoreDeliveryTime.BulkSynchronize(StoreDeliveryTimeDAOs);
        }

        private void InitTicketResolveTypeEnum()
        {
            List<TicketResolveTypeDAO> TicketResolveTypeDAOs = TicketResolveTypeEnum.TicketResolveTypeEnumList.Select(item => new TicketResolveTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.TicketResolveType.BulkSynchronize(TicketResolveTypeDAOs);
        }

        private void InitOpportunityResultTypeEnum()
        {
            List<OpportunityResultTypeDAO> OpportunityResultTypeDAOs = OpportunityResultTypeEnum.OpportunityResultTypeEnumList.Select(item => new OpportunityResultTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.OpportunityResultType.BulkSynchronize(OpportunityResultTypeDAOs);
        }

        private void InitFileTypeEnum()
        {
            List<FileTypeDAO> FileTypeDAOs = FileTypeEnum.FileTypeEnumList.Select(item => new FileTypeDAO
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
            }).ToList();
            DataContext.FileType.BulkSynchronize(FileTypeDAOs);
        }
        #endregion
    }
}