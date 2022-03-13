using System;
using System.Collections.Generic;

namespace University_Lab2
{
    public partial class Faculty
    {
        public Faculty()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
