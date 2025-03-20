using DigitalBookStoreManagement.Authentication;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DigitalBookStoreManagement.Repositories;
using DigitalBookStoreManagement.Repository;
using DigitalBookStoreManagement.Expections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IAuth jwtAuth;
        private readonly IOrderRepository _orderRepository;

        public UserController(IUserService service, IAuth jwtAuth, IOrderRepository orderRepository)
        {
            this.service = service;
            this.jwtAuth = jwtAuth;
            this._orderRepository = orderRepository;
        }
        //Get all the data stored in database 
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {

            return Ok(service.GetUserInfo());
             
        }

        //Get the particular data according to the id
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("UserById/{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(service.GetUserInfo(id));
            }
            catch (UserNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //Profile Management
        [Authorize(Roles = "Customer,Admin")]
        //[AllowAnonymous]
        [HttpGet("ProfileManagement")]
        public ActionResult GetProfile(int UsereId)
        {
            var profile = _orderRepository.GetOrderByUserId(UsereId);
            int count = profile.Count();
            try
            {
                if (count == 0)
                {
                    return NotFound("User doesn't exist");
                }
                return Ok(profile);


            }
            catch
            {

                return StatusCode(500, "Internal server error");
            }
        }

        //Insert the data in the data 
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Post(User userInfo)
        {
            try
            {
                return Ok(service.AddUser(userInfo));
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        }

        //Update the record
        [Authorize(Roles = "Customer,Admin")]
        [HttpPut]
        [Route("Update-Details")]
        public ActionResult Put(int id, [FromBody]string password)
        {
            return Ok(service.UpdateUser(id, password));
        }


        //Authentication of the user
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential credential)
        {
            var token = jwtAuth.Authentication(credential.Email, credential.Password);
            if (token == "invalid credential")
                return Unauthorized(new { Warning = "Invalid Credentials" });
            return Ok(token);
        }
    }
}
