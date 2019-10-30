﻿using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class RoleService:IRoleService
    {
        public List<RoleModel> GetAll()

        {
            var body = "";
            List<RoleModel> RoleModelList = ServerResponse.Invoke<List<RoleModel>>("api/role/getDetails", body, "get");
            return RoleModelList;
        }
        public RoleModel GetById(int? id)
        {
            var body = "";
            RoleModel RoleById = ServerResponse.Invoke<RoleModel>("api/role/getDetail?id=" + id, body, "get");
            return RoleById;
        }
        public bool Create(RoleModel roleModel)
        {
            var body = JsonConvert.SerializeObject(roleModel);
            bool RoleCreate = ServerResponse.Invoke<bool>("api/role/create", body, "Post");
            return RoleCreate;
        }
        public bool Edit(RoleModel roleModel)
        {
            var body = JsonConvert.SerializeObject(roleModel);
            bool RoleEdit = ServerResponse.Invoke<bool>("api/role/edit?id=" + roleModel.Id, body, "POST");
            return RoleEdit;
        }
        public RoleModel Delete(RoleModel roleModel)
        {
            var body = JsonConvert.SerializeObject(roleModel);
            var RoleDelete = ServerResponse.Invoke<RoleModel>("api/role/Delete?id=" + roleModel.Id, body, "POST");
            return RoleDelete;
        }
        public List<RoleModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<RoleModel>>>("api/role/getAreaPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
		public RoleModel GetByRoleName(string name)
		{
			return ServerResponse.Invoke<RoleModel>("api/role/getByName?name=" + name, "", "POST");
		}

		public List<RoleModel> GetSearchData(RoleModel styleSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(styleSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<RoleModel>>>("api/role/getSearchData", body, "Post");
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {
            }
            return result.data.ToList();
        }
        public List<PagePermissionModel> GetGrantPermission()
        {
            var result = new List<PagePermissionModel>();
            result = ServerResponse.Invoke<List<PagePermissionModel>>("api/role/getGrantPermission", "", "GET");
            return result;
        }
        //SetGrantPermission
        public List<PagePermissionModel> SetGrantPermission(List<PagePermissionModel> model)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var result = new List<PagePermissionModel>();
            result = ServerResponse.Invoke<List<PagePermissionModel>>("api/role/setGrantPermission", body, "POST");
            return result;
        }
        //getPageNameList
        public List<PageNameModel> getPageNameList(bool IsAdminPage)
        {
            var result = new List<PageNameModel>();
            result = ServerResponse.Invoke<List<Model.PageNameModel>>("api/role/getPageName?IsAdminPage=" + IsAdminPage, "", "GET");
            return result;
        }
        //getPagePermissionList
        public List<PagePermissionModel> getPagePermissionList(int RoleId, bool IsAdminPage)
        {
            var result = new List<PagePermissionModel>();
            result = ServerResponse.Invoke<List<Model.PagePermissionModel>>("api/role/getPagePermission?RoleId=" + RoleId + "&IsAdminPage=" + IsAdminPage, "", "GET");
            return result;
        }
    }
}