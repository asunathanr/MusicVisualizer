using NAudio.Wave;
using System;

namespace VisualizerAudio
{
    public delegate void OnReadHandler(object sender, ReadEventArgs e);

    /// <summary>
    /// Allows playback of an audio file but also reports when new bytes are sampled from the audio file.
    /// </summary>
    public class AudioFileObserverStream : AudioFileReader
    {
        public AudioFileObserverStream(string fileName) : base(fileName)
        {
        }

        public event OnReadHandler OnRead;

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = base.Read(buffer, offset, count);
            ReadEventArgs args = new ReadEventArgs(Position);
            OnRead(this, args);
            return bytesRead;
        }

        public new int Read(float[] buffer, int offset, int count)
        {
            int bytesRead = base.Read(buffer, offset, count);
            ReadEventArgs args = new ReadEventArgs(Position);
            OnRead(this, args);
            return bytesRead;
        }
    }
}
