﻿using Helper;
using Model;
using Newtonsoft.Json;
using PagedList;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AreaService : IAreaService
    {
        public List<AreaModel> GetAll()
        {
            List<AreaModel> areaModelList = ServerResponse.Invoke<List<AreaModel>>("api/area/getDetails", "", "GET");
            return areaModelList;
        }
        public List<AreaModel> GetPaging(int? page,out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<AreaModel>>>("api/area/getAreaPaging?pageNumber=" + page, body, "GET");
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
        public List<AreaModel> GetSearchData(AreaSearch areaSearch,int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<AreaModel>>>("api/area/getSearchData", body, "Post");
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
      
        public AreaModel GetById(int? id)
        {
            var body = "";
            AreaModel AreaById = ServerResponse.Invoke<AreaModel>("api/area/getDetail?id=" + id, body, "get");
            return AreaById;
        }
        public bool Create(AreaModel areaModel)
        {
            var body = JsonConvert.SerializeObject(areaModel);
           bool AreaCreate = ServerResponse.Invoke<bool>("api/area/create", body, "Post");
            return AreaCreate;
        }
        public AreaModel Edit(AreaModel areaModel)
        {
            var body = JsonConvert.SerializeObject(areaModel);
            AreaModel AreaEdit = ServerResponse.Invoke<AreaModel>("api/area/edit?id=" + areaModel.Id, body, "POST");
            return AreaEdit;
        }
        public AreaModel Delete(AreaModel areaModel)
        {
            var body = JsonConvert.SerializeObject(areaModel);
            AreaModel AreaDelete = ServerResponse.Invoke<AreaModel>("api/area/Delete?id=" + areaModel.Id, body, "POST");
            return AreaDelete;
        }

    }
}
