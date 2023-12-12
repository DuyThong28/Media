using Avalonia.Platform;
using System;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Color = Avalonia.Media.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp.Processing;

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
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                image.Mutate(x => x
                .Resize(new ResizeOptions { Sampler = KnownResamplers.NearestNeighbor, Size = new Size(100, 0) }));

                int width = image.Width;
                int height = image.Height;
                int min = height>width ? width : height;
                var colorFrequencies = new Dictionary<Color, int>();

                for (int x = 0; x < min; x++)
                {
                    for (int y = 0; y < min; y++)
                    {
                        var pixel = image[x, y];
                        Color wpfColor = Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
                        if (colorFrequencies.ContainsKey(wpfColor))
                        {
                            colorFrequencies[wpfColor]++;
                        }
                        else
                        {
                            colorFrequencies[wpfColor] = 1;
                        }
                    }
                }

                Color dominantColor = colorFrequencies.OrderByDescending(c => c.Value).First().Key;
                return dominantColor;
              
            }
        }

    }
}
