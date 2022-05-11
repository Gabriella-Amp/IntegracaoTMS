using Services.Shared.Model;
using Services.Shared.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Services.Shared.DAL
{
    public class DuplicataDAL
    {

        public List<RetornoProcModel> ExecutaProcInserirCTe(string cnpjEmp, string codDuplicata, string cnpjEmitente, string complemento,
                                          string dataEmissao, string dataVencimento, decimal valorFatura, string numFaturaTMS, string arquivo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT STATUS, MENSAGEM FROM JUM_INSERIRDUPLICATA('{0}', '{1}', '{2}', '{3}', {4}, '{5}', {6}, '{7}', '{8}') ",
                                cnpjEmp, codDuplicata, cnpjEmitente, complemento,
                                dataEmissao, dataVencimento, valorFatura, numFaturaTMS, arquivo);

            //EHelper.ExecuteCommand(sql.ToString());

            return EHelper.ExecutarSelectToDataReader<RetornoProcModel>(sql.ToString());
        }
    }
}
