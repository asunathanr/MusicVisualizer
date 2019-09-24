using _3DMusicVisualizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Windows.Media;

namespace _3D_Music_Visualizer.Tests
{
    [TestClass]
    public class MusicLoaderTest
    {
        private Mock<MediaPlayer> mMediaPlayerMock;
        private Mock<Uri> mUriMock;

        [TestInitialize]
        public void Initialize()
        {
            mMediaPlayerMock = new Mock<MediaPlayer>();
            mUriMock = new Mock<Uri>();
        }

        [TestMethod]
        public void TestMusicLoaderThrowsExceptionWhenMediaPlayerIsNull()
        {
            // Arrange
            mMediaPlayerMock = null;
            var uut = CreateUnitUnderTest();

            // Act

            // Assert
        }

        [TestMethod]
        public void TestMusicLoaderLoadsValidResource()
        {
            // Arrange
            
            var uut = CreateUnitUnderTest();

            // Act

            // Assert
        }

        private MusicLoader CreateUnitUnderTest()
        {
            return new MusicLoader(mMediaPlayerMock.Object, mUriMock.Object);
        }
    }
}
