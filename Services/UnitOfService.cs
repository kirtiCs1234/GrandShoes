﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.IService;
using Services.Service;

namespace Services
{
    public class UnitOfService : IUnitOfService
    {
        public IUserService UserService => new UserService();
        
        public IPurchaseOrderService PurchaseOrderService => new PurchaseOrderService();
        public IPurchaseOrderItemsService PurchaseOrderItemsService => new PurchaseOrderItemsService();
        public ISupplierService SupplierService => new SupplierService();
        public ISizeGridService SizeGridService => new SizeGridService();
        public IBranchService BranchService => new BranchService();
        public IRoleService RoleService => new RoleService();
        public IAreaService AreaService => new AreaService();
        public IBuyerService BuyerService => new BuyerService();
        public IStaffService StaffService => new StaffService();
        public IDesignationService DesignationService => new DesignationService();
        public IProductSourceService ProductSourceService => new ProductSourceService();
        public IProductCategoryService ProductCategoryService => new ProductCategoryService();
        public IProductService ProductService => new ProductService();
        public IImageService ImageService => new ImageService();
        public IProductStyleService ProductStyleService => new ProductStyleService();
        public IColorService ColorService => new ColorService();
        public ITemplateService TemplateService => new TemplateService();
        public IProductGroupService ProductGroupService => new ProductGroupService();
        public ISeasonService SeasonService => new SeasonService();
        public IReceiptOrderService ReceiptOrderService => new ReceiptOrderService();
     // public IReceiptOrderItemService ReceiptOrderItemService => new ReceiptOrderItemService();
        public IStockInventoryService StockInventoryService => new StockInventoryService();
        public IStockDistributionSummaryService StockDistributionSummaryService => new StockDistributionSummaryService();
        public IStockDistributionService StockDistributionService => new StockDistributionService();
        public IStockEnquiryService StockEnquiryService => new StockEnquiryService();
        public ICartonManagementDetailService CartonManagementDetailService => new CartonManagementDetailService();
        public ICartonManagementService CartonManagementService => new CartonManagementService();
        public IStockTapeService StockTapeService => new StockTapeService();
        public IStockBranchInventoryService StockBranchInventoryService => new StockBranchInventoryService();
        public IStockAuditService StockAuditService => new StockAuditService();
        public IStaffMemberService StaffMemberService => new StaffMemberService();
        public IStaffRoleService StaffRoleService => new StaffRoleService();
        public IStaffStatusService StaffStatusService => new StaffStatusService();
        public ILogService LogService => new LogService();
        public ISMIBranchDefaultService SMIBranchDefaultService => new SMIBranchDefaultService();
        public IMarkDownBranchService MarkDownBranchService => new MarkDownBranchService();
        public IMarkDownService MarkDownService => new MarkDownService();
        public IDiscountService DiscountService => new DiscountService();
        public IYearServices YearService => new YearServices();
        public IIBTCartonService IBTService => new IBTCartonService();
		public IPendingItemReceiptService PendingItemReceiptService => new PendingItemReceiptService();
        public IProductCat1Service ProductCat1Service => new ProductCat1Service();
        public IProductCat2Service ProductCat2Service => new ProductCat2Service();
        public IProductCat3Service ProductCat3Service => new ProductCat3Service();
        public IProductCat4Service ProductCat4Service => new ProductCat4Service();
        public ISalesOrderItemService SalesOrderItemService => new SalesOrderItemService();
        public IReport ReportService => new Report();
        public IStockTransferService StockTransferService => new StockTransferService();
        public IPageNameService PageNameService => new PageNameService();
        public ILoginService LoginService => new LoginService();
    }
}