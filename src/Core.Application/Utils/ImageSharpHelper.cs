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
        public static void ResizeAndSave(string imagePath, int maxHeight = 1080, int maxWidth = 1920)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return;
            if (!File.Exists(imagePath)) return;

            using (Image image = Image.Load(imagePath))
            {
                var newHeight = image.Height;
                var newWidth = image.Width;
                var shouldResize = newHeight > maxHeight || newWidth > maxWidth;
                

                if (newWidth > maxWidth)
                {
                    newHeight = (int)Math.Round(newHeight * Decimal.Divide(maxWidth, newWidth));
                    newWidth = maxWidth;
                }
                if (newHeight > maxHeight)
                {
                    newWidth = (int) Math.Round(newWidth * Decimal.Divide(maxHeight, newHeight));
                    newHeight = maxHeight;
                }

                if (!shouldResize) return;

                image.Mutate(x => x.Resize(newWidth, newHeight));
                image.Save(imagePath);
            }
        }
    }
}
