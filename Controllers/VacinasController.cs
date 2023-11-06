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
        private readonly IVacinaRepositorio _vacinaRepositorio;
        private readonly IPetRepositorio _petRepositorio;
        private readonly IValidarVacinas _validarVacinas;
        private readonly ILogger<VacinasController> _logger;

        public VacinasController(IVacinaRepositorio vacinaRepositorio, IPetRepositorio petRepositorio, IValidarVacinas validarVacinas, ILogger<VacinasController> logger)
        {
            _vacinaRepositorio = vacinaRepositorio;
            _petRepositorio = petRepositorio;
            _validarVacinas = validarVacinas;
            _logger = logger;
        }

        [HttpPost]
        public async Task<List<Vacina>> GetVacinasAsync(Pet pet)
        {
            try
            {
                List<Vacina> vacinasTomar = _validarVacinas.PegaVacinaPet(pet);
                await Task.WhenAny(
                    _petRepositorio.SalvarPetAsync(pet)
                    );
                await Task.WhenAny(
                   _vacinaRepositorio.SalvarVacinasAsync(vacinasTomar)
                   );

                return vacinasTomar;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }

        }
    }
}
