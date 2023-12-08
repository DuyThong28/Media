using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Color = Avalonia.Media.Color;
using SkiaSharp;

namespace Media.ViewModels
{
    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }

        public static Color GetDominantColor(string imagePath)
        {
            using (var stream = System.IO.File.OpenRead(imagePath))
            using (var bitmap = SKBitmap.Decode(stream))
            {
                // Get the dimensions of the image
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Dictionary to store color frequencies
                var colorFrequencies = new Dictionary<Color, int>();

                // Loop through each pixel in the image
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Get the color of the current pixel
                        SKColor pixelColor = bitmap.GetPixel(x, y);

                        // Convert SKColor to WPF Color
                        Color wpfColor = Color.FromArgb(pixelColor.Alpha, pixelColor.Red, pixelColor.Green, pixelColor.Blue);

                        // Count the frequency of each color
                        if (colorFrequencies.ContainsKey(wpfColor))
                        {
                            colorFrequencies[wpfColor]++;
                        }
                        else
                        {
                            colorFrequencies[wpfColor]=1;
                        }
                    }
                }

                // Find the color with the highest frequency
                Color dominantColor = colorFrequencies.OrderByDescending(c => c.Value).First().Key;
                return dominantColor;
            }
        }

    }
}
