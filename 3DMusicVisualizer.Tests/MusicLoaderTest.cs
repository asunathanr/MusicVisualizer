using System;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace _3DMusicVisualizer.Tests
{
    [TestClass]
    public class MusicLoaderTest
    {
        private Mock<MediaPlayer> mMediaPlayerMock;
        private Mock<Uri> mUriMock;

        [TestInitialize]
        public void Init()
        {
            mMediaPlayerMock = new Mock<MediaPlayer>();
            mUriMock = new Mock<Uri>();
        }

        [TestMethod]
        public void MusicLoaderMediaPlayerResourceIsOpenWhenValidURIIsGiven()
        {
            // Mock of file system opening a valid file.
            mMediaPlayerMock.Setup(o => o.Open(It.Is<Uri>(p => p == mUriMock.Object)));
            mUriMock.SetupGet(o => o.AbsolutePath).Returns("valid");
            var uut = CreateUnitUnderTest();
            Assert.IsTrue(uut.MusicPlayer.HasAudio);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void MusicLoaderThrowsExceptionWhenMusicPlayerArgumentIsNull()
        {
            mMediaPlayerMock = null;
            var uut = CreateUnitUnderTest();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void MusicLoaderThrowsExceptionWhenURIIsNull()
        {
            mUriMock = null;
            var uut = CreateUnitUnderTest();
        }

        private MusicLoader CreateUnitUnderTest()
        {
            return new MusicLoader(mMediaPlayerMock.Object, mUriMock.Object);
        }
    }
}
