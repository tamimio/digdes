using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evernote.Model
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public User User_ID { get; set; } // public IEnumerable<User> Users { get; set; }
    }
}
