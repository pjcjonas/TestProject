using api_gaming_global.Helpers;
using api_gaming_global.Models.Crud;
using api_gaming_global.Models.Request;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace api_gaming_global.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly SqlHelper _sqlHelper;
        private readonly UserCrud _userCrud;
        private readonly JwtHelper _jwtHelper;
        private readonly ProductCrud _productCrud;
        public ProductsController(SqlHelper sqlHelper, UserCrud userCrud, JwtHelper jwtHelper, ProductCrud productCrud)
        {
            _productCrud = productCrud;
            _userCrud = userCrud;
            _jwtHelper = jwtHelper;
            _sqlHelper = sqlHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() {
            DataTable products = await _productCrud.GetProducts();
            List<Product> productsList = new List<Product>();

            foreach (DataRow row in products.Rows) {
                productsList.Add(new Product
                {
                    CategoryID = Convert.ToInt32(row["CategoryID"]),
                    Description = Convert.ToString(row["Description"]),
                    ImageURL = Convert.ToString(row["ImageURL"]),
                    Price = Convert.ToUInt32(row["Price"]),
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    ProductName = Convert.ToString(row["ProductName"])
                });
            }

            return Ok(productsList);
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCart cartItem) {
            string authHeader = Request.Headers["Authorization"].FirstOrDefault();
            SessionValid session = _jwtHelper.verifyTokenSession(authHeader);

            if (session != null && session.valid == true) {
                if (cartItem?.ProductID != null)
                {
                    var userInfo = _jwtHelper.GetTokenInfo(session.token);
                    SessionUserID user = JsonConvert.DeserializeObject<SessionUserID>(userInfo);
                    DataTable item = await _productCrud.AddCartItem(cartItem.ProductID, user.UserID);
                    return Ok(item);
                }
            }
            return BadRequest();
        }
    }
}
