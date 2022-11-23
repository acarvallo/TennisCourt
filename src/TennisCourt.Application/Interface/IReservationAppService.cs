using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.GetReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.DTO.RescheduleReservation;
using TennisCourt.Domain.Interfaces.Repositories;
using TennisCourt.Domain.Models;

namespace TennisCourt.Application.Interface
{
    public interface IReservationAppService
    {
        Task<RootOutput<ProcessReservationOutput>> ProcessReservation(ProcessReservationInput input);
        Task<RootOutput<CancelReservationOutput>> CancelReservation(CancelReservationInput input);
        Task<RootOutput<RescheduleReservationOutput>> RescheduleReservation(RescheduleReservationInput input);
        Task<RootOutput<GetReservationOuput>> GetReservation(Guid id);
    }
}
