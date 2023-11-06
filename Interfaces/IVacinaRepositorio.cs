﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_VacinaPet
{
    public interface IVacinaRepositorio
    {
        public Task SalvarVacinasAsync(List<Vacina> vacinas);
    }
}