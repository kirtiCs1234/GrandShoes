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
    public class SeasonService:ISeasonService
    {
        public List<SeasonModel> GetAll()
        {
            var body = "";
            List<SeasonModel> SeasonModelList = ServerResponse.Invoke<List<SeasonModel>>("api/season/getDetails",body,"Get");
            return SeasonModelList;
        }
        public List<SeasonModel> GetSeasonDescription(int? id)
        {
            var body = "";
            List<SeasonModel> SeasonModelDescriptionList = ServerResponse.Invoke<List<SeasonModel>>("api/season/getSeasonDescription?id="+id, body, "Get");
            return SeasonModelDescriptionList;
        }
        public List<SeasonModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            //int pageSize = 1;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<SeasonModel>>>("api/season/getSeasonPaging?pageNumber=" + page, body, "GET");
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
		public Dictionary<string, string> CreateList(Dictionary<int,SeasonModel> list)
		{
			return ServerResponse.Invoke<Dictionary<string, string>>("api/season/CreateList", JsonConvert.SerializeObject(list), "Post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int, SeasonModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/season/UpdateList", JsonConvert.SerializeObject(list), "Post");
        }

        public List<SeasonModel> GetSearchData(SeasonSearch seasonSearch, int? page, out int TotalCount)
        {

            //int pageSize = 4;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(seasonSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<SeasonModel>>>("api/season/getSearchData", body, "Post");
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

        public SeasonModel GetById(int? id)
        {
            var body = "";
            SeasonModel SeasonById = ServerResponse.Invoke<SeasonModel>("api/season/getDetail?id=" + id, body, "get");
            return SeasonById;
        }
        public bool CheckSeasonCode(SeasonModel seasonModel)
        {
            var body = JsonConvert.SerializeObject(seasonModel);
            var CheckSeasonCode = ServerResponse.Invoke<bool>("api/season/checkSeasonCode", body, "POST");
            return CheckSeasonCode;
        }
        public bool CheckSeasonCode1(string chk)
        {
            var body = JsonConvert.SerializeObject(chk);
            var CheckSeasonCode = ServerResponse.Invoke<bool>("api/season/checkSeasonCode1?chk=" + chk, body, "POST");
            return CheckSeasonCode;
        }
        public bool Create(SeasonModel seasonModel)
        {
            var body = JsonConvert.SerializeObject(seasonModel);
           bool SeasonCreate = ServerResponse.Invoke<bool>("api/season/create", body, "Post");
            return SeasonCreate;
        }
        public SeasonModel Edit(SeasonModel seasonModel)
        {
            var body = JsonConvert.SerializeObject(seasonModel);
            SeasonModel SeasonEdit = ServerResponse.Invoke<SeasonModel>("api/season/edit?id=" + seasonModel.Id, body, "POST");
            return SeasonEdit;
        }
        public SeasonModel Delete(SeasonModel seasonModel)
        {
            var body = JsonConvert.SerializeObject(seasonModel);
            SeasonModel SeasonDelete = ServerResponse.Invoke<SeasonModel>("api/season/Delete?id=" + seasonModel.Id, body, "POST");
            return SeasonDelete;
        }
        public List<SeasonModel> SeasonAutocomplete(string name)
        {
            name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<SeasonModel>>("api/season/SeasonAutocomplete?name=" + name, "", "get");
        }
        public SeasonModel GetSeasonId(string sku)
        {
            var getSeasonId = ServerResponse.Invoke<SeasonModel>("api/season/getSeasonId?sku=" + sku, "", "POST");
            return getSeasonId;
        }
    }
}
