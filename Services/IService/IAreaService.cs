﻿using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IAreaService
    {
        List<AreaModel> GetAll();
        List<AreaModel> GetPaging(int? page,out int TotalCount);
        List<AreaModel> GetSearchData(AreaSearch areaSearch, int? page, out int TotalCount);
        AreaModel GetById(int? id);
        bool Create(AreaModel areaModel);
        AreaModel Edit(AreaModel areaModel);
        AreaModel Delete(AreaModel areaModel);
    }
}