using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public class VacinaRepositorio : IVacinaRepositorio
    {
        private readonly DbSessao _db;
        private readonly int ErroPrimaryKey = 2601;
        private readonly int ErroForeignKey = 547;
        private readonly ILogger<VacinaRepositorio> _logger;

        public VacinaRepositorio()
        {
        }

        public VacinaRepositorio(DbSessao dbSession, ILogger<VacinaRepositorio> logger)
        {
            _db = dbSession;
            _logger = logger;
        }

        public async Task SalvarVacinasAsync(List<Vacina> vacinas)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var quantidadeDeErroFK = 0;
            var quantidadeDeErroPK = 0;

            foreach (Vacina vacina in vacinas)
            {

                try
                {
                    var insertVacinas = @"BEGIN
                                            IF NOT EXISTS (SELECT V.NomeVacina FROM VacinaPet.dbo.Vacina V WHERE V.NomeVacina = @nomeVacina AND V.DataVacina = @dataVacina AND V.IdPet = @idPet)
                                            BEGIN
                                        INSERT INTO VacinaPet.dbo.Vacina (IdPet, NomeVacina, DataVacina, SobreVacina)
                                        VALUES (@idPet, @nomeVacina, @dataVacina, @sobreVacina);
                                        END
                                        ELSE
                                        BEGIN
                                        UPDATE VacinaPet.dbo.Vacina
                                        SET NomeVacina = @nomeVacina, DataVacina = @dataVacina, SobreVacina = @sobreVacina
                                        WHERE IdPet = @idPet;
                                            END
                                        END";

                    await _db.Connection.ExecuteAsync(insertVacinas,
                             new
                             {
                                 idPet = vacina.IdPet,
                                 nomeVacina = vacina.NomeVacina,
                                 dataVacina = vacina.DataVacina.ToString("dd-MM-yyyy"),
                                 sobreVacina = vacina.SobreVacina
                             });

                }
                catch (SqlException ex)
                {
                    //FK erro ou PK erro
                    if (ex.Number == ErroForeignKey)
                        quantidadeDeErroFK++;
                    if (ex.Number == ErroPrimaryKey)
                        quantidadeDeErroPK++;

                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro: {ex.Message}");
                    throw;
                }
            }
            _logger.LogInformation($"ErrosFK:{ErroForeignKey} - ErrosPK: {ErroPrimaryKey}");
            TimeSpan ts = stopwatch.Elapsed;
            _logger.LogInformation("Tempo de duracao {0:00}:{1:00}:{2:00} em Vacinas ", ts.Hours, ts.Minutes, ts.Seconds);
        }
    }
}
