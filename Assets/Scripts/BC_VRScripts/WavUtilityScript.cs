using System.IO;
using UnityEngine;

public static class WavUtility
{
    public static byte[] FromAudioClip(AudioClip clip)
    {
        MemoryStream stream = new MemoryStream();
        const int HEADER_SIZE = 44;

        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        byte[] data = new byte[samples.Length * 2];
        int offset = 0;
        foreach (float sample in samples)
        {
            short value = (short)(sample * short.MaxValue);
            byte[] bytes = System.BitConverter.GetBytes(value);
            data[offset++] = bytes[0];
            data[offset++] = bytes[1];
        }

        stream.Position = 0;
        // WAV header
        stream.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"), 0, 4);
        stream.Write(System.BitConverter.GetBytes(data.Length + HEADER_SIZE - 8), 0, 4);
        stream.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"), 0, 4);
        stream.Write(System.Text.Encoding.ASCII.GetBytes("fmt "), 0, 4);
        stream.Write(System.BitConverter.GetBytes(16), 0, 4); // Subchunk1Size
        stream.Write(System.BitConverter.GetBytes((ushort)1), 0, 2); // AudioFormat
        stream.Write(System.BitConverter.GetBytes((ushort)clip.channels), 0, 2);
        stream.Write(System.BitConverter.GetBytes(clip.frequency), 0, 4);
        stream.Write(System.BitConverter.GetBytes(clip.frequency * clip.channels * 2), 0, 4); // ByteRate
        stream.Write(System.BitConverter.GetBytes((ushort)(clip.channels * 2)), 0, 2); // BlockAlign
        stream.Write(System.BitConverter.GetBytes((ushort)16), 0, 2); // BitsPerSample
        stream.Write(System.Text.Encoding.ASCII.GetBytes("data"), 0, 4);
        stream.Write(System.BitConverter.GetBytes(data.Length), 0, 4);
        stream.Write(data, 0, data.Length);

        return stream.ToArray();
    }
}
