using Services.Shared.Model;
using Services.Shared.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Services.Shared.DAL
{
    public class NotaFiscalFornecedorDAL
    {

        public List<RetornoProcModel> ExecutaProcInserirCTe(string cnpjEmp, string numDocumento, string serie, string cnpjEmitente, long protocolo,
                                          string chaveAcesso, decimal valorTotal, string dataDocumento, string naturazaOperacao,
                                          string razaoSocial, string arquivoXml)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT STATUS, MENSAGEM FROM JUM_INSERIRCTE('{0}', '{1}', '{2}', '{3}', {4}, '{5}', {6}, '{7}', '{8}', '{9}', '{10}') ",
                                cnpjEmp, numDocumento, serie, cnpjEmitente, protocolo,
                                chaveAcesso, valorTotal, dataDocumento, naturazaOperacao,
                                razaoSocial, arquivoXml);

            //EHelper.ExecuteCommand(sql.ToString());

            return EHelper.ExecutarSelectToDataReader<RetornoProcModel>(sql.ToString());
        }
    }
}
