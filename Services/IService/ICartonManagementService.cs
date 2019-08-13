using Model;
using Model.ForStockTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface ICartonManagementService
    {
        List<CartonManagementModel> GetAllDetail(int? SummaryId);
        List<CartonManagementModel> GetAll();
        bool Create(SearchForCarton model);
        bool AddCartonOrder(int? CartonManagementId);
        CartonManagementModel Delete(CartonManagementModel cartonModel);
        CartonManagementModel GetById(int? id);
        bool EditCartonManagement(int? Id);
        List<CartonManagementModel> GetPaging(int? page, out int TotalCount);
        List<CartonManagementModel> GetSearchData(CartonManagementModel search, int? page, out int TotalCount);
        bool Edit(CartonManagementModel carton);
		bool DeleteNull();



		//------for stocktransfers
		List<CartonManagementForStockTransferModel> GetAllDetailForStock(int? SummaryId);
	}
}
