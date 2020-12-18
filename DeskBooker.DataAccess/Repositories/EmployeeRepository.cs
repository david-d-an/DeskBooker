using System.Collections.Generic;
using System.Linq;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.DataAccess.Contexts;

namespace DeskBooker.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MySqlContext _context;

        public EmployeeRepository(MySqlContext context)
        {
        _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
        return _context.Departments.ToList();
        }
    }
}