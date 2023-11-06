using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public class PetRepositorio : IPetRepositorio
    {
        private readonly DbSessao _db;
        private readonly int ErroPrimaryKey = 2601;
        private readonly int ErroForeignKey = 547;
        private readonly ILogger<VacinaRepositorio> _logger;

        public PetRepositorio()
        {
        }

        public PetRepositorio(DbSessao dbSession, ILogger<VacinaRepositorio> logger)
        {
            _db = dbSession;
            _logger = logger;
        }

        public async Task SalvarPetAsync(Pet pet)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var quantidadeDeErroFK = 0;
            var quantidadeDeErroPK = 0;

                try
                {
                    var insertPet = @"MERGE INTO VacinaPet.dbo.Pet AS Target
                                    USING (VALUES (@idPet, @dataNascimento, @nome, @raca, @genero)) AS Source (IdPet, DataNascimento, Nome, Raca, Genero)
                                    ON Target.IdPet = Source.IdPet
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            Target.DataNascimento = Source.DataNascimento, Target.Nome = Source.Nome, Target.Raca = Source.Raca, Target.Genero = Source.Genero
                                    WHEN NOT MATCHED THEN
                                        INSERT (DataNascimento, Nome, Raca, Genero)
                                        VALUES (Source.DataNascimento, Source.Nome, Source.Raca, Source.Genero);";

                    await _db.Connection.ExecuteAsync(insertPet,
                             new
                             {
                                 idPet = pet.IdPet,
                                 nome = pet.Nome,
                                 dataNascimento = pet.DataNascimento.ToString("yyyy-MM-dd"),
                                 raca = pet.Raca,
                                 genero = pet.Genero
                             });

                }
                catch (SqlException ex)
                {
                    //FK erro ou PK erro
                    if (ex.Number == ErroForeignKey)
                        quantidadeDeErroFK++;
                    if (ex.Number == ErroPrimaryKey)
                        quantidadeDeErroPK++;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro: {ex.Message}");
                    throw;
                }
            
            _logger.LogInformation($"ErrosFK:{ErroForeignKey} - ErrosPK: {ErroPrimaryKey}");
            TimeSpan ts = stopwatch.Elapsed;
            _logger.LogInformation("Tempo de duracao {0:00}:{1:00}:{2:00} em Pet ", ts.Hours, ts.Minutes, ts.Seconds);
        }
    }
}
