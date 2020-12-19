using System.Collections.Generic;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
  public class DeptManagerModel : PageModel
  {
    private readonly IDeptManagerRepository _deptManagerRepository;

    public DeptManagerModel(IDeptManagerRepository deptManagerRepository)
    {
      _deptManagerRepository = deptManagerRepository;
    }

    public IEnumerable<DeptManager> deptManager { get; set; }

    public void OnGet()
    {
      deptManager = _deptManagerRepository.GetAllDeptManager();
    }
  }
}