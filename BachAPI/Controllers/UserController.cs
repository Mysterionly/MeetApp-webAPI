using BachAPI.Models;
using BachAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IEventService eventService)
        {
            _userService = userService;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        ////[HttpGet("GetUsers", Name = "GetUsers")]
        //public List<User> GetUsers()
        //{
        //    //берем всех пользователей из бд
        //    List<User> usersDB = _userService.Gets();

        //    List<User> usersFull = new List<User>();

        //    foreach (User item in usersDB)
        //    {
        //        usersFull.Add(ConvertUser(item));
        //    }

        //    return usersFull;
        //}

        [ApiExplorerSettings(IgnoreApi = true)]
        // GET api/<MainController>/5
        [HttpGet("GetUser", Name = "GetUser")]
        public ActionResult<User> GetUser(string id)
        {
            //берем пользователя из бд
            User userDB = _userService.Get(id);

            if (userDB == null)
            {
                return NotFound();
            }

            return ConvertUser(userDB);
        }


        [HttpPost("GetUserOnGid", Name = "GetUserOnGid")]
        public ActionResult<User> GetUserOnGid([FromBody] string gid)
        {
            //берем пользователя из бд
            User userDB = _userService.GetOnGid(gid);

            if (userDB == null)
            {
                return NotFound();
            }

            return ConvertUser(userDB);
        }



        // GET api/<MainController>/5
        [HttpPost("GetUserOnUid", Name = "GetUserOnUid")]
        public ActionResult<User> GetUserOnUid([FromBody] string uid)
        {
            //берем пользователя из бд
            User userDB = _userService.Get(uid);
            User retUsr = new User();
            retUsr.Username = userDB.Username;

            return retUsr;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        //[HttpPost("PGetUser", Name = "PGetUser")]
        //public ActionResult<User> PGetUser([FromBody] string id)
        //{
        ////    берем пользователя из бд
        //    User userDB = _userService.Get(id);

        //    if (userDB == null)
        //    {
        //        return NotFound();
        //    }

        //    return ConvertUser(userDB);
        //}

        // POST api/<MainController>
        [HttpPost("CreateUser", Name = "CreateUser")]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            //Переходим в Get с именем GetUser
            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        ////[HttpPut("UpdateUser", Name = "UpdateUser")]
        //public IActionResult Update(string id, User userIn)
        //{
        //    var user = _userService.Get(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Update(id, userIn);

        //    return NoContent();
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        ////[HttpPost("PUpdateUser", Name = "PUpdateUser")]
        //public ActionResult<User> PUpdate(User userIn)
        //{
        //    var user = _userService.Get(userIn.Id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Update(userIn.Id, userIn);

        //    //Переходим в Get с именем GetUser
        //    return CreatedAtRoute("GetUser", new { id = userIn.Id.ToString() }, userIn);
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        ////[HttpDelete("DeleteUser", Name = "DeleteUser")]
        //public IActionResult Delete(string id)
        //{
        //    var user = _userService.Get(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Remove(user.Id);

        //    return NoContent();
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        ////[HttpPost("PDeleteUser", Name = "PDeleteUser")]
        //public IActionResult PDelete([FromBody] string id)
        //{
        //    var user = _userService.Get(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Remove(user.Id);

        //    return Ok();
        //}

        [ApiExplorerSettings(IgnoreApi = true)]
        public User ConvertUser(User userDB)
        {
            //Создаем и заполняем список событий пользователя
            List<Event> userEventsDB = new List<Event>();

            //if (userDB.UserEvents != null)
            //foreach (string item in userDB.UserEvents)
            //{
            //    //берем событие по EventTypeId
            //    Event eventDB = _eventService.Get(item);
            //    userEventsDB.Add(eventDB);
            //}
            //-----------------------------------------------

            //Создаем и заполняем список событий, в которых участвует пользователь
            List<Event> joinedEventsDB = new List<Event>();

            //if (userDB.JoinedEvents != null)
            //    foreach (string item in userDB.JoinedEvents)
            //{
            //    //берем событие по EventTypeId
            //    Event eventDB = _eventService.Get(item);
            //    joinedEventsDB.Add(eventDB);
            //}
            //--------------------------------------------------------------------

            //заполняем Пользователя + объекты Событие
            User userFull = new User()
            {
                //Avatar = userDB.Avatar,
                //CurrentLocation = userDB.CurrentLocation,
                GoogleID = userDB.GoogleID,
                //HomeLocation = userDB.HomeLocation,
                Id = userDB.Id,
                Username = userDB.Username,
                //JoinedEvents = joinedEventsDB,
                //UserEvents = userEventsDB
            };

            return userFull;
        }
    }
}
