
using Microsoft.AspNetCore.Mvc;
using Services.Shared.DAL;
using Services.Shared.Model;
using System;
using System.Collections.Generic;

namespace Services.Shared.Controllers
{
    public class NotaFiscalController : Controller
    {
        public List<NotaFiscalModel> ObterNotaFiscal(decimal empresa, decimal tipoControle, decimal codControle)
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            return notaFiscal.ObterNotaFiscal(empresa, tipoControle, codControle);
        }

        public List<NotaFiscalModel> ObterNotaFiscalEnvio(decimal empresa, decimal tipoControle, decimal codControle)
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            return notaFiscal.ObterNotaFiscalEnvio(empresa, tipoControle, codControle);
        }

        public List<NotaFiscalModel> ObterNotaFiscalEnvio()
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            return notaFiscal.ObterNotaFiscalEnvio();
        }

        public List<NotaFiscalModel> ObterPorNumDocumento(decimal numDocumento, string serie, string numCNPJ)
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            return notaFiscal.ObterPorNumDocumento(numDocumento, serie, numCNPJ);
        }

        public List<NotaFiscalModel> ObterPorChaveAcesso(string chaveAcesso)
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            return notaFiscal.ObterPorChaveAcesso(chaveAcesso);
        }

        public void InsereNotaFiscalTMS(NotaFiscalModel notafiscal)
        {
            NotaFiscalDAL notaFiscal = new NotaFiscalDAL();
            notaFiscal.InsereNotaFiscalTMS(notafiscal);            
        }

    }
}
