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
    class SMIBranchDefaultService: ISMIBranchDefaultService
    {
        public List<SMIBranchDefaultModel> GetAll()
        {
            var body = "";
            List<SMIBranchDefaultModel> SMIBranchDefaultModelList = ServerResponse.Invoke<List<SMIBranchDefaultModel>>("api/SMIBranchDefault/getDetails", body, "get");
            return SMIBranchDefaultModelList;
        }

        public List<SMIBranchDefaultModel> GetBranchByID(int id)
        {
            var body = "";
            List<SMIBranchDefaultModel> SMIBranchDefaultModelList = ServerResponse.Invoke<List<SMIBranchDefaultModel>>("api/SMIBranchDefault/getById?id="+id, body, "get");
            return SMIBranchDefaultModelList;
        }
        public SMIBranchDefaultModel SMIBranch(SMIBranchDefaultModel smiModel)
        {
            var body = JsonConvert.SerializeObject(smiModel);
            var SMIModel = ServerResponse.Invoke<SMIBranchDefaultModel>("api/SMIBranchDefault/smiPut", body, "POST");
            return SMIModel;
        }
    }
}
