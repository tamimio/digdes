using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using evernote.Model;

namespace evernote.DataLayer
{
    public interface CategoryRepository
    {
        Category Create(Guid _UserID, string name);
    }
}
