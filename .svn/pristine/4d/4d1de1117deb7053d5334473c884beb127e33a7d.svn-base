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
   public class DesignationService: IDesignationService
    {
        public List<DesignationModel> GetAll()
        {
            var body = "";
            // var result = JsonConvert.DeserializeObject<PaginationEntityClass<AreaModel>>(json);
            List<DesignationModel> DesignationModelList = ServerResponse.Invoke<List<DesignationModel>>("api/designation/getDetails", body, "get");
            return DesignationModelList;
        }
        public DesignationModel GetById(int? id)
        {
            var body = "";
            DesignationModel DesignationById = ServerResponse.Invoke<DesignationModel>("api/designation/getDetail?id=" + id, body, "get");
            return DesignationById;
        }
        public DesignationModel Create(DesignationModel designationModel)
        {
            var body = JsonConvert.SerializeObject(designationModel);
            DesignationModel DesignationCreate = ServerResponse.Invoke<DesignationModel>("api/designation/create", body, "Post");
            return DesignationCreate;
        }
        public DesignationModel Edit(DesignationModel designationModel)
        {
            var body = JsonConvert.SerializeObject(designationModel);
            DesignationModel DesignationEdit = ServerResponse.Invoke<DesignationModel>("api/designation/edit?id=" + designationModel.Id, body, "POST");
            return DesignationEdit;
        }
        public DesignationModel Delete(DesignationModel designationModel)
        {
            var body = JsonConvert.SerializeObject(designationModel);
            DesignationModel DesignationDelete = ServerResponse.Invoke<DesignationModel>("api/designation/Delete?id=" + designationModel.Id, body, "POST");
            return DesignationDelete;
        }
        public bool CheckDesignationName(DesignationModel designationModel)
        {
            var body = JsonConvert.SerializeObject(designationModel);
            var CheckDesignationName = ServerResponse.Invoke<bool>("api/designation/checkDesignationName", body, "POST");
            return CheckDesignationName;
        }
    }
}
