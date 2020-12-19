using System.Collections.Generic;
using System.Linq;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;
using DeskBooker.DataAccess.Contexts;

namespace DeskBooker.DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MySqlContext _context;

        public DepartmentRepository(MySqlContext context)
        {
            _context = context;
        }

        public IEnumerable<Departments> GetAllDepartments()
        {
        return _context.Departments.ToList();
        }
    }
}