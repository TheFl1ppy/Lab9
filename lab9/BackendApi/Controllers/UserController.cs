using OnlineShop.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Contracts;
using Mapster;

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение списка всех пользователей БД
        /// </summary>
        // POST api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        // POST api/<UsersController>
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            var response = new GetUserResponse()
            {
                id_user = result.UsersId,
                login = result.Name,
                role_id = result.Role,
                is_deleted = (bool)result.IsDeleted,
            };
            return Ok(response);
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "login" : "Login",
        ///         "password" : "12345",
        ///         "role_id" : 3,
        ///         "address" : "Address"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var userDto = request.Adapt<User>();
            await _userService.Create(userDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "id_user" : 1,
        ///         "login" : "Login",
        ///         "password" : "12345",
        ///         "role_id" : 3,
        ///         "address" : "Address"
        ///         "is_deleted" : false
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        // POST api/<UsersController>
        [HttpPut]
        public async Task<IActionResult> Update(GetUserResponse request)
        {
            var user = request.Adapt<User>();
            await _userService.Update(user);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        // POST api/<UsersController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
