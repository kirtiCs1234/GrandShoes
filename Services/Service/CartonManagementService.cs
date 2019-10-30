﻿using Helper;
using Model;
using Model.ForStockTransfer;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class CartonManagementService : ICartonManagementService
    {
        public List<CartonManagementModel> GetAllDetail(int? SummaryId)
        {
            List<CartonManagementModel> CartonManagementModelList = ServerResponse.Invoke<List<CartonManagementModel>>("api/cartonManagement/getDetails?SummaryId="+ SummaryId, "", "GET");
            return CartonManagementModelList;
        }
        public List<CartonManagementModel> GetAll()
        {
            List<CartonManagementModel> CartonManagementModelList = ServerResponse.Invoke<List<CartonManagementModel>>("api/cartonManagement/getDetail", "", "GET");
            return CartonManagementModelList;
        }
        public bool Edit(CartonManagementModel carton)
        {
            return ServerResponse.Invoke<bool>("api/cartonManagement/edit?id=" + carton.Id, JsonConvert.SerializeObject(carton), "POST");

        }
		public bool DeleteNull()
		{
			return ServerResponse.Invoke<bool>("api/cartonManagement/deleteAll", "", "Get");
		}

		public bool Create(SearchForCarton model)
        {
            return ServerResponse.Invoke<bool>("/api/cartonManagement/create", JsonConvert.SerializeObject(model), "post");

        }
        public List<CartonManagementModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<CartonManagementModel>>>("api/cartonManagement/getAreaPaging?pageNumber=" + page, body, "GET");
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
        public List<CartonManagementModel> GetSearchData(CartonManagementModel search, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(search);
            var result = ServerResponse.Invoke<ServiceResult<List<CartonManagementModel>>>("api/cartonManagement/getSearchData", body, "Post");
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
        public bool AddCartonOrder(int? CartonManagementId)
        {
            return ServerResponse.Invoke<bool>("/api/cartonManagement/AddCartonOrder?CartonManagementID=" + CartonManagementId, "", "POST");
        }
        public CartonManagementModel GetById(int? id)
        {
            var CartonById = ServerResponse.Invoke<CartonManagementModel>("api/cartonManagement/getById?Id="+id,"","GET");
            return CartonById;
        }
        public CartonManagementModel Delete(CartonManagementModel cartonModel)
        {
            var body = JsonConvert.SerializeObject(cartonModel);
            CartonManagementModel CartonManagementDelete = ServerResponse.Invoke<CartonManagementModel>("api/cartonManagement/delete?id=" + cartonModel.Id, body, "POST");
            return CartonManagementDelete;
        }
       
        public bool EditCartonManagement(int? Id)
        {
            return ServerResponse.Invoke<bool>("/api/cartonManagement/EditCartonManagement?Id=" + Id, "", "POST");
        }


		//---------------------for stock transfers
		public List<CartonManagementForStockTransferModel> GetAllDetailForStock(int? SummaryId)
		{
			List<CartonManagementForStockTransferModel> CartonManagementModelList = ServerResponse.Invoke<List<CartonManagementForStockTransferModel>>("api/cartonManagement/GetAllDetailForStock?SummaryId=" + SummaryId, "", "GET");
			return CartonManagementModelList;
		}
	}
}