using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface ISupplierService
    {
        List<SupplierModel> GetAll();
        List<SupplierModel> GetPaging(int? page, out int TotalCount);
        List<SupplierModel> GetSearchData(SupplierSearch supplierSearch, int? page, out int TotalCount);
        SupplierModel GetById(int? id);
        SupplierModel ExcelUpload(SupplierModel supplierModel);
        bool Create(SupplierModel supplierModel);
        bool Edit(SupplierModel supplierModel);
        SupplierModel Delete(SupplierModel supplierModel);
        List<SupplierModel> GetSupplierName(int? id);
        bool CheckSupplierCode(string chk);
        List<SupplierModel> SupplierAutocomplete(string name);
        SupplierModel GetSupplierId(string sku);
        Dictionary<string, string> CreateList(Dictionary<int, SupplierModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, SupplierModel> list);
        bool IsSupplierExist(int Id, string Code);
        Dictionary<int, bool> SupplierCheckFilter(Dictionary<int, string> list);
    }
}
