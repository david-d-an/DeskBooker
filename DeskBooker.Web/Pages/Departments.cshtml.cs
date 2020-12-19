using System.Collections.Generic;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
  public class DepartmentsModel : PageModel
  {
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentsModel(IDepartmentRepository departmentRepository)
    {
      _departmentRepository = departmentRepository;
    }

    public IEnumerable<Departments> deapartments { get; set; }

    public void OnGet()
    {
      deapartments = _departmentRepository.GetAllDepartments();
    }
  }
}