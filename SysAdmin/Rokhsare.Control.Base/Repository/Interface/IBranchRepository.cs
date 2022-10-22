using Rokhsare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Control.Base.Repository.Interface
{
    public interface IBranchRepository : IDisposable
    {
        IEnumerable<Branch> GetAllBranch();
        Branch GetBranchById(int studentId);
        int AddBranch(Branch branchentity);
        int UpdateBranch(Branch branchentity);
        void DeleteBranch(int branchId);
    }
}
