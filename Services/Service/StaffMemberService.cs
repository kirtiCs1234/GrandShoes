﻿
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.IService;
using Model;
using Helper;
using Newtonsoft.Json;

namespace Services.Service
{
    public  class StaffMemberService: IStaffMemberService
    {
        public List<StaffMemberModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<StaffMemberModel>>>("api/staffMember/getStaffMemberPaging?pageNumber=" + page, body, "GET");
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
        public List<StaffMemberModel> GetSearchData(StaffMemberSearch staffMemberSearch, int? page, out int TotalCount)
        {

            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(staffMemberSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<StaffMemberModel>>>("api/staffMember/getSearchData", body, "Post");
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
        public List<StaffMemberModel> GetAll()
        {
            var body = "";
            List<StaffMemberModel> StaffMemberModelList = ServerResponse.Invoke<List<StaffMemberModel>>("api/staffMember/getDetails", body, "get");
            return StaffMemberModelList;
        }
        public StaffMemberModel GetById(int? id)
        {
            var body = "";
            StaffMemberModel StaffMemberId = ServerResponse.Invoke<StaffMemberModel>("api/staffMember/getDetail?id=" + id, body, "get");
            return StaffMemberId;
        }
        public StaffMemberModel GetByIdLogCheckDetails(int? id)
        {
            var body = "";
            StaffMemberModel StaffMemberId = ServerResponse.Invoke<StaffMemberModel>("api/staffMember/LogCheckDetails?id=" + id, body, "get");
            return StaffMemberId;
        }
        public bool Create(StaffMemberModel staffMemberModel)
        {
            var body = JsonConvert.SerializeObject(staffMemberModel);
            bool StaffMemberCreate = ServerResponse.Invoke<bool>("api/staffMember/create", body, "Post");
            return StaffMemberCreate;
        }
        public bool Edit(StaffMemberModel staffMemberModel)
        {
            var body = JsonConvert.SerializeObject(staffMemberModel);
            bool StaffMemberEdit = ServerResponse.Invoke<bool>("api/staffMember/edit?id=" + staffMemberModel.Id, body, "POST");
            return StaffMemberEdit;
        }
        public bool Delete(StaffMemberModel staffMemberModel)
        {
            var body = JsonConvert.SerializeObject(staffMemberModel);
            bool StaffMemberDelete = ServerResponse.Invoke<bool>("api/staffMember/Delete?id=" + staffMemberModel.Id, body, "POST");
            return StaffMemberDelete;
        }       
        public bool Disable(StaffMemberModel model)
        {
            return ServerResponse.Invoke<bool>("api/staffMember/disable", JsonConvert.SerializeObject(model), "POST");
        }
        public bool Enable(StaffMemberModel model)
        {
            return ServerResponse.Invoke<bool>("api/staffMember/enable", JsonConvert.SerializeObject(model), "POST");
        }
    }
}
