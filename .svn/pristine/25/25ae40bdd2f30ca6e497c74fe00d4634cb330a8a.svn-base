using Helper;
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
    public class CartonManagementDetailService: ICartonManagementDetailService
    {
        public bool Create(CartonManagementDetailModel CartonManagement)
        {
            return ServerResponse.Invoke<bool>("/api/cartonManagementDetail/create", JsonConvert.SerializeObject(CartonManagement), "post");

           
        }
        public List<CartonManagementDetailModel> GetAll()
        {
            var CartonDetails=ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/getDetails", "", "GET");
            return CartonDetails;
        }
        public CartonManagementDetailModel GetById(int? id)
        {
            var GetById = ServerResponse.Invoke<CartonManagementDetailModel>("api/cartonManagementDetail/getDetail?id=" + id, "", "GET");
            return GetById;
        }
        public bool Edit(CartonManagementDetailModel Carton)
        {
            bool edit = ServerResponse.Invoke<bool>("api/cartonManagementDetail/edit?id="+Carton.Id, JsonConvert.SerializeObject(Carton), "post");
            return edit;
        }
        public CartonManagementDetailModel Delete(CartonManagementDetailModel carton)
        {
            var delete = ServerResponse.Invoke<CartonManagementDetailModel>("api/cartonManagementDetail/delete?id=" + carton.Id, JsonConvert.SerializeObject(carton), "POST");
            return delete;
        }
        public List<CartonManagementDetailModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
          //  int pageSize = 4;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<CartonManagementDetailModel>>>("api/cartonManagementDetail/getCartonPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            var model = result.data.ToList();
            return model;
        }
        public List<CartonManagementDetailModel> GetSearchData(CartonManagementDetailModel order, int? page, out int TotalCount)
        {
           // int pageSize = 4;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(order);
            var result = ServerResponse.Invoke<ServiceResult<List<CartonManagementDetailModel>>>("api/cartonManagementDetail/getSearchData", body, "Post");
            TotalCount = result.TotalCount;
            var model = result.data.ToList();
            return model;
        }
        public List<CartonManagementDetailModel> ByCartonID(int? id)
        {
            var list = ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/getByCartonId?Id=" + id, "", "GET");
            return list;
        }
    }
}
