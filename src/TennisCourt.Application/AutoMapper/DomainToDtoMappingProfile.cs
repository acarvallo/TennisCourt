using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Application.DTO;
using TennisCourt.Application.DTO.CancelReservation;
using TennisCourt.Application.DTO.ProcessReservation;
using TennisCourt.Application.DTO.RescheduleReservation;
using TennisCourt.Domain.Models;
using TennisCourt.Domain.Services;

namespace TennisCourt.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {  
            CreateMap<Reservation, ProcessReservationOutput>()
                 .ForMember(p => p.ReservationId, src => src.MapFrom(p => p.Id))
                 .ForMember(p=>p.ReservedDate,src=>src.MapFrom(p=>p.ReservedDate))
                 .ForMember(p => p.Amount, src => src.MapFrom(p => p.Amount.Value))
                 .ForMember(p => p.ReservationStatus, src => src.MapFrom(p => p.ReservationStatus.ToString()));

            CreateMap<Reservation, RescheduleReservationOutput>()
                 .ForMember(p => p.ReservationId, src => src.MapFrom(p => p.Id))
                 .ForMember(p => p.ReservedDate, src => src.MapFrom(p => p.ReservedDate))
                 .ForMember(p => p.Amount, src => src.MapFrom(p => p.Amount.Value))
                 .ForMember(p => p.ReservationStatus, src => src.MapFrom(p => p.ReservationStatus.ToString()));

            CreateMap<Reservation, CancelReservationOutput>()
                 .ForMember(p => p.ReservationId, src => src.MapFrom(p => p.Id))
                 .ForMember(p => p.ReservedDate, src => src.MapFrom(p => p.ReservedDate))
                 .ForMember(p => p.RefundAmount, src => src.MapFrom(p => p.RefundAmount.Value))
                 .ForMember(p => p.ReservationStatus, src => src.MapFrom(p => p.ReservationStatus.ToString()));


        }
        
    }
}
