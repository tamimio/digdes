using System;
using System.Collections.Generic;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface IUserRepository
    {
        User Create(User user);
        User Get(Guid id);
        void Delete(Guid id);
    }
}
