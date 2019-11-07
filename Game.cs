

using NAudio.Wave;
using System;
using System.Threading;

/// <summary>
/// The main class that runs the game loop.
/// </summary>
public class Game
{
    // The number of milliseconds after which the update loop will run
    private const int _MIN_UPDATE_INTERVAL_MILLISECONDS = 250;
    private const int _MAX_UPDATE_INTERVAL_MILLISECONDS = 500;

    // The paths of the audio files to play
    private readonly string[] _AUDIO_FILES = new string[] {
        "/Sounds/gasp.wav",
        "/Sounds/oink.wav"
    };

    // The random class that will determine a random time interval to update after
    private Random _randomUpdateTime = new Random();

    // The random class that will determine a random clip index from the clip collection
    private Random _randomClipIndex = new Random();

    // The output to play sounds wiwth
    private WaveOutEvent outputDevice = new WaveOutEvent();

    /// <summary>
    /// Do anything that only needs to happen once at the beginning of the game here.
    /// </summary>
    public void Init()
    {
        // Kick off the update loop
        UpdateLoop();
        ReadKeyInput();
    }

    private void UpdateLoop()
    {
        // Get a random time bewtween the min and max update intervals
        int updateTime = _randomUpdateTime.Next(_MIN_UPDATE_INTERVAL_MILLISECONDS, _MAX_UPDATE_INTERVAL_MILLISECONDS);

        // Pause this thread for the defined amount of time
        Thread.Sleep(updateTime);

        // Invoke the update method
        Update();

        // Restart the update loop
        UpdateLoop();
    }

    private void ReadKeyInput()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.A) Console.WriteLine("HEY YOU PRESSED A");
    }

    private void Update()
    {
        // Play a random sound
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        // Determine a random clip index to play
        int index = _randomClipIndex.Next(_AUDIO_FILES.Length);

        // Get a reference to the clip at the determined index
        AudioFileReader randomClip = new AudioFileReader(Environment.CurrentDirectory + _AUDIO_FILES[index]);

        // Create a new output device
        outputDevice = new WaveOutEvent();

        // Initialize the output device with the random clip
        outputDevice.Init(randomClip);

        // Play the random clip
        outputDevice.Play();
    }
}

