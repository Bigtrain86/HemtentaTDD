using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class Song : ISong
    {
        string songTitle;

        public Song(string songtitle)
        {
            this.songTitle = songtitle;
        }
        public string Title
        {
            get
            {
                return songTitle;
            }
        }
    }
}
