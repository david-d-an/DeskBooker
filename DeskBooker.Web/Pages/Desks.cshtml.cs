using System.Collections.Generic;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DeskBooker.Web.Pages
{
  public class DesksModel : PageModel
  {
    private readonly IDeskRepository _deskRepository;
    private readonly ILogger<DesksModel> _logger;

    public DesksModel(
        IDeskRepository deskRepository,
        ILogger<DesksModel> logger)
    {
      _deskRepository = deskRepository;
      this._logger = logger;
    }

    public IEnumerable<Desk> Desks { get; set; }

    public void OnGet()
    {
      this._logger.LogWarning("DeskModel OnGet() invoked.");
      Desks = _deskRepository.GetAll();
    }
  }
}