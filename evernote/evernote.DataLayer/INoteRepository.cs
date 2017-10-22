using System;
using System.Collections.Generic;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface INoteRepository
    {
        Note Create(Note note);
        Note Update(Note note);
        void Delete(Guid id);

        void AddCategory(short noteID, short categoryID);
        void AddShares(short noteID, short userID);    

        IEnumerable<Note> GetUserNotes(Guid userId);

        IEnumerable<Note> GetByCategory(short categoryID); 

    }
}
