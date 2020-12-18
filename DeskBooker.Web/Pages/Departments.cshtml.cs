using System.Collections.Generic;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
  public class DepartmentsModel : PageModel
  {
    private readonly IEmployeeRepository _employeeRepository;

    public DepartmentsModel(IEmployeeRepository employeeRepository)
    {
      _employeeRepository = employeeRepository;
    }

    public IEnumerable<Department> deapartments { get; set; }

    public void OnGet()
    {
      deapartments = _employeeRepository.GetAllDepartments();
    }
  }
}