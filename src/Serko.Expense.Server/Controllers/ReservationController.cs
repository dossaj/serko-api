using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serko.Expense.Domain.Services;
using Serko.Expense.Server.Dtos;
using Serko.Expense.Server.Extensions;

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
            var result = await reservations.Get(id);
            return result?.ToDto();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SaveReservationDto saveReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await reservations.Save(saveReservation.ToModel());
            return Ok(id);
        }
    }
}
