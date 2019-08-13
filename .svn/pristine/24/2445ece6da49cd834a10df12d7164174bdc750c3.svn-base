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
    public class TemplateService :ITemplateService
    {
        public List<TemplateModel> GetAll()
        {
            var body = "";
            List<TemplateModel> TemplateModelList = ServerResponse.Invoke<List<TemplateModel>>("api/template/getDetails",body,"GET");
            return TemplateModelList;
        }
        public int PageCount()
        {
            var count = ServerResponse.Invoke<int>("api/template/count", "","GET");
            return count;
        }
        public List<TemplateModel> GetAllWithPaging(int pageNumber, int pageSize)
        {
            var body = "";
            List<TemplateModel> TemplateModelList = ServerResponse.Invoke<List<TemplateModel>>("api/template/getAll?PageNumber=" + pageNumber + "&PageSize=" + pageSize, body, "GET");
            return TemplateModelList;
        }
        public TemplateModel GetTemplateId(string sku)
        {
            var getTemplateId = ServerResponse.Invoke<TemplateModel>("api/template/getTemplateId?sku=" + sku, "", "POST");
            return getTemplateId;
        }
		public List<LengthMeasureModel> LengthMeasure()
		{
			var list = ServerResponse.Invoke<List<LengthMeasureModel>>("api/template/lengthmeasure", "", "GET");
			return list;
		}
		public TemplateModel GetById(int? id)
		{
			var data = ServerResponse.Invoke<TemplateModel>("api/template/getDetail?id=" + id, "", "GET");
			return data;
		}
		public bool CheckTemplate(TemplateModel check)
		{
			return ServerResponse.Invoke<bool>("api/template/checkTemplate", JsonConvert.SerializeObject(check), "Post");
		}
		public TemplateModel Create(TemplateModel model)
		{
			TemplateModel result = ServerResponse.Invoke<TemplateModel>("api/template/create", JsonConvert.SerializeObject(model), "POST");
			return result;
		}
		public TemplateModel Edit(TemplateModel model)
		{
			TemplateModel result = ServerResponse.Invoke<TemplateModel>("api/template/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
			return result;
		}
        public TemplateModel Delete(TemplateModel model)
        {
            TemplateModel result = ServerResponse.Invoke<TemplateModel>("api/template/delete?id="+model.Id,"","POST");
            return result;
        }
	}
}
