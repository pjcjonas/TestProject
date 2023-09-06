using api_gaming_global.Models.Request;
using Helpers;
using settings;
using System.Data;
using System.Data.SqlClient;

namespace api_gaming_global.Models.Crud
{
    public  class UserCrud
    {

        SqlHelper _sqlHelper;
        Settings _settings;
        public UserCrud(SqlHelper sqlHelper, Settings settings)
        {
            _sqlHelper = sqlHelper;
            _settings = settings;
        }

        
        public async Task<DataTable> AddNewUser(UserRegistration registration)
        {
            SqlCommand command = new SqlCommand("AddUser");
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.Add(new SqlParameter("@ProviderID", SqlDbType.NVarChar));
            command.Parameters["@ProviderID"].Value = registration.ProviderID;

            command.Parameters.Add(new SqlParameter("@ProviderName", SqlDbType.NVarChar));
            command.Parameters["@ProviderName"].Value = registration.ProviderName;

            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
            command.Parameters["@Email"].Value = registration.Email;

            command.Parameters.Add(new SqlParameter("@DisplayName", SqlDbType.NVarChar));
            command.Parameters["@DisplayName"].Value = registration.DisplayName;

            command.Parameters.Add(new SqlParameter("@ProfilePictureURL", SqlDbType.NVarChar));
            command.Parameters["@ProfilePictureURL"].Value = registration.ProfilePictureURL;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> UpdateUser(UserRegistration registration)
        {
            SqlCommand command = new SqlCommand("UpdateUser");
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.Add(new SqlParameter("@ProviderID", SqlDbType.NVarChar));
            command.Parameters["@ProviderID"].Value = registration.ProviderID;

            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
            command.Parameters["@Email"].Value = registration.Email;

            command.Parameters.Add(new SqlParameter("@DisplayName", SqlDbType.NVarChar));
            command.Parameters["@DisplayName"].Value = registration.DisplayName;

            command.Parameters.Add(new SqlParameter("@ProfilePictureURL", SqlDbType.NVarChar));
            command.Parameters["@ProfilePictureURL"].Value = registration.ProfilePictureURL;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> GetUser(String providerID)
        {
            SqlCommand command = new SqlCommand("GetUser");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ProviderID", SqlDbType.VarChar));
            command.Parameters["@ProviderID"].Value = providerID;
            return await _sqlHelper.ExecuteStoredProcedure(command);
        }
    }
}
