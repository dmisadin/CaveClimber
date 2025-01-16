using System;
using Godot;

public partial class Torch : PointLight2D
{
    // Variables to control flicker
    [Export(PropertyHint.Range, "0.0,1.0")] public float IntensityMin = 0.7f;
    [Export(PropertyHint.Range, "0.0,1.0")] public float IntensityMax = 1.0f;
    [Export(PropertyHint.Range, "0.0,0.5")] public float FlickerSpeed = 0.1f;

    private float timer = 0.0f;
    private Random random = new Random();

    public override void _Process(double delta)
    {
        timer += (float)delta;

        if (timer < FlickerSpeed)
            return;

        timer = 0.0f;

        float intensityRange = IntensityMax - IntensityMin;
        float randomIntensity = (float)(random.NextDouble() * intensityRange + IntensityMin);
        Energy = randomIntensity;
    }
}
