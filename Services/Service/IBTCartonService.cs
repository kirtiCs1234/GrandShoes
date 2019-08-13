using Helper;
using Model;
using Model.ForStockTransfer;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class IBTCartonService:IIBTCartonService
    {
        public List<BranchModel> BranchAutocomplete(string name)
        {
            return ServerResponse.Invoke<List<BranchModel>>("api/branch/BranchAutocomplete?name=" + name, "", "post");
        }
        public BranchModel GetBranchAddress(string name)
        {
            return ServerResponse.Invoke<BranchModel>("api/branch/GetBranchAddress?name=" + name, "", "post");
        }
        public bool CreateCartonManagement(CartonManagementModel model)
        {
            return ServerResponse.Invoke<bool>("api/cartonManagement/GenerateIBT", JsonConvert.SerializeObject(model), "POST");
        }
        public List<CartonManagementModel> GetIBTNumber()
        {
            return ServerResponse.Invoke<List<CartonManagementModel>>("api/cartonManagement/GetIBTNumber", "", "GET");
        }
        public List<CartonManagementDetailModel> GetCartonDetail()
        {
            return ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/GetAllDetail", "", "GET");
        }
        public bool CreateCartonDetail1(CartonManagementModel model)
        {
            return ServerResponse.Invoke<bool>("api/cartonManagementDetail/GenerateDetailId", JsonConvert.SerializeObject(model), "POST");
        }
		public List<CartonManagementModel> GetAllCarton()
		{
			return ServerResponse.Invoke<List<CartonManagementModel>>("api/cartonManagement/getDetails", "", "GET");
		}
        public bool CompleteCreate(CartonManagementModel model)
        {
            return ServerResponse.Invoke<bool>("api/cartonManagement/completeCreate", JsonConvert.SerializeObject(model), "POST");
        }
        public List<CartonManagementDetailModel> GetProductByBranch(string BranchName)
        {
            return ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagement/GetByBranch?BranchName=" + BranchName, "", "GET");

        }
        public CartonManagementModel GetCartonByBranch(string BranchName)
        {
            return ServerResponse.Invoke<CartonManagementModel>("api/cartonManagement/getCartonByBranch?BranchName=" + BranchName, "", "GET");
        }
        public bool CheckBarcode(string Barcode)
        {
            var body = JsonConvert.SerializeObject(Barcode);
            var CheckBarCode = ServerResponse.Invoke<bool>("api/product/checkBarCode?chk=" + Barcode, body, "POST");
            return CheckBarCode;
        }
        public List<CartonManagementModel> GetDataBranch(string BranchName,int? DistributionSummaryID)
        {
            var list = ServerResponse.Invoke<List<CartonManagementModel>>("api/cartonManagement/GetData?BranchName=" + BranchName + "&DistributionSummaryID=" + DistributionSummaryID, "", "GET");
            return list;
        }
        public List<CartonManagementDetailModel> GetByCartonId(int? id)
        {
            return ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/getByCartonId?Id=" + id, "", "GET");

        }
        public List<StockDistributionSummaryModel> AutoCompleteDistributionSummaryID(int summary)
        {
            return ServerResponse.Invoke<List<StockDistributionSummaryModel>>("api/stockDistributionSummaries/SummaryAutocomplete?summary=" + summary, "", "post");

        }
        public bool EditStockDistribution(StockDistributionModel stock)
        {
            return ServerResponse.Invoke<bool>("api/stockDistributions/editData?Id=" + stock.Id, JsonConvert.SerializeObject(stock), "POST");
        }
        public StockDistributionModel GetLast()
        {
            return ServerResponse.Invoke<StockDistributionModel>("api/stockDistributions/getLast", "", "GET");
        }
        public bool DeleteWaste()
        {
            return ServerResponse.Invoke<bool>("api/cartonManagement/DeleteWaste", "", "GET");
        }
        public List<IBTDetailModel> GetIBTDetails(WinnerReportModel winner)
        {
            return ServerResponse.Invoke<List<IBTDetailModel>>("api/ibtBranch/getDetails", JsonConvert.SerializeObject(winner), "POST");
        }



		//------------------For stock transfercarton
		public bool CompleteCreateForStock(CartonManagementForStockTransferModel model)
		{
			return ServerResponse.Invoke<bool>("api/cartonManagement/CompleteCreateForStock", JsonConvert.SerializeObject(model), "POST");
		}

		public List<CartonManagementForStockTransferModel> GetIBTNumberStockTransfer()
		{
			return ServerResponse.Invoke<List<CartonManagementForStockTransferModel>>("api/cartonManagement/GetIBTNumberStockTransfer", "", "GET");
		}


		public bool CreateCartonManagementForStock(CartonManagementForStockTransferModel model)
		{
			return ServerResponse.Invoke<bool>("api/cartonManagement/GenerateIBTForStock", JsonConvert.SerializeObject(model), "POST");
		}

		public List<CartonManagementForStockTransferModel> GetIBTNumberForStock()
		{
			return ServerResponse.Invoke<List<CartonManagementForStockTransferModel>>("api/cartonManagement/GetIBTNumberForStock", "", "GET");
		}
		public CartonManagementModel GetByIdCarton(int? id)
		{
			return ServerResponse.Invoke<CartonManagementModel>("api/cartonManagement/getCartonById?id=" + id, "", "GET");
		}
	}
}
