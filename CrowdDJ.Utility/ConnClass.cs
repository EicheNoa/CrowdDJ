using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CrowdDJ.BL
{
    public class ConnClass
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CrowdDJ.Properties.Settings.CrowdDJDBConnectionString"].ConnectionString;
        }        
    }
}
