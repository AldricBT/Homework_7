using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7
{
    struct Worker
    {
        public Worker(int ID, DateTime DateTimeAddNote, string FIO, int Age, int Height, DateTime DateOfBirth, string PlaceOfBirth)
        {
            this.ID = ID;
            this.DateTimeAddNote = DateTimeAddNote;
            this.FIO = FIO;
            this.Age = Age;
            this.Height = Height;
            this.DateOfBirth = DateOfBirth;
            this.PlaceOfBirth = PlaceOfBirth;
        }

        public int ID { get; set; }
        public DateTime DateTimeAddNote { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }

        

    }
}
