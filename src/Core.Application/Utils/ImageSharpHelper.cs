using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Core.Application.Utils
{
    public static class ImageSharpHelper
    {
        /// <summary>
        /// If the image dimension are biger than resize, else nothing
        /// </summary>
        /// <param name="ImagePath"></param>
        public static long ResizeAndSave(string imagePath, int maxHeight = 1080, int maxWidth = 1920)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return 0;
            if (!File.Exists(imagePath)) return 0;

            using (Image image = Image.Load(imagePath))
            {
                var newHeight = image.Height;
                var newWidth = image.Width;
                var shouldResize = newHeight > maxHeight || newWidth > maxWidth;
                if (!shouldResize) return new FileInfo(imagePath).Length;

                if (newWidth > maxWidth)
                {
                    newHeight = (int)Math.Round(newHeight * Decimal.Divide(maxWidth, newWidth));
                    newWidth = maxWidth;
                }
                else if (newHeight > maxHeight)
                {
                    newWidth = (int) Math.Round(newWidth * Decimal.Divide(maxHeight, newHeight));
                    newHeight = maxHeight;
                }

                image.Mutate(x => x.Resize(newWidth, newHeight));
                image.Save(imagePath);
                return new FileInfo(imagePath).Length;
            }
        }
    }
}
