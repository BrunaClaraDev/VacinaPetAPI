using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
                    var insertVacinas = @"MERGE INTO VacinaPet.dbo.Vacina AS Target
                                        USING (VALUES (@idPet, @nomeVacina, @dataVacina, @sobreVacina)) AS Source (IdPet, NomeVacina, DataVacina, SobreVacina)
                                        ON Target.IdPet = Source.IdPet AND Target.NomeVacina = Source.NomeVacina AND Target.DataVacina = Source.DataVacina
                                        WHEN MATCHED THEN
                                            UPDATE SET Target.SobreVacina = Source.SobreVacina
                                        WHEN NOT MATCHED THEN
                                            INSERT (IdPet, NomeVacina, DataVacina, SobreVacina)
                                            VALUES (Source.IdPet, Source.NomeVacina, Source.DataVacina, Source.SobreVacina);";


                    await _db.Connection.ExecuteAsync(insertVacinas,
                             new
                             {
                                 idPet = vacina.IdPet,
                                 nomeVacina = vacina.NomeVacina,
                                 dataVacina = vacina.DataVacina.ToString("yyyy-MM-dd"),
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
