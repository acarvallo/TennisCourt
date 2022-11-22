using TennisCourt.Domain.Interfaces.Repositories;
using TennisCourt.Domain.Models;
using TennisCourt.Infra.Data.Context;
using TennisCourt.Infra.Data.Repositories.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TennisCourt.Infra.Data.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(TennisCourtContext context)
            : base(context)
        {
        }
        public override IQueryable<Reservation> GetAllQueryTracking => base.GetAllQueryTracking.Include(p=>p.ReservationHistory).AsQueryable();

        public async Task<IEnumerable<Reservation>> GetByDate(DateTime date)
        {
            try
            {
                return await GetAllQueryTracking.Where(p => p.ReservedDate == date).ToListAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}
