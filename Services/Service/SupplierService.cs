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
    class SupplierService : ISupplierService
    {
        public List<SupplierModel> GetAll()

        {
            var body = "";
            List<SupplierModel> SupplierModelList = ServerResponse.Invoke<List<SupplierModel>>("api/supplier/getDetails", body, "get");
            return SupplierModelList;
        }
       
        public List<SupplierModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
          //  int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<SupplierModel>>>("api/supplier/getSupplierPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            var model = result.data.ToList();
            return model;
        }
        public List<SupplierModel> GetSearchData(SupplierSearch supplierSearch, int? page, out int TotalCount)
        {
            
            //int pageSize = 10;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(supplierSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<SupplierModel>>>("api/supplier/getSearchData", body, "Post");
            
            TotalCount = result.TotalCount;
            if (result.data!= null)
            {
               var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public SupplierModel GetById(int? id)
        { 
            var body = "";
            SupplierModel SupplierById = ServerResponse.Invoke<SupplierModel>("api/supplier/getDetail?id="+id, body, "get");
            return SupplierById;
        }
        public bool Create(SupplierModel supplierModel)
        {
            var body = JsonConvert.SerializeObject(supplierModel);
            bool SupplierCreate = ServerResponse.Invoke<bool>("api/supplier/create", body, "Post");
            return SupplierCreate;
        }
        public SupplierModel ExcelUpload(SupplierModel supplierModel)
        {
            var body = JsonConvert.SerializeObject(supplierModel);
            SupplierModel SupplierCreate = ServerResponse.Invoke<SupplierModel>("api/supplier/supplierExcelData", body, "Post");
            return SupplierCreate;
        }
        public bool Edit(SupplierModel supplierModel)
        {
            var body = JsonConvert.SerializeObject(supplierModel);
           bool SupplierEdit = ServerResponse.Invoke<bool>("api/supplier/edit?id=" + supplierModel.Id, body, "POST");
            return SupplierEdit;
        }
        public bool CheckSupplierCode(string chk)
        {
            var body = JsonConvert.SerializeObject(chk);
            var CheckSupplierCode = ServerResponse.Invoke<bool>("api/supplier/checkSupplierCode?chk="+chk, body, "POST");
            return CheckSupplierCode;
        }
        public SupplierModel Delete(SupplierModel supplierModel)
        {
            var body = JsonConvert.SerializeObject(supplierModel);
            SupplierModel SupplierDelete = ServerResponse.Invoke<SupplierModel>("api/supplier/Delete?id=" + supplierModel.Id, body, "POST");
            return SupplierDelete;
        }
        public List<SupplierModel> GetSupplierName(int? id)
        {
            var body = "";
            List<SupplierModel> SupplierModelNameList = ServerResponse.Invoke<List<SupplierModel>>("api/supplier/getSupplierName?id=" + id, body, "Get");
            return SupplierModelNameList;
        }
        public List<SupplierModel> SupplierAutocomplete(string name)
        {
            return ServerResponse.Invoke<List<SupplierModel>>("api/supplier/SupplierAutocomplete?name=" + name, "", "post");
        }
        public SupplierModel GetSupplierId(string sku)
        {
            var getSupplierId = ServerResponse.Invoke<SupplierModel>("api/supplier/getSupplierId?sku=" + sku, "", "POST");
            return getSupplierId;
        }
		public Dictionary<string,string> CreateList(Dictionary<int,SupplierModel> list)
		 {
			return ServerResponse.Invoke<Dictionary<string, string>>("api/supplier/createList", JsonConvert.SerializeObject(list), "Post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int, SupplierModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/supplier/updateList", JsonConvert.SerializeObject(list), "Post");
        }
        public bool IsSupplierExist(int Id, string Code)
        {
            Code = System.Web.HttpUtility.UrlEncode(Code);
            return ServerResponse.Invoke<bool>("/api/supplier/issupplierexist?id=" + Id + "&code=" + Code, "", "Get");
        }
    }
}
