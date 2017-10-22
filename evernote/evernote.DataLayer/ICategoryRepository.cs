using System;
using System.Collections.Generic;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface ICategoryRepository
    {
        Category Create(Guid UserID, string name);
        Category Get(short id);
        void Delete(short CategoryID);

        IEnumerable<Category> GetNoteCategories(short NoteID);
    }
}
