using System;
using System.Text;
using SkiaSharp;

namespace Graphics;

static class Program
{
    private static GraphicsWindow Window = default!;
    private static readonly SKPaint BasicPaint = new()
    {
        Color = SKColors.Black,
        IsAntialias = true,
        TextSize = 30,
    };

    private static SKRect CubeRect = new(100, 100, 200, 200);
    private static SKPoint TextPoint = new(220, 160);
    private static readonly StringBuilder TextToShow = new("Hello World");

    static void Main()
    {
        Window = new GraphicsWindow("Hello Graphics", 1000, 800);
        Window.OnFrame += OnFrame;
        Window.OnLoaded += OnWindowLoaded;

        Window.Start();
    }

    static void OnWindowLoaded()
    {
        foreach (var keyboard in Window.Input.Keyboards)
        {
            keyboard.KeyChar += (keyboard, ch) =>
            {
                TextToShow.Clear();
                TextToShow.Append($"Pressed '{ch}'");
            };
        }

        foreach (var mouse in Window.Input.Mice)
        {
            mouse.MouseDown += (mouse, button) =>
            {
                TextToShow.Clear();
                TextToShow.Append($"Mouse {button} Down at {mouse.Position}");
            };

            mouse.MouseUp += (mouse, button) =>
            {
                TextToShow.Clear();
                TextToShow.Append($"Mouse {button} Up at {mouse.Position}");
            };

            mouse.MouseMove += (mouse, position) =>
            {
                TextToShow.Clear();
                TextToShow.Append($"Mouse Move {position}");
            };
        }
    }

    static void OnFrame(SKCanvas canvas)
    {
        BasicPaint.StrokeWidth = 2;
        BasicPaint.Style = SKPaintStyle.Stroke;
        canvas.DrawRect(CubeRect, BasicPaint);

        BasicPaint.StrokeWidth = 1;
        BasicPaint.Style = SKPaintStyle.Fill;
        canvas.DrawText(TextToShow.ToString(), TextPoint, BasicPaint);
    }
}