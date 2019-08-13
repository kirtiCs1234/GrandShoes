using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IBranchService
    {
        List<BranchModel> GetAll();
        List<BranchModel> GetPaging(int? page, out int TotalCount);
        List<BranchModel> GetSearchData(BranchSearch branchSearch, int? page, out int TotalCount);
        BranchModel GetById(int? id);
        bool Create(BranchModel branchModel);
        bool Edit(BranchModel branchModel);
        BranchModel Delete(BranchModel branchModel);
       List<BranchModel> SearchBranch(BranchModel branchModel);
        bool CheckBranchName(BranchModel branchModel);
        bool CheckBranchCode(BranchModel branchModel);
        Dictionary<string, string> CreateList(Dictionary<int,BranchModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, BranchModel> list);
		bool CheckBranchCode(string chk);
		BranchModel GetByName(string name);

	}
}
