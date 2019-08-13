using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface ISeasonService
    {
        List<SeasonModel> GetAll();
        List<SeasonModel> GetPaging(int? page, out int TotalCount);
        List<SeasonModel> GetSearchData(SeasonSearch seasonSearch, int? page, out int TotalCount);
        List<SeasonModel> GetSeasonDescription(int? id);
        SeasonModel GetById(int? id);
        bool Create(SeasonModel seasonModel);
        SeasonModel Edit(SeasonModel seasonModel);
        SeasonModel Delete(SeasonModel seasonModel);
        bool CheckSeasonCode(SeasonModel seasonModel);
        List<SeasonModel> SeasonAutocomplete(string name);
        bool CheckSeasonCode1(string chk);
        SeasonModel GetSeasonId(string sku);
        Dictionary<string, string> CreateList(Dictionary<int, SeasonModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, SeasonModel> list);

	}
}
