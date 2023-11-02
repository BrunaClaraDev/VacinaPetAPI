using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_VacinaPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinasController : ControllerBase
    {
        [HttpPost]
        public List<VacinaDto> GetVacinas(PetDto pet)
        {
            var classe = new ValidarVacinas();
            return classe.PegaVacinaPet(pet);
        }
    }
}
