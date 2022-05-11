
using Microsoft.AspNetCore.Mvc;
using Services.Shared.DAL;
using Services.Shared.Model;
using System;
using System.Collections.Generic;

namespace Services.Shared.Controllers
{
    public class NotaFiscalFornecedorController : Controller
    {
        public List<RetornoProcModel> ExecutaProcInserirCTe(string cnpjEmp, string numDocumento, string serie, string cnpjEmitente, long protocolo,
                                          string chaveAcesso, decimal valorTotal, string dataDocumento, string naturazaOperacao,
                                          string razaoSocial, string arquivoXml)
        {
            NotaFiscalFornecedorDAL notaFiscal = new NotaFiscalFornecedorDAL();
            return notaFiscal.ExecutaProcInserirCTe(cnpjEmp, numDocumento, serie, cnpjEmitente, protocolo,
                                             chaveAcesso, valorTotal, dataDocumento, naturazaOperacao,
                                             razaoSocial, arquivoXml);
        }
    }
}
