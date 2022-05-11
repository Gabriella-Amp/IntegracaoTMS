using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Shared.Util
{
    public abstract class EHelper
    {
        public static List<Y> ExecutarSelectToDataReader<Y>(string selectSQL)
        {
            //using (SqlConnection con = new SqlConnection(ApplicationSettings.ConnectionString))
            FbConnectionStringBuilder teste = new FbConnectionStringBuilder(ApplicationSettings.ConnectionString);

            string connectionString =
                        "User=SYSDBA;" +
                        "Password=masterkey;" +
                        "Database=/dados/RSYS2003/RSYS2003.FDB;" +
                        "DataSource=192.168.0.230;" +
                        "Port=3050;" +
                        "Dialect=3;" +
                        "Charset=NONE;" +
                        "Role=;" +
                        "Connection lifetime=15;" +
                        "Pooling=true;" +
                        "MinPoolSize=0;" +
                        "MaxPoolSize=50;" +
                        "Packet Size=8192;" +
                        "ServerType=0;";

            using (FbConnection con = new FbConnection(ApplicationSettings.ConnectionString))
            {
                con.Open();

                FbCommand cmd = new FbCommand(selectSQL, con);
                cmd.CommandTimeout = 60;
                var dataReader = cmd.ExecuteReader();

                var retorno = new List<Y>();
                var props = typeof(Y).GetProperties().ToList();

                while (dataReader.Read())
                {

                    if (!dataReader.HasRows) continue;

                    var obj = Activator.CreateInstance<Y>();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {

                        var columnName = dataReader.GetName(i);
                        var propObj = props.Find(p => p.Name.Equals(columnName, StringComparison.CurrentCultureIgnoreCase));

                        if (propObj == null) continue;

                        var valor = dataReader[columnName];

                        if (valor == DBNull.Value)
                        {
                            propObj.SetValue(obj, null, null);
                        }
                        else
                        {
                            propObj.SetValue(obj, valor, null);
                        }
                    }

                    retorno.Add(obj);
                }

                dataReader.Close();

                return retorno;
            }
        }

        public static void ExecuteCommand(string comando)
        {
            using (FbConnection con = new FbConnection(ApplicationSettings.ConnectionString))
            {
                con.Open();

                FbCommand cmd = new FbCommand(comando, con);
                cmd.CommandTimeout = 60;
                cmd.ExecuteReader();
            }
        }
        public async static void ExecuteCommandAsync(string comando)
        {
            using (SqlConnection con = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.CommandTimeout = 60;
                await cmd.ExecuteReaderAsync();
            }
        }
    }
}
