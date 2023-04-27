using Microsoft.AspNetCore.Mvc;
using TrenRezervasyonSistemi.Models;
using TrenRezervasyonSistemi.Models.Entities;

namespace TrenRezervasyonSistemi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IBiletOperations _biletOperations;

        public ReservationController(IBiletOperations biletOperations)
        {
            _biletOperations = biletOperations;
        }

        [HttpPost]
        public IActionResult GetReservation(Girdi girdi)
        {
            SunucuCevap sunucuCevap = _biletOperations.Yerlestir(girdi);
            return Ok(sunucuCevap);
        }
    }
}