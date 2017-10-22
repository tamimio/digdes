using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using evernote.DataLayer;
using evernote.DataLayer.Sql;
using evernote.Model;

namespace evernote.Api.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    public class UserController : ApiController
    {
		private const string ConnectionString = @"Data Source=usera\SQLE; Initial Catalog=evernote; User ID=sa";
        private readonly IUsersRepository _usersRepository;

        public UserController()
        {
            _usersRepository = new UsersRepository(ConnectionString, new CategoriesRepository(ConnectionString));
        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users/{id}")]
        public User Get(Guid id)
        {
            return _usersRepository.Get(id);
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/create")]
        public User Post([FromBody] User user)
        {
            return _usersRepository.Create(user);
        }

        /// <summary>
		/// Удаление пользователя
		/// </summary>
		/// <param name="id">Идентификатор пользователя</param>
		/// <returns></returns>
		[HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            _usersRepository.Delete(id);
        }

        /// <summary>
		/// Получение категорий пользователя
		/// </summary>
		/// <param name="id">Идентификатор пользователя</param>
		/// <returns></returns>
		[HttpGet]
        [Route("api/users/{id}/categories")]
        public IEnumerable<Category> GetUserCategories(Guid id)
        {
            return _usersRepository.Get(id).Categories;
        }
    }
}
