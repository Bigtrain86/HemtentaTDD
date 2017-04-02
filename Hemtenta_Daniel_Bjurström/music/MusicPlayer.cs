using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {
        List<ISong> _listOfSongs;
        ISoundMaker _soundMaker;
        IMediaDatabase _mediaDatabase;

        public MusicPlayer(IMediaDatabase mediaDatabase, ISoundMaker soundMaker)
        {
            _listOfSongs = new List<ISong>();
            _mediaDatabase = mediaDatabase;
            _soundMaker = soundMaker;
        }

        public int NumSongsInQueue
        {
            get
            {
                return _listOfSongs.Count;
            }
        }

        public void LoadSongs(string search)
        {
            if (!_mediaDatabase.IsConnected)
            {
                throw new DatabaseClosedException();
            }
            _mediaDatabase.OpenConnection();


            if (!string.IsNullOrEmpty(search))
            {
                _listOfSongs = _mediaDatabase.FetchSongs(search);
            }
            _mediaDatabase.CloseConnection();
        }

        public void NextSong()
        {
            if (NumSongsInQueue > 0)
            {
                _soundMaker.Play(_listOfSongs.FirstOrDefault());
            }
            else
            {
                Stop();
            }
        }

        public string NowPlaying()
        {
            return string.IsNullOrEmpty(_soundMaker.NowPlaying) ? "Tystnad råder" : string.Format("Spelar {0}", _soundMaker.NowPlaying);
        }

        public void Play()
        {
            if (string.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                _soundMaker.Play(_listOfSongs.FirstOrDefault());
            }
        }

        public void Stop()
        {
            _soundMaker.Stop();
        }

        public void OpenConnection()
        {
            if (_mediaDatabase.IsConnected)
            {
                throw new DatabaseAlreadyOpenException();
            }

            _mediaDatabase.OpenConnection();
        }
    }
}