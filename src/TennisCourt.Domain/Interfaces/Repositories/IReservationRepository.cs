using TennisCourt.Domain.Models;
using System.Linq.Expressions;


namespace TennisCourt.Domain.Interfaces.Repositories
{
    public interface IReservationRepository :IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetByDate(DateTime date);    
    }
}
