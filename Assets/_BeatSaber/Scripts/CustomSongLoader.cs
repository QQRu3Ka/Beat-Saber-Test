using System.Collections;
using System.Collections.Generic;
using System.IO;
using NVorbis;
using UnityEngine;

public class CustomSongLoader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _audioClip;

    private void Start()
    {
        var path = Application.persistentDataPath + "/TheMaster.ogg";
        var audioClip = LoadOgg(path);
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private AudioClip LoadOgg(string path)
    {
        using var vorbis = new VorbisReader(File.OpenRead(path), false);
        var sampleRate = vorbis.SampleRate;
        var channels = vorbis.Channels;
        var samples = new float[vorbis.TotalSamples * channels];
        vorbis.ReadSamples(samples, 0, samples.Length);
            
        var audioClip = AudioClip.Create("LoadedOgg",  samples.Length / channels, channels, sampleRate, false);
        audioClip.SetData(samples, 0);
            
        return audioClip;
    }
}
