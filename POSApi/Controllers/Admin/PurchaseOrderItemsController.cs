using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DAL;
using System.Data.Entity;
using Model;
using System.Web.Http.Description;
using Helper.ExtensionMethod;
using System.Data.Entity.Infrastructure;
using Helper;
using Newtonsoft.Json;

namespace POSApi.Controllers
{
    [RoutePrefix("api/PurchaseOrderItems")]
    public class PurchaseOrderItemsController : ApiController
    {
        private GrandShoesEntities Entities = new GrandShoesEntities();
        public PurchaseOrderItemsController()
        {
            Entities.Configuration.LazyLoadingEnabled = false;
            Entities.Configuration.ProxyCreationEnabled = false;
        }
        [Route("AddPurchaseOrderItems")]
        // Post: AddPurchaseOrderItems
        public IHttpActionResult AddPurchaseOrderItems(PurchaseOrderItemModel OrderItem)
        {
			
			DAL.PurchaseOrderItem Order = new DAL.PurchaseOrderItem();

            Order.PurchaseOrderId = OrderItem.PurchaseOrderId;
            var Product = Entities.Products.Where(x => x.IsActive == true && x.ProductSKU == OrderItem.ProductSKU && x.StyleSKU == OrderItem.StyleSKU).Include(x=>x.SizeGrid).FirstOrDefault();
            Order.ProductID= Product.Id;
            var sizeGrid = Entities.SizeGrids.Where(x => x.Id == Product.SizeGridID).FirstOrDefault();
            Order.Amount = (OrderItem.Amount)??0;
          // Order.ProductStyleId = OrderItem.ProductStyleId;
            Order.SizeGridId = OrderItem.SizeGridId;
           Order.ColorId = Entities.Colors.Where(x=>x.IsActive==true && x.Code==OrderItem.autoCompleteColorName).FirstOrDefault().Id;
            Order.SuplierStyle = OrderItem.SuplierStyle;
            var dict = new Dictionary<string, string>();
            var keyval = Utility.getKeyVaue(sizeGrid).Where(x => x.Key.Contains("Z")).ToList();
            foreach (var item2 in keyval)
            {
                if (!string.IsNullOrEmpty(item2.Value))
                {
                    if (item2.Value.Contains(".0"))
                    {
                        //item2[item2.Key] = 	item2.Value.Replace(".0", "");
                        dict.Add(item2.Key, item2.Value.Replace(".0", ""));
                    }
                    else
                    {
                        dict.Add(item2.Key, item2.Value);
                    }
                }
            }
            var ItemDict = new Dictionary<string, string>();
            var CostDict = new Dictionary<string, string>();
            var QuantityDict = new Dictionary<string, string>();
           var dataItemSize= Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("ItemSize")).ToList();
            foreach(var item in dataItemSize)
            {
                if (item.Value !="")
                {
                    var data = dict.Where(x => x.Value == item.Value).FirstOrDefault();
                    var data1 = data.Key;
                    var b = "";
                    var a = data1.Substring(0, 2);
                    if (a == "Z0")
                    {
                        b = data1.Replace("Z0", "");
                    }
                    else
                    {
                        b=data1.Replace("Z","");
                    }
                    var itemKey = Utility.getKeyVaue(Order).Where(x => x.Key.Contains("ItemSize")).ToList();
                    var value = itemKey.Where(x => x.Key.Replace("ItemSize", "") == b).FirstOrDefault();
                    ItemDict.Add(value.Key, data.Value);
                    var CostKey1 = Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("CostSize")).ToList();
                    var costValue1= CostKey1.Where(x => x.Key.Replace("CostSize", "") == item.Key.Replace("ItemSize", "")).FirstOrDefault();
                    var CostKey= Utility.getKeyVaue(Order).Where(x => x.Key.Contains("CostSize")).ToList();
                    var costValue = CostKey.Where(x => x.Key.Replace("CostSize", "") == value.Key.Replace("ItemSize", "")).FirstOrDefault();
                    CostDict.Add(costValue.Key, costValue1.Value);
                    var QuantityKey1 = Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("QuantitySize")).ToList();
                    var QuantityValue1 = QuantityKey1.Where(x => x.Key.Replace("QuantitySize", "") == item.Key.Replace("ItemSize", "")).FirstOrDefault();

