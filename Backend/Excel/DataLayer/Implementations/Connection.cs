using DataLayer.Interfaces;
using System.Data.SqlClient;


namespace DataLayer.Implementations
{
    public class Connection:IConnection
    {
        public Connection() { }

        public SqlConnection GetConnection()
        {

            return new SqlConnection(@"Data Source=KANINI-LTP-625\SQLEXPRESS;Integrated Security=true;Initial Catalog=Excel");
        }
    }
}
