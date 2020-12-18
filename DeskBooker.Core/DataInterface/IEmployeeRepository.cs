using System.Collections.Generic;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.DataInterface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Department> GetAllDepartments();
    }
}