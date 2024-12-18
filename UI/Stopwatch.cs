using Godot;
using System;

public partial class Stopwatch : Label
{
    private int _elapsedTime = 0; // Time in seconds
    private Timer _timer;

    public override void _Ready()
    {
        // Get references to nodes
        _timer = GetNode<Timer>("Timer");

        // Configure the Timer
        _timer.WaitTime = 1.0f; // Trigger every second
        _timer.OneShot = false; // Keep repeating
        _timer.Timeout += OnTimerTimeout; // Connect to method

        // Start the Timer
        _timer.Start();
    }

    private void OnTimerTimeout()
    {
        // Increment elapsed time
        _elapsedTime++;

        // Format and update the label
        this.Text = FormatTime(_elapsedTime);
    }

    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;
        return $"{minutes:D2}:{remainingSeconds:D2}"; // Format as mm:ss
    }

    public void ResetStopwatch()
    {
        _elapsedTime = 0;
        this.Text = FormatTime(_elapsedTime);
    }
}
