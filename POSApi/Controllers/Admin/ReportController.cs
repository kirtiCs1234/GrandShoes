 using DAL;
using Helper;
using Model;
using Model.Report;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/report")]
    public class ReportController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ReportController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("branch/getAll")]
        public IHttpActionResult getAllBranch()
        {
            Dictionary<int, string> branchlist = new Dictionary<int, string>();
            var branches = db.Branches.Where(x => x.IsActive == true).OrderBy(o => o.Id).ToList();
            branches.ForEach(x => branchlist.Add(x.Id, x.Name));
            return Ok(branchlist);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("branchStockStatus/getAll")]
        public IHttpActionResult getAll()
        {
            Dictionary<int, int> branchlist = new Dictionary<int, int>();
            List<BranchStockStatusReport> result = new List<BranchStockStatusReport>();
            var products = db.Products.Where(x => x.IsActive == true && x.StockInventories.Any(y => y.IsActive??false) == true && x.StockBranchInventories.Any(y => y.IsActive ?? false) == true)

                                .Include(i=>i.Color)
                                .Include(i=>i.Template1)
                                .Include(i=>i.StockInventories)
                                .Include(i => i.StockBranchInventories)
                                .Include(i=>i.StockBranchInventories.Select(y => y.Branch))
                                .ToList();

           

            var branches = db.Branches.Where(x => x.IsActive == true).OrderBy(o=>o.Id).ToList();
            branches.ForEach(x => branchlist.Add(x.Id, 0));

            var modellist = products.Where(x=>x.StockBranchInventories != null && x.StockBranchInventories.Count > 0).ToList();
            foreach(var item in modellist)
            {
                Dictionary<int, int> branch = new Dictionary<int, int>();
                 BranchStockStatusReport model = new BranchStockStatusReport();
                var Branches = item.StockBranchInventories.Where(x=>x.Branch.IsActive == true).Select(s => s.Branch).ToList();

                model.Product = item.ProductSKU + " " + item.StyleSKU + " " + item.ShortDescription;
                model.Color = item.Color?.Code + " " + item.Color?.ColorShort;
                model.CostPrice = item.CostPrice??0;
                model.OriginalPrice = item.ActualSellingPrice??0;
                var sumQunatity = SumQuantityInventory(item.StockBranchInventories.ToList(), out branch);
                var sumQuantity2 = SumQuantityInventory(item.StockInventories.ToList());
                model.TotalQuantiy = sumQunatity + sumQuantity2;
                model.WarehoustQuantity = sumQuantity2;
                model.MarkdownTemplate = item.Template1?.Name;
                model.BranchId = new Dictionary<int, int>();
                foreach(var bran in branchlist)
                {
                    if(branch.ContainsKey(bran.Key))
                    {
                        var value = branch[bran.Key];
                        model.BranchId.Add(bran.Key, value);
                    }
                    else
                    {
                        model.BranchId.Add(bran.Key, bran.Value);
                    }
                }

                // model.TotalQuantiy = "";
                result.Add(model);
            }
            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("branchStockStatus/filter")]
        public IHttpActionResult branchStockStatusFilter(InputBranchStockStatusReportModel inputmodel)
        {
            List<string> BranchIdList;// = new List<string>();
            List<string> ProductSKUList;// = new List<string>();
            List<string> StyleSKUList;// = new List<string>();

            var moddel = getProcessingData(inputmodel, out BranchIdList, out ProductSKUList, out StyleSKUList);

            Dictionary<int, int> branchlist = new Dictionary<int, int>();
            List<BranchStockStatusReport> result = new List<BranchStockStatusReport>();
            var ProductList = db.Products.Where(x => x.IsActive == true && x.StockInventories.Any(y => y.IsActive ?? false) == true && x.StockBranchInventories.Any(y => y.IsActive ?? false) == true)

                                .Include(i => i.Color)
                                .Include(i => i.Template1)
                                .Include(i => i.StockInventories)
                                .Include(i => i.StockBranchInventories)
                                .Include(i => i.StockBranchInventories.Select(y => y.Branch))
                                //.ToList()
                                ;
           var ptemp = ProductList.ToList();
            if(ProductSKUList.Count > 0)
            {
                ProductList = ProductList.Where(x => ProductSKUList.Contains(x.ProductSKU));
                ptemp = ProductList.ToList();
            }
            if (StyleSKUList.Count > 0)
            {
                ProductList = ProductList.Where(x => StyleSKUList.Contains(x.StyleSKU));
                ptemp = ProductList.ToList();
            }

            var products = ProductList.ToList();
            var branchSearchlist = db.Branches.Where(x => x.IsActive == true);
            var btemp = branchSearchlist.ToList();
            if (BranchIdList.Count > 0)
            {
                branchSearchlist = branchSearchlist.Where(x => BranchIdList.Contains(x.BranchCode));
                btemp = branchSearchlist.ToList();
            }


            var branches = branchSearchlist.OrderBy(o => o.Id).ToList();
            branches.ForEach(x => branchlist.Add(x.Id, 0));

            var modellist = products.Where(x => x.StockBranchInventories != null && x.StockBranchInventories.Count > 0).ToList();
            foreach (var item in modellist)
            {
                Dictionary<int, int> branch = new Dictionary<int, int>();
                BranchStockStatusReport model = new BranchStockStatusReport();
                var Branches = item.StockBranchInventories.Where(x => x.Branch.IsActive == true).Select(s => s.Branch).ToList();

                model.Product = item.ProductSKU + " " + item.StyleSKU + " " + item.ShortDescription;
                model.Color = item.Color?.Code + " " + item.Color?.ColorShort;
                model.CostPrice = item.CostPrice ?? 0;
                model.OriginalPrice = item.ActualSellingPrice ?? 0;
                var sumQunatity = SumQuantityInventory(item.StockBranchInventories.ToList(), out branch);
                var sumQuantity2 = SumQuantityInventory(item.StockInventories.ToList());
                model.TotalQuantiy = sumQunatity + sumQuantity2;
                model.WarehoustQuantity = sumQuantity2;
                model.MarkdownTemplate = item.Template1?.Name;
                model.BranchId = new Dictionary<int, int>();
                foreach (var bran in branchlist)
                {
                    if (branch.ContainsKey(bran.Key))
                    {
                        var value = branch[bran.Key];
                        model.BranchId.Add(bran.Key, value);
                    }
                    else
                    {
                        model.BranchId.Add(bran.Key, bran.Value);
                    }
                }

                // model.TotalQuantiy = "";
                result.Add(model);
            }
            return Ok(result);
        }
        private string getProcessingData(InputBranchStockStatusReportModel inputmodel, out List<string> BranchIdList, out List<string> ProductSKUList, out List<string> StyleSKUList)
        {
            BranchIdList = new List<string>();
            ProductSKUList = new List<string>();
            StyleSKUList = new List<string>();
            var BranchFrom = Convert.ToInt32(inputmodel.BramchFrom);
            var BranchTo = Convert.ToInt32(inputmodel.BramchTo);
            var ProductSKUFrom = Convert.ToInt32(inputmodel.ProductSKUFrom);
            var ProductSKUTo = Convert.ToInt32(inputmodel.ProductSKUTo);
            var StyleSKUFrom = Convert.ToInt32(inputmodel.StyleSKUFrom);
            var StyleSKUTo = Convert.ToInt32(inputmodel.StyleSKUTo);

            BranchIdList = Utility.getRangeList(BranchFrom,BranchTo,3);
            ProductSKUList = Utility.getRangeList(ProductSKUFrom, ProductSKUTo, 3);
            StyleSKUList = Utility.getRangeList(StyleSKUFrom, StyleSKUTo, 4);
            return "";
        }


        private int SumQuantityInventory<T>(List<T> list, out Dictionary<int, int> branch)
        {
            var sum = 0;
            branch = new Dictionary<int, int>();
            if(list != null && list.Count > 0)
            {
                foreach(T item in list)
                {
                    var flagValue = item.GetType().GetProperty("Branch").GetValue(item);
                    if(flagValue != null)
                    {
                        bool flag = (bool)flagValue.GetType().GetProperty("IsActive").GetValue(flagValue);
                        if(flag != null && flag)
                        {
                            var count = Helper.Utility.getKeyVaue(item).Where(x => x.Key.Contains("Quantity")).Sum(s => Convert.ToInt32(s.Value));
                            sum += count;
                            int branchId = (int)item.GetType().GetProperty("BranchId").GetValue(item);
                            branch.Add(branchId, count);
                        }
                        //else
                        //{
                        //    int branchId = (int)item.GetType().GetProperty("BranchId").GetValue(item);
                        //    branch.Add(branchId, 0);
                        //}
                    }
                    
                    
                }
                return sum;
            }
            return 0;
        }
        private int SumQuantityInventory<T>(List<T> list)
        {
            var sum = 0;
            if (list != null)
            {
                foreach(var item in list)
                {
                    var quan = Helper.Utility.getKeyVaue(item).Where(x => x.Key.Contains("Quantity")).ToList();//.Sum(s => Convert.ToInt32(s.Value));
                    sum += quan.Sum(s => Convert.ToInt32(s.Value));
                }
                return sum;
            }
            return 0;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("staff/getAll")]
        public IHttpActionResult Staff()
        {
            var userT = db.Users.Where(x => x.RoleID == 3 && x.IsActive == true).ToList();
            var userTId = userT.Select(s => s.Id).ToList();

            var staffT = db.StaffMembers.Where(x => x.IsActive == true && userTId.Contains(x.UserId??0))
                                        .Include(i=>i.User)
                                        .Include(i=>i.User.Branch)
                                        .Include(i=>i.StaffCommitions)
                                        .Include(i=>i.SalesOrders)
                                        .Include(i=>i.SalesOrders.Select(s=>s.Transactions))
                                        .ToList();

            //var branch = branches.FirstOrDefault();
            //var users = branch.Users.Where(x=>x.IsActive == true).ToList();
            //var user = users.FirstOrDefault();
            //var staffs = user.StaffMembers.Where(x=>x.IsActive == true).ToList();
            //var staff = staffs.FirstOrDefault();
            var staffCommition = new List<StaffCommitionListModel>();
            staffCommition = StaffCommissionProcess(staffT);
            //if(branches != null && branches.Count > 0)
            //{
            //    foreach (var branch in branches)
            //    {
            //        if(branch.Users != null && branch.Users.Count > 0)
            //        {
            //            foreach(var user in branch.Users)
            //            {
            //                if(user.IsActive == true)
            //                {
            //                    if (user.StaffMembers != null && user.StaffMembers.Count > 0)
            //                    {
            //                        foreach (var staff in user.StaffMembers)
            //                        {
            //                            if(staff.IsActive == true)
            //                            {
            //                                var model = new StaffCommitionListModel();
            //                                model.BranchNumber = branch.BranchCode;
            //                                model.Staff = user.FirstName + " " + user.LastName;
            //                                model.SalesId = user.Id;
            //                                model.CommitionPercent = staff.StaffCommitions?.FirstOrDefault().CommitionPercent??0;
            //                                model.Total = staff.SalesOrders.Sum(x => x.TotalAmount)??0;
            //                                model.Total = 0;
            //                                if(staff.SalesOrders != null && staff.SalesOrders.Count > 0)
            //                                {
            //                                    foreach(var salesOrder in staff.SalesOrders)
            //                                    {
            //                                        model.Total += salesOrder.Transactions.Sum(a => a.Amount) ?? 0;
            //                                    }
            //                                }
            //                                model.CommitionValue = (model.CommitionPercent / 100) * model.Total;
            //                                staffCommition.Add(model);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return Ok(staffCommition);
        }

        private List<StaffCommitionListModel> StaffCommissionProcess(List<StaffMember> Staffs)
        {
            var staffCommition = new List<StaffCommitionListModel>();
            if (Staffs != null && Staffs.Count > 0)
            {
                foreach (var staff in Staffs)
                {
                    if (staff.IsActive == true)
                    {
                        var model = new StaffCommitionListModel();
                        model.BranchNumber = staff.User?.Branch?.BranchCode;
                        model.Category = "A";
                        model.EmployeeCode = "emp001";
                        model.Staff = staff.User.FirstName + " " + staff.User.LastName;
                        model.SalesId = staff.Id;
                        model.CommitionPercent = staff.StaffCommitions?.FirstOrDefault().CommitionPercent ?? 0;
                        model.Total = staff.SalesOrders.Sum(x => x.TotalAmount) ?? 0;
                        model.Total = 0;
                        if (staff.SalesOrders != null && staff.SalesOrders.Count > 0)
                        {
                            foreach (var salesOrder in staff.SalesOrders)
                            {
                                model.Total += salesOrder.Transactions.Sum(a => a.Amount) ?? 0;
                            }
                        }
                        model.CommitionValue = (model.CommitionPercent / 100) * model.Total;
                        staffCommition.Add(model);
                    }
                }
            }
            return staffCommition;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gnr/getAll")]
        public IHttpActionResult GNRReport()//Reciept Report
        {
            List<GNRReciept> list = new List<GNRReciept>();
            var gnr = db.ReceiveOrders.Where(x => x.IsActive == true).ToList();
            if(gnr != null && gnr.Count > 0)
            {
                foreach(var item in gnr)
                {
                    GNRReciept model = new GNRReciept();
                    model.RecieptNumber = item.ReceiptNumber;
                    model.TotalQuantity = item.TotalQuantity??0;
                    model.TotalCost = item.TotalCost??0;
                    model.TotalVat = item.TotalVAT??0;
                    model.RecieptDate = item.ReceiptDate;
                    list.Add(model);
                }
            }
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dailySellbyBranch/getAll")]                                    //Time Method is day month and year
        public IHttpActionResult dailySell(int BranchId, string date, string TimeMethod)//Reciept Report
        {
            //BranchId = 1;
            var SalesOrderItemId = new List<int>();
            List<DailySellReport> list = new List<DailySellReport>();

            var res = db.SalesOrders.Where(x => x.IsActive == true && x.BranchId == BranchId)
                                .Include(i=>i.SalesOrderItems)
                                .Include(i => i.SalesOrderItems.Select(s => s.Product))
                                .Include(i=>i.ReleaseLayBayItems)
                                .Include(i=> i.Transactions);
            //.ToList();
            var restemp = res.ToList();
            if(date != null)
            {
                if(TimeMethod.ToLower() == "day")
                {
                    var dateonly = Convert.ToDateTime(date).Date;
                    var nextdate = dateonly.AddDays(1);
                    res = res.Where(x => x.TransactionDate >= dateonly && x.TransactionDate < nextdate);
                    restemp = res.ToList();
                }
                else if (TimeMethod.ToLower() == "month")
                {
                    var startDate = Convert.ToDateTime(date).Date.AddMonths(-1);
                    var endDate = Convert.ToDateTime(date).Date.AddDays(1);
                    res = res.Where(x => x.TransactionDate >= startDate && x.TransactionDate < endDate);
                    restemp = res.ToList();
                }
                else if (TimeMethod.ToLower() == "year")
                {
                    var startDate = Convert.ToDateTime(date).Date.AddYears(-1);
                    var endDate = Convert.ToDateTime(date).Date.AddDays(1);
                    res = res.Where(x => x.TransactionDate >= startDate && x.TransactionDate < endDate);
                    restemp = res.ToList();
                }
            }

            //var Cash = res.Where(x=>x.SaleType.ToLower().Equals("cash")).ToList();
            //var LayBaySale = res.Where(x => x.SaleType.ToLower().Contains("laybay")).ToList();
            var result = res.ToList();
            //var CashList = DailySellProcess(Cash);
            //var LayBaySaleList = DailySellProcess(LayBaySale);
            var ok = DailySellProcess(result).Select(x=>x.Value).ToList();
            return Ok(ok);
        }
        private Dictionary<int,DailySellReport> DailySellProcess(List<SalesOrder> result)
        {
            Dictionary<int,DailySellReport> list = new Dictionary<int, DailySellReport>();
            var SalesOrderItemId = new List<int>();

            var cash = result.Where(x => x.SaleType.ToLower().Contains("cash")).ToList();
            var laybay = result.Where(x => x.SaleType.ToLower().Contains("laybay")).ToList();

            Dictionary<int, int> lay = new Dictionary<int, int>();
            List<ReleaseProduct> rProduct = new List<ReleaseProduct>();
            
            if(laybay != null && laybay.Count > 0)
            {

                foreach(var item in laybay)
                {
                    decimal TotalPaidAmount = 0;
                    decimal TotalOriginalAmount = 0;
                    if (item.ReleaseLayBayItems != null && item.ReleaseLayBayItems.Count > 0)
                    {
                        foreach(var itemj in item.ReleaseLayBayItems)
                        {
                            lay.Add(itemj.ProductId??0,itemj.SalesOrderId??0);
                        }
                    }

                    if (item.Transactions != null && item.Transactions.Count > 0)
                    {
                        TotalPaidAmount = item.Transactions.Sum(s=>s.Amount)??0;
                    }
                    if (item != null && item.SalesOrderItems != null && item.SalesOrderItems.Count > 0)
                    {
                        item.SalesOrderItems.Sum(x => x.TotalPrice);
                        foreach(var itemj in item.SalesOrderItems)
                        {
                            var releaseitem = new Model.ReleaseProduct();
                            releaseitem.ProductId = itemj.ProductId;
                            releaseitem.Quantity = itemj.Quantity;
                            releaseitem.Amount = itemj.PricePerUnit;
                            releaseitem.TotalAmount = itemj.TotalPrice;
                            releaseitem.SalesOrderId = itemj.SalesOrderId;
                            if(lay.Count > 0)
                            {
                                var dec = lay.Where(x => x.Key == releaseitem.ProductId && x.Value == releaseitem.SalesOrderId).FirstOrDefault();
                                if(dec.Key > 0)
                                {
                                    releaseitem.IsRelease = true;
                                    releaseitem.PaidAmount = releaseitem.TotalAmount;
                                }
                                else
                                {
                                    releaseitem.IsRelease = false;
                                }
                            }
                            else { releaseitem.IsRelease = false; }
                            rProduct.Add(releaseitem);
                        }
                    }
                    if(rProduct != null && rProduct.Count > 0)
                    {
                        var sum = rProduct.Where(x => x.IsRelease == true).Sum(x => x.PaidAmount);
                        var pd = rProduct.Where(x => x.IsRelease == false).OrderBy(x => x.TotalAmount).ToList();
                        foreach(var itemk in pd)
                        {
                            if(TotalPaidAmount > sum)
                            {
                                var amount = TotalPaidAmount - sum;
                                if(amount >= itemk.TotalAmount)
                                {
                                    itemk.PaidAmount = itemk.TotalAmount;
                                    sum = sum + itemk.TotalAmount;
                                }
                                else
                                {
                                    itemk.PaidAmount = amount;
                                    sum = sum + amount;
                                }
                            }
                        }
                    }
                }
            }


            SalesOrderItemId = getSalesOrderId(result);
            var cashSaleOrderItemId = getSalesOrderId(cash);
            var laybaySaleOrderItemId = getSalesOrderId(laybay);

            var modell = db.SalesOrderItems.Where(x => x.IsActive == true && SalesOrderItemId.Contains(x.Id)).GroupBy(x => x.ProductId).ToList();
            var modellcash = db.SalesOrderItems.Where(x => x.IsActive == true && cashSaleOrderItemId.Contains(x.Id)).GroupBy(x => x.ProductId).ToList();
            var modelllaybay = db.SalesOrderItems.Where(x => x.IsActive == true && laybaySaleOrderItemId.Contains(x.Id)).GroupBy(x => x.ProductId).ToList();

            if (modellcash != null && modellcash.Count > 0)
            {
                foreach (var item in modellcash)
                {
                    var obj = new DailySellReport();
                    obj.Product = item.FirstOrDefault().Product.ProductSKU + " " + item.FirstOrDefault().Product.StyleSKU;
                    obj.SalesQuantity = item.ToList().Sum(s => s.Quantity) ?? 0;
                    obj.SalesPrice = item.ToList().Sum(s => s.TotalPrice) ?? 0;
                    list.Add(item.FirstOrDefault().ProductId??0,obj);
                }
            }
            if (modelllaybay != null && modelllaybay.Count > 0)
            {
                foreach (var item in modelllaybay)
                {
                    var model = list.Where(x => x.Key == item.Key).FirstOrDefault();
                    var model2 = list.Where(x => x.Key == 0).FirstOrDefault();
                    if (model.Key == 0)
                    {
                        var obj = new DailySellReport();
                        obj.Product = item.FirstOrDefault().Product.ProductSKU + " " + item.FirstOrDefault().Product.StyleSKU;
                        obj.LayBayQuantity = item.ToList().Sum(s => s.Quantity) ?? 0;
                        obj.LayBayPrice = rProduct.Where(x => x.ProductId == model.Key).Sum(s => s.PaidAmount)??0;
                        obj.TotalPrice = obj.SalesPrice + obj.LayBayPrice;
                        obj.TotalQuantity = obj.SalesQuantity + obj.LayBayQuantity;
                        list.Add(item.FirstOrDefault().ProductId ?? 0, obj);
                    }
                    else
                    {
                        var value = list[model.Key];
                        value.LayBayQuantity = item.ToList().Sum(s => s.Quantity) ?? 0;
                        value.LayBayPrice = rProduct.Where(x => x.ProductId == model.Key).Sum(s => s.PaidAmount) ?? 0;
                        value.TotalPrice = value.SalesPrice + value.LayBayPrice;
                        value.TotalQuantity = value.SalesQuantity + value.LayBayQuantity;
                        list[model.Key] = value;
                    }

                }
            }
            return list;
        }
        private List<int> getSalesOrderId(List<SalesOrder> result)
        {
            List<int> listint = new List<int>();
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    item.SalesOrderItems.ToList().ForEach(x => listint.Add(x.Id));
                }
            }
            return listint;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dailySellSummary/getAll")]
        public IHttpActionResult DailySellSummary(string date,string TimeMethod)
        {
            var branches = db.Branches.Where(x => x.IsActive == true).ToList();
            var branchesId = branches.Select(x => x.Id).ToList();


            var salesorders = db.SalesOrders.Where(x => branchesId.Contains(x.BranchId ?? 0) && x.IsActive == true)
                                            .Include(i=>i.SalesOrderItems)
                                            .Include(i=>i.Transactions)
                                            .ToList();
            if (date != null)
            {
                if (TimeMethod.ToLower() == "day")
                {
                    var dateonly = Convert.ToDateTime(date).Date;
                    var nextdate = dateonly.AddDays(1);
                    salesorders = salesorders.Where(x => x.TransactionDate >= dateonly && x.TransactionDate < nextdate).ToList();
                }
                else if (TimeMethod.ToLower() == "month")
                {
                    var startDate = Convert.ToDateTime(date).Date.AddMonths(-1);
                    var endDate = Convert.ToDateTime(date).Date.AddDays(1);
                    salesorders = salesorders.Where(x => x.TransactionDate >= startDate && x.TransactionDate < endDate).ToList();
                }
                else if (TimeMethod.ToLower() == "year")
                {
                    var startDate = Convert.ToDateTime(date).Date.AddYears(-1);
                    var endDate = Convert.ToDateTime(date).Date.AddDays(1);
                    salesorders = salesorders.Where(x => x.TransactionDate >= startDate && x.TransactionDate < endDate).ToList();
                }
            }

            //.Include(i=>i.SalesOrders)
            //.Include(i=>i.SalesOrders.Select(s=>s.SalesOrderItems.Select(v=>v.Product)))
            //.Include(i=>i.SalesOrders.Select(s=>s.Transactions))
            //.ToList();
            List<DailySellSummaryModel> list = new List<DailySellSummaryModel>();
            if(branches != null && branches.Count > 0)
            {
                foreach(var item in branches)
                {
                    var SalesOrders = salesorders.Where(x => x.BranchId == item.Id).ToList();
                    if (SalesOrders != null && SalesOrders.Count > 0)
                    {
                        var cashsell = SalesOrders.Where(x => x.SaleType.ToLower().Contains("cash")).ToList();
                        var laybaysell = SalesOrders.Where(x => x.SaleType.ToLower().Contains("laybay")).ToList();

                        var cashsellAmount = DailySellSummaryProcess(cashsell);
                        var laybayAmount = DailySellSummaryProcess(laybaysell);

                        DailySellSummaryModel obj = new DailySellSummaryModel();
                        obj.BranchId = item.Id;
                        obj.BranchCode = item.BranchCode;
                        obj.CashSell = cashsellAmount;
                        obj.LayBaySell = laybayAmount;
                        obj.Total = obj.CashSell + obj.LayBaySell;
                        list.Add(obj);
                    }
                    else
                    {
                        DailySellSummaryModel obj = new DailySellSummaryModel();
                        obj.BranchCode = item.BranchCode;
                        obj.BranchId = item.Id;
                        list.Add(obj);
                    }
                }
            }
            return Ok(list.OrderByDescending(x=>x.Total));
        }

        private decimal DailySellSummaryProcess(List<SalesOrder> model)
        {
            decimal sum = 0;
            if (model != null)
            {
                foreach (var item in model)
                {
                    if (item.Transactions != null)
                    {
                        foreach (var itemj in item.Transactions)
                        {
                            sum += itemj.Amount ?? 0;
                        }
                    }
                }
            }
            return sum;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("outstandingPurchaseOrderReport")]
        public IHttpActionResult getDataAll()
        {
            List<OutstandingPurchaseOrderReport> modellist = new List<OutstandingPurchaseOrderReport>();
            List<OutstandingPurchaseOrderReport> temp = new List<OutstandingPurchaseOrderReport>();
            var list = db.PurchaseOrders.Where(x => x.IsActive == true)
                                        .Include(i=>i.PurchaseOrderItems)
                                        .Include(i=>i.PurchaseOrderItems.Select(s=>s.Product))
                                        .Include(i=>i.ReceiveOrders)
                                        .Include(i=>i.ReceiveOrders.Select(s=>s.ReceiptOrderItems))
                                        .Include(i=>i.ReceiveOrders.Select(s=>s.ReceiptOrderItems.Select(v=>v.Product)))
                                        .Include(i=>i.Supplier)
                                        .ToList();
            var poitem = OutstandingPurchaseOrderProcessing1(list);//po = purchase order
            var roitem = OutstandingPurchaseOrderProcessing2(list);// ro = recieve order
            if(poitem != null)
            {
                foreach(var item in poitem)
                {
                    var ro = roitem.Where(x => x.ProductId == item.ProductId && x.SupplierId == item.SupplierId && x.PurchaseOrderId == item.PurchaseOrderId).ToList();
                    item.Quantity = item.Quantity - ro.Sum(s=>s.Quantity);
                    modellist.Add(item);
                }
            }
            return Ok(modellist);
        }

        private List<OutstandingPurchaseOrderReport> OutstandingPurchaseOrderProcessing1(List<PurchaseOrder> list)
        {
            List<OutstandingPurchaseOrderReport> modellist = new List<OutstandingPurchaseOrderReport>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.PurchaseOrderItems != null)
                    {
                        foreach (var itemj in item.PurchaseOrderItems)
                        {
                            OutstandingPurchaseOrderReport obj = new OutstandingPurchaseOrderReport();
                            obj.ProductId = itemj.ProductID??0;
                            obj.SupplierId = item.SupplierId;
                            obj.PurchaseOrderId = item.ID;

                            obj.PurchaseOrderNumber = item.OrderNumber;
                            obj.Product = itemj.Product.ProductSKU + " " + itemj.Product.StyleSKU;
                            obj.Supplier = item.Supplier.Name;
                            obj.Quantity = Utility.getKeyVaue(itemj).Where(x => x.Key.ToLower().Contains("quantity")).Sum(x => Convert.ToInt32(x.Value));
                            modellist.Add(obj);
                        }
                    }
                }
            }
            return modellist;
        }
        private List<OutstandingPurchaseOrderReport> OutstandingPurchaseOrderProcessing2(List<PurchaseOrder> list)
        {
            List<OutstandingPurchaseOrderReport> modellist = new List<OutstandingPurchaseOrderReport>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.ReceiveOrders != null)
                    {
                        foreach (var itemj in item.ReceiveOrders)
                        {
                            if(itemj.ReceiptOrderItems != null)
                            {
                                foreach(var itemk in itemj.ReceiptOrderItems)
                                {
                                    OutstandingPurchaseOrderReport obj = new OutstandingPurchaseOrderReport();
                                    obj.ProductId = itemk.ProductId ?? 0;
                                    obj.SupplierId = item.SupplierId;
                                    obj.PurchaseOrderId = item.ID;

                                    obj.PurchaseOrderNumber = item.OrderNumber;
                                    obj.Product = itemk.Product.ProductSKU + " " + itemk.Product.StyleSKU;
                                    obj.Supplier = item.Supplier.Name;
                                    obj.Quantity = Utility.getKeyVaue(itemk).Where(x => x.Key.ToLower().Contains("quantity")).Sum(x => Convert.ToInt32(x.Value));
                                    modellist.Add(obj);
                                }
                            }
                        }
                    }

                }
            }
            return modellist;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("transactionEnquiry")]
        public IHttpActionResult TransactionEnquiryReport(HelpReportModel model)
        {
            

            var _modellist = db.Branches.Where(x => x.IsActive == true);
            if(!string.IsNullOrEmpty(model.branchFrom) && !string.IsNullOrEmpty(model.branchTo))
            {
                var bfrom = Convert.ToInt32(model.branchFrom);
                var bto = Convert.ToInt32(model.branchTo);
                var bcodelist = Utility.getRangeList(bfrom, bto, 3);
                _modellist = _modellist.Where(x=> bcodelist.Contains(x.BranchCode));
            }

            var modellist = _modellist.ToList();//db.Branches.Where(x => x.IsActive == true && bcodelist.Contains(x.BranchCode)).ToList();//.Select(s => s.Id).ToList();
                                                                                //.Include(i => i.SalesOrders)
                                                                                //.Include(i => i.SalesOrders.Select(s => s.SalesOrderItems))
                                                                                //.Include(i => i.SalesOrders.Select(s => s.SalesOrderItems.Select(p => p.Product)))
                                                                                //.Include(i => i.SalesOrders.Select(s => s.SalesOrderItems.Select(p => p.Product.Color)));
            var branchList = new List<Branch>();
            foreach (var item in modellist)
            {
                var _saleorder = db.SalesOrders.Where(x => x.IsActive == true && x.BranchId == item.Id);
                if(!string.IsNullOrEmpty(model.startDate) && !string.IsNullOrEmpty(model.endDate))
                {
                    var StartDate = Convert.ToDateTime(model.startDate).Date;
                    var EndDate = Convert.ToDateTime(model.endDate).AddDays(1).Date;
                    _saleorder = _saleorder.Where(x=>x.TransactionDate >= StartDate && x.TransactionDate < EndDate);
                }
                var SalesOrders = _saleorder.ToList();//db.SalesOrders.Where(x => x.IsActive == true && x.BranchId == item.Id).ToList();//.ToList();
                if(SalesOrders != null && SalesOrders.Count > 0)
                {
                    var salesOrderList = new List<SalesOrder>();
                    foreach(var salesOrder in SalesOrders)
                    {
                        var _salesOrderItem = db.SalesOrderItems.Where(x => x.IsActive == true && x.SalesOrderId == salesOrder.Id &&
                                                                        x.Product != null &&
                                                                        x.Product.IsActive == true &&
                                                                        x.Product.Color != null &&
                                                                        x.Product.Color.IsActive == true
                                                                        )
                                                                        .Include(i => i.Product)
                                                                        .Include(i => i.Product.Color);
                        if (!string.IsNullOrEmpty(model.productSKUFrom) && !string.IsNullOrEmpty(model.productSKUTo))
                        {
                            var bfrom = Convert.ToInt32(model.productSKUFrom);
                            var bto = Convert.ToInt32(model.productSKUTo);
                            var bcodelist = Utility.getRangeList(bfrom, bto, 3);
                            _salesOrderItem = _salesOrderItem.Where(x => bcodelist.Contains(x.Product.ProductSKU));
                        }
                        if (!string.IsNullOrEmpty(model.styleSKUFrom) && !string.IsNullOrEmpty(model.styleSKUFrom))
                        {
                            var bfrom = Convert.ToInt32(model.styleSKUFrom);
                            var bto = Convert.ToInt32(model.styleSKUTo);
                            var bcodelist = Utility.getRangeList(bfrom, bto, 4);
                            _salesOrderItem = _salesOrderItem.Where(x => bcodelist.Contains(x.Product.StyleSKU));
                        }
                        var salesOrderItem = _salesOrderItem.ToList();
                        salesOrder.SalesOrderItems = new List<SalesOrderItem>();
                        salesOrder.SalesOrderItems = salesOrderItem;
                        salesOrderList.Add(salesOrder);
                    }
                    item.SalesOrders = new List<SalesOrder>();
                    item.SalesOrders = salesOrderList;
                    branchList.Add(item);
                }
            }
            var Branches = modellist.ToList();
            var branch = TransactionEnquiryProcessing(branchList);


            return Ok(branch);
        }

        private List<TransactionEnquiryModel> TransactionEnquiryProcessing(List<Branch> Branches)
        {
            List<TransactionEnquiryModel> transaction = new List<TransactionEnquiryModel>();
            if (Branches != null && Branches.Count > 0)
            {
                foreach (var list in Branches)
                {
                    string branch = list.BranchCode;
                    if (list.SalesOrders != null && list.SalesOrders.Count > 0)
                    {
                        foreach (var item in list.SalesOrders)
                        {
                            if (item.SalesOrderItems != null && item.SalesOrderItems.Count > 0)
                            {
                                foreach (var itemj in item.SalesOrderItems)
                                {
                                    TransactionEnquiryModel temodel = new TransactionEnquiryModel();
                                    temodel.Branch = branch;
                                    temodel.Color = itemj.Product?.Color?.ColorShort;
                                    temodel.ProductSKU = itemj.Product.ProductSKU;
                                    temodel.StyleSKU = itemj.Product.StyleSKU;
                                    temodel.StyleSKU = itemj.Product.StyleSKU;
                                    temodel.Description = item.SaleType;
                                    temodel.OrderNumber = item.OrderNumber;
                                    temodel.Quantity = itemj.Quantity ?? 0;
                                    temodel.Item = item.SalesOrderItems.Count;
                                    temodel.Price = itemj.PricePerUnit ?? 0;
                                    temodel.Cost = itemj.Product.CostPrice ?? 0;
                                    temodel.Size = itemj.ProductSize;
                                    temodel.Date = item.TransactionDate.Value.ToShortDateString();
                                    transaction.Add(temodel);
                                }
                            }
                        }
                    }
                }
            }
            
            return transaction;
        }


            //[HttpGet]
            //[AllowAnonymous]
            //[Route("tableDate/getAll")]
            //public IHttpActionResult getDataAll(string tableName)
            //{
            //    var list = getTable<Branch>(tableName).ToList();
            //    return Ok(list);
            //}
            //private List<T> getTable<T>(string tableName)
            //{
            //    List<T> list = new List<T>();
            //    using (var ctx = new GrandShoesEntities())
            //    {
            //        //Get student name of string type
            //        list = ctx.Database.SqlQuery<T>("Select * from "+tableName.RemoveWhiteSpace())
            //                                .ToList();

            //        //or
            //        //var student = ctx.Database.SqlQuery<Branch>("Select studentname from Student where studentid=@id", new SqlParameter("@id", 1))
            //        //  .FirstOrDefault();
            //    }
            //    return list;
            //}
        }
}
