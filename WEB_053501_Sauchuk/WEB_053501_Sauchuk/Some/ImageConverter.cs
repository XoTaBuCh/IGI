using System.Drawing;

namespace WEB_053501_Sauchuk.Some;

public class ImageConverter
{
    public static MemoryStream Base64ToImage(string base64)
    {
        return new MemoryStream(Convert.FromBase64String(base64));
    }

    public static string ImageToBase64(string file)
    {
        Bitmap bitmap = new Bitmap(file);
        byte[] bytes = (byte[])new System.Drawing.ImageConverter().ConvertTo(bitmap, typeof(byte[]));

        return Convert.ToBase64String(bytes);
    }

    public static string ImageToBase64(IFormFile formFile, bool trim = false)
    {
        try
        {
            Stream stream = formFile.OpenReadStream();
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            Bitmap bitmap;
            if (trim)
            {
                bitmap = new Bitmap(Math.Min(image.Width, image.Height), Math.Min(image.Width, image.Height));
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                graphics.Dispose();
                bitmap = new Bitmap(bitmap, new Size(256, 256));
            }
            else
            {
                bitmap = new Bitmap(image);
            }

            byte[] bytes = (byte[])new System.Drawing.ImageConverter().ConvertTo(bitmap, typeof(byte[]));

            return Convert.ToBase64String(bytes);
        }
        catch
        {
            return "";
        }
    }
}