﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
	public interface IPendingItemReceiptService
	{
		bool Create(PendingItemReceiptModel model);
	}
}
