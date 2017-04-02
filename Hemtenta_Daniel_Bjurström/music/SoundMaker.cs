using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class SoundMaker : ISoundMaker
    {
        ISong _currentSong;
        public string NowPlaying
        {
            get
            {
                return _currentSong == null ? "" : _currentSong.Title;
            }
        }

        public void Play(ISong song)
        {
            _currentSong = song;
        }

        public void Stop()
        {
            _currentSong = null;
        }
    }
}
