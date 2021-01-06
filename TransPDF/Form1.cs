using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PDFToWord;
using Pechkin;
using Pechkin.Synchronized;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransPDF
{
    public partial class Form1 : Form
    {
        Convertors convert = new Convertors();
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 图片生成PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnimg_Click(object sender, EventArgs e)
        {

            string beforPath = txtimgb.Text;
            string afterPath = txtimga.Text;

            List<string> imgList = null;
            DirectoryInfo folder = new DirectoryInfo(beforPath);
            DirectoryInfo[] dics = folder.GetDirectories();
            string type = "*";
            try
            {
                foreach (var item in dics)
                {
                    imgList = new List<string>();
                    string deletePath = string.Empty;
                    var fileList = item.GetFiles(type).OrderBy(s => Regex.Match(s.Name, @"\d+").Value != string.Empty ? int.Parse(Regex.Match(s.Name, @"\d+").Value) : 0).ToList();
                    if (item.Name.Contains("扫描全能王"))
                    {
                        fileList = item.GetFiles(type).OrderBy(s => Regex.Match(s.Name, @"\d+").Value != string.Empty ? int.Parse(Regex.Match(s.Name.Replace(item.Name, ""), @"\d+").Value) : 0).ToList();
                    }
                    foreach (FileInfo file in fileList)
                    {
                        if (file.Name.ToLower().EndsWith(".png") || file.Name.ToLower().EndsWith(".jpg"))
                        {
                            deletePath = file.Directory.FullName;
                            string editPath = GetImgCutPath(file.FullName, file.Directory.FullName, file.Name);
                            imgList.Add(editPath);
                        }
                    }
                    string fn = afterPath + "\\" + item.Name + ".pdf";
                    ImgTransPDF(imgList, fn);

                    DirectoryInfo editDire = new DirectoryInfo(deletePath + "\\CutImg\\");
                    foreach (FileInfo file in editDire.GetFiles(type))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("图片生成PDF异常,请联系管理员！" + "\r\n" + ex.ToString());
            }
        }
        /// <summary>
        /// word生成PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnword_Click(object sender, EventArgs e)
        {
            string beforPath = txtwordb.Text;
            string afterPath = txtworda.Text;
            DirectoryInfo folder = new DirectoryInfo(beforPath);
            WordToPdfHelper word = new WordToPdfHelper();
            string type = "*";
            foreach (FileInfo file in folder.GetFiles(type))
            {
                string fn = afterPath + "\\" + file.Name + ".pdf";
                word.ToPdf(file.FullName, fn);
            }
        }
        /// <summary>
        /// html生成PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhtml_Click(object sender, EventArgs e)
        {
            string beforPath = txthtmlb.Text;
            string afterPath = txthtmla.Text;
            List<string> htmlList = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(beforPath);

            SynchronizedPechkin sc = new SynchronizedPechkin(new GlobalConfig()
.SetMargins(new Margins() { Left = 10, Right = 10, Top = 10, Bottom = 10 }) //设置边距
.SetPaperOrientation(false) //设置纸张方向为横向
.SetPaperSize(210, 297) //设置纸张大小210mm * 297mm
);
            string type = "*";
            try
            {
                foreach (FileInfo file in folder.GetFiles(type))
                {
                    StreamReader sr = new StreamReader(file.FullName, Encoding.Default);
                    string html = sr.ReadToEnd();
                    html = html.Replace("font-family", "font-familyss");
                    byte[] buf = sc.Convert(new ObjectConfig(), html);
                    string afterName = afterPath + "\\" + file.Name.Split('.')[0] + ".pdf";
                    FileStream fs = new FileStream(afterName, FileMode.Create);
                    fs.Write(buf, 0, buf.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTML生成PDF异常,请联系管理员！" + "\r\n" + ex.ToString());
            }
        }
        public void ImgTransPDF(List<string> imgList, string afterPath)
        {
            List<string> imageList = imgList;
            //生成PDF路径
            string fileName = string.Empty;
            fileName = afterPath;
            //width:794px;height:1090px
            iTextSharp.text.Rectangle page = new iTextSharp.text.Rectangle(0f, 0f);
            Document document = new Document(page, 1f, 1f, 1f, 1f);
            //设置纸张横向
            //document.SetPageSize(page.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            for (int i = 0; i < imageList.Count; i++)
            {
                document.Open();
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageList[i]);
                img.ScaleAbsolute(460f, 700f);

                //图片居中
                img.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                //图片绝对定位
                img.SetAbsolutePosition(55, 30);

                //图片打印到PDF
                writer.DirectContent.AddImage(img);
                document.NewPage();
            }

            document.Close();
        }

        public string GetImgCutPath(string beforPath, string nowPath, string imgName)
        {
            string edimagePath = nowPath + "\\CutImg\\";
            if (!Directory.Exists(edimagePath))
            {
                Directory.CreateDirectory(edimagePath);
            }
            edimagePath += imgName;
            ImgHelper.GetPicThumbnail(beforPath, edimagePath, 50);
            return edimagePath;
        }
    }
}
