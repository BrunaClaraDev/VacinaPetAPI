using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_VacinaPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinasController : ControllerBase
    {

        private readonly IValidarVacinas _validarVacinas;

        public VacinasController(IValidarVacinas validarVacinas)
        {
            _validarVacinas = validarVacinas;
        }

        [HttpPost]
        public async Task<List<Vacina>> GetVacinasAsync(Pet pet)
        {
            return  _validarVacinas.PegaVacinaPet(pet);
        }
    }
}
