using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface UserRepository
    {
        User Create(User _user);
        User Get(Guid _id);
        void Delete(Guid _id);
    }
}
