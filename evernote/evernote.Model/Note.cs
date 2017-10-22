using System;
using System.Collections.Generic;

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

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<User> Shares { get; set; }
    }
}
