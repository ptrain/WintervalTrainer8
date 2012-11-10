using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.MusicClasses
{
    class Note
    {

        public string Name { get; set; }
        public int Num { get; set; }
        public string File { get; set; }

        public string getFilePath()
        {
            return "sounds/" + this.File;
        }

    }
}
