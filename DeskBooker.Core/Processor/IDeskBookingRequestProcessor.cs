using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public interface IDeskBookingRequestProcessor
    {
        public DeskBookingResult BookDesk(DeskBookingRequest request);
    }
}