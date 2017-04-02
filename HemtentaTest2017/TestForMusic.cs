using HemtentaTdd2017.music;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class TestForMusic
    {
        const string Track_1 = "Enter Sandman ";
        const string Track_2 = "I Stand Alone";

        MusicPlayer _mplayer;
        Mock<IMediaDatabase> _mediaDatabase;
        ISoundMaker _soundMaker;
        List<ISong> _songs;

        public TestForMusic()
        {
            _mediaDatabase = new Mock<IMediaDatabase>();
            _soundMaker = new SoundMaker();

            _songs = new List<ISong>
            {
                new Song(Track_1),
                new Song(Track_2)
            };

            _mediaDatabase.Setup(x => x.IsConnected).Returns(true);
            _mediaDatabase.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs);

            _mplayer = new MusicPlayer(_mediaDatabase.Object, _soundMaker);
        }

        [Fact]
        public void NumSongsInQueue_LoadSongsWithSearch_Success()
        {
            _mediaDatabase.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs.Where(s => s.Title.Contains(Track_1)).ToList());

            _mplayer.LoadSongs(Track_1);

            Assert.Equal(1, _mplayer.NumSongsInQueue);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void LoadSongs_IncorrectValue_Failure(string search)
        {
            _mplayer.LoadSongs(search);

            Assert.Equal(0, _mplayer.NumSongsInQueue);
        }

        [Fact]
        public void OpenConnection_DatabaseAlreadyOpen_Throws()
        {
            Assert.Throws<DatabaseAlreadyOpenException>(() => _mplayer.OpenConnection());
        }

        [Fact]
        public void LoadSongs_DatabaseClosed_Throws()
        {
            _mediaDatabase.Setup(x => x.IsConnected).Returns(false);
            Assert.Throws<DatabaseClosedException>(() => _mplayer.LoadSongs("search"));
        }

        [Fact]
        public void NowPlaying_Play_Success()
        {
            _mplayer.LoadSongs("search");
            _mplayer.Play();

            Assert.Equal(string.Format("Spelar {0}", Track_1), _mplayer.NowPlaying());
        }

        [Fact]
        public void NowPlaying_NextSong_Success()
        {
            _mplayer.LoadSongs("search");
            _mplayer.NextSong();

            Assert.Equal(string.Format("Spelar {0}", Track_2), _mplayer.NowPlaying());
        }

        [Fact]
        public void NowPlaying_Stop_Success()
        {
            _mplayer.LoadSongs("search");
            _mplayer.Play();
            _mplayer.Stop();

            Assert.Equal("Tystnad råder", _mplayer.NowPlaying());
        }
    }
}