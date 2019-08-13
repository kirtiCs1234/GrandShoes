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
    public class MarkDownBranchService: IMarkDownBranchService
    {
       public MarkDownBranchModel Create(MarkDownAddModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var create = ServerResponse.Invoke<MarkDownBranchModel>("api/markDownBranch/create", body, "POST");
            return create;
        }
    }
}
