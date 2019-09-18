using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ISizeGridService
    {
        List<SizeGridModel> GetAll();
        List<SizeGridModel> GetPaging(int? page, out int TotalCount);
        List<SizeGridModel> GetSearchData(SizeGridSearch sizeGridSearch, int? page, out int TotalCount);
        SizeGridModel GetById(int? id);
        List<string> GetGridSize(int? id);
        bool Create(SizeGridModel sizeGridModel);
        bool Edit(SizeGridModel sizeGridModel);
        List<SizeGridModel> SizeGridAutocomplete(string name);
        SizeGridModel Delete(SizeGridModel sizeGridModel);
        List<SizeGridModel> SizeGridAutocompleteOffer(string name);
        bool CheckGridNo(SizeGridModel model);
        bool CheckGridNo1(string chk);
        SizeGridModel GetSizeGridId(string sku);
        Dictionary<string, string> CreateList(Dictionary<int, SizeGridModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, SizeGridModel> list);
        Dictionary<int, bool> CheckGridNumber(Dictionary<int, string> list);
    }
}
