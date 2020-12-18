using System;
using System.Collections.Generic;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace DeskBooker.Web.Pages
{
  public class BookDeskModelTests
  {
    private Mock<IDeskBookingRequestProcessor> _processorMock;
    private BookDeskModel _bookDeskModel;
    private DeskBookingResult _deskBookingResult;

    public BookDeskModelTests()
    {
      _processorMock = new Mock<IDeskBookingRequestProcessor>();
      _bookDeskModel = new BookDeskModel(_processorMock.Object) {
        DeskBookingRequest = new DeskBookingRequest{
          FirstName = "John",
          LastName= "Smith",
          Email = "jsmith@contoso.com",
          Date = new System.DateTime(2021, 12, 31)
        }
      };

      _deskBookingResult = new DeskBookingResult {
        // Code = DeskBookingResultCode.Success,
        DeskBookingId = 1,
        FirstName = "John",
        LastName= "Smith",
        Email = "jsmith@contoso.com",
        Date = new System.DateTime(2021, 12, 31)
      };

      _processorMock
          .Setup(x => x.BookDesk(_bookDeskModel.DeskBookingRequest))
          .Returns(_deskBookingResult);
    }


    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    public void ShouldCallBookDeskMethodOfProcessorIfModelIsValid(
      int expectedBookDeskCalls, 
      bool isModelValid) {
    
      // Arrange
      // var processorMock = new Mock<IDeskBookingRequestProcessor>();
    
      // var bookDeskModel = new BookDeskModel(processorMock.Object)
      // {
      //   DeskBookingRequest = new DeskBookingRequest{
      //     FirstName = "John",
      //     LastName= "Smith",
      //     Email = "jsmith@contoso.com",
      //     Date = new System.DateTime(2021, 12, 31)
      //   }
      // };
      _deskBookingResult.Code = DeskBookingResultCode.Success;

      // Act
      if (!isModelValid) {
        _bookDeskModel.ModelState.AddModelError("Error", "Invalid Model");
      }

      // processorMock
      //     .Setup(x => x.BookDesk(bookDeskModel.DeskBookingRequest))
      //     .Returns(new DeskBookingResult{
      //         Code = DeskBookingResultCode.Success,
      //         DeskBookingId = 1
      //     });

      _bookDeskModel.OnPost();

      // Assert
      _processorMock.Verify(
        p => p.BookDesk(_bookDeskModel.DeskBookingRequest), 
        Times.Exactly(expectedBookDeskCalls));
    }

    [Fact]
    public void ShouldAddModelErrorIfNoDeskIsAvailable() {
      // Arrange
      // var processorMock = new Mock<IDeskBookingRequestProcessor>();
    
      // var bookDeskModel = new BookDeskModel(processorMock.Object)
      // {
      //   DeskBookingRequest = new DeskBookingRequest{
      //     FirstName = "John",
      //     LastName= "Smith",
      //     Email = "jsmith@contoso.com",
      //     Date = new System.DateTime(2021, 12, 31)
      //   }
      // };

      // processorMock
      //     .Setup(x => x.BookDesk(bookDeskModel.DeskBookingRequest))
      //     .Returns(new DeskBookingResult{
      //         Code = DeskBookingResultCode.NoDeskAvailable,
      //         DeskBookingId = null
      //     });
      _deskBookingResult.Code = DeskBookingResultCode.NoDeskAvailable;

      // Act
      _bookDeskModel.OnPost();
      
      // Assert
      var modelStateEntry =
        Assert.Contains("DeskBookingRequest.Date", _bookDeskModel.ModelState);
      var modelError = Assert.Single(modelStateEntry.Errors);

      Assert.Equal("No desk is available for the selected date", modelError.ErrorMessage);
    }

    [Fact]
    public void ShouldNotAddModelErrorIfDeskIsAvailable() {
      // Arrange
      // var processorMock = new Mock<IDeskBookingRequestProcessor>();
    
      // var bookDeskModel = new BookDeskModel(processorMock.Object)
      // {
      //   DeskBookingRequest = new DeskBookingRequest{
      //     FirstName = "John",
      //     LastName= "Smith",
      //     Email = "jsmith@contoso.com",
      //     Date = new System.DateTime(2021, 12, 31)
      //   }
      // };

      // processorMock
      //     .Setup(x => x.BookDesk(bookDeskModel.DeskBookingRequest))
      //     .Returns(new DeskBookingResult{
      //         Code = DeskBookingResultCode.Success,
      //         DeskBookingId = 1
      //     });
      _deskBookingResult.Code = DeskBookingResultCode.Success;

      // Act
      _bookDeskModel.OnPost();
      
      // Assert
      Assert.DoesNotContain("DeskBookingRequest.Date", _bookDeskModel.ModelState);
    }

    [Theory]
    [InlineData(typeof(PageResult), false, null)]
    [InlineData(typeof(PageResult), true, DeskBookingResultCode.NoDeskAvailable)]
    [InlineData(typeof(RedirectToPageResult), true, DeskBookingResultCode.Success)]
    public void ShouldReturnExpectedActionResult(
      Type expectedActionResultType,
      bool isModelValid,
      DeskBookingResultCode? deskBookingResultCode
    ) {
      // Arrange
      if (!isModelValid) {
        _bookDeskModel.ModelState.AddModelError("Error", "Invalid Model");
      }

      if (deskBookingResultCode.HasValue) {
        _deskBookingResult.Code = deskBookingResultCode.Value;
      }

      // Act
      IActionResult actionResult = _bookDeskModel.OnPost();


      // Assert
      //Assert.Equal(actionResult.GetType(), expectedActionResultType);
      Assert.IsType(expectedActionResultType, actionResult);
    }

    [Fact]
    public void ShouldRedirectIfSuccessful() {
      // Arrange
      _deskBookingResult.Code = DeskBookingResultCode.Success;

      // Act
      // var actionResult = _bookDeskModel.OnPost() as RedirectToRouteResult;
      IActionResult actionResult = _bookDeskModel.OnPost();
      
      // Assert
      var redirectResult = Assert.IsType<RedirectToPageResult>(actionResult);
      Assert.Equal(redirectResult.PageName, "BookDeskConfirmation");

      IDictionary<string, object> routeValues = redirectResult.RouteValues;
      Assert.Equal(redirectResult.RouteValues.Count, 3);

      var deskBookingId = Assert.Contains("deskBookingId", routeValues);
      Assert.Equal(deskBookingId, _deskBookingResult.DeskBookingId);
      var firstName = Assert.Contains("firstName", routeValues);
      Assert.Equal(firstName, _deskBookingResult.FirstName);
      var date = Assert.Contains("date", routeValues);
      Assert.Equal(date, _deskBookingResult.Date);
    }
  }
}

