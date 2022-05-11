using Services.Shared.Model;
using Services.Shared.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Services.Shared.DAL
{
    public class NotaFiscalDAL
    {
        public List<NotaFiscalModel> ObterNotaFiscal(decimal empresa, decimal tipoControle, decimal codControle)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT NF.Empresa, NF.TipoControle, NF.CodControle, NF.NumDocumento, NF.CodAnexo
                                FROM NotasFiscais NF 
                                WHERE NF.Empresa = {0}
                                  AND NF.TipoControle = {1}
                                  AND NF.CodControle = {2}",
                                empresa, tipoControle, codControle);

            return EHelper.ExecutarSelectToDataReader<NotaFiscalModel>(sql.ToString());
        }

        public List<NotaFiscalModel> ObterNotaFiscalEnvio(decimal empresa, decimal tipoControle, decimal codControle)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT NF.Empresa, NF.TipoControle, NF.CodControle, NF.NumDocumento, NF.CodAnexo, 
                                       NF.ChaveAcessoNFe, NFE.ArquivoXMLNFE
                                FROM NotasFiscais NF (NOLOCK)
                                LEFT JOIN NotaFiscalEletronica NFE (NOLOCK) on NFE.Empresa = NF.Empresa
                                                                  and NFE.TipoControle = NF.TipoControle
                                                                  and NFE.CodControle = NF.CodControle
                                WHERE NF.Empresa = {0}
                                  AND NF.TipoControle = {1}
                                  AND NF.CodControle = {2}",
                                empresa, tipoControle, codControle);

            return EHelper.ExecutarSelectToDataReader<NotaFiscalModel>(sql.ToString());
        }

        public List<NotaFiscalModel> ObterNotaFiscalEnvio()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT FIRST 10 NF.Empresa, NF.TipoControle, NF.CodControle, NF.DataMovimento,
                                       NF.NumDocumento, NF.CodigoCliente, 
                                       NF.ChaveAcesso, NF.ArquivoXML,
                                       NF.CodTransportadora, NF.CnpjTransportadora
                                FROM VIEWCARREGANOTASFISCAISTMS NF");

            return EHelper.ExecutarSelectToDataReader<NotaFiscalModel>(sql.ToString());
        }

        public List<NotaFiscalModel> ObterPorNumDocumento(decimal numDocumento, string serie, string numCNPJ)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT NF.Empresa, NF.TipoControle, NF.CodControle, NF.NumDocumento, NF.CodAnexo
                                FROM NotasFiscais NF (NOLOCK)
                                Left JOIN Entidades E (NOLOCK) on E.Codigo = NF.CodEntidade
                                WHERE NF.NumDocumento = {0}
                                  --AND NF.Serie = {1}
                                  AND E.NumCNPJ = {2}",
                                numDocumento, serie, numCNPJ);

            return EHelper.ExecutarSelectToDataReader<NotaFiscalModel>(sql.ToString());
        }

        public List<NotaFiscalModel> ObterPorChaveAcesso(string chaveAcesso)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT NF.Empresa, NF.TipoControle, NF.CodControle, NF.NumDocumento, NF.CodAnexo
                                FROM NotasFiscais NF (NOLOCK)
                                Left JOIN Net_GoopNotaFiscalGNRE G on G.Empresa = NF.Empresa
                                                                  and G.TipoControle = NF.TipoControle
                                                                  and G.CodControle = NF.CodControle
                                WHERE NF.ChaveAcessoNFe = '{0}'
                                ORDER BY NF.TipoControle DESC
                                  --AND G.Status = 1 ",
                                chaveAcesso);

            return EHelper.ExecutarSelectToDataReader<NotaFiscalModel>(sql.ToString());
        }

        public void InsereNotaFiscalTMS(NotaFiscalModel notaFiscal)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" 
                                INSERT INTO JUMORI_NOTAFISCALTMS (Empresa, TipoControle, CodControle, DataInclusao)
                                VALUES( {0}, {1}, {2}, CURRENT_TIMESTAMP)",
                                notaFiscal.Empresa,
                                notaFiscal.TipoControle,
                                notaFiscal.CodControle);

            EHelper.ExecuteCommand(sql.ToString());
        }
    }
}
