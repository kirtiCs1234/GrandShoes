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
    public class MarkDownService: IMarkDownService
    {
        public bool Create(MarkDownAddModel model)
        {
           
            var body = JsonConvert.SerializeObject(model);
            bool create = ServerResponse.Invoke<bool>("api/markDown/create", body, "POST");
            return create;
        }
        public List<MarkDownModel> GetAllMarkDown()
        {
            var markDownBranchList = ServerResponse.Invoke<List<MarkDownModel>>("api/markDown/getDetails", "", "GET");
            return markDownBranchList;
        }
		public List<MarkDownModel> GetByDate()
		{
			var markList = ServerResponse.Invoke<List<MarkDownModel>>("api/markDown/GetByDate", "", "GET");
			var markDownBranchList = ServerResponse.Invoke<List<MarkDownBranchModel>>("api/markDownBranch/getDetails", "", "GET");
			foreach (var item in markList)
			{
				var BranchList = markDownBranchList.Where(x => x.MarkDownID == item.Id);

				List<string> termsList = new List<string>();
				foreach (var b in BranchList)
				{
					termsList.Add(b.Branch.Name);
				}
				item.Branches = termsList.Aggregate((s1, s2) => s1 + "," + s2);
			}

			return markList;
		}
        public List<MarkDownModel> GetAll()
        {
            var markList = ServerResponse.Invoke<List<MarkDownModel>>("api/markDown/getDetails", "", "GET");
            var markDownBranchList = ServerResponse.Invoke<List<MarkDownBranchModel>>("api/markDownBranch/getDetails", "", "GET");
            foreach(var item in markList)
            {
               var BranchList = markDownBranchList.Where(x => x.MarkDownID == item.Id);
                
               List<string> termsList = new List<string>();
              foreach (var b in BranchList)
              {
                  termsList.Add(b.Branch.Name);
               }
               item.Branches= termsList.Aggregate((s1,s2)=>s1+","+s2);
            }
            
            return markList;
        }
		public List<MarkDownModel> GetByProduct(string ProductSKU,string StyleSKU)
		{
			return ServerResponse.Invoke<List<MarkDownModel>>("api/markDown/getByProduct?ProductSKU=" + ProductSKU + "&&StyleSKU=" + StyleSKU, "", "GET");
		}

	}
}