                    var QuantityKey = Utility.getKeyVaue(Order).Where(x => x.Key.Contains("QuantitySize")).ToList();
                    var QuantityValue = QuantityKey.Where(x => x.Key.Replace("QuantitySize", "") ==value.Key.Replace("ItemSize", "")).FirstOrDefault();
                    QuantityDict.Add(QuantityValue.Key, QuantityValue1.Value);
                }
            }
            Order.ItemSize1 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize1").FirstOrDefault().Value);
            Order.ItemSize2 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize2").FirstOrDefault().Value);
            Order.ItemSize3 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize3").FirstOrDefault().Value);
            Order.ItemSize4 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize4").FirstOrDefault().Value);
            Order.ItemSize5 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize5").FirstOrDefault().Value);
            Order.ItemSize6 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize6").FirstOrDefault().Value);
            Order.ItemSize7 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize7").FirstOrDefault().Value);
            Order.ItemSize8 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize8").FirstOrDefault().Value);
            Order.ItemSize9 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize9").FirstOrDefault().Value);
            Order.ItemSize10 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize10").FirstOrDefault().Value);
            Order.ItemSize11= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize11").FirstOrDefault().Value);
            Order.ItemSize12= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize12").FirstOrDefault().Value);
            Order.ItemSize13= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize13").FirstOrDefault().Value);
            Order.ItemSize14= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize14").FirstOrDefault().Value);
            Order.ItemSize15= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize15").FirstOrDefault().Value);
            Order.ItemSize16= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize16").FirstOrDefault().Value);
            Order.ItemSize17= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize17").FirstOrDefault().Value);
            Order.ItemSize18= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize18").FirstOrDefault().Value);
            Order.ItemSize19= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize19").FirstOrDefault().Value);
            Order.ItemSize20= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize20").FirstOrDefault().Value);
            Order.ItemSize21= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize21").FirstOrDefault().Value);
            Order.ItemSize22= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize22").FirstOrDefault().Value);
            Order.ItemSize23= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize23").FirstOrDefault().Value);
            Order.ItemSize24= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize24").FirstOrDefault().Value);
            Order.ItemSize25= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize25").FirstOrDefault().Value);
            Order.ItemSize26= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize26").FirstOrDefault().Value);
            Order.ItemSize27= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize27").FirstOrDefault().Value);
            Order.ItemSize28= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize28").FirstOrDefault().Value);
            Order.ItemSize29= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize29").FirstOrDefault().Value);
            Order.ItemSize30= Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize30").FirstOrDefault().Value);
            Order.CostSize1 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize1").FirstOrDefault().Value);
            Order.CostSize2 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize2").FirstOrDefault().Value);
            Order.CostSize3 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize3").FirstOrDefault().Value);
            Order.CostSize4 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize4").FirstOrDefault().Value);
            Order.CostSize5 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize5").FirstOrDefault().Value);
            Order.CostSize6 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize6").FirstOrDefault().Value);
            Order.CostSize7 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize7").FirstOrDefault().Value);
            Order.CostSize8 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize8").FirstOrDefault().Value);
            Order.CostSize9 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize9").FirstOrDefault().Value);
            Order.CostSize10 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize10").FirstOrDefault().Value);
            Order.CostSize11= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize11").FirstOrDefault().Value);
            Order.CostSize12= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize12").FirstOrDefault().Value);
            Order.CostSize13= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize13").FirstOrDefault().Value);
            Order.CostSize14= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize14").FirstOrDefault().Value);
            Order.CostSize15= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize15").FirstOrDefault().Value);
            Order.CostSize16= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize16").FirstOrDefault().Value);
            Order.CostSize17= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize17").FirstOrDefault().Value);
            Order.CostSize18= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize18").FirstOrDefault().Value);
            Order.CostSize19= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize19").FirstOrDefault().Value);
            Order.CostSize20= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize20").FirstOrDefault().Value);
            Order.CostSize21= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize21").FirstOrDefault().Value);
            Order.CostSize22= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize22").FirstOrDefault().Value);
            Order.CostSize23= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize23").FirstOrDefault().Value);
            Order.CostSize24= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize24").FirstOrDefault().Value);
            Order.CostSize25= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize25").FirstOrDefault().Value);
            Order.CostSize26= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize26").FirstOrDefault().Value);
            Order.CostSize27= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize27").FirstOrDefault().Value);
            Order.CostSize28= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize28").FirstOrDefault().Value);
            Order.CostSize29= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize29").FirstOrDefault().Value);
            Order.CostSize30= Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize30").FirstOrDefault().Value);
            Order.QuantitySize1 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize1").FirstOrDefault().Value);
            Order.QuantitySize2 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize2").FirstOrDefault().Value);
            Order.QuantitySize3 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize3").FirstOrDefault().Value);
            Order.QuantitySize4 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize4").FirstOrDefault().Value);
            Order.QuantitySize5 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize5").FirstOrDefault().Value);
            Order.QuantitySize6 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize6").FirstOrDefault().Value);
            Order.QuantitySize7 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize7").FirstOrDefault().Value);
            Order.QuantitySize8 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize8").FirstOrDefault().Value);
            Order.QuantitySize9 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize9").FirstOrDefault().Value);
            Order.QuantitySize10 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize10").FirstOrDefault().Value);
            Order.QuantitySize11= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize11").FirstOrDefault().Value);
            Order.QuantitySize12= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize12").FirstOrDefault().Value);
            Order.QuantitySize13= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize13").FirstOrDefault().Value);
            Order.QuantitySize14= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize14").FirstOrDefault().Value);
            Order.QuantitySize15= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize15").FirstOrDefault().Value);
            Order.QuantitySize16= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize16").FirstOrDefault().Value);
            Order.QuantitySize17= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize17").FirstOrDefault().Value);
            Order.QuantitySize18= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize18").FirstOrDefault().Value);
            Order.QuantitySize19= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize19").FirstOrDefault().Value);
            Order.QuantitySize20= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize20").FirstOrDefault().Value);
            Order.QuantitySize21= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize21").FirstOrDefault().Value);
            Order.QuantitySize22= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize22").FirstOrDefault().Value);
            Order.QuantitySize23= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize23").FirstOrDefault().Value);
            Order.QuantitySize24= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize24").FirstOrDefault().Value);
            Order.QuantitySize25= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize25").FirstOrDefault().Value);
            Order.QuantitySize26= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize26").FirstOrDefault().Value);
            Order.QuantitySize27= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize27").FirstOrDefault().Value);
            Order.QuantitySize28= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize28").FirstOrDefault().Value);
            Order.QuantitySize29= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize29").FirstOrDefault().Value);
            Order.QuantitySize30= Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize30").FirstOrDefault().Value);
            Order.IsActive = true;
            Entities.PurchaseOrderItems.Add(Order);
            Entities.SaveChanges();
            var purchaseOrderItem = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId== OrderItem.PurchaseOrderId).ToList();
            int TotalQuantity = 0;
            decimal TotalAmount = 0;
            foreach (var a in purchaseOrderItem)
            {
                TotalQuantity += (a.QuantitySize1 ?? 0) + (a.QuantitySize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) + (a.QuantitySize4 ?? 0) + (a.QuantitySize5 ?? 0) + (a.QuantitySize6 ?? 0) + (a.QuantitySize7 ?? 0) + (a.QuantitySize8 ?? 0) + (a.QuantitySize9 ?? 0) +
                    (a.QuantitySize10 ?? 0) + (a.QuantitySize10 ?? 0) + (a.QuantitySize11 ?? 0) + (a.QuantitySize12 ?? 0) +
                    (a.QuantitySize13 ?? 0) + (a.QuantitySize14 ?? 0) + (a.QuantitySize15 ?? 0) + (a.QuantitySize16 ?? 0) +
                    (a.QuantitySize17 ?? 0) + (a.QuantitySize18 ?? 0) + (a.QuantitySize19 ?? 0) + (a.QuantitySize20 ?? 0) +
                    (a.QuantitySize21 ?? 0) + (a.QuantitySize22 ?? 0) + (a.QuantitySize23 ?? 0 )+ (a.QuantitySize24 ?? 0) +
                    (a.QuantitySize25 ?? 0) + (a.QuantitySize26 ?? 0)+ (a.QuantitySize27 ?? 0) + (a.QuantitySize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) + (a.QuantitySize30 ?? 0);
                TotalAmount += (a.QuantitySize1 ?? 0) * (a.CostSize1 ?? 0) + (a.QuantitySize2 ?? 0) * (a.CostSize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) * (a.CostSize3 ?? 0) + (a.QuantitySize4 ?? 0) * (a.CostSize4 ?? 0) +
                    (a.QuantitySize5 ?? 0) * (a.CostSize5 ?? 0) + (a.QuantitySize6 ?? 0) * (a.CostSize6 ?? 0) + (a.QuantitySize7 ?? 0) * (a.CostSize7 ?? 0) +
                    (a.QuantitySize8 ?? 0) * (a.CostSize8 ?? 0) + (a.QuantitySize9 ?? 0) * (a.CostSize10 ?? 0) +
                    (a.QuantitySize11 ?? 0) * (a.CostSize12 ?? 0) + (a.QuantitySize13 ?? 0) * (a.CostSize13 ?? 0) +
                    (a.QuantitySize14 ?? 0) * (a.CostSize14 ?? 0) + (a.QuantitySize15 ?? 0) * (a.CostSize15 ?? 0) +
                    (a.QuantitySize16 ?? 0) * (a.CostSize16 ?? 0) + (a.QuantitySize17 ?? 0) * (a.CostSize17 ?? 0) +
                    (a.QuantitySize18 ?? 0) * (a.CostSize18 ?? 0) + (a.QuantitySize19 ?? 0) * (a.CostSize19 ?? 0) +
                    (a.QuantitySize20 ?? 0) * (a.CostSize20 ?? 0) + (a.QuantitySize21 ?? 0) * (a.CostSize21 ?? 0) +
                    (a.QuantitySize22 ?? 0) * (a.CostSize22 ?? 0) + (a.QuantitySize23 ?? 0) * (a.CostSize23 ?? 0) +
                    (a.QuantitySize24 ?? 0) * (a.CostSize24 ?? 0) + (a.QuantitySize25 ?? 0) * (a.CostSize25 ?? 0) +
                    (a.QuantitySize26 ?? 0) * (a.CostSize26 ?? 0) + (a.QuantitySize27 ?? 0) * (a.CostSize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) * (a.CostSize29 ?? 0) + (a.QuantitySize30 ?? 0) * (a.CostSize30 ?? 0);
                                    

                    }
            var PurchaseOrder = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == OrderItem.PurchaseOrderId).FirstOrDefault();
            PurchaseOrder.Quantity = TotalQuantity;
            PurchaseOrder.Amount = TotalAmount;
			var product = Entities.Products.Where(x => x.IsActive == true && x.ProductSKU == OrderItem.ProductSKU && x.StyleSKU == OrderItem.StyleSKU).FirstOrDefault();
			if (OrderItem.ItemSize1 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize1.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize1.ToString().Replace(".0","");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize1.ToString());
                }
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize1 + ",";
				}
			}
			if (OrderItem.ItemSize2 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize2.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize2.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize2.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize2 + ",";
				}
			}
			if (OrderItem.ItemSize3 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize3.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize3.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize3.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize3 + ",";
				}
			}
			if (OrderItem.ItemSize4 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize4.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize4.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize4.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize4+ ",";
				}
			}
			if (OrderItem.ItemSize5 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize5.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize5.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize5.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize5+ ",";
				}
			}
			if (OrderItem.ItemSize6 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize6.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize6.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize6.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize6 + ",";
				}
			}
			if (OrderItem.ItemSize7 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize7.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize7.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize7.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize7 + ",";
				}
			}
			if (OrderItem.ItemSize8 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize8.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize8.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize8.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize8+ ",";
				}
			}
			if (OrderItem.ItemSize9 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize9.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize9.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize9.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize9+ ",";
				}
			}
			if (OrderItem.ItemSize10 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize10.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize10.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize10.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize10 + ",";
				}
			}
			if (OrderItem.ItemSize11!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize11.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize11.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize11.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize11 + ",";
				}
			}
			if (OrderItem.ItemSize12!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize12.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize12.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize12.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize12 + ",";
				}
			}
			if (OrderItem.ItemSize13!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize13.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize13.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize13.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize13 + ",";
				}
			}
			if (OrderItem.ItemSize14!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize14.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize14.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize14.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize14+ ",";
				}
			}
			if (OrderItem.ItemSize15!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize15.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize15.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize15.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize15+ ",";
				}
			}
			if (OrderItem.ItemSize16!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize16.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize16.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize16.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize16 + ",";
				}
			}
			if (OrderItem.ItemSize17 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize17.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize17.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize17.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize17+ ",";
				}
			}
			if (OrderItem.ItemSize18!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize18.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize18.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize18.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize18+ ",";
				}
			}
			if (OrderItem.ItemSize19!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize19.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize19.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize19.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize19+ ",";
				}
			}
			if (OrderItem.ItemSize20!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize20.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize20.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize20.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize20+ ",";
				}
			}
			if (OrderItem.ItemSize21!= null)
            {
                bool sizes1 = false;
                if (OrderItem.ItemSize21.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize21.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize21.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize21+ ",";
				}
			}
			if (OrderItem.ItemSize22!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize22.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize22.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize22.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize22+ ",";
				}
			}
			if (OrderItem.ItemSize23!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize23.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize23.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize23.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize23+ ",";
				}
			}
			if (OrderItem.ItemSize24!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize24.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize24.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize24.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize24+ ",";
				}
			}
			if (OrderItem.ItemSize25!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize25.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize25.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize25.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize25+ ",";
				}
			}
			if (OrderItem.ItemSize26 != null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize26.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize26.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize26.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize26 + ",";
				}
			}
			if (OrderItem.ItemSize27!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize27.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize27.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize27.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize27+ ",";
				}
			}
			if (OrderItem.ItemSize28!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize28.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize28.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize28.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize28+ ",";
				}
			}
			if (OrderItem.ItemSize29!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize29.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize29.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize29.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize29+ ",";
				}
			}
			if (OrderItem.ItemSize30!= null)
			{
                bool sizes1 = false;
                if (OrderItem.ItemSize30.ToString().Contains(".0"))
                {
                    var Isize1 = OrderItem.ItemSize30.ToString().Replace(".0", "");
                    sizes1 = product.AvailableSize.Contains(Isize1);
                }
                else
                {
                    sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize30.ToString());
                }
                if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize30+ ",";
				}
			}
	    	Entities.SaveChanges();
			int id = Order.PurchaseOrderId;
            return Ok(id);
        }
        [HttpGet]
        [Route("ProductsAutocomplete")]
        public IHttpActionResult ProductsAutocomplete(string name)
        {
            var list = Entities.PurchaseOrderItems.GroupBy(a => a.ProductID)
                 .Select(g => g.FirstOrDefault()).Include(x=>x.Product)
                 .ToList(); 
            
            var data = list.Where(x => x.IsActive == true && x.Product.ProductSKU.Contains(name)).Distinct().ToList().Select(m => new PurchaseOrderItemModel
            {
                ProductSKU = m.Product.ProductSKU,
                ID = m.ID,
               
            }).ToList();
            return Ok();
        }
        [HttpGet]
        [Route("ProductsStyleAutocomplete")]
        public IHttpActionResult ProductsStyleAutocomplete(string name)
        {
            //var list = Entities.PurchaseOrderItems.GroupBy(a => a.ProductStyleId)
            //     .Select(g => g.FirstOrDefault()).Include(x => x.Product).Include(x => x.ProductStyle).Include(x => x.SizeGrid)
            //     .ToList();

            //var data = list.Where(x => x.IsActive == true && x.Product.ProductSKU.Contains(name)).Distinct().ToList().Select(m => new PurchaseOrderItemModel
            //{
            //    ProductStyleSKU = m.ProductStyle.StyleSKU,
            //    ID = m.ID,

            //}).ToList();
            return Ok();
        }
        [HttpGet]
        [Route("getByProductId")]
        public IHttpActionResult GetByProduct(int ProductID)
        {
            var data = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.ProductID == ProductID).Include(x=>x.Product).Include(x=>x.Color).Include(x=>x.PurchaseOrder).Include(x=>x.PurchaseOrder.PurchaseOrderStatu).ToList().RemoveReferences();
            return Ok(data);
        }
		[HttpPost]
		[Route("checkProductByOrder")]
		public IHttpActionResult CheckProductByProduct(PurchaseOrderItemModel model)
		{
			var status = Entities.PurchaseOrderItems.Any(x => x.IsActive == true && x.PurchaseOrderId == model.PurchaseOrderId && x.Product.ProductSKU == model.autoCompleteProductName && x.Product.StyleSKU==model.autoCompleteProductStyleName);
			return Ok(status);
		}
		[HttpPost]
		[Route("checkProduct")]
		public IHttpActionResult CheckProduct(PurchaseOrderItemModel purchase)
		{
			var list = Entities.PurchaseOrderItems.Where(x => x.IsActive == true).Include(x => x.Product).ToList();
			bool status = list.Any(x => x.Product.ProductSKU == purchase.autoCompleteProductName && x.Product.StyleSKU == purchase.autoCompleteProductStyleName);
			return Ok(status);
		}
        // GET: All PurchaseOrderItems
        [Route("GetAllPurchaseOrderItems")]
        public IHttpActionResult GetAllPurchaseOrderItems()
        {
            var OrderList = Entities.PurchaseOrderItems.Include(s => s.Color).Where(s => s.IsActive == true).ToList().
            Select(m => new Model.PurchaseOrderItemModel
            {
                ID = m.ID,
                PurchaseOrderId = m.PurchaseOrderId,
               //ProductId = m.ProductId,
                Amount = m.Amount,
                SizeGridId = m.SizeGridId,
                ColorId = m.ColorId,
                SuplierStyle = m.SuplierStyle,
               // ProductStyleId = m.ProductStyleId,

                ItemSize1 = m.ItemSize1.ToString(),
                ItemSize2 = m.ItemSize2.ToString(),
                ItemSize3 = m.ItemSize3.ToString(),
                ItemSize4 = m.ItemSize4.ToString(),
                ItemSize5 = m.ItemSize5.ToString(),
                ItemSize6 = m.ItemSize6.ToString(),
                ItemSize7 = m.ItemSize7.ToString(),
                ItemSize8 = m.ItemSize8.ToString(),
                ItemSize9 = m.ItemSize9.ToString(),
                ItemSize10 = m.ItemSize10.ToString(),
                ItemSize11 = m.ItemSize11.ToString(),
                ItemSize12 = m.ItemSize12.ToString(),
                ItemSize13 = m.ItemSize13.ToString(),
                ItemSize14 = m.ItemSize14.ToString(),
                ItemSize15 = m.ItemSize15.ToString(),
                ItemSize16 = m.ItemSize16.ToString(),
                ItemSize17 = m.ItemSize17.ToString(),
                ItemSize18 = m.ItemSize18.ToString(),
                ItemSize19 = m.ItemSize19.ToString(),
                ItemSize20 = m.ItemSize20.ToString(),
                ItemSize21 = m.ItemSize21.ToString(),
                ItemSize22 = m.ItemSize22.ToString(),
                ItemSize23 = m.ItemSize23.ToString(),
                ItemSize24 = m.ItemSize24.ToString(),
                ItemSize25 = m.ItemSize25.ToString(),
                ItemSize26 = m.ItemSize26.ToString(),
                ItemSize27 = m.ItemSize27.ToString(),
                ItemSize28 = m.ItemSize28.ToString(),
                ItemSize29 = m.ItemSize29.ToString(),
                ItemSize30 = m.ItemSize30.ToString(),

                QuantitySize1 = m.QuantitySize1,
                QuantitySize2 = m.QuantitySize2,
                QuantitySize3 = m.QuantitySize3,
                QuantitySize4 = m.QuantitySize4,
                QuantitySize5 = m.QuantitySize5,
                QuantitySize6 = m.QuantitySize6,
                QuantitySize7 = m.QuantitySize7,
                QuantitySize8 = m.QuantitySize8,
                QuantitySize9 = m.QuantitySize9,
                QuantitySize10 = m.QuantitySize10,
                QuantitySize11 = m.QuantitySize11,
                QuantitySize12 = m.QuantitySize12,
                QuantitySize13 = m.QuantitySize13,
                QuantitySize14 = m.QuantitySize14,
                QuantitySize15 = m.QuantitySize15,
                QuantitySize16 = m.QuantitySize16,
                QuantitySize17 = m.QuantitySize17,
                QuantitySize18 = m.QuantitySize18,
                QuantitySize19 = m.QuantitySize19,
                QuantitySize20 = m.QuantitySize20,
                QuantitySize21 = m.QuantitySize21,
                QuantitySize22 = m.QuantitySize22,
                QuantitySize23 = m.QuantitySize23,
                QuantitySize24 = m.QuantitySize24,
                QuantitySize25 = m.QuantitySize25,
                QuantitySize26 = m.QuantitySize26,
                QuantitySize27 = m.QuantitySize27,
                QuantitySize28 = m.QuantitySize28,
                QuantitySize29 = m.QuantitySize29,
                QuantitySize30 = m.QuantitySize30,

                CostSize1 = m.CostSize1,
                CostSize2 = m.CostSize2,
                CostSize3 = m.CostSize3,
                CostSize4 = m.CostSize4,
                CostSize5 = m.CostSize5,
                CostSize6 = m.CostSize6,
                CostSize7 = m.CostSize7,
                CostSize8 = m.CostSize8,
                CostSize9 = m.CostSize9,
                CostSize10 = m.CostSize10,
                CostSize11 = m.CostSize11,
                CostSize12 = m.CostSize12,
                CostSize13 = m.CostSize13,
                CostSize14 = m.CostSize14,
                CostSize15 = m.CostSize15,
                CostSize16 = m.CostSize16,
                CostSize17 = m.CostSize17,
                CostSize18 = m.CostSize18,
                CostSize19 = m.CostSize19,
                CostSize20 = m.CostSize20,
                CostSize21 = m.CostSize21,
                CostSize22 = m.CostSize22,
                CostSize23 = m.CostSize23,
                CostSize24 = m.CostSize24,
                CostSize25 = m.CostSize25,
                CostSize26 = m.CostSize26,
                CostSize27 = m.CostSize27,
                CostSize28 = m.CostSize28,
                CostSize29 = m.CostSize29,
                CostSize30 = m.CostSize30,
                IsActive = m.IsActive,
                autoCompleteColorName = m.Color?.Code,
                //autoCompleteProductName=m.Product.ProductSKU,
                //autoCompleteProductStyleName=m.ProductStyle.StyleSKU,
            }).ToList();
            return Ok(OrderList);
        }
       [HttpGet]
       [Route("getData")]
       public IHttpActionResult GetData(int Id,int ProductId)
        {
            var data = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == Id && x.ProductID == ProductId).Include(x => x.Product).Include(x => x.PurchaseOrder).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetItemList")]
        public IHttpActionResult GetItemList(int? PurchaseOrderId)
        {
            List<DictModel> list = new List<DictModel>();
            var data = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == PurchaseOrderId)
                                                    .Include(x=>x.Product)
                                                    .Include(x => x.Product.Color)
                                                    .Include(x=>x.PurchaseOrder)
                                                    .Include(x=>x.PurchaseOrder.Supplier)
                                                    .Include(x=>x.PurchaseOrder.Buyer)
                                                    .Include(x=>x.Color)
                                                    .ToList();
            foreach(var item in data)
            {
                DictModel model = new DictModel();
                model.ItemSize = new Dictionary<string, string>();
                model.CostSize = new Dictionary<string, string>();
                model.QuantitySize = new Dictionary<string, string>();
                model.ProductId = item.ProductID;

                var body = Newtonsoft.Json.JsonConvert.SerializeObject(item.Product.RemoveReferences());
                model.Product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(body);
                var body1 = Newtonsoft.Json.JsonConvert.SerializeObject(item.PurchaseOrder.RemoveReferences());
                model.PurchaseOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseOrderModel>(body1);

                model.PurchaseOrderId = item.PurchaseOrderId;
                model.SupplierStyle = item.SuplierStyle;
                var utility = Utilities.getKeyVaue(item);
                model.ItemSize = new Dictionary<string, string>();
                var keyData= Utilities.getFilterDictionary(utility, "ItemSize"); //utility.Where(x => x.Key.Contains("ItemSize")).ToList();
                foreach(var keyValue in keyData)
                {
                    if (keyValue.Value.Contains(".0"))
                    {
                        model.ItemSize.Add(keyValue.Key, keyValue.Value.Replace(".0", ""));
                    }
                    else
                    {
                        model.ItemSize.Add(keyValue.Key, keyValue.Value);
                    }
                }
                //ItemSize.ForEach(f => model.ItemSize.Add(f.Key, f.Value));
                model.CostSize = Utilities.getFilterDictionary(utility, "CostSize"); //utility.Where(x => x.Key.Contains("CostSize")).ToList();
                model.QuantitySize= Utilities.getFilterDictionary(utility, "QuantitySize"); //utility.Where(x => x.Key.Contains("QuantitySize")).ToList();
                //CostSize.ForEach(f => model.CostSize.Add(f.Key,f.Value));
                //QuantitySize.ForEach(f => model.QuantitySize.Add(f.Key,f.Value));
                list.Add(model);
            }
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<PurchaseOrderItem> GetProductStyles()
        {
            var list = Entities.PurchaseOrderItems.Where(x => x.IsActive == true).Include(x=>x.PurchaseOrder).Include(x=>x.Product).Include(x=>x.PurchaseOrder).ToList();
            return list;
        }
		[HttpGet]
		[Route("getByOrderId")]
		public List<PurchaseOrderItem> GetItemByOrderId(int id)
		{
			var list = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == id).Include(x => x.Product).Include(x => x.Color).ToList();
			return list;
		}
		//get All order items by order number
		[HttpGet]
        [Route("getByOrderIdReceipt")]
        public IHttpActionResult GetItemByOrder(int id)
        {
			var modelList = new List<PurchaseOrderItemModel>();
            var list = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == id).Include(x=>x.Product).Include(x=>x.Color).ToList().RemoveReferences();
			var receipt = Entities.ReceiveOrders.Where(x => x.IsActive == true && x.PurchaseOrderId == id && x.TotalQuantity != 0).Include(x => x.PurchaseOrder).Include(x=>x.ReceiptOrderItems).ToList();
			var model = new PurchaseOrderItemModel();
			if (receipt.Count > 0)
			{
				foreach (var a in list)
				{
					foreach (var b in receipt)
					{
						foreach (var c in b.ReceiptOrderItems)
						{
							
							if (a.ProductID == c.ProductId)
							{
								a.QuantitySize1 = a.QuantitySize1 - c.Quantity01;
								a.QuantitySize2 = a.QuantitySize2 - c.Quantity02;
								a.QuantitySize3 = a.QuantitySize3 - c.Quantity03;
								a.QuantitySize4 = a.QuantitySize4 - c.Quantity04;
								a.QuantitySize5 = a.QuantitySize5 - c.Quantity05;
								a.QuantitySize6 = a.QuantitySize6 - c.Quantity06;
								a.QuantitySize7 = a.QuantitySize7 - c.Quantity07;
								a.QuantitySize8 = a.QuantitySize8 - c.Quantity08;
								a.QuantitySize9 = a.QuantitySize9 - c.Quantity09;
								a.QuantitySize10 = a.QuantitySize10 - c.Quantity10;
								a.QuantitySize11 = a.QuantitySize11 - c.Quantity11;
								a.QuantitySize12 = a.QuantitySize12 - c.Quantity12;
								a.QuantitySize13 = a.QuantitySize13 - c.Quantity13;
								a.QuantitySize14 = a.QuantitySize14 - c.Quantity14;
								a.QuantitySize15 = a.QuantitySize15 - c.Quantity15;
								a.QuantitySize16 = a.QuantitySize16 - c.Quantity16;
								a.QuantitySize17 = a.QuantitySize17 - c.Quantity17;
								a.QuantitySize18 = a.QuantitySize18 - c.Quantity18;
								a.QuantitySize19 = a.QuantitySize19 - c.Quantity19;
								a.QuantitySize20 = a.QuantitySize20 - c.Quantity20;
								a.QuantitySize21 = a.QuantitySize21 - c.Quantity21;
								a.QuantitySize22 = a.QuantitySize22 - c.Quantity22;
								a.QuantitySize23 = a.QuantitySize23 - c.Quantity23;
								a.QuantitySize24 = a.QuantitySize24 - c.Quantity24;
								a.QuantitySize25 = a.QuantitySize25 - c.Quantity25;
								a.QuantitySize26 = a.QuantitySize26 - c.Quantity26;
								a.QuantitySize27 = a.QuantitySize27 - c.Quantity27;
								a.QuantitySize28 = a.QuantitySize28 - c.Quantity28;
								a.QuantitySize29 = a.QuantitySize29 - c.Quantity29;
								a.QuantitySize30 = a.QuantitySize30 - c.Quantity30;
								a.Amount = a.Amount-c.SalesCost;
								a.ProductID = c.ProductId;
								//var total=model.ItemCount;
								//model.TotalQuantity = total;
								// model.IsActive = true;
								//model.ID = a.ID;
								//model.ItemSize1 = a.ItemSize1; model.ItemSize2 = a.ItemSize2; model.ItemSize3 = a.ItemSize3; model.ItemSize4 = a.ItemSize4; model.ItemSize5 = a.ItemSize5; model.ItemSize6 = a.ItemSize6; model.ItemSize7 = a.ItemSize7; model.ItemSize8 = a.ItemSize8; model.ItemSize9 = a.ItemSize9; model.ItemSize10 = a.ItemSize10; model.ItemSize11 = a.ItemSize11; model.ItemSize12 = a.ItemSize12; model.ItemSize13= a.ItemSize13; model.ItemSize14 = a.ItemSize14; model.ItemSize15 = a.ItemSize15; model.ItemSize16 = a.ItemSize16; model.ItemSize17 = a.ItemSize17; model.ItemSize18= a.ItemSize18; model.ItemSize19 = a.ItemSize19; model.ItemSize20 = a.ItemSize20; model.ItemSize24 = a.ItemSize24; model.ItemSize22 = a.ItemSize22; model.ItemSize26 = a.ItemSize26; model.ItemSize27 = a.ItemSize27; model.ItemSize25 = a.ItemSize25; model.ItemSize29 = a.ItemSize29; model.ItemSize30 = a.ItemSize30;
								//model.ProductId = a.ProductID;
								//model.PurchaseOrderId = a.PurchaseOrderId;
								//model.SizeGridId = a.SizeGridId;
								//modelList.Add(model);
								if(a.QuantitySize1==0 && a.QuantitySize2 == 0 && a.QuantitySize3 == 0 && a.QuantitySize4 == 0 &&
									a.QuantitySize5== 0 && a.QuantitySize6== 0 && a.QuantitySize7 == 0 && a.QuantitySize8 == 0 && a.QuantitySize9 == 0 && a.QuantitySize10 == 0 && a.QuantitySize11 == 0 && a.QuantitySize12 == 0 && a.QuantitySize13 == 0 && a.QuantitySize14 == 0 && a.QuantitySize15 == 0 && a.QuantitySize16== 0 && a.QuantitySize17== 0 && a.QuantitySize18 == 0 && a.QuantitySize19 == 0 && a.QuantitySize20== 0 && a.QuantitySize21 == 0 && a.QuantitySize22== 0 && a.QuantitySize23== 0 && a.QuantitySize24== 0 && a.QuantitySize25 == 0 && a.QuantitySize26 == 0 && a.QuantitySize27== 0 && a.QuantitySize28== 0 && a.QuantitySize29== 0 && a.QuantitySize30 == 0)
								{
									a.IsActive = false;
								}
							}
						}
					}
				}
			}
			//else
			//{
			//	foreach(var item in list)
			//	{
			//		model.Amount = item.Amount;model.ColorId = item.ColorId;model.CostSize1 = item.CostSize1; model.CostSize2 = item.CostSize2; model.CostSize3= item.CostSize3; model.CostSize4 = item.CostSize4; model.CostSize5 = item.CostSize5; model.CostSize6 = item.CostSize6; model.CostSize7 = item.CostSize7; model.CostSize8= item.CostSize8; model.CostSize9 = item.CostSize9; model.CostSize10= item.CostSize10; model.CostSize11 = item.CostSize11; model.CostSize12 = item.CostSize12; model.CostSize13 = item.CostSize13; model.CostSize14 = item.CostSize14; model.CostSize15 = item.CostSize15; model.CostSize16 = item.CostSize16; model.CostSize17 = item.CostSize17; model.CostSize18 = item.CostSize18; model.CostSize19 = item.CostSize19; model.CostSize20 = item.CostSize20; model.CostSize21 = item.CostSize21; model.CostSize22 = item.CostSize22; model.CostSize23 = item.CostSize23; model.CostSize24= item.CostSize24; model.CostSize25 = item.CostSize25; model.CostSize26 = item.CostSize26; model.CostSize27 = item.CostSize27; model.CostSize28 = item.CostSize28; model.CostSize29 = item.CostSize29; model.CostSize30 = item.CostSize30;
			//		model.ItemSize1 = item.ItemSize1; model.ItemSize2 = item.ItemSize2; model.ItemSize3 = item.ItemSize3; model.ItemSize4 = item.ItemSize4; model.ItemSize5 = item.ItemSize5; model.ItemSize6 = item.ItemSize6; model.ItemSize7 = item.ItemSize7; model.ItemSize8 = item.ItemSize8; model.ItemSize9 = item.ItemSize9; model.ItemSize10 = item.ItemSize10; model.ItemSize11 = item.ItemSize11; model.ItemSize12 = item.ItemSize12; model.ItemSize13 = item.ItemSize13; model.ItemSize14 = item.ItemSize14; model.ItemSize15 = item.ItemSize15; model.ItemSize16 = item.ItemSize16; model.ItemSize17 = item.ItemSize17; model.ItemSize18 = item.ItemSize18; model.ItemSize19 = item.ItemSize19; model.ItemSize20 = item.ItemSize20; model.ItemSize24 = item.ItemSize24; model.ItemSize22 = item.ItemSize22; model.ItemSize26 = item.ItemSize26; model.ItemSize27 = item.ItemSize27; model.ItemSize25 = item.ItemSize25; model.ItemSize29 = item.ItemSize29; model.ItemSize30 = item.ItemSize30;
			//		model.ID = item.ID;model.IsActive = item.IsActive;model.ProductId = item.ProductID;model.Product = item.Product;
			//	}
			//}
            return Ok(list);
        }
        [Route("GetAllPurchaseOrderItemsByPurchaseOrderId")]
        public IHttpActionResult GetAllPurchaseOrderItemsByPurchaseOrderId(int PurchaseOrderId)
        {

            var OrderList = Entities.PurchaseOrderItems.Where(s => s.IsActive == true && s.PurchaseOrderId == PurchaseOrderId).Include(s => s.Color).Include(x => x.Product).ToList();
            
            return Ok(OrderList);
        }
