using System;
using System.Collections.Generic;

namespace University_Lab2
{
    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Facultyid { get; set; }

        public virtual Faculty Faculty { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; }
    }
}
