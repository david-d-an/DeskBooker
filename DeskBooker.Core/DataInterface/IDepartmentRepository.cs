using System.Collections.Generic;
using DeskBooker.Core.Models;

namespace DeskBooker.Core.DataInterface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Departments> GetAllDepartments();
    }
}