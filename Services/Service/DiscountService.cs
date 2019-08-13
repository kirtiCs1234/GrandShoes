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
    public class DiscountService:IDiscountService
    {
        public DiscountSummaryModel Create(DiscountSummaryModel discount)
        {
            var list2 = new List<ProductModel>();
            var intList = new List<int>();
            var list = new List<ProductModel>();
            var x = Convert.ToInt32(discount.FromProductSKU);
            var y = Convert.ToInt32(discount.ToProductSKU);
            for(var i = x; i <= y; i++)
            {
                var str = "";
                if (i < 10)
                {
                    str = "00" + i;
                    
                }
                else if ( i < 100)
                {
                    str = "0" + i;
                }
                else 
                {
                    str = "" + i;
                }
                var ProductList = ServerResponse.Invoke<List<ProductModel>>("api/product/getByProductSKU?productSku=" + str, "", "GET");
                list.AddRange(ProductList);
            }
            var style1 = Convert.ToInt32(discount.FromStyleSKU);
            var style2 = Convert.ToInt32(discount.ToStyleSKU);
            for(int j = style1; j <= style2; j++)
            {
                var str1 = "";
                if (j < 10)
                {
                    str1 = "000" + j;
                }
                else if (j < 100)
                {
                    str1 = "00" + j;
                }
                else if (j < 1000)
                {
                    str1 = "0" + j;
                }
                else
                {
                    str1 = "" + j;
                }
                var listFF = list.Where(m => m.StyleSKU.Contains(str1)).ToList();
                list2.AddRange(listFF);
            }
            if(discount.FromPrice!=null && discount.ToPrice != null)
            {
                list2 = list2.Where(ym => Convert.ToDecimal(ym.CostPrice)>=Convert.ToDecimal(discount.FromPrice) && Convert.ToDecimal(ym.CostPrice) <= Convert.ToDecimal(discount.ToPrice)).ToList();
            }
            foreach (var item in list2)
            {
                var id = item.Id;
                intList.Add(id);
            }
            string[] value=intList.Select(i => i.ToString()).ToArray();
            discount.ProductList = value; ;
            var create = ServerResponse.Invoke<DiscountSummaryModel>("api/discount/create", JsonConvert.SerializeObject(discount), "POST");
            return create;
        }
        public List<PromotionalDiscountModel> GetProductList(int? id)
        {
            return ServerResponse.Invoke<List<PromotionalDiscountModel>>("api/promotionalDiscount/getProductList?Id=" + id, "", "Get");
        }
        public List<DiscountModel> GetAll()
        {
            var DiscountList = ServerResponse.Invoke<List<DiscountModel>>("api/discount/getDetails", "", "GET");
            var BranchDiscountList = ServerResponse.Invoke<List<BranchDiscountModel>>("api/branchDiscount/getDetails", "", "GET");
            foreach (var item in DiscountList)
            {
                var BranchList = BranchDiscountList.Where(x => x.DiscountID == item.Id);

                List<string> termsList = new List<string>();
                foreach (var b in BranchList)
                {
                    termsList.Add(b.Branch.Name);
                }
                item.Branches = termsList.Aggregate((s1, s2) => s1 + "," + s2);
            }

            return DiscountList;
        }
        public bool AllowDiscount(PromotionalDiscountModel model)
        {
            return ServerResponse.Invoke<bool>("api/promotionalDiscount/allow", JsonConvert.SerializeObject(model), "POST");
        }
        public List<DiscountSummaryModel> GetDataSummary()
        {
            return ServerResponse.Invoke<List<DiscountSummaryModel>>("api/discountSummary/getDetails", "", "Get");
        }
        public List<PromotionalDiscountModel> GetProtionalDiscount(int? id)
        {
            return ServerResponse.Invoke<List<PromotionalDiscountModel>>("api/discountSummary/getPromotional?id=" + id, "", "GET");
        }
        public DiscountSummaryModel GetByIdSummary(int? id)
        {
            return ServerResponse.Invoke<DiscountSummaryModel>("api/discountSummary/getDetail?id=" + id, "", "GET");
        }
        public bool Delete(DiscountSummaryModel model)
        {
            return ServerResponse.Invoke<bool>("api/discountSummary/delete", JsonConvert.SerializeObject(model), "POST");
        }
    }
}
