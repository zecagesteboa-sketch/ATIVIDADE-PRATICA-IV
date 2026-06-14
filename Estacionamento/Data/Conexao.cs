using System;
using System.Data.SqlClient;

namespace Estacionamento_.Data
{
    public class Conexao
    {
        private readonly string stringConexao = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=EstacionamentoDB;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(stringConexao);
        }
    }
}