[HttpPost]
[Route("getValue")]
public List<PurchaseOrderItem> GetSearchValue(PurchaseOrderSearch model)
        {
            var list = Entities.PurchaseOrderItems.Where(x=>x.IsActive==true && x.PurchaseOrderId==model.PurchaseOrderId);
            if(model!=null)
            {
               if (!string.IsNullOrEmpty(model.autoCompleteProductName))
               {
                   list = list.Where(x => x.Product.ProductSKU == model.autoCompleteProductName);
                }
               if (!string.IsNullOrEmpty(model.autoCompleteProductStyleName))
               {
                    list = list.Where(x => x.Product.StyleSKU == model.autoCompleteProductStyleName);
                }
            }
            return list.ToList().RemoveReferences();
        }
        // delete DeletePurchaseOrderItems
        [HttpPost]
        [Route("DeletePurchaseOrderItems")]
        public IHttpActionResult DeletePurchaseOrderItems(int Id)
        {
            DAL.PurchaseOrderItem order = new DAL.PurchaseOrderItem();
            order = Entities.PurchaseOrderItems.Where(x=>x.ID==Id).FirstOrDefault();
            order.IsActive = false;
            Entities.SaveChanges();
            var purchaseOrderItem = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == order.PurchaseOrderId).ToList();
            int TotalQuantity = 0;
            decimal TotalAmount = 0;
            foreach (var a in purchaseOrderItem)
            {
                TotalQuantity += (a.QuantitySize1 ?? 0) + (a.QuantitySize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) + (a.QuantitySize4 ?? 0) + (a.QuantitySize5 ?? 0) + (a.QuantitySize6 ?? 0) + (a.QuantitySize7 ?? 0) + (a.QuantitySize8 ?? 0) + (a.QuantitySize9 ?? 0) +
                    (a.QuantitySize10 ?? 0) + (a.QuantitySize10 ?? 0) + (a.QuantitySize11 ?? 0) + (a.QuantitySize12 ?? 0) +
                    (a.QuantitySize13 ?? 0) + (a.QuantitySize14 ?? 0) + (a.QuantitySize15 ?? 0) + (a.QuantitySize16 ?? 0) +
                    (a.QuantitySize17 ?? 0) + (a.QuantitySize18 ?? 0) + (a.QuantitySize19 ?? 0) + (a.QuantitySize20 ?? 0) +
                    (a.QuantitySize21 ?? 0) + (a.QuantitySize22 ?? 0) + (a.QuantitySize23 ?? 0) + (a.QuantitySize24 ?? 0) +
                    (a.QuantitySize25 ?? 0) + (a.QuantitySize26 ?? 0) + (a.QuantitySize27 ?? 0) + (a.QuantitySize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) + (a.QuantitySize30 ?? 0);
                TotalAmount += (a.QuantitySize1 ?? 0) * (a.CostSize1 ?? 0) + (a.QuantitySize2 ?? 0) * (a.CostSize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) * (a.CostSize3 ?? 0) + (a.QuantitySize4 ?? 0) * (a.CostSize4 ?? 0) +
                    (a.QuantitySize5 ?? 0) * (a.CostSize5 ?? 0) + (a.QuantitySize6 ?? 0) * (a.CostSize6 ?? 0) + (a.QuantitySize7 ?? 0) * (a.CostSize7 ?? 0) +
                    (a.QuantitySize8 ?? 0) * (a.CostSize8 ?? 0) + (a.QuantitySize9 ?? 0) * (a.CostSize10 ?? 0) +
                    (a.QuantitySize11 ?? 0) * (a.CostSize12 ?? 0) + (a.QuantitySize13 ?? 0) * (a.CostSize13 ?? 0) +
                    (a.QuantitySize14 ?? 0) * (a.CostSize14 ?? 0) + (a.QuantitySize15 ?? 0) * (a.CostSize15 ?? 0) +
                    (a.QuantitySize16 ?? 0) * (a.CostSize16 ?? 0) + (a.QuantitySize17 ?? 0) * (a.CostSize17 ?? 0) +
                    (a.QuantitySize18 ?? 0) * (a.CostSize18 ?? 0) + (a.QuantitySize19 ?? 0) * (a.CostSize19 ?? 0) +
                    (a.QuantitySize20 ?? 0) * (a.CostSize20 ?? 0) + (a.QuantitySize21 ?? 0) * (a.CostSize21 ?? 0) +
                    (a.QuantitySize22 ?? 0) * (a.CostSize22 ?? 0) + (a.QuantitySize23 ?? 0) * (a.CostSize23 ?? 0) +
                    (a.QuantitySize24 ?? 0) * (a.CostSize24 ?? 0) + (a.QuantitySize25 ?? 0) * (a.CostSize25 ?? 0) +
                    (a.QuantitySize26 ?? 0) * (a.CostSize26 ?? 0) + (a.QuantitySize27 ?? 0) * (a.CostSize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) * (a.CostSize29 ?? 0) + (a.QuantitySize30 ?? 0) * (a.CostSize30 ?? 0);


            }
            var PurchaseOrder = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == order.PurchaseOrderId).FirstOrDefault();


            PurchaseOrder.Quantity = TotalQuantity;
            PurchaseOrder.Amount = TotalAmount;
            Entities.SaveChanges();
            int id = order.PurchaseOrderId;
            return Ok(id);
        }
		[Route("GetItemById")]
		public IHttpActionResult GetByID(int id)
		{
			var orderItem = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.ID == id).Include(x => x.SizeGrid).Include(x => x.Product).Include(x => x.Color).Include(x => x.PurchaseOrder).FirstOrDefault();
			return Ok(orderItem);
		}
        //GetPurchaseOrderItemsById
        [Route("GetPurchaseOrderItemsById")]
        public IHttpActionResult GetPurchaseOrderItemsById(int id)
        {
            //var orderItem = Entities.PurchaseOrderItems.Include(s=>s.Color).Include(s=>s.SizeGrid).Where(s => s.IsActive == true && s.ID == id).ToList().Select(m => new Model.PurchaseOrderItemModel
            //{
            //    ID=m.ID,
            //    PurchaseOrderId = m.PurchaseOrderId,
            //    //ProductId = m.ProductId,
            //    Amount = m.Amount,
            //    SizeGridId = m.SizeGridId,
            //    ColorId = m.ColorId,
            //    SuplierStyle = m.SuplierStyle,
            //  //ProductStyleId = m.ProductStyleId,

            //    ItemSize1 = m.ItemSize1,ItemSize2 = m.ItemSize2,ItemSize3 = m.ItemSize3,ItemSize4 = m.ItemSize4,ItemSize5 = m.ItemSize5,
            //    ItemSize6 = m.ItemSize6,ItemSize7 = m.ItemSize7,ItemSize8 = m.ItemSize8,ItemSize9 = m.ItemSize9,ItemSize10 = m.ItemSize10,
            //    ItemSize11 = m.ItemSize11,ItemSize12 = m.ItemSize12,ItemSize13 = m.ItemSize13,ItemSize14 = m.ItemSize14,ItemSize15 = m.ItemSize15,
            //    ItemSize16 = m.ItemSize16,ItemSize17 = m.ItemSize17,ItemSize18 = m.ItemSize18,ItemSize19 = m.ItemSize19,ItemSize20 = m.ItemSize20,
            //    ItemSize21 = m.ItemSize21,ItemSize22 = m.ItemSize22,ItemSize23 = m.ItemSize23,ItemSize24 = m.ItemSize24,ItemSize25 = m.ItemSize25,
            //    ItemSize26 = m.ItemSize26,ItemSize27 = m.ItemSize27,ItemSize28 = m.ItemSize28,ItemSize29 = m.ItemSize29,ItemSize30 = m.ItemSize30,

            //    QuantitySize1 = m.QuantitySize1,QuantitySize2=m.QuantitySize2,QuantitySize3=m.QuantitySize3,QuantitySize4=m.QuantitySize4,
            //    QuantitySize5 = m.QuantitySize5,QuantitySize6 = m.QuantitySize6,QuantitySize7 = m.QuantitySize7,QuantitySize8 = m.QuantitySize8,
            //    QuantitySize9 = m.QuantitySize9,QuantitySize10 = m.QuantitySize10,QuantitySize11 = m.QuantitySize11,QuantitySize12 = m.QuantitySize12,
            //    QuantitySize13 = m.QuantitySize13,QuantitySize14 = m.QuantitySize14,QuantitySize15 = m.QuantitySize15,QuantitySize16 = m.QuantitySize16,
            //    QuantitySize17 = m.QuantitySize17,QuantitySize18 = m.QuantitySize18,QuantitySize19 = m.QuantitySize19,QuantitySize20 = m.QuantitySize20,
            //    QuantitySize21 = m.QuantitySize21,QuantitySize22 = m.QuantitySize22,QuantitySize23 = m.QuantitySize23,QuantitySize24 = m.QuantitySize24,
            //    QuantitySize25 = m.QuantitySize25,QuantitySize26 = m.QuantitySize26,QuantitySize27 = m.QuantitySize27,QuantitySize28 = m.QuantitySize28,
            //    QuantitySize29 = m.QuantitySize29,QuantitySize30 = m.QuantitySize30,

            //    CostSize1 = m.CostSize1,CostSize2 = m.CostSize2,CostSize3 = m.CostSize3,CostSize4 = m.CostSize4,CostSize5 = m.CostSize5,
            //    CostSize6 = m.CostSize6,CostSize7 = m.CostSize7,CostSize8 = m.CostSize8,CostSize9 = m.CostSize9,CostSize10 = m.CostSize10,
            //    CostSize11 = m.CostSize11,CostSize12 = m.CostSize12,CostSize13 = m.CostSize13,CostSize14 = m.CostSize14,CostSize15 = m.CostSize15,
            //    CostSize16 = m.CostSize16,CostSize17 = m.CostSize17,CostSize18 = m.CostSize18,CostSize19 = m.CostSize19,CostSize20 = m.CostSize20,
            //    CostSize21 = m.CostSize21,CostSize22 = m.CostSize22,CostSize23 = m.CostSize23,CostSize24 = m.CostSize24,CostSize25 = m.CostSize25,
            //    CostSize26 = m.CostSize26,CostSize27 = m.CostSize27,CostSize28 = m.CostSize28,CostSize29 = m.CostSize29,CostSize30 = m.CostSize30,
            //    IsActive=m.IsActive,

            //    autoCompleteColorName=m.Color.Code,
            //    autoCompleteGridName=m.SizeGrid.GridNumber,
            //    //autoCompleteProductName=m.Product.ProductSKU,
            //   // autoCompleteProductStyleName=m.ProductStyle.StyleSKU,
            //}).FirstOrDefault();
            var orderItem = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.ID == id).Include(x => x.SizeGrid).Include(x => x.Product).Include(x => x.Color).Include(x=>x.PurchaseOrder).FirstOrDefault();
            Model.PurchaseOrderItemModel model = new PurchaseOrderItemModel();
            model.Amount = orderItem.Amount;
            model.ColorId = orderItem.ColorId;
            
            model.autoCompleteColorName = orderItem.Color.Code;
            model.autoCompleteGridName = orderItem.SizeGrid.GridNumber;
            model.autoCompleteProductName = orderItem.Product.ProductSKU;
            model.autoCompleteProductStyleName = orderItem.Product.StyleSKU;
            model.CostSize1 = orderItem.CostSize1; model.CostSize2 = orderItem.CostSize2; model.CostSize3 = orderItem.CostSize3;
            model.CostSize4 = orderItem.CostSize4; model.CostSize5 = orderItem.CostSize5; model.CostSize6 = orderItem.CostSize6;
            model.CostSize7 = orderItem.CostSize7; model.CostSize8 = orderItem.CostSize8; model.CostSize9 = orderItem.CostSize9;
            model.CostSize10 = orderItem.CostSize10; model.CostSize11 = orderItem.CostSize11; model.CostSize12 = orderItem.CostSize12;
            model.CostSize13 = orderItem.CostSize13; model.CostSize14 = orderItem.CostSize14; model.CostSize15 = orderItem.CostSize15;
            model.CostSize16 = orderItem.CostSize16; model.CostSize17= orderItem.CostSize17; model.CostSize18 = orderItem.CostSize18;
            model.CostSize19 = orderItem.CostSize19; model.CostSize20 = orderItem.CostSize20; model.CostSize21 = orderItem.CostSize21;
            model.CostSize22 = orderItem.CostSize22; model.CostSize23 = orderItem.CostSize23; model.CostSize24= orderItem.CostSize24;
            model.CostSize25 = orderItem.CostSize25; model.CostSize26 = orderItem.CostSize26; model.CostSize27 = orderItem.CostSize27;
            model.CostSize28 = orderItem.CostSize28; model.CostSize29 = orderItem.CostSize29; model.CostSize30 = orderItem.CostSize30;
            model.QuantitySize1 = orderItem.QuantitySize1; model.QuantitySize2 = orderItem.QuantitySize2; model.QuantitySize3 = orderItem.QuantitySize3;
            model.QuantitySize4 = orderItem.QuantitySize4; model.QuantitySize5 = orderItem.QuantitySize5; model.QuantitySize6 = orderItem.QuantitySize6;
            model.QuantitySize7 = orderItem.QuantitySize7; model.QuantitySize8 = orderItem.QuantitySize8; model.QuantitySize9 = orderItem.QuantitySize9;
            model.QuantitySize10 = orderItem.QuantitySize10; model.QuantitySize11 = orderItem.QuantitySize11; model.QuantitySize12= orderItem.QuantitySize12;
            model.QuantitySize13 = orderItem.QuantitySize13; model.QuantitySize14 = orderItem.QuantitySize14; model.QuantitySize15= orderItem.QuantitySize15;
            model.QuantitySize16 = orderItem.QuantitySize16; model.QuantitySize17 = orderItem.QuantitySize17; model.QuantitySize18= orderItem.QuantitySize18;
            model.QuantitySize19 = orderItem.QuantitySize19; model.QuantitySize20 = orderItem.QuantitySize20; model.QuantitySize21 = orderItem.QuantitySize21;
            model.QuantitySize22 = orderItem.QuantitySize22; model.QuantitySize23 = orderItem.QuantitySize23; model.QuantitySize24 = orderItem.QuantitySize24;
            model.QuantitySize25 = orderItem.QuantitySize25; model.QuantitySize26 = orderItem.QuantitySize26; model.QuantitySize27 = orderItem.QuantitySize27;
            model.QuantitySize28= orderItem.QuantitySize28; model.QuantitySize29 = orderItem.QuantitySize29; model.QuantitySize30 = orderItem.QuantitySize30;
            model.ItemSize1 = orderItem.ItemSize1.ToString();
            
            model.ItemSize2 = orderItem.ItemSize2.ToString(); model.ItemSize3 = orderItem.ItemSize3.ToString();
            model.ItemSize4 = orderItem.ItemSize4.ToString(); model.ItemSize5 = orderItem.ItemSize5.ToString(); model.ItemSize6 = orderItem.ItemSize6.ToString();
            model.ItemSize7 = orderItem.ItemSize7.ToString(); model.ItemSize8 = orderItem.ItemSize8.ToString(); model.ItemSize9 = orderItem.ItemSize9.ToString();
            model.ItemSize10 = orderItem.ItemSize10.ToString(); model.ItemSize11 = orderItem.ItemSize11.ToString(); model.ItemSize12 = orderItem.ItemSize12.ToString();
            model.ItemSize13= orderItem.ItemSize13.ToString(); model.ItemSize14= orderItem.ItemSize14.ToString(); model.ItemSize15= orderItem.ItemSize15.ToString();
            model.ItemSize16= orderItem.ItemSize16.ToString(); model.ItemSize17 = orderItem.ItemSize17.ToString(); model.ItemSize18= orderItem.ItemSize18.ToString();
            model.ItemSize19= orderItem.ItemSize19.ToString(); model.ItemSize20= orderItem.ItemSize20.ToString(); model.ItemSize21= orderItem.ItemSize21.ToString();
            model.ItemSize22= orderItem.ItemSize22.ToString(); model.ItemSize23= orderItem.ItemSize23.ToString(); model.ItemSize24= orderItem.ItemSize24.ToString();
            model.ItemSize25= orderItem.ItemSize25.ToString(); model.ItemSize26= orderItem.ItemSize26.ToString(); model.ItemSize27= orderItem.ItemSize27.ToString();
            model.ItemSize28= orderItem.ItemSize28.ToString(); model.ItemSize29 = orderItem.ItemSize29.ToString(); model.ItemSize30= orderItem.ItemSize30.ToString();
            model.ID = orderItem.ID;
            model.IsActive = orderItem.IsActive;
            model.PurchaseOrderId = orderItem.PurchaseOrderId;
            model.SizeGridId = orderItem.SizeGridId;
            model.SuplierStyle = orderItem.SuplierStyle;
			model.PurchaseOrder = new PurchaseOrderModel();
			var purchaseModel = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == model.PurchaseOrderId).FirstOrDefault();
			model.PurchaseOrder.Id = purchaseModel.ID;
			model.PurchaseOrder.IsActive = purchaseModel.IsActive;
			model.PurchaseOrder.IsFinalize = purchaseModel.IsFinalize;
			model.ProductId = orderItem.ProductID;
            return Ok(model);
        }

        //Update PurchaseOrderItems
        [HttpPost]
        [Route("UpdatePurchaseOrderItems")]
        public IHttpActionResult UpdatePurchaseOrderItems(PurchaseOrderItemModel OrderItem)
        {
			
			DAL.PurchaseOrderItem Order = new DAL.PurchaseOrderItem();
            Order = Entities.PurchaseOrderItems.Where(s => s.ID == OrderItem.ID).FirstOrDefault();
             Order.PurchaseOrderId = OrderItem.PurchaseOrderId;
            Order.ProductID = OrderItem.ProductId;
            Order.Amount = (OrderItem.Amount)??0;
            //Order.ProductStyleId = OrderItem.ProductStyleId;
            Order.SizeGridId = OrderItem.SizeGridId;
            var sizeGrid = Entities.SizeGrids.Where(x => x.IsActive == true && x.Id == Order.SizeGridId).FirstOrDefault();
            Order.ColorId = OrderItem.ColorId;
            Order.SuplierStyle = OrderItem.SuplierStyle;

            var dict = new Dictionary<string, string>();
            var keyval = Utility.getKeyVaue(sizeGrid).Where(x => x.Key.Contains("Z")).ToList();
            foreach (var item2 in keyval)
            {
                if (!string.IsNullOrEmpty(item2.Value))
                {
                    if (item2.Value.Contains(".0"))
                    {
                        //item2[item2.Key] = 	item2.Value.Replace(".0", "");
                        dict.Add(item2.Key, item2.Value.Replace(".0", ""));
                    }
                    else
                    {
                        dict.Add(item2.Key, item2.Value);
                    }
                }
            }
            var ItemDict = new Dictionary<string, string>();
            var CostDict = new Dictionary<string, string>();
            var QuantityDict = new Dictionary<string, string>();
            var dataItemSize = Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("ItemSize")).ToList();
            foreach (var item in dataItemSize)
            {
                if (item.Value != "")
                {
                    var data = dict.Where(x => x.Value == item.Value).FirstOrDefault();
                    var data1 = data.Key;
                    var b = "";
                    var a = data1.Substring(0, 2);
                    if (a == "Z0")
                    {
                        b = data1.Replace("Z0", "");
                    }
                    else
                    {
                        b = data1.Replace("Z", "");
                    }
                    var itemKey = Utility.getKeyVaue(Order).Where(x => x.Key.Contains("ItemSize")).ToList();
                    var value = itemKey.Where(x => x.Key.Replace("ItemSize", "") == b).FirstOrDefault();
                    ItemDict.Add(value.Key, data.Value);
                    var CostKey1 = Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("CostSize")).ToList();
                    var costValue1 = CostKey1.Where(x => x.Key.Replace("CostSize", "") == item.Key.Replace("ItemSize", "")).FirstOrDefault();
                    var CostKey = Utility.getKeyVaue(Order).Where(x => x.Key.Contains("CostSize")).ToList();
                    var costValue = CostKey.Where(x => x.Key.Replace("CostSize", "") == value.Key.Replace("ItemSize", "")).FirstOrDefault();
                    CostDict.Add(costValue.Key, costValue1.Value);
                    var QuantityKey1 = Utility.getKeyVaue(OrderItem).Where(x => x.Key.Contains("QuantitySize")).ToList();
                    var QuantityValue1 = QuantityKey1.Where(x => x.Key.Replace("QuantitySize", "") == item.Key.Replace("ItemSize", "")).FirstOrDefault();

                    var QuantityKey = Utility.getKeyVaue(Order).Where(x => x.Key.Contains("QuantitySize")).ToList();
                    var QuantityValue = QuantityKey.Where(x => x.Key.Replace("QuantitySize", "") == value.Key.Replace("ItemSize", "")).FirstOrDefault();
                    QuantityDict.Add(QuantityValue.Key, QuantityValue1.Value);
                }
            }
            Order.ItemSize1 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize1").FirstOrDefault().Value);
            Order.ItemSize2 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize2").FirstOrDefault().Value);
            Order.ItemSize3 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize3").FirstOrDefault().Value);
            Order.ItemSize4 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize4").FirstOrDefault().Value);
            Order.ItemSize5 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize5").FirstOrDefault().Value);
            Order.ItemSize6 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize6").FirstOrDefault().Value);
            Order.ItemSize7 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize7").FirstOrDefault().Value);
            Order.ItemSize8 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize8").FirstOrDefault().Value);
            Order.ItemSize9 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize9").FirstOrDefault().Value);
            Order.ItemSize10 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize10").FirstOrDefault().Value);
            Order.ItemSize11 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize11").FirstOrDefault().Value);
            Order.ItemSize12 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize12").FirstOrDefault().Value);
            Order.ItemSize13 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize13").FirstOrDefault().Value);
            Order.ItemSize14 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize14").FirstOrDefault().Value);
            Order.ItemSize15 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize15").FirstOrDefault().Value);
            Order.ItemSize16 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize16").FirstOrDefault().Value);
            Order.ItemSize17 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize17").FirstOrDefault().Value);
            Order.ItemSize18 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize18").FirstOrDefault().Value);
            Order.ItemSize19 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize19").FirstOrDefault().Value);
            Order.ItemSize20 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize20").FirstOrDefault().Value);
            Order.ItemSize21 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize21").FirstOrDefault().Value);
            Order.ItemSize22 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize22").FirstOrDefault().Value);
            Order.ItemSize23 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize23").FirstOrDefault().Value);
            Order.ItemSize24 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize24").FirstOrDefault().Value);
            Order.ItemSize25 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize25").FirstOrDefault().Value);
            Order.ItemSize26 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize26").FirstOrDefault().Value);
            Order.ItemSize27 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize27").FirstOrDefault().Value);
            Order.ItemSize28 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize28").FirstOrDefault().Value);
            Order.ItemSize29 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize29").FirstOrDefault().Value);
            Order.ItemSize30 = Convert.ToDecimal(ItemDict.Where(x => x.Key == "ItemSize30").FirstOrDefault().Value);
            Order.CostSize1 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize1").FirstOrDefault().Value);
            Order.CostSize2 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize2").FirstOrDefault().Value);
            Order.CostSize3 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize3").FirstOrDefault().Value);
            Order.CostSize4 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize4").FirstOrDefault().Value);
            Order.CostSize5 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize5").FirstOrDefault().Value);
            Order.CostSize6 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize6").FirstOrDefault().Value);
            Order.CostSize7 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize7").FirstOrDefault().Value);
            Order.CostSize8 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize8").FirstOrDefault().Value);
            Order.CostSize9 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize9").FirstOrDefault().Value);
            Order.CostSize10 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize10").FirstOrDefault().Value);
            Order.CostSize11 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize11").FirstOrDefault().Value);
            Order.CostSize12 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize12").FirstOrDefault().Value);
            Order.CostSize13 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize13").FirstOrDefault().Value);
            Order.CostSize14 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize14").FirstOrDefault().Value);
            Order.CostSize15 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize15").FirstOrDefault().Value);
            Order.CostSize16 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize16").FirstOrDefault().Value);
            Order.CostSize17 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize17").FirstOrDefault().Value);
            Order.CostSize18 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize18").FirstOrDefault().Value);
            Order.CostSize19 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize19").FirstOrDefault().Value);
            Order.CostSize20 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize20").FirstOrDefault().Value);
            Order.CostSize21 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize21").FirstOrDefault().Value);
            Order.CostSize22 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize22").FirstOrDefault().Value);
            Order.CostSize23 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize23").FirstOrDefault().Value);
            Order.CostSize24 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize24").FirstOrDefault().Value);
            Order.CostSize25 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize25").FirstOrDefault().Value);
            Order.CostSize26 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize26").FirstOrDefault().Value);
            Order.CostSize27 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize27").FirstOrDefault().Value);
            Order.CostSize28 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize28").FirstOrDefault().Value);
            Order.CostSize29 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize29").FirstOrDefault().Value);
            Order.CostSize30 = Convert.ToDecimal(CostDict.Where(x => x.Key == "CostSize30").FirstOrDefault().Value);
            Order.QuantitySize1 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize1").FirstOrDefault().Value);
            Order.QuantitySize2 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize2").FirstOrDefault().Value);
            Order.QuantitySize3 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize3").FirstOrDefault().Value);
            Order.QuantitySize4 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize4").FirstOrDefault().Value);
            Order.QuantitySize5 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize5").FirstOrDefault().Value);
            Order.QuantitySize6 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize6").FirstOrDefault().Value);
            Order.QuantitySize7 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize7").FirstOrDefault().Value);
            Order.QuantitySize8 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize8").FirstOrDefault().Value);
            Order.QuantitySize9 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize9").FirstOrDefault().Value);
            Order.QuantitySize10 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize10").FirstOrDefault().Value);
            Order.QuantitySize11 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize11").FirstOrDefault().Value);
            Order.QuantitySize12 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize12").FirstOrDefault().Value);
            Order.QuantitySize13 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize13").FirstOrDefault().Value);
            Order.QuantitySize14 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize14").FirstOrDefault().Value);
            Order.QuantitySize15 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize15").FirstOrDefault().Value);
            Order.QuantitySize16 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize16").FirstOrDefault().Value);
            Order.QuantitySize17 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize17").FirstOrDefault().Value);
            Order.QuantitySize18 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize18").FirstOrDefault().Value);
            Order.QuantitySize19 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize19").FirstOrDefault().Value);
            Order.QuantitySize20 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize20").FirstOrDefault().Value);
            Order.QuantitySize21 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize21").FirstOrDefault().Value);
            Order.QuantitySize22 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize22").FirstOrDefault().Value);
            Order.QuantitySize23 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize23").FirstOrDefault().Value);
            Order.QuantitySize24 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize24").FirstOrDefault().Value);
            Order.QuantitySize25 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize25").FirstOrDefault().Value);
            Order.QuantitySize26 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize26").FirstOrDefault().Value);
            Order.QuantitySize27 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize27").FirstOrDefault().Value);
            Order.QuantitySize28 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize28").FirstOrDefault().Value);
            Order.QuantitySize29 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize29").FirstOrDefault().Value);
            Order.QuantitySize30 = Convert.ToInt16(QuantityDict.Where(x => x.Key == "QuantitySize30").FirstOrDefault().Value);
            Order.IsActive = true;
            Entities.SaveChanges();
            var purchaseOrderItem = Entities.PurchaseOrderItems.Where(x => x.IsActive == true && x.PurchaseOrderId == OrderItem.PurchaseOrderId).ToList();
            int TotalQuantity = 0;
            decimal TotalAmount = 0;
            foreach (var a in purchaseOrderItem)
            {
                TotalQuantity += (a.QuantitySize1 ?? 0) + (a.QuantitySize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) + (a.QuantitySize4 ?? 0) + (a.QuantitySize5 ?? 0) + (a.QuantitySize6 ?? 0) + (a.QuantitySize7 ?? 0) + (a.QuantitySize8 ?? 0) + (a.QuantitySize9 ?? 0) +
                    (a.QuantitySize10 ?? 0) + (a.QuantitySize10 ?? 0) + (a.QuantitySize11 ?? 0) + (a.QuantitySize12 ?? 0) +
                    (a.QuantitySize13 ?? 0) + (a.QuantitySize14 ?? 0) + (a.QuantitySize15 ?? 0) + (a.QuantitySize16 ?? 0) +
                    (a.QuantitySize17 ?? 0) + (a.QuantitySize18 ?? 0) + (a.QuantitySize19 ?? 0) + (a.QuantitySize20 ?? 0) +
                    (a.QuantitySize21 ?? 0) + (a.QuantitySize22 ?? 0) + (a.QuantitySize23 ?? 0) + (a.QuantitySize24 ?? 0) +
                    (a.QuantitySize25 ?? 0) + (a.QuantitySize26 ?? 0) + (a.QuantitySize27 ?? 0) + (a.QuantitySize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) + (a.QuantitySize30 ?? 0);
                TotalAmount += (a.QuantitySize1 ?? 0) * (a.CostSize1 ?? 0) + (a.QuantitySize2 ?? 0) * (a.CostSize2 ?? 0) +
                    (a.QuantitySize3 ?? 0) * (a.CostSize3 ?? 0) + (a.QuantitySize4 ?? 0) * (a.CostSize4 ?? 0) +
                    (a.QuantitySize5 ?? 0) * (a.CostSize5 ?? 0) + (a.QuantitySize6 ?? 0) * (a.CostSize6 ?? 0) + (a.QuantitySize7 ?? 0) * (a.CostSize7 ?? 0) +
                    (a.QuantitySize8 ?? 0) * (a.CostSize8 ?? 0) + (a.QuantitySize9 ?? 0) * (a.CostSize10 ?? 0) +
                    (a.QuantitySize11 ?? 0) * (a.CostSize12 ?? 0) + (a.QuantitySize13 ?? 0) * (a.CostSize13 ?? 0) +
                    (a.QuantitySize14 ?? 0) * (a.CostSize14 ?? 0) + (a.QuantitySize15 ?? 0) * (a.CostSize15 ?? 0) +
                    (a.QuantitySize16 ?? 0) * (a.CostSize16 ?? 0) + (a.QuantitySize17 ?? 0) * (a.CostSize17 ?? 0) +
                    (a.QuantitySize18 ?? 0) * (a.CostSize18 ?? 0) + (a.QuantitySize19 ?? 0) * (a.CostSize19 ?? 0) +
                    (a.QuantitySize20 ?? 0) * (a.CostSize20 ?? 0) + (a.QuantitySize21 ?? 0) * (a.CostSize21 ?? 0) +
                    (a.QuantitySize22 ?? 0) * (a.CostSize22 ?? 0) + (a.QuantitySize23 ?? 0) * (a.CostSize23 ?? 0) +
                    (a.QuantitySize24 ?? 0) * (a.CostSize24 ?? 0) + (a.QuantitySize25 ?? 0) * (a.CostSize25 ?? 0) +
                    (a.QuantitySize26 ?? 0) * (a.CostSize26 ?? 0) + (a.QuantitySize27 ?? 0) * (a.CostSize28 ?? 0) +
                    (a.QuantitySize29 ?? 0) * (a.CostSize29 ?? 0) + (a.QuantitySize30 ?? 0) * (a.CostSize30 ?? 0);
            }
            var PurchaseOrder = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == OrderItem.PurchaseOrderId).FirstOrDefault();
            PurchaseOrder.Quantity = TotalQuantity;
            PurchaseOrder.Amount = TotalAmount;
			var product = Entities.Products.Where(x => x.IsActive == true && x.ProductSKU == OrderItem.ProductSKU && x.StyleSKU == OrderItem.StyleSKU).FirstOrDefault();
			Order.ProductID = product.Id;
			if (OrderItem.ItemSize1 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize1.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize1 + ",";
				}
			}
			if (OrderItem.ItemSize2 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize2.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize2 + ",";
				}
			}
			if (OrderItem.ItemSize3 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize3.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize3 + ",";
				}
			}
			if (OrderItem.ItemSize4 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize4.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize4 + ",";
				}
			}
			if (OrderItem.ItemSize5 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize5.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize5 + ",";
				}
			}
			if (OrderItem.ItemSize6 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize6.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize6 + ",";
				}
			}
			if (OrderItem.ItemSize7 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize7.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize7 + ",";
				}
			}
			if (OrderItem.ItemSize8 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize8.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize8 + ",";
				}
			}
			if (OrderItem.ItemSize9 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize9.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize9 + ",";
				}
			}
			if (OrderItem.ItemSize10 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize10.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize10 + ",";
				}
			}
			if (OrderItem.ItemSize11 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize10.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize11 + ",";
				}
			}
			if (OrderItem.ItemSize12 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize12.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize12 + ",";
				}
			}
			if (OrderItem.ItemSize13 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize13.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize13 + ",";
				}
			}
			if (OrderItem.ItemSize14 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize14.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize14 + ",";
				}
			}
			if (OrderItem.ItemSize15 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize15.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize15 + ",";
				}
			}
			if (OrderItem.ItemSize16 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize16.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize16 + ",";
				}
			}
			if (OrderItem.ItemSize17 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize17.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize17 + ",";
				}
			}
			if (OrderItem.ItemSize18 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize18.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize18 + ",";
				}
			}
			if (OrderItem.ItemSize19 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize19.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize19 + ",";
				}
			}
			if (OrderItem.ItemSize20 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize20.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize20 + ",";
				}
			}
			if (OrderItem.ItemSize21 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize21.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize21 + ",";
				}
			}
			if (OrderItem.ItemSize22 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize22.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize22 + ",";
				}
			}
			if (OrderItem.ItemSize23 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize23.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize23 + ",";
				}
			}
			if (OrderItem.ItemSize24 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize24.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize24 + ",";
				}
			}
			if (OrderItem.ItemSize25 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize25.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize25 + ",";
				}
			}
			if (OrderItem.ItemSize26 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize26.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize26 + ",";
				}
			}
			if (OrderItem.ItemSize27 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize27.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize27 + ",";
				}
			}
			if (OrderItem.ItemSize28 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize28.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize28 + ",";
				}
			}
			if (OrderItem.ItemSize29 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize29.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize29 + ",";
				}
			}
			if (OrderItem.ItemSize30 != null)
			{
				bool sizes1 = product.AvailableSize.Contains(OrderItem.ItemSize30.ToString());
				if (!sizes1)
				{
					product.AvailableSize += OrderItem.ItemSize30 + ",";
				}
			}
			
				Entities.SaveChanges();
	
			int id = Order.PurchaseOrderId;
            return Ok(id);
        }

    }
}