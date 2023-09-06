using settings;
using System.Data;
using System.Data.SqlClient;

namespace Helpers
{
    public class SqlHelper
    {
        private ILogger<SqlHelper> _logger;
        private Settings _settings;
        public SqlHelper(ILogger<SqlHelper> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public async Task<DataTable> ExecuteStoredProcedure(SqlCommand command)
        {
            DataTable dataTable = new DataTable();
            try
            {
                await using (SqlConnection sqlcon = new SqlConnection(_settings.DbConnectionString))
                {
                    command.Connection = sqlcon;
                    using(SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dataTable);
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}