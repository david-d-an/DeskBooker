

// using System;
// using DeskBooker.Core.Domain;
// using Xunit;

// namespace DeskBooker.Core.Processor {    
//   public class DeskBookingRequestProcessorTests_Practice {


//     [Fact]
//     public void ShouldReturnDeskBookingResultWithRequestValues()
//     {
//       // Arrange
//       var request = new DeskBookingRequest {
//         FirstName = "John",
//         LastName = "Smith",
//         Email = "jsmith@ex.com",
//         Date = new DateTime()
//       };
//       var deskBookingRequestProcessor = new DeskBookingRequestProcessor(
//         null, null
//       );

//       // Act
//       var bookResult = deskBookingRequestProcessor.BookDesk(request);

//       // Assert
//     }
//   }
// }