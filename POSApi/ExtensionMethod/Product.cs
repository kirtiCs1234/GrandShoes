 using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper.ExtensionMethod
{
    public static class ProductExtensionMethod
    {
        public static List<Product> RemoveReferences(this List<Product> list)
        {
            List<Product> list1 = new List<Product>();
            foreach (var item in list)
            {
                var obj = new Product();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<ProductSource> RemoveReferences(this List<ProductSource> list)
        {
            List<ProductSource> list1 = new List<ProductSource>();
            foreach(var item in list)
            {
                var obj = new ProductSource();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<Buyer> RemoveReferences(this List<Buyer> list)
        {
            List<Buyer> list1 = new List<Buyer>();
            foreach(var item in list)
            {
                var obj = new Buyer();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<SalesOrderItem> RemoveReferences(this List<SalesOrderItem> list)
        {
            List<SalesOrderItem> list1 = new List<SalesOrderItem>();
            foreach(var item in list)
            {
                var obj = new SalesOrderItem();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<PageName> RemoveReferences(this List<PageName> list)
        {
            var newList = new List<PageName>();
            if (list != null)
            {
                list.ForEach(item =>
                {
                    PageName obj = new PageName();
                    if (item != null)
                    {
                       // obj = item.RemoveReference();
                    }
                    newList.Add(item);
                });
            }
            return newList;
        }
        
        public static List<DiscountBranch> RemoveReferences(this List<DiscountBranch> list)
        {
            List<DiscountBranch> list1 = new List<DiscountBranch>();
            foreach(var item in list)
            {
                var obj = new DiscountBranch();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<DiscountSummary> RemoveReferences(this List<DiscountSummary> list)
        {
            List<DiscountSummary> list1 = new List<DiscountSummary>();
            foreach(var item in list)
            {
                var obj = new DiscountSummary();
                obj = item.RemoveReferences(); 
                list1.Add(obj);
            }
            return list1;
        }
        public static List<PromotionalDiscount> RemoveReferences(this List<PromotionalDiscount> list)
        {
            List<PromotionalDiscount> list1 = new List<PromotionalDiscount>();
            foreach(var item in list)
            {
                var obj = new PromotionalDiscount();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<CartonManagement> RemoveReferences(this List<CartonManagement> list)
        {
            List<CartonManagement> list1 = new List<CartonManagement>();
            foreach(var item in list)
            {
                var obj = new CartonManagement();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<PurchaseOrderItem> RemoveReferences(this List<PurchaseOrderItem> list)
        {
            List<PurchaseOrderItem> list1 = new List<PurchaseOrderItem>();
            foreach(var item in list)
            {
                var obj = new PurchaseOrderItem();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<PurchaseOrder> RemoveReferences(this List<PurchaseOrder> list)
        {
            List<PurchaseOrder> list1 = new List<PurchaseOrder>();
            foreach(var item in list)
            {
                var obj = new PurchaseOrder();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<Supplier> RemoveReferences(this List<Supplier> list)
        {
            List<Supplier> list1 = new List<Supplier>();
            foreach(var item in list)
            {
                var obj = new Supplier();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<Template> RemoveReferences(this List<Template> list)
        {
            List<Template> list1 = new List<Template>();
            foreach(var item in list)
            {
                var obj = new Template();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<SizeGrid> RemoveReferences(this List<SizeGrid> list)
        {
            List<SizeGrid> list1 = new List<SizeGrid>();
            foreach(var item in list)
            {
                var obj = new SizeGrid();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }

        public static List<MarkDown> RemoveReferences(this List<MarkDown> list)
		{
			List<MarkDown> list1 = new List<MarkDown>();
			foreach (var item in list)
			{
				var obj = new MarkDown();
				obj = item.RemoveReferences();
				list1.Add(obj);
			}
			return list1;
		}
		public static List<MarkDownBranch> RemoveReferences(this List<MarkDownBranch> list)
		{
			List<MarkDownBranch> list1 = new List<MarkDownBranch>();
			foreach(var item in list)
			{
				var obj = new MarkDownBranch();
				obj = item.RemoveReferences();
				list1.Add(obj);
			}
			return list1;
		}

		public static List<StaffMember> RemoveReferences(this List<StaffMember> list)
        {
            List<StaffMember> list1= new List<StaffMember>();
            foreach(var item in list)
            {
                var obj = new StaffMember();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<CartonManagementDetail> RemoveReferences(this List<CartonManagementDetail> list)
        {
            List<CartonManagementDetail> list1 = new List<CartonManagementDetail>();
            foreach(var item in list)
            {
                CartonManagementDetail obj = new CartonManagementDetail();
                obj = item.RemoveReferences();
            }
            return list1;
        }
		public static List<StockDistribution> RemoveReferences(this List<StockDistribution> list)
		{
			List<StockDistribution> list1 = new List<StockDistribution>();
			foreach(var item in list)
			{
				var obj = new StockDistribution();
				obj = item.RemoveReferences();
				list1.Add(obj);
			}
			return list1;
		}

		public static List<ReceiveOrder> RemoveReferences(this List<ReceiveOrder> list)
        {
            List<ReceiveOrder> list1 = new List<ReceiveOrder>();
            foreach(var item in list)
            {
                var obj = new ReceiveOrder();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<ReceiptOrderItem> RemoveReferences(this List<ReceiptOrderItem> list)
        {
            List<ReceiptOrderItem> list1 = new List<ReceiptOrderItem>();
            foreach(var item in list)
            {
                var obj = new ReceiptOrderItem();
                obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<Season> RemoveReferences(this List<Season> list)
        {
            List<Season> list1 = new List<Season>();
            foreach(var item in list)
            {
                var obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
        public static List<User> RemoveReferences(this List<User> list)
        {
            List<User> list1 = new List<User>();
            foreach(var item in list)
            {
                var obj= item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
		public static List<StockInventory> RemoveReferences(this List<StockInventory> list)
		{
			List<StockInventory> list1 = new List<StockInventory>();
			foreach(var item in list)
			{
				var obj = item.RemoveReferences();
				list1.Add(obj);
			}
			return list1;
		}

		public static List<Branch> RemoveReferences(this List<Branch> list)
        {
            List<Branch> list1 = new List<Branch>();
            foreach(var item in list)
            {
                var obj = item.RemoveReferences();
                list1.Add(obj);
            }
            return list1;
        }
		public static List<Log> RemoveReferences(this List<Log> list)
		{
			List<Log> list1 = new List<Log>();
			foreach(var item in list)
			{
				var obj = item.RemoveReferences();
				list1.Add(obj);
			}
			return list1;
		}
		public static Log RemoveReferences(this Log item)
		{
			Log obj = new Log();
			obj = item.RemoveReference();
			if (item.ActionLog != null)
			{
				obj.ActionLog = new ActionLog();
				obj.ActionLog = item.ActionLog.RemoveReference();
			}
			if (item.User != null)
			{
				obj.User = new User();
				obj.User = item.User.RemoveReference();
			}
			if (item.PageName != null)
			{
				obj.PageName = new PageName();
				obj.PageName = item.PageName.RemoveReference();
			}
			return obj;
		}
        public static DiscountBranch RemoveReferences(this DiscountBranch item)
        {
            DiscountBranch obj = new DiscountBranch();
            obj = item.RemoveReference();
            if (item.Branch != null)
            {
                obj.Branch = new Branch();
                obj.Branch = item.Branch.RemoveReference();
            }
            if (item.PromotionalDiscount != null)
            {
                obj.PromotionalDiscount = new PromotionalDiscount();
                obj.PromotionalDiscount = item.PromotionalDiscount.RemoveReference();
            }
            if (item.PromotionalDiscount.Product != null)
            {
                obj.PromotionalDiscount.Product = new Product();
                obj.PromotionalDiscount.Product = item.PromotionalDiscount.Product.RemoveReference();
            }
            if (item.PromotionalDiscount.DiscountSummary != null)
            {
                obj.PromotionalDiscount.DiscountSummary = new DiscountSummary();
                obj.PromotionalDiscount.DiscountSummary = item.PromotionalDiscount.DiscountSummary.RemoveReference();
            }
            return obj;
        }
		public static StockInventory RemoveReferences(this StockInventory item)
		{
			StockInventory obj = new StockInventory();
			obj=item.RemoveReference();
			if (item.Color != null)
			{
				obj.Color = new Color();
				obj.Color = item.Color.RemoveReference();
			}
			if (item.Product != null)
			{
				obj.Product = new Product();
				obj.Product = item.Product.RemoveReference();
				if (item.Product.Color != null)
				{
					obj.Product.Color = new Color();
					obj.Product.Color = item.Product.Color.RemoveReference();
				}
			}
			
			return obj;
		}
        public static SalesOrderItem RemoveReferences(this SalesOrderItem item)
        {
            SalesOrderItem obj = new SalesOrderItem();
            obj=item.RemoveReference();
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
                if (item.Product.Color != null)
                {
                    obj.Product.Color = new Color();
                    obj.Product.Color = item.Product.Color.RemoveReference();
                }
            }
            if (item.SalesOrder != null)
            {
                obj.SalesOrder = new SalesOrder();
                obj.SalesOrder = item.SalesOrder.RemoveReference();
                if (item.SalesOrder.Branch != null)
                {
                    obj.SalesOrder.Branch = new Branch();
                    obj.SalesOrder.Branch = item.SalesOrder.Branch.RemoveReference();
                }
                if (item.SalesOrder.StaffMember != null)
                {
                    obj.SalesOrder.StaffMember = new StaffMember();
                    obj.SalesOrder.StaffMember = item.SalesOrder.StaffMember.RemoveReference();
                }
            }
           
            return obj;

        }
        public static DiscountSummary RemoveReferences(this DiscountSummary item)
        {
            DiscountSummary obj = new DiscountSummary();
            obj = item.RemoveReference();
            
            return obj;
        }

        public static User RemoveReferences(this User item)
        {
            User obj = new User();
            obj = item.RemoveReference();
            if (item.Branch != null)
            {
                obj.Branch = new Branch();
                obj.Branch = item.Branch.RemoveReferences();
            }
            if (item.Role != null)
            {
                obj.Role = new Role();
                obj.Role = item.Role.RemoveReference();
            }
            return obj;
        }
        public static PromotionalDiscount RemoveReferences(this PromotionalDiscount item)
        {
            PromotionalDiscount obj = new PromotionalDiscount();
            obj = item.RemoveReference();
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            if (item.DiscountSummary != null)
            {
                obj.DiscountSummary = new DiscountSummary();
                obj.DiscountSummary = item.DiscountSummary.RemoveReference();
            }
            if (item.DiscountBranches != null)
            {
                obj.DiscountBranches = new List<DiscountBranch>();
                if(item.DiscountBranches.Count > 0)
                {
                    foreach(var itemj in item.DiscountBranches)
                    {
                        var obj2 = new DiscountBranch();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Branch != null)
                        {
                            obj2.Branch = new Branch();
                            obj2.Branch = itemj.Branch.RemoveReference();
                        }
                        obj.DiscountBranches.Add(obj2);
                    }
                }

            }
            return obj;
        }
        public static Buyer RemoveReferences(this Buyer item)
        {
            Buyer obj = new Buyer();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach(var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2!= null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3!= null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year!= null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static Supplier RemoveReferences(this Supplier item)
        {
            Supplier obj = new Supplier();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach (var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2 != null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3 != null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year != null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static Template RemoveReferences(this Template item)
        {
            Template obj = new Template();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach (var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2 != null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3 != null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year != null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static SizeGrid RemoveReferences(this SizeGrid item)
        {
            SizeGrid obj = new SizeGrid();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach (var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2 != null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3 != null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year != null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static Season RemoveReferences(this Season item)
        {
            Season obj = new Season();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach (var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2 != null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3 != null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year != null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static ProductSource RemoveReferences(this ProductSource item)
        {
            ProductSource obj = new ProductSource();
            obj = item.RemoveReference();
            if (item.Products != null)
            {
                obj.Products = new List<Product>();
                if (item.Products.Count > 0)
                {
                    foreach (var itemj in item.Products)
                    {
                        var obj2 = new Product();
                        obj2 = itemj.RemoveReference();
                        if (itemj.Buyer != null)
                        {
                            obj2.Buyer = new Buyer();
                            obj2.Buyer = itemj.Buyer.RemoveReference();
                        }
                        if (itemj.Color != null)
                        {
                            obj2.Color = new Color();
                            obj2.Color = itemj.Color.RemoveReference();
                        }
                        if (itemj.ProductCat1 != null)
                        {
                            obj2.ProductCat1 = new ProductCat1();
                            obj2.ProductCat1 = itemj.ProductCat1.RemoveReference();
                        }
                        if (itemj.ProductCat2 != null)
                        {
                            obj2.ProductCat2 = new ProductCat2();
                            obj2.ProductCat2 = itemj.ProductCat2.RemoveReference();
                        }
                        if (itemj.ProductCat3 != null)
                        {
                            obj2.ProductCat3 = new ProductCat3();
                            obj2.ProductCat3 = itemj.ProductCat3.RemoveReference();
                        }
                        if (itemj.ProductCat4 != null)
                        {
                            obj2.ProductCat4 = new ProductCat4();
                            obj2.ProductCat4 = itemj.ProductCat4.RemoveReference();
                        }
                        if (itemj.ProductSource != null)
                        {
                            obj2.ProductSource = new ProductSource();
                            obj2.ProductSource = itemj.ProductSource.RemoveReference();
                        }
                        if (itemj.Season != null)
                        {
                            obj2.Season = new Season();
                            obj2.Season = itemj.Season.RemoveReference();
                        }
                        if (itemj.SizeGrid != null)
                        {
                            obj2.SizeGrid = new SizeGrid();
                            obj2.SizeGrid = itemj.SizeGrid.RemoveReference();
                        }
                        if (itemj.Supplier != null)
                        {
                            obj2.Supplier = new Supplier();
                            obj2.Supplier = itemj.Supplier.RemoveReference();
                        }
                        if (itemj.Year != null)
                        {
                            obj2.Year = new Year();
                            obj2.Year = itemj.Year.RemoveReference();
                        }
                        obj.Products.Add(obj2);
                    }
                }
            }
            return obj;
        }
        public static PurchaseOrder RemoveReferences(this PurchaseOrder item)
        {
            PurchaseOrder obj = new PurchaseOrder();
            obj = item.RemoveReference();
            if (item.Supplier != null)
            {
                obj.Supplier = new Supplier();
                obj.Supplier = item.Supplier.RemoveReference();
            }
            if (item.Buyer != null)
            {
                obj.Buyer = new Buyer();
                obj.Buyer = item.Buyer.RemoveReference();
            }
            if (item.PurchaseOrderStatu != null)
            {
               obj.PurchaseOrderStatu = new PurchaseOrderStatu();
                obj.PurchaseOrderStatu = item.PurchaseOrderStatu.RemoveReference();
            }
            return obj;
        }
		public static MarkDown RemoveReferences(this MarkDown item)
		{
			MarkDown obj = new MarkDown();
			obj = item.RemoveReference();
			return obj;
		}
		public static MarkDownBranch RemoveReferences(this MarkDownBranch item)
		{
			MarkDownBranch obj = new MarkDownBranch();
			obj = item.RemoveReference();
			if (item.Branch != null)
			{
				obj.Branch = new Branch();
				obj.Branch = item.Branch.RemoveReference();
			}
			if (item.MarkDown != null)
			{
				obj.MarkDown = new MarkDown();
				obj.MarkDown = item.MarkDown.RemoveReference();
			}
			return obj;
		}
		public static PurchaseOrderItem RemoveReferences(this PurchaseOrderItem item)
        {
            PurchaseOrderItem obj = new PurchaseOrderItem();
            obj = item.RemoveReference();
            if (item.Color != null)
            {
                obj.Color = new Color();
                obj.Color = item.Color.RemoveReference();
            }
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
            }
            //if (item.ProductStyle != null)
            //{
            //    obj.ProductStyle = new ProductStyle();
            //    obj.ProductStyle = item.ProductStyle.RemoveReference();
            //}
            if (item.PurchaseOrder != null)
            {
                obj.PurchaseOrder = new PurchaseOrder();
                obj.PurchaseOrder = item.PurchaseOrder.RemoveReference();
            }
            if (item.PurchaseOrder.PurchaseOrderStatu != null)
            {
                obj.PurchaseOrder.PurchaseOrderStatu = new PurchaseOrderStatu();
                obj.PurchaseOrder.PurchaseOrderStatu = item.PurchaseOrder.PurchaseOrderStatu.RemoveReference();
            }
            if (item.SizeGrid != null)
            {
                obj.SizeGrid = new SizeGrid();
                obj.SizeGrid = item.SizeGrid.RemoveReference();
            }
            return obj;
        }
        public static PurchaseOrderStatu RemoveReferences(this PurchaseOrderStatu item)
        {
            PurchaseOrderStatu obj = new PurchaseOrderStatu();
            obj = item.RemoveReference();
            
            return obj;
        }
        //public static Buyer RemoveReferences(this Buyer item)
        //{
        //    Buyer obj = new Buyer();
        //    obj.BuyLimit
        //    return obj;
        //}
        public static Product RemoveReferences(this Product item)
        {
            Product obj = new Product();
            obj = item.RemoveReference();
            if (item.Buyer != null)
            {
                obj.Buyer = new Buyer();
                obj.Buyer = item.Buyer.RemoveReference();
            }
            if (item.Color != null)
            {
                obj.Color = new Color();
                obj.Color = item.Color.RemoveReference();
            }
            if (item.ProductCat1 != null)
            {
                obj.ProductCat1 = new ProductCat1();
                obj.ProductCat1 = item.ProductCat1.RemoveReference();
            }
            if (item.ProductCat2!= null)
            {
                obj.ProductCat2 = new ProductCat2();
                obj.ProductCat2 = item.ProductCat2.RemoveReference();
            }
            if (item.ProductCat3!= null)
            {
                obj.ProductCat3= new ProductCat3();
                obj.ProductCat3 = item.ProductCat3.RemoveReference();
            }
            if (item.ProductCat4!= null)
            {
                obj.ProductCat4= new ProductCat4();
                obj.ProductCat4= item.ProductCat4.RemoveReference();
            }
         
            if (item.ProductSource != null)
            {
                obj.ProductSource = new ProductSource();
                obj.ProductSource = item.ProductSource.RemoveReference();

            }
           
            if (item.Season != null)
            {
                obj.Season = new Season();
                obj.Season = item.Season.RemoveReference();
            }
            if (item.SizeGrid != null)
            {
                obj.SizeGrid = new SizeGrid();
                obj.SizeGrid = item.SizeGrid.RemoveReference();
            }
            if (item.Supplier != null)
            {
                obj.Supplier = new Supplier();
                obj.Supplier = item.Supplier.RemoveReference();
            }
            if (item.Year != null)
            {
                obj.Year = new Year();
                obj.Year = item.Year.RemoveReference();
            }
           
            return obj;
        }
        public static StaffMember RemoveReferences(this StaffMember item)
        {
            StaffMember obj = new StaffMember();
            obj = item.RemoveReference();
            if (item.User != null)
            {
                obj.User = new User();
                obj.User = item.User.RemoveReference();
                obj.User.Branch = item.User.Branch.RemoveReference();
            }
            if (item.StaffStatu != null)
            {
                obj.StaffStatu = new StaffStatu();
                obj.StaffStatu = item.StaffStatu.RemoveReference();
            }
            if (item.User.Role != null)
            {
                obj.User.Role = new Role();
                obj.User.Role = item.User.Role.RemoveReference();
            }
            return obj;
        }
		public static StockDistribution RemoveReferences(this StockDistribution item)
		{
			StockDistribution obj = new StockDistribution();
			obj = item.RemoveReference();
			if (item.Branch != null)
			{
				obj.Branch = new Branch();
				obj.Branch = item.Branch.RemoveReference();
			}
			if (item.StockDistributionStatu != null)
			{
				obj.StockDistributionStatu = new StockDistributionStatu();
				obj.StockDistributionStatu = item.StockDistributionStatu.RemoveReference();
			}
			if (item.StockDistributionSummary != null)
			{
				obj.StockDistributionSummary = new StockDistributionSummary();
				obj.StockDistributionSummary = item.StockDistributionSummary.RemoveReference();
			}
			if (item.Product != null)
			{
				obj.Product = new Product();
				obj.Product = item.Product.RemoveReference();
				if(item.Product.Color != null)
				{
					obj.Product.Color = new Color();
					obj.Product.Color = item.Product.Color.RemoveReference();
				}
                if (item.Product.Supplier != null)
                {
                    obj.Product.Supplier = new Supplier();
                    obj.Product.Supplier = item.Product.Supplier.RemoveReference();
                }
            }
            
			return obj;
		}

		public static ReceiveOrder RemoveReferences(this ReceiveOrder item)
        {
            ReceiveOrder obj = new ReceiveOrder();
            obj = item.RemoveReference();
           
            if (item.PurchaseOrder != null)
            {
                obj.PurchaseOrder = new PurchaseOrder();
                obj.PurchaseOrder = item.PurchaseOrder.RemoveReference();

            }
            
            return obj;
        }
        public static ReceiptOrderItem RemoveReferences(this ReceiptOrderItem item)
        {
            ReceiptOrderItem obj = new ReceiptOrderItem();
            obj = item.RemoveReference();
            if (item.Product != null)
            {
                obj.Product = new Product();
                obj.Product = item.Product.RemoveReferences();
            }
           
            if (item.ReceiveOrder != null)
            {
                obj.ReceiveOrder = new ReceiveOrder();
                obj.ReceiveOrder = item.ReceiveOrder.RemoveReferences();
            }
            if (item.ReceiveOrder.PurchaseOrder != null)
            {
                obj.ReceiveOrder.PurchaseOrder = new PurchaseOrder();
                obj.ReceiveOrder.PurchaseOrder = item.ReceiveOrder.PurchaseOrder.RemoveReferences();
            }
            if (item.ReceiveOrder.PurchaseOrder.PurchaseOrderStatu != null)
            {
                obj.ReceiveOrder.PurchaseOrder.PurchaseOrderStatu = new PurchaseOrderStatu();
                obj.ReceiveOrder.PurchaseOrder.PurchaseOrderStatu = item.ReceiveOrder.PurchaseOrder.PurchaseOrderStatu.RemoveReferences();
            }
            return obj;
        }
        public static Branch RemoveReferences(this Branch item)
        {
            Branch obj = new Branch();
            obj = item.RemoveReference();
            return obj;
        }
        public static CartonManagement RemoveReferences(this CartonManagement item)
        {
            CartonManagement obj = new CartonManagement();
            obj = item.RemoveReference();
            if (item.Branch != null)
            {
                obj.Branch = new Branch();
                obj.Branch = item.Branch.RemoveReference();
            }
            if (item.StockDistributionSummary != null)
            {
                obj.StockDistributionSummary = new StockDistributionSummary();
                obj.StockDistributionSummary = item.StockDistributionSummary.RemoveReference();
            }
                return obj;
        }
        public static PageName RemoveReferences(this PageName item)
        {
            PageName obj = new PageName();
            if (item != null)
            {
                obj = item.RemoveReference();
            }
            return obj;
        }
        public static CartonManagementDetail RemoveReferences(this CartonManagementDetail item)
        {
            CartonManagementDetail obj = new CartonManagementDetail();
            obj = item.RemoveReference();
            if (item.CartonManagement != null)
            {
                obj.CartonManagement = new CartonManagement();
                obj.CartonManagement = item.CartonManagement.RemoveReference();
            }
            if (item.Product != null)
            {
              obj.Product = new Product();
                obj.Product = item.Product.RemoveReference();
				if (item.Product.Color != null)
				{
					obj.Product.Color = new Color();
					obj.Product.Color = item.Product.Color.RemoveReference();
				}
            }
            
            return obj;
        }
        private static CartonManagementDetail RemoveReference(this CartonManagementDetail item)
        {
            CartonManagementDetail obj = new CartonManagementDetail();
            obj.CartonManagementID = item.CartonManagementID;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
           // obj.ProductID = item.ProductID;
            obj.Z01 = item.Z01;
            obj.Z02 = item.Z02;
            obj.Z03 = item.Z03;
            obj.Z04  = item.Z04;
            obj.Z05= item.Z05;
            obj.Z06 = item.Z06;
            obj.Z07 = item.Z07;
            obj.Z08 = item.Z08;
            obj.Z09 = item.Z09;
            obj.Z10 = item.Z10;
            obj.Z11 = item.Z11;
            obj.Z12 = item.Z12;
            obj.Z13 = item.Z13;
            obj.Z14 = item.Z14;
            obj.Z15 = item.Z15;
            obj.Z16 = item.Z16;
            obj.Z17 = item.Z17;
            obj.Z18 = item.Z18;
            obj.Z19 = item.Z19;
            obj.Z20 = item.Z20;
            obj.Z21 = item.Z21;
            obj.Z22 = item.Z22;
            obj.Z23 = item.Z23;
            obj.Z24 = item.Z24;
            obj.Z25 = item.Z25;
            obj.Z26 = item.Z26;
            obj.Z27 = item.Z27;
            obj.Z28 = item.Z28;
            obj.Z29 = item.Z29;
            obj.Z30 = item.Z30;
            return obj;
        }
        private static StockDistributionSummary RemoveReference(this StockDistributionSummary item)
        {
            StockDistributionSummary obj = new StockDistributionSummary();
            obj.DateClose = item.DateClose;
            obj.DateOpen = item.DateOpen;
            obj.DistributionNumber = item.DistributionNumber;
            obj.Id = item.Id;
            obj.InvoiceId = item.InvoiceId;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            return obj;
        }
        private static Role RemoveReference(this Role item)
        {
            Role obj = new Role();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.RoleName = item.RoleName;
            return obj;
        }
		private static PageName RemoveReference(this PageName item)
		{
			PageName obj = new PageName();
			obj.Id = item.Id;
			obj.IsActive = item.IsActive;
			obj.Page = item.Page;
			obj.IsAdminPage = item.IsAdminPage;
			return obj;
		}

		private static Branch RemoveReference(this Branch item)
        {
            Branch obj = new Branch();
            obj.BranchCode = item.BranchCode;
            obj.AddressLine1 = item.AddressLine1;
            obj.AddressLine2 = item.AddressLine2;
            obj.AddressLine3 = item.AddressLine3;
            obj.AreaCode = item.AreaCode;
            obj.DateClosed = item.DateClosed;
            obj.DateOpen = item.DateOpen;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.IsClosed = item.IsClosed;
            obj.IsHeadOffice = item.IsHeadOffice;
            obj.IsSendStock = item.IsSendStock;
            obj.LogId = item.LogId;
            obj.Name = item.Name;
            obj.PostalCode = item.PostalCode;
            obj.StoreSize = item.StoreSize;
            obj.Telephone = item.Telephone;
            return obj;
        }
	   private static ActionLog RemoveReference(this ActionLog item)
		{
			ActionLog obj = new ActionLog();
			obj.Id = item.Id;
			obj.ActionLogType = item.ActionLogType;
			obj.IsActive = item.IsActive;
			return obj;
		}
		private static StockInventory RemoveReference(this StockInventory item)
		{
			StockInventory obj = new StockInventory();
			obj.BracketNumber = item.BracketNumber;
			obj.ColorID = item.ColorID;
			obj.Id = item.Id;
			obj.IsActive = item.IsActive;
			obj.LogId = item.LogId;
			obj.ProductID = item.ProductID;
			obj.Quantity01 = item.Quantity01;
			obj.Quantity02 = item.Quantity02;
			obj.Quantity03 = item.Quantity03;
			obj.Quantity04 = item.Quantity04;
			obj.Quantity05 = item.Quantity05;
			obj.Quantity06 = item.Quantity06;
			obj.Quantity07 = item.Quantity07;
			obj.Quantity08 = item.Quantity08;
			obj.Quantity09 = item.Quantity09;
			obj.Quantity10 = item.Quantity10;
			obj.Quantity11 = item.Quantity11;
			obj.Quantity12 = item.Quantity12;
			obj.Quantity13 = item.Quantity13;
			obj.Quantity14 = item.Quantity14;
			obj.Quantity15 = item.Quantity15;
			obj.Quantity16 = item.Quantity16;
			obj.Quantity17 = item.Quantity17;
			obj.Quantity18 = item.Quantity18;
			obj.Quantity19 = item.Quantity19;
			obj.Quantity20 = item.Quantity20;
			obj.Quantity21 = item.Quantity21;
			obj.Quantity22 = item.Quantity22;
			obj.Quantity23 = item.Quantity23;
			obj.Quantity24 = item.Quantity24;
			obj.Quantity25 = item.Quantity25;
			obj.Quantity26 = item.Quantity26;
			obj.Quantity27 = item.Quantity27;
			obj.Quantity28 = item.Quantity28;
			obj.Quantity29 = item.Quantity29;
			obj.Quantity30 = item.Quantity30;
			return obj;
		}

		private static CartonManagement RemoveReference(this CartonManagement item)
        {
            CartonManagement obj = new CartonManagement();
            obj.BranchID = item.BranchID;
            obj.CartonNumber = item.CartonNumber;
            obj.DistributionSummaryID = item.DistributionSummaryID;
            obj.IBTNumber = item.IBTNumber;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.IsDispatched = item.IsDispatched;
            obj.PackDate = item.PackDate;
            obj.TotalItems = item.TotalItems;
            return obj;
        }
        private static ReceiptOrderItem RemoveReference(this ReceiptOrderItem item)
        {
            ReceiptOrderItem obj = new ReceiptOrderItem();
           // obj.ColumnNumber = item.ColumnNumber;
            obj.Cost01 = item.Cost01;
            obj.Cost02 = item.Cost02;
            obj.Cost03 = item.Cost03;
            obj.Cost04 = item.Cost04;
            obj.Cost05 = item.Cost05;
            obj.Cost06 = item.Cost06;
            obj.Cost07 = item.Cost07;
            obj.Cost08 = item.Cost08;
            obj.Cost09 = item.Cost09;
            obj.Cost10 = item.Cost10;
            obj.Cost11 = item.Cost11;
            obj.Cost12 = item.Cost12;
            obj.Cost13 = item.Cost13;
            obj.Cost14 = item.Cost14;
            obj.Cost15 = item.Cost15;
            obj.Cost16 = item.Cost16;
            obj.Cost17 = item.Cost17;
            obj.Cost18 = item.Cost18;
            obj.Cost19 = item.Cost19;
            obj.Cost20 = item.Cost20;
            obj.Cost21 = item.Cost21;
            obj.Cost22 = item.Cost22;
            obj.Cost23 = item.Cost23;
            obj.Cost24 = item.Cost24;
            obj.Cost25 = item.Cost25;
            obj.Cost26 = item.Cost26;
            obj.Cost27 = item.Cost27;
            obj.Cost28 = item.Cost28;
            obj.Cost29 = item.Cost29;
            obj.Cost30 = item.Cost30;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.ProductId = item.ProductId;
           // obj.ProductStyleId = item.ProductStyleId;
            obj.Quantity01 = item.Quantity01;
            obj.Quantity02 = item.Quantity02;
            obj.Quantity03 = item.Quantity03;
            obj.Quantity04 = item.Quantity04;
            obj.Quantity05 = item.Quantity05;
            obj.Quantity06 = item.Quantity06;
            obj.Quantity07 = item.Quantity07;
            obj.Quantity08 = item.Quantity08;
            obj.Quantity09 = item.Quantity09;
            obj.Quantity10 = item.Quantity10;
            obj.Quantity11 = item.Quantity11;
            obj.Quantity12 = item.Quantity12;
            obj.Quantity13 = item.Quantity13;
            obj.Quantity14 = item.Quantity14;
            obj.Quantity15 = item.Quantity15;
            obj.Quantity16 = item.Quantity16;
            obj.Quantity17 = item.Quantity17;
            obj.Quantity18 = item.Quantity18;
            obj.Quantity19 = item.Quantity19;
            obj.Quantity20 = item.Quantity20;
            obj.Quantity21 = item.Quantity21;
            obj.Quantity22 = item.Quantity22;
            obj.Quantity23 = item.Quantity23;
            obj.Quantity24 = item.Quantity24;
            obj.Quantity25 = item.Quantity25;
            obj.Quantity26 = item.Quantity26;
            obj.Quantity27 = item.Quantity27;
            obj.Quantity28 = item.Quantity28;
            obj.Quantity29 = item.Quantity29;
            obj.Quantity30 = item.Quantity30;
            obj.ReceiptOrderId = item.ReceiptOrderId;
            obj.SalesCost = item.SalesCost;
            
            return obj;
        }
        private static ReceiveOrder RemoveReference(this ReceiveOrder item)
        {
            ReceiveOrder obj = new ReceiveOrder();
            obj.SupplierInvoice = item.SupplierInvoice;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.PurchaseOrderId = item.PurchaseOrderId;
            obj.ReceiptDate = item.ReceiptDate;
            obj.ReceiptNumber = item.ReceiptNumber;
            obj.ReceiptStatusId = item.ReceiptStatusId;
            obj.TotalCost = item.TotalCost;
            obj.TotalQuantity = item.TotalQuantity;
            obj.TotalVAT = item.TotalVAT;
            return obj;
        }
        
        private static StaffStatu RemoveReference(this StaffStatu item)
        {
            StaffStatu obj = new StaffStatu();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.StatusName = item.StatusName;
            return obj;
        }
        private static User RemoveReference(this User item)
        {
            User obj = new User();
            obj.BranchID = item.BranchID;
            obj.Email = item.Email;
            obj.FirstName = item.FirstName;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.IsPrimaryAccountHolder = item.IsPrimaryAccountHolder;
            obj.IsVerified = item.IsVerified;
            obj.LastName = item.LastName;
            obj.MiddleName = item.MiddleName;
            obj.Password = item.Password;
            obj.PasswordHash = item.PasswordHash;
            obj.PasswordSalt = item.PasswordSalt;
            obj.Phone = item.Phone;
            obj.RoleID = item.RoleID;
            return obj;
    }
		private static StockDistribution RemoveReference(this StockDistribution item)
		{
			StockDistribution obj = new StockDistribution();
			obj.BranchId = item.BranchId;
			obj.DistributionDate = item.DistributionDate;
			obj.Id = item.Id;
			obj.InvoiceId = item.InvoiceId;
			obj.IsActive = item.IsActive;
			obj.LogId = item.LogId;
			obj.ProductID = item.ProductID;
			obj.Quantity01 = item.Quantity01;
			obj.Quantity02 = item.Quantity02;
			obj.Quantity03 = item.Quantity03;
			obj.Quantity04 = item.Quantity04;
			obj.Quantity05 = item.Quantity05;
			obj.Quantity06 = item.Quantity06;
			obj.Quantity07 = item.Quantity07;
			obj.Quantity08 = item.Quantity08;
			obj.Quantity09 = item.Quantity09;
			obj.Quantity10 = item.Quantity10;
			obj.Quantity11 = item.Quantity11;
			obj.Quantity12 = item.Quantity12;
			obj.Quantity13 = item.Quantity13;
			obj.Quantity14 = item.Quantity14;
			obj.Quantity15 = item.Quantity15;
			obj.Quantity16 = item.Quantity16;
			obj.Quantity17 = item.Quantity17;
			obj.Quantity18 = item.Quantity18;
			obj.Quantity19 = item.Quantity19;
			obj.Quantity20 = item.Quantity20;
			obj.Quantity21 = item.Quantity21;
			obj.Quantity22 = item.Quantity22;
			obj.Quantity23 = item.Quantity23;
			obj.Quantity24 = item.Quantity24;
			obj.Quantity25 = item.Quantity25;
			obj.Quantity26 = item.Quantity26;
			obj.Quantity27 = item.Quantity27;
			obj.Quantity28 = item.Quantity28;
			obj.Quantity29 = item.Quantity29;
			obj.Quantity30 = item.Quantity30;
			obj.StockDistributionStatusId = item.StockDistributionStatusId;
			obj.StockDistributionSummaryId = item.StockDistributionSummaryId;
			return obj;
		}

		private static StaffMember RemoveReference(this StaffMember item)
        {
            StaffMember obj = new StaffMember();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.IsFingurPrintAccess = item.IsFingurPrintAccess;
            obj.JoiningDate = item.JoiningDate;
            obj.LogId = item.LogId;
            obj.ProfilePic = item.ProfilePic;
            obj.StaffStatusId = item.StaffStatusId;
            obj.UserId = item.UserId;
            return obj;
        }
		private static MarkDown RemoveReference(this MarkDown item)
		{
			MarkDown obj = new MarkDown();
			obj.EffectiveDate = item.EffectiveDate;
			obj.Id = item.Id;
			obj.IsActive = item.IsActive;
			obj.IsPercentageOriginalPrice = item.IsPercentageOriginalPrice;
			obj.NewCashPrice = item.NewCashPrice;
			obj.NewSellingPrice = item.NewSellingPrice;
			obj.OriginalSellingPrice = item.OriginalSellingPrice;
			obj.PercentageDecrease = item.PercentageDecrease;
			obj.ProductSKU = item.ProductSKU;
			obj.StyleSKU = item.StyleSKU;
			return obj;
		}

		private static PurchaseOrder RemoveReference(this PurchaseOrder item)
        {
            PurchaseOrder obj = new PurchaseOrder();
            obj.Amount = item.Amount;
            obj.BuyerId = item.BuyerId;
            obj.ClientInvoiceNumber = item.ClientInvoiceNumber;
            obj.ExpectedDeliveryDate = item.ExpectedDeliveryDate;
            obj.FirstDeliveryDate = item.FirstDeliveryDate;
            obj.ID = item.ID;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.OrderCompletionDate = item.OrderCompletionDate;
            obj.OrderDate = item.OrderDate;
            obj.OrderNumber = item.OrderNumber;
            obj.PurchaseOrderStatusId = item.PurchaseOrderStatusId;
            obj.Quantity = item.Quantity;
            obj.SupplierId = item.SupplierId;
            obj.VatAmount = item.VatAmount;
            return obj;
        }
        private static DiscountBranch RemoveReference(this DiscountBranch item)
        {
            DiscountBranch obj = new DiscountBranch();
            obj.BranchID = item.BranchID;
            obj.DiscountID = item.DiscountID;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static PurchaseOrderItem RemoveReference(this PurchaseOrderItem item)
        {
            PurchaseOrderItem obj = new PurchaseOrderItem();
            obj.Amount = item.Amount;
            obj.ColorId = item.ColorId;
            obj.CostSize1 = item.CostSize1;
            obj.CostSize2 = item.CostSize2;
            obj.CostSize3 = item.CostSize3;
            obj.CostSize4 = item.CostSize4;
            obj.CostSize5 = item.CostSize5;
            obj.CostSize6 = item.CostSize6;
            obj.CostSize7 = item.CostSize7;
            obj.CostSize8 = item.CostSize8;
            obj.CostSize9 = item.CostSize9;
            obj.CostSize10 = item.CostSize10;
            obj.CostSize11 = item.CostSize11;
            obj.CostSize12 = item.CostSize12;
            obj.CostSize13 = item.CostSize13;
            obj.CostSize14 = item.CostSize14;
            obj.CostSize15 = item.CostSize15;
            obj.CostSize16 = item.CostSize16;
            obj.CostSize17 = item.CostSize17;
            obj.CostSize18 = item.CostSize18;
            obj.CostSize19 = item.CostSize19;
            obj.CostSize20 = item.CostSize20;
            obj.CostSize21 = item.CostSize21;
            obj.CostSize22 = item.CostSize22;
            obj.CostSize23 = item.CostSize23;
            obj.CostSize24= item.CostSize24;
            obj.CostSize25= item.CostSize25;
            obj.CostSize26 = item.CostSize26;
            obj.CostSize27= item.CostSize27;
            obj.CostSize28= item.CostSize28;
            obj.CostSize29= item.CostSize29;
            obj.CostSize30= item.CostSize30;
            obj.ID = item.ID;
            obj.IsActive = item.IsActive;
            obj.ItemSize1 = item.ItemSize1;
            obj.ItemSize2 = item.ItemSize2;
            obj.ItemSize3 = item.ItemSize3;
            obj.ItemSize4 = item.ItemSize4;
            obj.ItemSize5 = item.ItemSize5;
            obj.ItemSize6 = item.ItemSize6;
            obj.ItemSize7 = item.ItemSize7;
            obj.ItemSize8 = item.ItemSize8;
            obj.ItemSize9 = item.ItemSize9;
            obj.ItemSize10 = item.ItemSize10;
            obj.ItemSize11 = item.ItemSize11;
            obj.ItemSize12 = item.ItemSize12;
            obj.ItemSize13 = item.ItemSize13;
            obj.ItemSize14 = item.ItemSize14;
            obj.ItemSize15 = item.ItemSize15;
            obj.ItemSize16 = item.ItemSize16;
            obj.ItemSize17 = item.ItemSize17;
            obj.ItemSize18 = item.ItemSize18;
            obj.ItemSize19 = item.ItemSize19;
            obj.ItemSize20= item.ItemSize20;
            obj.ItemSize21 = item.ItemSize21;
            obj.ItemSize22 = item.ItemSize22;
        
            obj.ItemSize23 = item.ItemSize23;
            obj.ItemSize24= item.ItemSize24;
            obj.ItemSize25= item.ItemSize25;
            obj.ItemSize26= item.ItemSize26;
            obj.ItemSize27= item.ItemSize27;
            obj.ItemSize28= item.ItemSize28;
            obj.ItemSize29= item.ItemSize29;
            obj.ItemSize30= item.ItemSize30;
            obj.LogId = item.LogId;
            obj.OrderItemDate = item.OrderItemDate;
            obj.ProductID = item.ProductID;
           // obj.ProductStyleId = item.ProductStyleId;
            obj.PurchaseOrderId = item.PurchaseOrderId;
            obj.QuantitySize1 = item.QuantitySize1;
            obj.QuantitySize2 = item.QuantitySize2;
            obj.QuantitySize3 = item.QuantitySize3;
            obj.QuantitySize4 = item.QuantitySize4;
            obj.QuantitySize5 = item.QuantitySize5;
            obj.QuantitySize6 = item.QuantitySize6;
            obj.QuantitySize7 = item.QuantitySize7;
            obj.QuantitySize8 = item.QuantitySize8;
            obj.QuantitySize9 = item.QuantitySize9;
            obj.QuantitySize10 = item.QuantitySize10;
            obj.QuantitySize11 = item.QuantitySize11;
            obj.QuantitySize12 = item.QuantitySize12;
            obj.QuantitySize13 = item.QuantitySize13;
            obj.QuantitySize14 = item.QuantitySize14;
            obj.QuantitySize15 = item.QuantitySize15;
            obj.QuantitySize16 = item.QuantitySize16;
            obj.QuantitySize17 = item.QuantitySize17;
            obj.QuantitySize18 = item.QuantitySize18;
            obj.QuantitySize19 = item.QuantitySize19;
            obj.QuantitySize20= item.QuantitySize20;
            obj.QuantitySize21 = item.QuantitySize21;
            obj.QuantitySize22= item.QuantitySize22;
            obj.QuantitySize23= item.QuantitySize23;
            obj.QuantitySize24= item.QuantitySize24;
            obj.QuantitySize25= item.QuantitySize25;
            obj.QuantitySize26= item.QuantitySize26;
            obj.QuantitySize27= item.QuantitySize27;
            obj.QuantitySize28= item.QuantitySize28;
            obj.QuantitySize29= item.QuantitySize29;
            obj.QuantitySize30= item.QuantitySize30;
            obj.SizeGridId = item.SizeGridId;
            obj.SuplierStyle = item.SuplierStyle;
            return obj;

        }
        private static Log RemoveReference(this Log item)
        {
            Log obj = new Log();
            obj.ActionLog = item.ActionLog;
            obj.ActionLogId = item.ActionLogId;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogDate = item.LogDate;
			//obj.LogTable = item.LogTable;
			
            obj.PageId = item.PageId;
            obj.UserId = item.UserId;
            obj.Comment = item.Comment;
            return obj;
        }
        private static Year RemoveReference(this Year item)
        {
            Year obj = new Year();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.Year1 = item.Year1;
            return obj;
        }
        private static Supplier RemoveReference(this Supplier item)
        {
            Supplier obj = new Supplier();
            obj.Code = item.Code;
            obj.ContactNumber = item.ContactNumber;
            obj.CorrespondanceAddress1 = item.CorrespondanceAddress1;
            obj.CorrespondanceAddress2 = item.CorrespondanceAddress2;
            obj.CorrespondanceAddress3 = item.CorrespondanceAddress3;
            obj.CorrespondanceCity = item.CorrespondanceCity;
            obj.CorrespondanceCountry = item.CorrespondanceCountry;
            obj.CorrespondancePostalCode = item.CorrespondancePostalCode;
            obj.FaxNumber = item.FaxNumber;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.Limit = item.Limit;
            obj.LogId = item.LogId;
            obj.Name = item.Name;
            obj.PermanentAddress1 = item.PermanentAddress1;
            obj.PermanentAddress2 = item.PermanentAddress2;
            obj.PermanentAddress3 = item.PermanentAddress3;
            obj.PermanentCity = item.PermanentCity;
            obj.PermanentCountry = item.PermanentCountry;
            obj.PermanentPostalCode = item.PermanentPostalCode;
            obj.RegistrationDate = item.RegistrationDate;
            return obj;
        }
        private static SizeGrid RemoveReference(this SizeGrid item)
        {
            SizeGrid obj = new SizeGrid();
            obj.GridNumber = item.GridNumber;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.Z01 = item.Z01;
            obj.Z02 = item.Z02;
            obj.Z03 = item.Z03;
            obj.Z04 = item.Z04;
            obj.Z05 = item.Z05;
            obj.Z06 = item.Z06;
            obj.Z07 = item.Z07;
            obj.Z08 = item.Z08;
            obj.Z09 = item.Z09;
            obj.Z10 = item.Z10;
            obj.Z11 = item.Z11;
            obj.Z12 = item.Z12;
            obj.Z13 = item.Z13;
            obj.Z14 = item.Z14;
            obj.Z15 = item.Z15;
            obj.Z16 = item.Z16;
            obj.Z17 = item.Z17;
            obj.Z18 = item.Z18;
            obj.Z19 = item.Z19;
            obj.Z20 = item.Z20;
            obj.Z21 = item.Z21;
            obj.Z22 = item.Z22;
            obj.Z23 = item.Z23;
            obj.Z24 = item.Z24;
            obj.Z25 = item.Z25;
            obj.Z26 = item.Z26;
            obj.Z27 = item.Z27;
            obj.Z28 = item.Z28;
            obj.Z29 = item.Z29;
            obj.Z30 = item.Z30;
            return obj;
        }
        private static SalesOrder RemoveReference(this SalesOrder item)
        {
            SalesOrder obj = new SalesOrder();
            obj.BranchId = item.BranchId;
            obj.Discount = item.Discount;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.OrderNumber = item.OrderNumber;
            obj.PacketQuantity = item.PacketQuantity;
            obj.PaymentType = item.PaymentType;
            obj.PhoneNo = item.PhoneNo;
            obj.PromoCode = item.PromoCode;
            obj.SalesId = item.SalesId;
            obj.SaleType = item.SaleType;
            obj.TotalAmount = item.TotalAmount;
            obj.TotalQuantity = item.TotalQuantity;
            obj.TransactionDate = item.TransactionDate;
            return obj;
        }
        private static SalesOrderItem RemoveReference(this SalesOrderItem item)
        {
            SalesOrderItem obj = new SalesOrderItem();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.IsMarkDown = item.IsMarkDown;
            obj.PricePerUnit = item.PricePerUnit;
            obj.ProductSize = item.ProductSize;
            obj.Quantity = item.Quantity;
            obj.RemainingQuantity = item.RemainingQuantity;
            obj.SalesOrderId = item.SalesOrderId;
            obj.TotalPrice = item.TotalPrice;
            obj.ProductId = item.ProductId;
            return obj;
        }
        private static Season RemoveReference(this Season item)
        {
            Season obj = new Season();
            obj.Code = item.Code;
            obj.Description = item.Code;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductCat1 RemoveReference(this ProductCat1 item)
        {
            ProductCat1 obj = new ProductCat1();
            obj.CateName = item.CateName;
            obj.Code = item.Code;
            obj.CreatedOn = item.CreatedOn;
            obj.UpdatedOn = item.UpdatedOn;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductCat2 RemoveReference(this ProductCat2 item)
        {
            ProductCat2 obj = new ProductCat2();
            obj.CateName = item.CateName;
            obj.Code = item.Code;
            obj.CreatedOn = item.CreatedOn;
            obj.UpdatedOn = item.UpdatedOn;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductCat3 RemoveReference(this ProductCat3 item)
        {
            ProductCat3 obj = new ProductCat3();
            obj.CateName = item.CateName;
            obj.Code = item.Code;
            obj.CreatedOn = item.CreatedOn;
            obj.UpdatedOn = item.UpdatedOn;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductCat4 RemoveReference(this ProductCat4 item)
        {
            ProductCat4 obj = new ProductCat4();
            obj.CateName = item.CateName;
            obj.Code = item.Code;
            obj.CreatedOn = item.CreatedOn;
            obj.UpdatedOn = item.UpdatedOn;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static PromotionalDiscount RemoveReference(this PromotionalDiscount item)
        {
            PromotionalDiscount obj = new PromotionalDiscount();
            obj.DiscountSummaryID = item.DiscountSummaryID;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.ProductID = item.ProductID;
            return obj;
        }
        private static DiscountSummary RemoveReference(this DiscountSummary item)
        {
            DiscountSummary obj = new DiscountSummary();
            obj.DiscountType = item.DiscountType;
            obj.Id = item.Id;
            obj.DiscountValue = item.DiscountValue;
            obj.FromDate = item.FromDate;
            obj.FromPrice = item.FromPrice;
            obj.FromProductSKU = item.FromProductSKU;
            obj.FromStyleSKU = item.FromStyleSKU;
            obj.ToDate = item.ToDate;
            obj.ToPrice = item.ToPrice;
            obj.ToProductSKU = item.ToProductSKU;
            obj.ToStyleSKU = item.ToStyleSKU;
            return obj;
        }
        private static Product RemoveReference(this Product item)
        {
            Product obj = new Product();
            obj.ActualSellingPrice = item.ActualSellingPrice??0;
            obj.AvailableSize = item.AvailableSize;
            obj.Barcode = item.Barcode;
            obj.BuyerID = item.BuyerID;
            obj.ColorID = item.ColorID;
            obj.CostPrice = item.CostPrice;
            obj.CostPriceUSD = item.CostPriceUSD;
            obj.CreatedOn = item.CreatedOn;
            obj.DefaultTemplateID = item.DefaultTemplateID;
            obj.Id = item.Id;
            obj.Image1 = item.Image1;
            obj.Image2 = item.Image2;
            obj.Image3 = item.Image3;
            obj.Image4 = item.Image4;
            obj.IsActive = item.IsActive;
            obj.IsAllowZero = item.IsAllowZero;
            obj.IsConsignment = item.IsConsignment;
            obj.IsDiscontinue = item.IsDiscontinue;
            obj.IsFreeGift = item.IsFreeGift;
            obj.IsMarkDown = item.IsMarkDown;
            obj.IsVPI = item.IsVPI;
            obj.LogId = item.LogId;
            obj.LongDescription = item.LongDescription;
            obj.MarkDownTemplateID = item.MarkDownTemplateID;
            obj.PrimaryImage = item.PrimaryImage;
            obj.ProdCat1ID = item.ProdCat1ID;
            obj.ProdCat2ID = item.ProdCat2ID;
            obj.ProdCat3ID = item.ProdCat3ID;
            obj.ProdCat4ID = item.ProdCat4ID;
            obj.ProductSKU = item.ProductSKU;
            obj.ProductSourceID = item.ProductSourceID;
            obj.StyleSKU= item.StyleSKU;
            obj.RecommendedSellingPrice = item.RecommendedSellingPrice;
            obj.SeasonID = item.SeasonID;
            obj.ShortDescription = item.ShortDescription;
            obj.SizeGridID = item.SizeGridID;
            obj.SupplierID = item.SupplierID;
            obj.SupplierStyle = item.SupplierStyle;
            obj.UpdatedOn = item.UpdatedOn;
            obj.YearID = item.YearID;
            return obj;
        }
        private static StockDistributionStatu RemoveReference(this StockDistributionStatu item)
        {
            StockDistributionStatu obj = new StockDistributionStatu();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.Status = item.Status;
            return obj;
        }
        private static PurchaseOrderStatu RemoveReference(this PurchaseOrderStatu item)
        {
            PurchaseOrderStatu obj = new PurchaseOrderStatu();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.OrderStatus = item.OrderStatus;
            return obj;
        }
		private static MarkDownBranch RemoveReference(this MarkDownBranch item)
		{
			MarkDownBranch obj = new MarkDownBranch();
			obj.Id = item.Id;
			obj.BranchID = item.BranchID;
			obj.IsActive = item.IsActive;
			obj.MarkDownID = item.MarkDownID;
			return obj;
		}

		private static Buyer RemoveReference(this Buyer item)
        {
            Buyer obj = new Buyer();
            obj.BuyLimit = item.BuyLimit;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            obj.Name = item.Name;
            return obj;
        }
        private static Color RemoveReference(this Color item)
        {
            Color obj = new Color();
            obj.Code = item.Code;
            obj.ColorLong = item.ColorLong;
            obj.ColorShort = item.ColorShort;
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductCategory RemoveReference(this ProductCategory item)
        {
            ProductCategory obj = new ProductCategory();
            obj.Code = item.Code;
            obj.Id = item.Id;
            obj.Category1 = item.Category1;
            obj.Category2= item.Category2;
            obj.Category3 = item.Category3;
            obj.Category4 = item.Category4;
            obj.IsActive = item.IsActive;
            obj.LogId = item.LogId;
            return obj;
        }
        private static ProductGrp RemoveReference(this ProductGrp item)
        {
			ProductGrp obj = new ProductGrp();
            obj.GroupName = item.GroupName;
            obj.ID= item.ID;
            obj.IsActive = item.IsActive;
            return obj;
        }
        private static ProductSource RemoveReference(this ProductSource item)
        {
            ProductSource obj = new ProductSource();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.Source = item.Source;
            return obj;
        }
        public static Template RemoveReference(this Template item)
        {
            Template obj = new Template();
            obj.Id = item.Id;
            obj.CreatedOn = item.CreatedOn;
            obj.UpdatedOn = item.UpdatedOn;
            obj.Height = item.Height;
            obj.IsActive = item.IsActive;
            obj.LengthId = item.LengthId;
            obj.LengthMeasure = item.LengthMeasure;
            obj.Name = item.Name;
            obj.paramId = item.paramId;
            obj.SizeParameter = item.SizeParameter;
            obj.TemplateHtml = item.TemplateHtml;
            obj.Width = item.Width;
            return obj;
        }
        private static ProductStyle RemoveReference(this ProductStyle item)
        {
            ProductStyle obj = new ProductStyle();
            obj.Id = item.Id;
            obj.IsActive = item.IsActive;
            obj.StyleSKU = item.StyleSKU;
            return obj;

        }
    }
   
}