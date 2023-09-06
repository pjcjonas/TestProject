using api_gaming_global.Models.Request;
using Helpers;
using settings;
using System.Data;
using System.Data.SqlClient;

namespace api_gaming_global.Models.Crud
{
    public class ProductCrud
    {
        SqlHelper _sqlHelper;
        Settings _settings;
        public ProductCrud(SqlHelper sqlHelper, Settings settings)
        {
            _sqlHelper = sqlHelper;
            _settings = settings;
        }

        public async Task<DataTable> GetCategories()
        {
            SqlCommand command = new SqlCommand("GetCategories");
            command.CommandType = CommandType.StoredProcedure;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> GetProducts()
        {
            SqlCommand command = new SqlCommand("GetProducts");
            command.CommandType = CommandType.StoredProcedure;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> AddCartItem(int productID, int userID)
        {
            SqlCommand command = new SqlCommand("AddToCart");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
            command.Parameters["@UserID"].Value = userID;

            command.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
            command.Parameters["@ProductID"].Value = productID;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> GetCart(UserRegistration user)
        {
            SqlCommand command = new SqlCommand("GetCart");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
            command.Parameters["@UserID"].Value = user.UserID;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }

        public async Task<DataTable> RemoveCartItem(Cart cart, UserRegistration user)
        {
            SqlCommand command = new SqlCommand("DeleteCartItem");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
            command.Parameters["@UserID"].Value = user.UserID;

            command.Parameters.Add(new SqlParameter("@CartID", SqlDbType.Int));
            command.Parameters["@CartID"].Value = cart.CartID;

            return await _sqlHelper.ExecuteStoredProcedure(command);
        }
    }
}
