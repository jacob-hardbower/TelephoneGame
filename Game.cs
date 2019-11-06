/// <summary>
/// The main class that runs the game loop.
/// </summary>

using NAudio.Wave;
using System;
using System.Threading;

public class Game
{
    // The number of milliseconds after which the update loop will run
    private const int _UPDATE_TIME_MILLISECONDS_MIN = 250;
    private const int _UPDATE_TIME_MILLISECONDS_MAX = 500;
    private readonly string[] _AUDIO_FILES = new string[] {
        "/Sounds/gasp.wav",
        "/Sounds/oink.wav"
    };
    private Random _randomUpdateTime = new Random();
    private Random _randomClipIndex = new Random();

    /// <summary>
    /// Do anything that only needs to happen once at the beginning of the game here.
    /// </summary>
    public void Init()
    {
        UpdateLoop();
        Console.WriteLine(Environment.CurrentDirectory);
    }

    private void UpdateLoop()
    {
        // Get a random time
        int updateTime = _randomUpdateTime.Next(_UPDATE_TIME_MILLISECONDS_MIN, _UPDATE_TIME_MILLISECONDS_MAX);

        Thread.Sleep(updateTime);

        Update();

        UpdateLoop();
    }

    private void Update()
    {
        int index = _randomClipIndex.Next(_AUDIO_FILES.Length);

        AudioFileReader randomClip = new AudioFileReader(Environment.CurrentDirectory + _AUDIO_FILES[index]);

        WaveOutEvent outputDevice = new WaveOutEvent();

        outputDevice.Init(randomClip);
        outputDevice.Play();
    }
}

