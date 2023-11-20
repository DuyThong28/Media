using Avalonia.Platform;
using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using System.Drawing;
using Color = Avalonia.Media.Color;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace Media.ViewModels
{
    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }
        public static Bitmap ConvertToAvaloniaBitmap(this System.Drawing.Image bitmap)
        {
            if (bitmap == null)
                return null;
            System.Drawing.Bitmap bitmapTmp = new System.Drawing.Bitmap(bitmap);
            var bitmapdata = bitmapTmp.LockBits(new Rectangle(0, 0, bitmapTmp.Width, bitmapTmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap bitmap1 = new Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                bitmapdata.Scan0,
                new Avalonia.PixelSize(bitmapdata.Width, bitmapdata.Height),
                new Avalonia.Vector(96, 96),
                bitmapdata.Stride);
            bitmapTmp.UnlockBits(bitmapdata);
            bitmapTmp.Dispose();
            return bitmap1;
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
