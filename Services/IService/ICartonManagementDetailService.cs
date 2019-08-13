using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ICartonManagementDetailService
    {
        bool Create(CartonManagementDetailModel CartonManagement);
        List<CartonManagementDetailModel> GetAll();
        bool Edit(CartonManagementDetailModel Carton);
        CartonManagementDetailModel GetById(int? id);
        CartonManagementDetailModel Delete(CartonManagementDetailModel carton);
        List<CartonManagementDetailModel> GetPaging(int? page, out int TotalCount);
        List<CartonManagementDetailModel> GetSearchData(CartonManagementDetailModel order, int? page, out int TotalCount);
        List<CartonManagementDetailModel> ByCartonID(int? id);
    }
}
