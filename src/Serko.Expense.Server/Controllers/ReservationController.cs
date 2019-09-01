using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serko.Expense.Domain.Models;
using Serko.Expense.Domain.Services;
using Serko.Expense.Server.Dtos;

namespace Serko.Expense.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservations;

        public ReservationController(IReservationService reservations)
        {
            this.reservations = reservations;
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationDto>> Get()
        {
            return (await reservations.Get())
                .Select(x => x.ToDto());
        }

        [HttpGet("{id}")]
        public async Task<ReservationDto> Get(int id)
        {
            return (await reservations.Get(id))
                .ToDto();
        }

        [HttpPost]
        public Task Post([FromBody]SaveReservationDto saveReservation)
        {
            return reservations.Save(saveReservation.ToModel());
        }
    }

    public static class DtoExtensions
    {
        public static Reservation ToModel(this SaveReservationDto dto)
        {
            return new Reservation
            {
                Date = dto.DateTime,
                Description = dto.Description,
                Vendor = new Vendor
                {
                    Name = dto.Vendor
                },
                Expense = new Domain.Models.Expense
                {
                    CostCentre = dto.Expense.CostCentre,
                    PaymentMethod = dto.Expense.PaymentMethod,
                    Total = dto.Expense.Total,
                    Gst = Math.Round(dto.Expense.Total * 0.15m, 2)
                }
            };
        }

        public static ReservationDto ToDto(this Reservation reservation)
        {
            return new ReservationDto
            {
                Id = reservation.Id,
                Date = reservation.Date,
                Vendor = reservation.Vendor.Name,
                Description = reservation.Description,
                PaymentMethod = reservation.Expense.PaymentMethod,
                CostCentre = reservation.Expense.CostCentre,
                Total = reservation.Expense.Total,
                PreGst = reservation.Expense.PreGst,
                Gst = reservation.Expense.Gst
            };
        }
    }
}
