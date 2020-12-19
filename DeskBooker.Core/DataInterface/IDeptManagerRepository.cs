using System.Collections.Generic;
using DeskBooker.Core.Models;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeptManagerRepository
    {
        IEnumerable<DeptManager> GetAllDeptManager();
    }
}