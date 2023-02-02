using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Testes.PdfCSharp.ConsoleApp.Infra.Data
{
    public class SqlRepository
    {
        private readonly string _strCommand = @"SELECT cast(adoc_imagem as varbinary(max)) as Imagem FROM [DB_SE_CAV_DOCUMENTOS].[dbo].[tb_adoc_atendimento_documento] where ate_idt = 10094";
        private readonly string _connectionString;
        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<byte[]> GetImagesFromDB()
        {
            IEnumerable<byte[]> images;
            using(var connection = this.CreateConnection())
            {
                images = connection.Query<byte[]>(_strCommand);
            }

            return images.ToList();
        }

        private IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
