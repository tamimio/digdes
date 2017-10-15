using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface NoteRepository
    {
        Note Create(Note _note);
        Note Update(Note _note);
        void Delete(Guid _id);

        IEnumerable<Note> GetUserNotes(Guid userId);
    }
}
