using System;
using Serko.Expense.Domain.Models;
using Serko.Expense.Server.Dtos;

namespace Serko.Expense.Server.Extensions
{
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
                CostCentre = reservation.Expense.CostCentre ?? "UNKNOWN",
                Total = reservation.Expense.Total,
                PreGst = reservation.Expense.PreGst,
                Gst = reservation.Expense.Gst
            };
        }
    }
}