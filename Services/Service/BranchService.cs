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
    public class BranchService:IBranchService
    {
        public List<BranchModel> GetAll()

        {
            var body = "";
            List<BranchModel> BranchModelList = ServerResponse.Invoke<List<BranchModel>>("api/branch/getDetails", body, "get");
           
            return BranchModelList;
        }
        public List<BranchModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<BranchModel>>>("api/branch/getBranchPaging?pageNumber=" + page, body, "GET");
           
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
        public Dictionary<string, string> CreateList(Dictionary<int, BranchModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/branch/createList", JsonConvert.SerializeObject(list), "Post");
        }
        public Dictionary<string, string> UpdateList(Dictionary<int, BranchModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/branch/updateList", JsonConvert.SerializeObject(list), "Post");
        }
        public List<BranchModel> GetSearchData(BranchSearch branchSearch, int? page, out int TotalCount)
        {

           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(branchSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<BranchModel>>>("api/branch/getSearchData", body, "Post");
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
       public BranchModel GetByName(string name)
		{
			return ServerResponse.Invoke<BranchModel>("api/branch/getByName?name=" + name, "", "POST");
		}

		public bool CheckBranchCode(string chk)
        {
            var body = JsonConvert.SerializeObject(chk);
            var CheckBranchCode = ServerResponse.Invoke<bool>("api/branch/checkBranchCode?chk=" + chk, body, "POST");
            return CheckBranchCode;
        }
        public Dictionary<int, bool> CheckBranch(Dictionary<int, string> list)
        {
            return ServerResponse.Invoke<Dictionary<int, bool>>("api/branch/checkBranch", JsonConvert.SerializeObject(list), "Post");
        }
        public BranchModel GetById(int? id)
        {
            var body = "";
            BranchModel BranchById = ServerResponse.Invoke<BranchModel>("api/branch/getDetail?id=" + id, body, "get");
            return BranchById;
        }
        public bool Create(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            bool BranchCreate = ServerResponse.Invoke<bool>("api/branch/create", body, "Post");
            return BranchCreate;
        }
        public bool Edit(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            bool BranchEdit = ServerResponse.Invoke<bool>("api/branch/edit?id=" + branchModel.Id, body, "POST");
            return BranchEdit;
        }
        public BranchModel Delete(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            BranchModel BranchDelete = ServerResponse.Invoke<BranchModel>("api/branch/Delete?id=" + branchModel.Id, body, "POST");
            return BranchDelete;
        }
        public List<BranchModel> SearchBranch(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            return ServerResponse.Invoke<List<BranchModel>>("/api/branch/SearchBranch", body, "post");
        }
        public bool CheckBranchCode(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            var CheckBranchCode = ServerResponse.Invoke<bool>("api/branch/checkBranchCode", body, "POST");
            return CheckBranchCode;
        }
        public bool CheckBranchName(BranchModel branchModel)
        {
            var body = JsonConvert.SerializeObject(branchModel);
            var CheckBranchName = ServerResponse.Invoke<bool>("api/branch/checkBranchName", body, "POST");
            return CheckBranchName;
        }
    }
}
