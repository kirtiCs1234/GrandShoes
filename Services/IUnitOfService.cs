﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.IService;
namespace Services
{
    public interface IUnitOfService
    {
        IUserService UserService { get; }
        IColorService ColorService { get; }
        IPurchaseOrderService PurchaseOrderService { get; }
        ISupplierService SupplierService { get; }
        ISizeGridService SizeGridService { get; }
        IBranchService BranchService { get; }
        IRoleService RoleService { get; }
        IAreaService AreaService { get; }
        IBuyerService BuyerService { get; }
        IStaffService StaffService { get; }
        IDesignationService DesignationService { get; }
        IProductSourceService ProductSourceService { get; }
        IProductCategoryService ProductCategoryService { get; }
        IProductService ProductService { get; }
        IImageService ImageService { get; }
        IProductStyleService ProductStyleService { get; }
        ITemplateService TemplateService { get; }
        IProductGroupService ProductGroupService { get; }
        ISeasonService SeasonService { get; }
        IReceiptOrderService ReceiptOrderService { get; }
      //  IReceiptOrderItemService ReceiptOrderItemService { get; }
        IStockInventoryService StockInventoryService { get; }
        IPurchaseOrderItemsService PurchaseOrderItemsService { get; }
        IStockDistributionSummaryService StockDistributionSummaryService { get; }
        IStockDistributionService StockDistributionService { get; }
        IStockEnquiryService StockEnquiryService { get; }
        ICartonManagementDetailService CartonManagementDetailService { get; }
        ICartonManagementService CartonManagementService { get; }
        IStockTapeService StockTapeService { get; }
        IStockBranchInventoryService StockBranchInventoryService { get; }
        IStockAuditService StockAuditService { get; }
        ISMIBranchDefaultService SMIBranchDefaultService { get; }
        IStaffMemberService StaffMemberService { get; }
        IStaffRoleService StaffRoleService { get; }
        IStaffStatusService StaffStatusService { get; }
        ILogService LogService { get; }
        IMarkDownBranchService MarkDownBranchService { get; }
        IMarkDownService MarkDownService { get; }
        IDiscountService DiscountService { get; }
        IYearServices YearService { get; }
        IIBTCartonService IBTService { get; }
		IPendingItemReceiptService PendingItemReceiptService { get; }
        IProductCat1Service ProductCat1Service { get; }
        IProductCat2Service ProductCat2Service { get; }
        IProductCat3Service ProductCat3Service { get; }
        IProductCat4Service ProductCat4Service { get; }
        ISalesOrderItemService SalesOrderItemService { get; }
        IReport ReportService { get; }
        IStockTransferService StockTransferService { get; }
        IPageNameService PageNameService { get; }
        ILoginService LoginService { get; }
    }
}