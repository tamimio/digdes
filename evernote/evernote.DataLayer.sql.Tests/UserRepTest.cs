using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting; 

using evernote.Model;
using evernote.DataLayer.sql;

namespace evernote.DataLayer.sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private const string connectStr = @"Data Source=usera\SQLE;Initial Catalog=evernote;User ID=sa";
        private readonly List<Guid> _tempUsers = new List<Guid>();

        [TestMethod]
        public void CreatingUser()
        {
            var user = new User
            {
                Name = "test-user-name"
            };

			var repository = new UserRep(connectStr, new CategoryRep(connectStr));
			var result = repository.Create(user);

            _tempUsers.Add(user.ID);

            var userFromDB = repository.Get(result.ID);

            Assert.AreEqual(user.Name, userFromDB.Name);
        }

		
        [TestCleanup]
        public void CleanData()
        {
            foreach (var id in _tempUsers)
                new UserRep(connectStr).Delete(id);
        }
    }
}
