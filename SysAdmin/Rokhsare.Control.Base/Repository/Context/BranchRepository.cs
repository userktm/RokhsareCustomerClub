using Rokhsare.Models;
using Rokhsare.Control.Base.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Control.Base.Repository.Context
{
    //public class BranchRepository : IBranchRepository
    //{
    //    private readonly testRokhsarehClubDBContext _context = ConfigReader.ConfigReader.GetRokhsarehClubDb;
    //    int _UserID = 0;

    //    public BranchRepository(int UserID)
    //    {
    //        _UserID = UserID;
    //    }

    //    public IEnumerable<Branch> GetAllBranch()
    //    {
    //        return _context.Branches.ToList();
    //    }

    //    public Branch GetBranchById(int branchId)
    //    {
    //        return _context.Branches.Find(branchId);
    //    }

    //    public int AddBranch(Branch branchEntity)

    //    {
    //        int result = -1;

    //        if (branchEntity != null)
    //        {
    //            _context.Branches.Add(branchEntity);
    //            _context.SaveChanges();
    //            result = branchEntity.BranchId;
    //        }
    //        return result;

    //    }

    //    public int UpdateBranch(Branch branchEntity)
    //    {
    //        int result = -1;

    //        if (branchEntity != null)
    //        {
    //            _context.Entry(branchEntity).State = EntityState.Modified;
    //            _context.SaveChanges();
    //            result = branchEntity.BranchId;
    //        }
    //        return result;
    //    }
    //    public void DeleteBranch(int branchId)
    //    {
    //        Branch branchEntity = _context.Branches.Find(branchId);
    //        _context.Branches.Remove(branchEntity);
    //        _context.SaveChanges();

    //    }

    //    private bool disposed = false;

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //        }
    //        this.disposed = true;
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);

    //        GC.SuppressFinalize(this);
    //    }
    //}
}
