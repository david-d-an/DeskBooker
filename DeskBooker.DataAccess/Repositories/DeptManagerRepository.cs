using System.Collections.Generic;
using System.Linq;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;
using DeskBooker.DataAccess.Contexts;

namespace DeskBooker.DataAccess.Repositories
{
    public class DeptManagerRepository: IDeptManagerRepository
    {
        private readonly MySqlContext _context;

        public DeptManagerRepository(MySqlContext context)
        {
            _context = context;
        }

        public IEnumerable<DeptManager> GetAllDeptManager()
        {
            return _context.DeptManager.ToList();
        }
    }
}