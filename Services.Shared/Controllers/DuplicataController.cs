
using Microsoft.AspNetCore.Mvc;
using Services.Shared.DAL;
using Services.Shared.Model;
using System;
using System.Collections.Generic;

namespace Services.Shared.Controllers
{
    public class DuplicataController : Controller
    {
        public List<RetornoProcModel> ExecutaProcInserirDuplicata(string cnpjEmp, string codDuplicata, string cnpjEmitente, string complemento,
                                          string dataEmissao, string dataVencimento, decimal valorFatura, string numFaturaTMS, string arquivo)
        {
            DuplicataDAL duplicata = new DuplicataDAL();
            return duplicata.ExecutaProcInserirCTe(cnpjEmp, codDuplicata, cnpjEmitente, complemento,
                                          dataEmissao, dataVencimento, valorFatura, numFaturaTMS, arquivo);
        }

    }
}
