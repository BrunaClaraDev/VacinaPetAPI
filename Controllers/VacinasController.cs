using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_VacinaPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinasController : ControllerBase
    {
        private readonly IVacinaRepositorio _vacinaRepositorio;
        private readonly IPetRepositorio _petRepositorio;
        private readonly IValidarVacinas _validarVacinas;

        public VacinasController(IVacinaRepositorio vacinaRepositorio, IPetRepositorio petRepositorio, IValidarVacinas validarVacinas)
        {
            _vacinaRepositorio = vacinaRepositorio;
            _petRepositorio = petRepositorio;
            _validarVacinas = validarVacinas;
        }

        [HttpPost]
        public async Task<List<Vacina>> GetVacinasAsync(Pet pet)
        {
            var vacinasTomar = _validarVacinas.PegaVacinaPet(pet);
            await Task.WhenAll(
                _petRepositorio.SalvarPetAsync(pet)
                );
            await Task.WhenAll(
               _vacinaRepositorio.SalvarVacinasAsync(vacinasTomar)
               );


            return vacinasTomar;
        }
    }
}
