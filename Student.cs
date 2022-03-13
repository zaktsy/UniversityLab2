using System;
using System.Collections.Generic;

namespace University_Lab2
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Surname { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}
