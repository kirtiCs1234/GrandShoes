using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ITemplateService
    {
        List<TemplateModel> GetAllWithPaging(int pageNumber, int pageSize);
        List<TemplateModel> GetAll();
        TemplateModel GetTemplateId(string sku);
		List<LengthMeasureModel> LengthMeasure();
		TemplateModel GetById(int? id);
		bool CheckTemplate(TemplateModel check);
		TemplateModel Create(TemplateModel model);
		TemplateModel Edit(TemplateModel model);
        TemplateModel Delete(TemplateModel model);
        int PageCount();

    }
}
