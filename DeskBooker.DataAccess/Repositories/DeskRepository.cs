using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeskBooker.DataAccess.Repositories
{
  public class DeskRepository : IDeskRepository
  {
    private readonly SQLiteContext _context;

    public DeskRepository(SQLiteContext context)
    {
      _context = context;
    }

    public IEnumerable<Desk> GetAll()
    {
      return _context.Desk.ToList();
    }

    public IEnumerable<Desk> GetAvailableDesks(DateTime date)
    {
      var bookedDeskIds = _context.DeskBooking.
        Where(x => x.Date == date)
        .Select(b => b.DeskId)
        .ToList();

      return _context.Desk
        .Where(x => !bookedDeskIds.Contains(x.Id))
        .ToList();
    }
  }
}