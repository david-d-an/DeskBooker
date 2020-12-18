using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using DeskBooker.DataAccess.Contexts;

namespace DeskBooker.DataAccess.Repositories
{
  public class DeskBookingRepository : IDeskBookingRepository
  {
    private readonly SQLiteContext _context;

    public DeskBookingRepository(SQLiteContext context)
    {
      _context = context;
    }

    public IEnumerable<DeskBooking> GetAll()
    {
      return _context.DeskBooking.OrderBy(x => x.Date).ToList();
    }

    public void Save(DeskBooking deskBooking)
    {
      _context.DeskBooking.Add(deskBooking);
      _context.SaveChanges();
    }
  }
}