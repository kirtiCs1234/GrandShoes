﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IStockTapeService
    {
        List<StockVarianceModel> ShowVariance(int? BranchId);
        bool Create(StockTapeModel stock);
        bool ExcelUpload(StockTapeModel stockTape);
        List<StockTapeModel> GetByBranchId(int? BranchId);
		bool DeleteRecord(List<StockTapeModel> stockTapeList);

	}
}
