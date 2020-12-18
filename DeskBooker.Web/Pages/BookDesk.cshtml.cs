using System;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
  public class BookDeskModel : PageModel
  {
    [BindProperty]
    public DeskBookingRequest DeskBookingRequest { get; set; }
    private IDeskBookingRequestProcessor _deskBookingRequestProcessor;

    public BookDeskModel(IDeskBookingRequestProcessor deskBookingRequestProcessor)
    {
        this._deskBookingRequestProcessor = deskBookingRequestProcessor;
    }

    public IActionResult OnPost()
    {
      IActionResult actionResult = Page();

      if (this.ModelState.IsValid) {
        var result = _deskBookingRequestProcessor.BookDesk(DeskBookingRequest);
        if (result.Code == DeskBookingResultCode.NoDeskAvailable) {
          this.ModelState.AddModelError(
              "DeskBookingRequest.Date", 
              "No desk is available for the selected date");

          return actionResult;
        }

        var customObject = new{
          deskBookingId = result.DeskBookingId,
          firstName = result.FirstName,
          // lastName = result.LastName,
          // email = result.Email,
          date = result.Date
        };
        return RedirectToPage("BookDeskConfirmation", customObject);
      }

      return actionResult;
    }
  }
}