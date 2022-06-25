using BorisKnowsAllApi.Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BorisKnowsAllApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDBService service;
        private readonly BorisKnowsAllApiContext _context;

        public UsersController(BorisKnowsAllApiContext context)
        {
            _context = context;
            service = new UserDBService(context);
        }

        [HttpPost("token")]
        public void SendToken(string username, [FromBody] String token)
        {
            if (username == null || token == null)
            {
                Response.StatusCode = 400;
                return;
            }
            User user = service.Get(username);

            if (user == null)
            {
                Response.StatusCode = 404;
                return;
            }
            service.AddFirebaseToken(user, token);
        }


        [HttpGet("token")]
        public string GetToken(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                Response.StatusCode = 400;
                return null;
            }

            User u = service.Get(username);

            if (u == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (!service.ContainsTokenForUser(u))
            {
                Response.StatusCode = 404;
                return null;
            }

            string token = service.GetUserToken(u);
            return token;
        }
        
        
        [HttpPost, Route("Login")]
        public void Login([FromBody] User user)
        {
            var u = service.Get(user.Username);
            if (u == null)
            {
                Response.StatusCode = 404;
                return;
            }

            if (u.Password != user.Password)
            {
                Response.StatusCode = 404;
                return;
            }
            // correct details
            //HttpContext.Session.SetString("username", u.Username);
            Response.StatusCode = 200;
        }

        
        [HttpPost, Route("Signup")]
        public void Signup([FromBody] User user)
        {
            if (service.Get(user.Username) != null)
            {
                Response.StatusCode = 404;
                return;
            }
            // maybe HttpContext.Session.Clear();
            service.Create(user.Username, user.Password, user.Nickname);
            Response.StatusCode = 200;
        }
    }
}
