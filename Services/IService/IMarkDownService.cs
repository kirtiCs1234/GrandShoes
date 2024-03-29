﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IMarkDownService
    {
        bool Create(MarkDownAddModel model);
        List<MarkDownModel> GetAll();
        List<MarkDownModel> GetAllMarkDown();
		List<MarkDownModel> GetByProduct(string ProductSKU, string StyleSKU);
		List<MarkDownModel> GetByDate();
	}
}
