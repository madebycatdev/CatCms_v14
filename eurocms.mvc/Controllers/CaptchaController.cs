using GTCaptchaSolution;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult Index()
        {
            GTCaptcha gtCaptcha = new GTCaptcha();
            var captchaResult = gtCaptcha.GenerateCaptcha(5, false);

            // string captchaText = GenerateRandomCode("6", true).Trim().ToLower();
            Session.Add("AdminCaptchaImageText", captchaResult.text);

            string imageBase64 = Convert.ToBase64String(captchaResult.bmp);
            //imageBase64 = CaptchaImageToBase64(System.Web.HttpContext.Current, captchaText, 300, 75);

            return File(captchaResult.bmp, "image/jpeg");

        }

        public string CaptchaImageToBase64(HttpContext context, string word, int width, int height)
        {
            string base64String = "";

            RandomImage ci = new RandomImage(word, width, height);


            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            //resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ci.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = stream.ToArray();
            base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
        private string GenerateRandomCode(string characterLength, bool captchaisnumeric)
        {
            int characterCount = 5;

            if (Int32.TryParse(characterLength, out characterCount))
            {
                characterCount = Convert.ToInt32(characterLength);
            }

            Random r = new Random();
            string s = "";
            for (int j = 0; j < characterCount; j++)
            {
                int i = r.Next(3);
                int ch;

                if (captchaisnumeric)
                {
                    ch = r.Next(48, 57);
                    s = s + Convert.ToChar(ch).ToString();
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            ch = r.Next(0, 9);
                            s = s + ch.ToString();
                            break;
                        case 2:
                            ch = r.Next(65, 90);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                        case 3:
                            ch = r.Next(97, 122);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                        default:
                            ch = r.Next(97, 122);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                    }
                }

                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }
    }

    public class RandomImage
    {
        //Default Constructor 
        public RandomImage() { }
        //property
        public string Text
        {
            get { return this.text; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public int Height
        {
            get { return this.height; }
        }
        //Private variable
        private string text;
        private int width;
        private int height;
        private Bitmap image;
        private Random random = new Random();
        //Methods declaration
        public RandomImage(string s, int width, int height)
        {
            this.text = s;
            this.SetDimensions(width, height);
            this.GenerateImage();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.image.Dispose();
        }
        private void SetDimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", width,
                    "Argument out of range, must be greater than zero.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", height,
                    "Argument out of range, must be greater than zero.");
            this.width = width;
            this.height = height;
        }


        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(
              this.width,
              this.height,
              PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);

            // Fill in the background.
            HatchBrush hatchBrush = new HatchBrush(
              HatchStyle.SmallConfetti,
              Color.LightGray,
              Color.White);
            g.FillRectangle(hatchBrush, rect);

            // Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(
                  FontFamily.GenericSansSerif,
                  fontSize,
                  FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(
              this.text,
              font.FontFamily,
              (int)font.Style,
              font.Size, rect,
              format);
            float v = 4F;
            PointF[] points =
  {
        new PointF(
          this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v)
      };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draw the text.
            hatchBrush = new HatchBrush(
              HatchStyle.LargeConfetti,
              Color.Black,
              Color.Black);
            g.FillPath(hatchBrush, path);

            // Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = this.random.Next(rect.Width);
                int y = this.random.Next(rect.Height);
                int w = this.random.Next(m / 50);
                int h = this.random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }


        private void GenerateImage2()
        {
            Bitmap bitmap = new Bitmap
              (this.width, this.height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;

            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, 75, rect, format);
            float v = 4F;
            PointF[] points =
      {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(
                   rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v,
                    this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v,
                    rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v,
                    rect.Height - this.random.Next(rect.Height) / v)
          };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
            g.FillPath(hatchBrush, path);
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = this.random.Next(rect.Width);
                int y = this.random.Next(rect.Height);
                int w = this.random.Next(m / 50);
                int h = this.random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            this.image = bitmap;
        }


    }
}