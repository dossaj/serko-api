using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public Task<IEnumerable<ReservationDto>> Get()
        {
            return Task.FromResult<IEnumerable<ReservationDto>>(new[]{ new ReservationDto() });
        }

        [HttpGet("{id}")]
        public Task<ReservationDto> Get(int id)
        {
            return Task.FromResult(new ReservationDto());
        }

        [HttpPost]
        public Task Post([FromBody]SaveReservationDto saveReservation)
        {
            return Task.CompletedTask;
        }
    }
}
