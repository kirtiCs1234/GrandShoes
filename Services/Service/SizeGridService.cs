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
   public class SizeGridService :ISizeGridService
    {
        public List<SizeGridModel> GetAll()

        {
            var body = "";
            List<SizeGridModel> SizeGridModelList = ServerResponse.Invoke<List<SizeGridModel>>("api/sizeGrid/getDetails", body, "get");
            return SizeGridModelList;
        }
        public List<SizeGridModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 1;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<SizeGridModel>>>("api/sizeGrid/getSizeGridPaging?pageNumber=" + page, body, "GET");
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
		public Dictionary<string, string> CreateList(Dictionary<int,SizeGridModel> list)
		{
			return ServerResponse.Invoke<Dictionary<string, string>>("api/sizeGrid/createList", JsonConvert.SerializeObject(list), "post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int, SizeGridModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/sizeGrid/updateList", JsonConvert.SerializeObject(list), "post");
        }

        public SizeGridModel GetSizeGridId(string sku)
        {
            var getSizeGridId = ServerResponse.Invoke<SizeGridModel>("api/sizeGrid/getGridNoId?sku=" + sku, "", "POST");
            return getSizeGridId;
        }
        public List<SizeGridModel> GetSearchData(SizeGridSearch sizeGridSearch, int? page, out int TotalCount)
        {

           // int pageSize = 4;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(sizeGridSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<SizeGridModel>>>("api/sizeGrid/getSearchData", body, "Post");
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

        public SizeGridModel GetById(int? id)
        {
            var body = "";
            SizeGridModel SizeGridById = ServerResponse.Invoke<SizeGridModel>("api/sizeGrid/getDetail?id=" + id, body, "get");
            return SizeGridById;
        }
        public List<string> GetGridNo()
        {
            var body = "";
            List<string> GridSizeCodeList = ServerResponse.Invoke<List<string>>("api/sizeGrid/getGridNo", body, "get");
            return GridSizeCodeList;
        }
        public bool CheckGridNo1(string chk)
        {
            var body = JsonConvert.SerializeObject(chk);
            var CheckBranchCode = ServerResponse.Invoke<bool>("api/sizeGrid/checkGridNo?chk=" + chk, body, "POST");
            return CheckBranchCode;
        }
        public List<string> GetGridSize(int? id)
        {
            var body = "";
            List<string> SizeGridListById = ServerResponse.Invoke<List<string>>("api/sizeGrid/getGridSize?id=" + id, body, "get");
            return SizeGridListById;
        }
        public bool Create(SizeGridModel sizeGridModel)
        {
            var body = JsonConvert.SerializeObject(sizeGridModel);
            bool SizeGridCreate = ServerResponse.Invoke<bool>("api/sizeGrid/create", body, "Post");
            return SizeGridCreate;
        }
        public bool Edit(SizeGridModel sizeGridModel)
        {
            var body = JsonConvert.SerializeObject(sizeGridModel);
            bool SizeGridEdit = ServerResponse.Invoke<bool>("api/sizeGrid/edit?id=" + sizeGridModel.Id, body, "POST");
            return SizeGridEdit;
        }
        public SizeGridModel Delete(SizeGridModel sizeGridModel)
        {
            var body = JsonConvert.SerializeObject(sizeGridModel);
            SizeGridModel SizeGridDelete = ServerResponse.Invoke<SizeGridModel>("api/sizeGrid/Delete?id=" + sizeGridModel.Id, body, "POST");
            return SizeGridDelete;
        }

        public List<SizeGridModel> SizeGridAutocomplete(string name)
        {
            name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<SizeGridModel>>("api/sizeGrid/SizeGridAutocomplete?name=" + name, "", "get");             
        }
        public List<SizeGridModel> SizeGridAutocompleteOffer(string name)
        {
            // name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<SizeGridModel>>("api/sizeGrid/SizeGridAutocompleteOffer?name=" + name, "", "get");
        }
        public bool CheckGridNo(SizeGridModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var CheckGridNo = ServerResponse.Invoke<bool>("api/sizeGrid/checkGridNo", body, "POST");
            return CheckGridNo;
        }
    }
}
