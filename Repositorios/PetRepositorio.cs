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
                    var insertPet = @"
                                    IF NOT EXISTS (SELECT 1 FROM VacinaPet.dbo.Pet WHERE IdPet = @idPet)
                                    BEGIN
                                        INSERT INTO VacinaPet.dbo.Pet (DataNascimento, Nome, Raca, Genero)
                                        VALUES (@dataNascimento, @nome, @raca, @genero);
                                    END";

                    await _db.Connection.ExecuteAsync(insertPet,
                             new
                             {
                                 idPet = pet.IdPet,
                                 nome = pet.Nome,
                                 dataNascimento = pet.DataNascimento.ToString("dd-MM-yyyy"),
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
            _logger.LogInformation("Tempo de duracao {0:00}:{1:00}:{2:00} em Vacinas ", ts.Hours, ts.Minutes, ts.Seconds);
        }
    }
}
