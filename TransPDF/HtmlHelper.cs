using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TransPDF
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlToPicHelper
    {
        private static Bitmap ConverHTML(string htmPath)
        {
            string ImagePath = string.Empty;
            WebBrowser web = new WebBrowser();
            web.Navigate(htmPath);
            while (web.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            Size? imgsize = null;
            //set the webbrowser width and hight
            web.Width = screen.Width;
            web.Height = screen.Height;
            //suppress script errors and hide scroll bars
            web.ScriptErrorsSuppressed = true;
            web.ScrollBarsEnabled = false;
            Rectangle body = web.Document.Body.ScrollRectangle;

            //check if the document width/height is greater than screen width/height
            Rectangle docRectangle = new Rectangle()
            {
                Location = new Point(0, 0),
                Size = new Size(body.Width > screen.Width ? body.Width : screen.Width,
                 body.Height > screen.Height ? body.Height : screen.Height)
            };
            //set the width and height of the WebBrowser object
            web.Width = docRectangle.Width;
            web.Height = docRectangle.Height;

            //if the imgsize is null, the size of the image will
            //be the same as the size of webbrowser object
            //otherwise  set the image size to imgsize
            Rectangle imgRectangle;
            if (imgsize == null)
                imgRectangle = docRectangle;
            else
                imgRectangle = new Rectangle()
                {
                    Location = new Point(0, 0),
                    Size = imgsize.Value
                };
            //create a bitmap object
            Bitmap bitmap = new Bitmap(imgRectangle.Width, imgRectangle.Height);
            //get the viewobject of the WebBrowser
            IViewObject ivo = web.Document.DomDocument as IViewObject;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Graphics.SmoothingMode属性: 例如SmoothingMode.HighQuality可以产生高质量图片,但是效率低.
                //Graphics.CompositingQuality 属性: 例如: CompositingQuality.HighQuality也是产生高质量图,效率低下.
                //Graphics.InterpolationMode 属性,例如: InterpolationMode.HighQualityBicubic与前两个也是同样的效果.
                //g.SmoothingMode = SmoothingMode.HighQuality;
                //g.CompositingQuality = CompositingQuality.HighQuality;
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                ////去除锯齿
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                //get the handle to the device context and draw
                IntPtr hdc = g.GetHdc();
                ivo.Draw(1, -1, IntPtr.Zero, IntPtr.Zero,
                         IntPtr.Zero, hdc, ref imgRectangle,
                         ref docRectangle, IntPtr.Zero, 0);
                g.ReleaseHdc(hdc);
            }
            return bitmap;
        }

        public static byte[] ConverPic(string htmlPath)
        {
            //必须放到独立的线程里面来处理
            Bitmap m_Bitmap = null;
            Thread m_thread = new Thread(new ThreadStart(() =>
            {
                m_Bitmap = ConverHTML(htmlPath);
            }));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();

            if (m_Bitmap != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                m_Bitmap.Save(ms, ImageFormat.Jpeg);
                byte[] bs = ms.GetBuffer();
                ms.Close();
                return bs;
            }
            return new byte[0];
        }


        public static void SaveHTMLPDF(string html, string afterPath)
        {
            ////避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签
            //string htmlText = string.Empty;
            ////if (string.IsNullOrEmpty(html))
            ////{
            ////}
            ////else
            ////{
            ////    htmlText = html;
            ////}
            //htmlText = html;
            //htmlText.Replace("<br>", "<br/>");
            //MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流
            //byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]
            //MemoryStream msInput = new MemoryStream(data);
            //Document doc = new Document();//要写PDF的文件，建构子没填的话预设直式A4
            //PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            ////指定文件预设开档时的缩放为100%

            //PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            ////开启Document文件 
            //doc.Open();

            ////使用XMLWorkerHelper把Html parse到PDF档里
            //// XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

            ////将pdfDest设定的资料写到PDF档
            //PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            //writer.SetOpenAction(action);
            //doc.Close();
            //msInput.Close();
            //outputStream.Close();
            ////回传PDF档案 
            //var bytes = outputStream.ToArray();
            //var ret = Convert.ToBase64String(bytes);
            //string strbase64 = ret;
            //strbase64 = strbase64.Replace(' ', '+');
            //System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(strbase64));
            //System.IO.FileStream fs = new System.IO.FileStream(afterPath, FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            //byte[] b = stream.ToArray();
            ////byte[] b = stream.GetBuffer();
            //fs.Write(b, 0, b.Length);
            //fs.Close();
        }
        /// <summary>
        /// 图片切割
        /// </summary>
        public static void Slice(string imgfile, string imgName)
        {
            // 图片路径
            var file = imgfile + "\\" + imgName;
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            // 水平切分
            int height = Image.FromFile(file, true).Height;
            int total = Convert.ToInt32(height / screen.Height) + 1;
            if (!Directory.Exists(imgfile + "\\ImgCut\\"))
                Directory.CreateDirectory(imgfile + "\\ImgCut\\");
            for (int i = 0; i < total; i++)
            {
                using (Image image = Image.FromFile(file, true))
                {
                    // 每一块的高
                    int s_h = 978;
                    // 每一块的宽
                    //int s_w = 1000;
                    int s_w = image.Width;
                    // 整除判断自己加吧,懒得写了

                    /*
                     0,0 ; 1,0 ; 2,0 ; 3,0
                     0,1 ; 1,1 ; 2,1 ; 3,1
                     0,2 ; 1,2 ; 2,2 ; 3,2
                     0,3 ; 1,3 ; 2,3 ; 3,3
                     */
                    // 左上角为0,0
                    var rect = new Rectangle();
                    rect.X = 0;
                    rect.Y = i * 978;
                    if (i == total - 1)
                    {
                        rect.Y = height - i * 978;
                    }
                    rect.Width = s_w;
                    rect.Height = s_h;
                    CutForCustom(image, imgfile + "\\ImgCut\\" + imgName.Replace(".jpg", "") + i + ".jpg", rect);
                }
            }
        }

        /// <summary>
        /// 切图
        /// </summary>
        /// <param name="image">图源</param>
        /// <param name="fileSaveUrl">要保存的文件名</param>
        /// <param name="rect">位置大小</param>
        public static void CutForCustom(Image image, string fileSaveUrl, Rectangle rect)
        {
            try
            {

                var templateImage = new Bitmap(image);
                var bitCrop = templateImage.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitCrop.Save(fileSaveUrl);
                bitCrop.Dispose();
                templateImage.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }


    [ComVisible(true), ComImport()]
    [GuidAttribute("0000010d-0000-0000-C000-000000000046")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IViewObject
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Draw(
            [MarshalAs(UnmanagedType.U4)] UInt32 dwDrawAspect,
            int lindex,
            IntPtr pvAspect,
            [In] IntPtr ptd,
            IntPtr hdcTargetDev,
            IntPtr hdcDraw,
            [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcBounds,
            [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcWBounds,
            IntPtr pfnContinue,
            [MarshalAs(UnmanagedType.U4)] UInt32 dwContinue);
        [PreserveSig]
        int GetColorSet([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
           int lindex, IntPtr pvAspect, [In] IntPtr ptd,
            IntPtr hicTargetDev, [Out] IntPtr ppColorSet);
        [PreserveSig]
        int Freeze([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
                        int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);
        [PreserveSig]
        int Unfreeze([In, MarshalAs(UnmanagedType.U4)] int dwFreeze);
        void SetAdvise([In, MarshalAs(UnmanagedType.U4)] int aspects,
          [In, MarshalAs(UnmanagedType.U4)] int advf,
          [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);
        void GetAdvise([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] paspects,
          [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] advf,
          [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);
    }
}
