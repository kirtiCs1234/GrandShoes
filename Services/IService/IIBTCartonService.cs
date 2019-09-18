using Model;
using Model.ForStockTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IIBTCartonService
    {
        List<BranchModel> BranchAutocomplete(string name);
        BranchModel GetBranchAddress(string name);
        List<CartonManagementModel> GetIBTNumber();
        bool CreateCartonManagement(CartonManagementModel model);
        List<CartonManagementDetailModel> GetCartonDetail();
        bool CreateCartonDetail1(CartonManagementModel model);
        bool CompleteCreate(CartonManagementModel model);
        List<CartonManagementDetailModel> GetProductByBranch(string BranchName);
        CartonManagementModel GetCartonByBranch(string BranchName);
        bool CheckBarcode(string Barcode);
        List<CartonManagementModel> GetDataBranch(string BranchName, int? DistributionSummaryID);
        List<CartonManagementDetailModel> GetByCartonId(int? id);
        List<StockDistributionSummaryModel> AutoCompleteDistributionSummaryID(int summary);
        bool EditStockDistribution(StockDistributionModel stock);
        StockDistributionModel GetBySummaryId(int id);
        bool DeleteWaste();
		CartonManagementModel GetByIdCarton(int? id);
		List<CartonManagementModel> GetAllCarton();
        List<IBTDetailModel> GetIBTDetails(WinnerReportModel winner);
        StockDistributionModel GetLast();
        //----for stock transfer
        bool CompleteCreateForStock(CartonManagementForStockTransferModel data);
		List<CartonManagementForStockTransferModel> GetIBTNumberStockTransfer();
		bool CreateCartonManagementForStock(CartonManagementForStockTransferModel model);
		List<CartonManagementForStockTransferModel> GetIBTNumberForStock();
        List<CartonManagementModel> GetLastSummaryData();
        CartonManagementModel GetByIBTNumber(string IbTNumber,int? BranchID);
        bool DispatchData(List<int> CartonList);
        List<CartonManagementModel> GetSearchData(int? summaryID, int? branchId);
    }
}
