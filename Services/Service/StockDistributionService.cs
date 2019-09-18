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
    public class StockDistributionService:IStockDistributionService
    {
        public List<StockDistributionModel> GetAll()
        {
            List<StockDistributionModel> areaModelList = ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getDetails", "", "GET");
            return areaModelList;
        }
        public List<StockDistributionModel> GetByProductId(int? id)
        {
            var SummaryList = ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getSummary?id=" + id, "", "GET");
            return SummaryList;
        }
      public StockDistributionModel GetByBranchId(int? id)
        {
            StockDistributionModel stockDistributionModels = ServerResponse.Invoke<StockDistributionModel>("api/stockDistributions/getByBranchId?BranchId=" + id, "", "GET");
            return stockDistributionModels;
        }
        public List<StockDistributionModel> GetByProduct(int? id)
        {
            return ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getByProductId?ProductId=" + id, "", "Get");
        }
        public List<StockDistributionModel> GetValue(Model.SearchData searchdata)
        {
            var body = JsonConvert.SerializeObject(searchdata);
            var stockDistributionModelValues = ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getValue", body, "POST");
            //var CartonDetailModelValues = ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/getValue", body, "POST");
            //var distributionModelList = new List<AddItemModel>();
            //foreach (var item in stockDistributionModelValues)
            //{
            //    AddItemModel distributionModel = new AddItemModel();
            //    distributionModel.Product = item.Product;
            //    distributionModel.Id = Convert.ToInt32(item.Id);
            //    distributionModel.BranchId = Convert.ToInt32(item.BranchId);
            //    distributionModel.ProductId = Convert.ToInt32(item.ProductId);
            //    distributionModel.StockDistributionSummaryId = (int)item.StockDistributionSummaryId;
            //    distributionModel.Quantity01 = Convert.ToInt32(item.Quantity01);
            //    distributionModel.Quantity02 = Convert.ToInt32(item.Quantity02);
            //    distributionModel.Quantity03 = Convert.ToInt32(item.Quantity03);
            //    distributionModel.Quantity04 = Convert.ToInt32(item.Quantity04);
            //    distributionModel.Quantity05 = Convert.ToInt32(item.Quantity05);
            //    distributionModel.Quantity06 = Convert.ToInt32(item.Quantity06);
            //    distributionModel.Quantity07 = Convert.ToInt32(item.Quantity07);
            //    distributionModel.Quantity08 = Convert.ToInt32(item.Quantity08);
            //    distributionModel.Quantity09 = Convert.ToInt32(item.Quantity09);
            //    distributionModel.Quantity10 = Convert.ToInt32(item.Quantity10);
            //    distributionModel.Quantity11 = Convert.ToInt32(item.Quantity11);
            //    distributionModel.Quantity12 = Convert.ToInt32(item.Quantity12);
            //    distributionModel.Quantity13 = Convert.ToInt32(item.Quantity13);
            //    distributionModel.Quantity14 = Convert.ToInt32(item.Quantity14);
            //    distributionModel.Quantity15 = Convert.ToInt32(item.Quantity15);
            //    distributionModel.Quantity16 = Convert.ToInt32(item.Quantity16);
            //    distributionModel.Quantity17 = Convert.ToInt32(item.Quantity17);
            //    distributionModel.Quantity18 = Convert.ToInt32(item.Quantity18);
            //    distributionModel.Quantity19 = Convert.ToInt32(item.Quantity19);
            //    distributionModel.Quantity20 = Convert.ToInt32(item.Quantity20);
            //    distributionModel.Quantity21 = Convert.ToInt32(item.Quantity21);
            //    distributionModel.Quantity22 = Convert.ToInt32(item.Quantity22);
            //    distributionModel.Quantity23 = Convert.ToInt32(item.Quantity23);
            //    distributionModel.Quantity24 = Convert.ToInt32(item.Quantity24);
            //    distributionModel.Quantity25 = Convert.ToInt32(item.Quantity25);
            //    distributionModel.Quantity26 = Convert.ToInt32(item.Quantity26);
            //    distributionModel.Quantity27 = Convert.ToInt32(item.Quantity27);
            //    distributionModel.Quantity28 = Convert.ToInt32(item.Quantity28);
            //    distributionModel.Quantity29 = Convert.ToInt32(item.Quantity29);
            //    distributionModel.Quantity30 = Convert.ToInt32(item.Quantity30);
            //    distributionModel.IsActive = true;
            //    var cartonByProduct = CartonDetailModelValues.Where(x => x.ProductID == distributionModel.ProductId).ToList();
               
            //        CartonManagementDetailModel cartonList = new CartonManagementDetailModel();
                    
            //        cartonList.Z01 = cartonByProduct.Sum(x=>x.Z01);
            //        cartonList.Z02 = cartonByProduct.Sum(x => x.Z02);
            //    cartonList.Z03 = cartonByProduct.Sum(x => x.Z03);
            //    cartonList.Z04 = cartonByProduct.Sum(x => x.Z04);
            //    cartonList.Z05 = cartonByProduct.Sum(x => x.Z05);
            //    cartonList.Z06 = cartonByProduct.Sum(x => x.Z06);
            //    cartonList.Z07 = cartonByProduct.Sum(x => x.Z07);
            //    cartonList.Z08 = cartonByProduct.Sum(x => x.Z08);
            //    cartonList.Z09 = cartonByProduct.Sum(x => x.Z09);
            //    cartonList.Z10 = cartonByProduct.Sum(x => x.Z10);

            //    cartonList.Z11 = cartonByProduct.Sum(x => x.Z11);
            //    cartonList.Z12 = cartonByProduct.Sum(x => x.Z12);
            //    cartonList.Z13 = cartonByProduct.Sum(x => x.Z13);
            //    cartonList.Z14 = cartonByProduct.Sum(x => x.Z14);
            //    cartonList.Z15 = cartonByProduct.Sum(x => x.Z15);
            //    cartonList.Z16 = cartonByProduct.Sum(x => x.Z16);
            //    cartonList.Z17 = cartonByProduct.Sum(x => x.Z17);
            //    cartonList.Z18 = cartonByProduct.Sum(x => x.Z18);
            //    cartonList.Z19 = cartonByProduct.Sum(x => x.Z19);
            //    cartonList.Z20 = cartonByProduct.Sum(x => x.Z20);
            //    cartonList.Z21 = cartonByProduct.Sum(x => x.Z21);
            //    cartonList.Z22 = cartonByProduct.Sum(x => x.Z22);
            //    cartonList.Z23 = cartonByProduct.Sum(x => x.Z23);
            //    cartonList.Z24 = cartonByProduct.Sum(x => x.Z24);
            //    cartonList.Z25 = cartonByProduct.Sum(x => x.Z25);
            //    cartonList.Z26 = cartonByProduct.Sum(x => x.Z26);
            //    cartonList.Z27 = cartonByProduct.Sum(x => x.Z27);
            //    cartonList.Z28 = cartonByProduct.Sum(x => x.Z28);
            //    cartonList.Z29 = cartonByProduct.Sum(x => x.Z29);
            //    cartonList.Z30 = cartonByProduct.Sum(x => x.Z30);
                
                
            //    distributionModel.Quantity01 = distributionModel.Quantity01 - cartonList.Z01;
            //    distributionModel.Quantity02 = distributionModel.Quantity02 - cartonList.Z02;
            //    distributionModel.Quantity03 = distributionModel.Quantity03 - cartonList.Z03;
            //    distributionModel.Quantity04 = distributionModel.Quantity04 - cartonList.Z04;
            //    distributionModel.Quantity05 = distributionModel.Quantity05 - cartonList.Z05;
            //    distributionModel.Quantity06 = distributionModel.Quantity06 - cartonList.Z06;

            //    distributionModel.Quantity07 = distributionModel.Quantity07 - cartonList.Z07;
            //    distributionModel.Quantity08 = distributionModel.Quantity08 - cartonList.Z08;
            //    distributionModel.Quantity09 = distributionModel.Quantity09 - cartonList.Z09;
            //    distributionModel.Quantity10 = distributionModel.Quantity10 - cartonList.Z10;
            //    distributionModel.Quantity11 = distributionModel.Quantity11 - cartonList.Z11;
            //    distributionModel.Quantity12 = distributionModel.Quantity12 - cartonList.Z12;
        
            //    distributionModel.Quantity13 = distributionModel.Quantity13 - cartonList.Z13;
            //    distributionModel.Quantity14 = distributionModel.Quantity14 - cartonList.Z14;
            //    distributionModel.Quantity15 = distributionModel.Quantity15 - cartonList.Z15;
            //    distributionModel.Quantity16 = distributionModel.Quantity16 - cartonList.Z16;
            //    distributionModel.Quantity17 = distributionModel.Quantity17 - cartonList.Z17;
            //    distributionModel.Quantity18 = distributionModel.Quantity18 - cartonList.Z18;
            
            //    distributionModel.Quantity19 = distributionModel.Quantity19 - cartonList.Z19;
            //    distributionModel.Quantity20 = distributionModel.Quantity20 - cartonList.Z20;
            //    distributionModel.Quantity21 = distributionModel.Quantity21 - cartonList.Z21;
            //    distributionModel.Quantity22 = distributionModel.Quantity22 - cartonList.Z22;
            //    distributionModel.Quantity23 = distributionModel.Quantity23 - cartonList.Z23;
            //    distributionModel.Quantity24 = distributionModel.Quantity24 - cartonList.Z24;
            
            //    distributionModel.Quantity25 = distributionModel.Quantity25 - cartonList.Z25;
            //    distributionModel.Quantity26 = distributionModel.Quantity26 - cartonList.Z26;
            //    distributionModel.Quantity27 = distributionModel.Quantity27 - cartonList.Z27;
            //    distributionModel.Quantity28 = distributionModel.Quantity28 - cartonList.Z28;
            //    distributionModel.Quantity29 = distributionModel.Quantity29 - cartonList.Z29;
            //    distributionModel.Quantity30 = distributionModel.Quantity30 - cartonList.Z30;
            //    distributionModelList.Add(distributionModel);
            //}
           
            return stockDistributionModelValues;

        }
        public StockDistributionModel GetById(int? id)
        {
            StockDistributionModel stockDistributionModels = ServerResponse.Invoke<StockDistributionModel>("api/stockDistributions/getById?Id=" + id, "", "GET");
            return stockDistributionModels;
        }
        public List<StockDistributionCartonModel> GetProducts(int? StockDistributionSummaryId, int? BranchId)

        {
            
            var ProductList = ServerResponse.Invoke<List<StockDistributionCartonModel>>("api/stockDistributions/getProducts?StockDistributionSummaryId=" + StockDistributionSummaryId + "&&BranchId=" + BranchId, "", "GET");
           

            return ProductList;
        }
        public List<StockDistributionModel> GetLastSummaryData()
        {
            return ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getLastSummaryData", "", "GET");
        }
        public List<AddItemModel> GetProductForCarton(SearchForCarton model)
        {
            var ProductList = ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getProducts?StockDistributionSummaryId=" + model.StockDistributionSummaryId + "&&BranchId=" + model.BranchId, "", "GET");
            var ProductListCarton = ServerResponse.Invoke<List<CartonManagementDetailModel>>("api/cartonManagementDetail/getProducts?StockDistributionSummaryId=" + model.StockDistributionSummaryId, "", "GET");
            var distributionModelList = new List<AddItemModel>();
            foreach (var item in ProductList)
            {
                AddItemModel distributionModel = new AddItemModel();
                distributionModel.Product = item.Product;
                distributionModel.Id = Convert.ToInt32(item.Id);
                distributionModel.BranchId = Convert.ToInt32(item.BranchId);
                distributionModel.ProductId = Convert.ToInt32(item.ProductId);
                distributionModel.StockDistributionSummaryId = (int)item.StockDistributionSummaryId;
                distributionModel.Quantity01 = Convert.ToInt32(item.Quantity01);
                distributionModel.Quantity02 = Convert.ToInt32(item.Quantity02);
                distributionModel.Quantity03 = Convert.ToInt32(item.Quantity03);
                distributionModel.Quantity04 = Convert.ToInt32(item.Quantity04);
                distributionModel.Quantity05 = Convert.ToInt32(item.Quantity05);
                distributionModel.Quantity06 = Convert.ToInt32(item.Quantity06);
                distributionModel.Quantity07 = Convert.ToInt32(item.Quantity07);
                distributionModel.Quantity08 = Convert.ToInt32(item.Quantity08);
                distributionModel.Quantity09 = Convert.ToInt32(item.Quantity09);
                distributionModel.Quantity10 = Convert.ToInt32(item.Quantity10);
                distributionModel.Quantity11 = Convert.ToInt32(item.Quantity11);
                distributionModel.Quantity12 = Convert.ToInt32(item.Quantity12);
                distributionModel.Quantity13 = Convert.ToInt32(item.Quantity13);
                distributionModel.Quantity14 = Convert.ToInt32(item.Quantity14);
                distributionModel.Quantity15 = Convert.ToInt32(item.Quantity15);
                distributionModel.Quantity16 = Convert.ToInt32(item.Quantity16);
                distributionModel.Quantity17 = Convert.ToInt32(item.Quantity17);
                distributionModel.Quantity18 = Convert.ToInt32(item.Quantity18);
                distributionModel.Quantity19 = Convert.ToInt32(item.Quantity19);
                distributionModel.Quantity20 = Convert.ToInt32(item.Quantity20);
                distributionModel.Quantity21 = Convert.ToInt32(item.Quantity21);
                distributionModel.Quantity22 = Convert.ToInt32(item.Quantity22);
                distributionModel.Quantity23 = Convert.ToInt32(item.Quantity23);
                distributionModel.Quantity24 = Convert.ToInt32(item.Quantity24);
                distributionModel.Quantity25 = Convert.ToInt32(item.Quantity25);
                distributionModel.Quantity26 = Convert.ToInt32(item.Quantity26);
                distributionModel.Quantity27 = Convert.ToInt32(item.Quantity27);
                distributionModel.Quantity28 = Convert.ToInt32(item.Quantity28);
                distributionModel.Quantity29 = Convert.ToInt32(item.Quantity29);
                distributionModel.Quantity30 = Convert.ToInt32(item.Quantity30);
                distributionModel.IsActive = true;
                var cartonByProduct = ProductListCarton.Where(x => x.ProductID == distributionModel.ProductId).ToList();

                CartonManagementDetailModel cartonList = new CartonManagementDetailModel();

                cartonList.Z01 = cartonByProduct.Sum(x => x.Z01);
                cartonList.Z02 = cartonByProduct.Sum(x => x.Z02);
                cartonList.Z03 = cartonByProduct.Sum(x => x.Z03);
                cartonList.Z04 = cartonByProduct.Sum(x => x.Z04);
                cartonList.Z05 = cartonByProduct.Sum(x => x.Z05);
                cartonList.Z06 = cartonByProduct.Sum(x => x.Z06);
                cartonList.Z07 = cartonByProduct.Sum(x => x.Z07);
                cartonList.Z08 = cartonByProduct.Sum(x => x.Z08);
                cartonList.Z09 = cartonByProduct.Sum(x => x.Z09);
                cartonList.Z10 = cartonByProduct.Sum(x => x.Z10);

                cartonList.Z11 = cartonByProduct.Sum(x => x.Z11);
                cartonList.Z12 = cartonByProduct.Sum(x => x.Z12);
                cartonList.Z13 = cartonByProduct.Sum(x => x.Z13);
                cartonList.Z14 = cartonByProduct.Sum(x => x.Z14);
                cartonList.Z15 = cartonByProduct.Sum(x => x.Z15);
                cartonList.Z16 = cartonByProduct.Sum(x => x.Z16);
                cartonList.Z17 = cartonByProduct.Sum(x => x.Z17);
                cartonList.Z18 = cartonByProduct.Sum(x => x.Z18);
                cartonList.Z19 = cartonByProduct.Sum(x => x.Z19);
                cartonList.Z20 = cartonByProduct.Sum(x => x.Z20);
                cartonList.Z21 = cartonByProduct.Sum(x => x.Z21);
                cartonList.Z22 = cartonByProduct.Sum(x => x.Z22);
                cartonList.Z23 = cartonByProduct.Sum(x => x.Z23);
                cartonList.Z24 = cartonByProduct.Sum(x => x.Z24);
                cartonList.Z25 = cartonByProduct.Sum(x => x.Z25);
                cartonList.Z26 = cartonByProduct.Sum(x => x.Z26);
                cartonList.Z27 = cartonByProduct.Sum(x => x.Z27);
                cartonList.Z28 = cartonByProduct.Sum(x => x.Z28);
                cartonList.Z29 = cartonByProduct.Sum(x => x.Z29);
                cartonList.Z30 = cartonByProduct.Sum(x => x.Z30);


                distributionModel.Quantity01 = distributionModel.Quantity01 - cartonList.Z01;
                distributionModel.Quantity02 = distributionModel.Quantity02 - cartonList.Z02;
                distributionModel.Quantity03 = distributionModel.Quantity03 - cartonList.Z03;
                distributionModel.Quantity04 = distributionModel.Quantity04 - cartonList.Z04;
                distributionModel.Quantity05 = distributionModel.Quantity05 - cartonList.Z05;
                distributionModel.Quantity06 = distributionModel.Quantity06 - cartonList.Z06;

                distributionModel.Quantity07 = distributionModel.Quantity07 - cartonList.Z07;
                distributionModel.Quantity08 = distributionModel.Quantity08 - cartonList.Z08;
                distributionModel.Quantity09 = distributionModel.Quantity09 - cartonList.Z09;
                distributionModel.Quantity10 = distributionModel.Quantity10 - cartonList.Z10;
                distributionModel.Quantity11 = distributionModel.Quantity11 - cartonList.Z11;
                distributionModel.Quantity12 = distributionModel.Quantity12 - cartonList.Z12;

                distributionModel.Quantity13 = distributionModel.Quantity13 - cartonList.Z13;
                distributionModel.Quantity14 = distributionModel.Quantity14 - cartonList.Z14;
                distributionModel.Quantity15 = distributionModel.Quantity15 - cartonList.Z15;
                distributionModel.Quantity16 = distributionModel.Quantity16 - cartonList.Z16;
                distributionModel.Quantity17 = distributionModel.Quantity17 - cartonList.Z17;
                distributionModel.Quantity18 = distributionModel.Quantity18 - cartonList.Z18;

                distributionModel.Quantity19 = distributionModel.Quantity19 - cartonList.Z19;
                distributionModel.Quantity20 = distributionModel.Quantity20 - cartonList.Z20;
                distributionModel.Quantity21 = distributionModel.Quantity21 - cartonList.Z21;
                distributionModel.Quantity22 = distributionModel.Quantity22 - cartonList.Z22;
                distributionModel.Quantity23 = distributionModel.Quantity23 - cartonList.Z23;
                distributionModel.Quantity24 = distributionModel.Quantity24 - cartonList.Z24;

                distributionModel.Quantity25 = distributionModel.Quantity25 - cartonList.Z25;
                distributionModel.Quantity26 = distributionModel.Quantity26 - cartonList.Z26;
                distributionModel.Quantity27 = distributionModel.Quantity27 - cartonList.Z27;
                distributionModel.Quantity28 = distributionModel.Quantity28 - cartonList.Z28;
                distributionModel.Quantity29 = distributionModel.Quantity29 - cartonList.Z29;
                distributionModel.Quantity30 = distributionModel.Quantity30 - cartonList.Z30;
                distributionModelList.Add(distributionModel);
            }
            return distributionModelList;
        }
        public List<StockDistributionModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<StockDistributionModel>>>("api/StoreDeliveryReport/getPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public List<StockDistributionModel> GetSearchData(StockDistributionSearch order, int? page, out int TotalCount)
        {
            //  int pageSize = 4;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(order);
            var result = ServerResponse.Invoke<ServiceResult<List<StockDistributionModel>>>("api/StoreDeliveryReport/getSearchData", body, "Post");
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {
            }
            return result.data.ToList();
        }
        public List<StockDistributionModel> GetStockData(int? DistributionSummaryID, string BranchName)
        {
            return ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/GetStockData?DistributionSummaryID=" + DistributionSummaryID + "&&BranchName=" + BranchName, "", "Get");
        }
        public List<StockDistributionModel> GetBySummaryId(int id)
        {
            return ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getBySummaryId?id=" + id, "", "Get");
        }
    }
}
