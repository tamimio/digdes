using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evernote.Model
{
    public class Note
    {
        public Guid ID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public User User_ID { get; set; } // owner
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        // связь с Category
        // связь с Share
    }
}
