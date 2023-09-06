using api_gaming_global.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Data.SqlClient;
using System.Data;
using api_gaming_global.Models.Crud;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Any;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Helpers;
using api_gaming_global.Helpers;

namespace api_gaming_global.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SqlHelper _sqlHelper;
        private readonly UserCrud _userCrud;
        private readonly JwtHelper _jwtHelper;

        public AuthController(SqlHelper sqlHelper, UserCrud userCrud, JwtHelper jwtHelper) { 
            _userCrud = userCrud;
            _sqlHelper = sqlHelper;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("signin-user")]
        public async Task<IActionResult> SignInUser([FromBody] UserSigninInfo requestInfo)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(requestInfo.access_token);
                var service = new Oauth2Service(new BaseClientService.Initializer { HttpClientInitializer = credential });
                Userinfo userinfo = service.Userinfo.Get().Execute();
                DataTable registeredUser;
                int userID = 0;
                var userDataTable = await _userCrud.GetUser(userinfo.Id);
                var userRegistration = new UserRegistration
                {
                    DisplayName = userinfo.Name,
                    Email = userinfo.Email,
                    ProfilePictureURL = userinfo.Picture,
                    ProviderID = userinfo.Id,
                    ProviderName = "Google",
                    NewRegistration = false,
                };

                if (userDataTable.Rows.Count == 0) {
                    userRegistration.NewRegistration = true;
                    registeredUser = await _userCrud.AddNewUser(userRegistration);
                    DataRow registeredUserRow = registeredUser.Rows[0];
                    userID = (int)registeredUserRow["UserID"];
                } else
                {
                    DataRow registeredUserRow = userDataTable.Rows[0];
                    userID = (int)registeredUserRow["UserID"];
                }
                
                await _userCrud.UpdateUser(userRegistration);

                var accessToken = new JwtToken
                {
                    token = _jwtHelper.GetJwtToken(new UserJWTClaim { UserID = userID })
                };

                return Ok(accessToken);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify-session")]
        public IActionResult verifySession()
        {
            string authHeader = Request.Headers["Authorization"].FirstOrDefault();
            SessionValid session = _jwtHelper.verifyTokenSession(authHeader);
            return Ok(session.valid);
        }
    }
}
