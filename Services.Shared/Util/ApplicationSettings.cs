using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Shared.Util
{
    public static class ApplicationSettings
    {
        public static string WebApiUrl { get; set; }
        public static string ConnectionString { get; set; }

    }
}
