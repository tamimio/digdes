using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using evernote.Model;

namespace evernote.DataLayer.sql.Tests
{
	[TestClass]
	public class NotesRepositoryTests
	{
		private const string connectStr = @"Data Source=usera\SQLE;Initial Catalog=evernote;User ID=sa";
		private readonly List<Guid> _tempUsers = new List<Guid>();

		[TestMethod]
		public void CreatingNote()
		{
			var user = new User
			{
				Name = "test-user-name"
			};

			var categoriesRepository = new CategoryRep(connectStr);
			var usersRepository = new UserRep(connectStr, categoriesRepository);

			var users = usersRepository.Create(user);
			_tempUsers.Add(user.ID);
			
			var repository = new NotesRepository(connectStr, categoriesRepository);
			var result = repository.Create(user, "test-note", "test-note-content");
			var notesFromDb = repository.GetUserNotes(user.ID);

			//asserts
			Assert.AreEqual(result.ID, notesFromDb.Single().Name);
		}
		[TestCleanup]
		public void CleanData()
		{
			foreach (var id in _tempUsers)
				new UserRep(connectStr, new CategoryRep(connectStr)).Delete(id);
		}
	}
}
