using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NAudio.Wave;

namespace VisualizerAudio.Tests
{
    [TestClass]
    public class AudioStatisticsTest
    {
        private Mock<AudioFileReader> mockAudioFileReader;

        [TestInitialize]
        public void Initialize()
        {
            mockAudioFileReader = new Mock<AudioFileReader>();
        }

        

        private AudioStatistics CreateUnitUnderTest()
        {
            return new AudioStatistics();
        }
    }
}
