using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransPDF
{
    public class WordTransPDF
    {
        public static void WordToPDF(string sourcePath, string targetPath)
        {
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            Document document = null;
            try
            {
                application.Visible = false;
                document = application.Documents.Open(sourcePath);
                document.ExportAsFixedFormat(targetPath, WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception e)
            {
            }
            finally
            {
                document.Close();
            }
        }
        public void WordToPdfWithWPS(string sourcePath, string targetPath)
        {
            //WPS.ApplicationClass app = new WPS.ApplicationClass();
            //WPS.Document doc = null;
            //try
            //{
            //    doc = app.Documents.Open(sourcePath, true, true, false, null, null, false, "", null, 100, 0, true, true, 0, true);
            //    doc.ExportPdf(targetPath, "", "");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            //finally
            //{
            //    doc.Close();
            //}
            //return true;
        }

        static dynamic wps;

        public static void WordToPdfHelper()
        {
            //创建wps实例，需提前安装wps
            Type type = Type.GetTypeFromProgID("KWps.Application");
            wps = Activator.CreateInstance(type);
        }

        /// <summary>
        /// Word转PDF
        /// </summary>
        /// <param name="wordPath">Wps文件路径</param>
        /// <param name="pdfPath">Pdf文件路径</param>
        /// <returns></returns>
        public void ToPdf(string wordPath, string pdfPath = null)
        {
            try
            {
                if (wordPath == null)
                {
                    throw new ArgumentNullException("wpsFilename");
                }
                if (pdfPath == null)
                {
                    pdfPath = Path.ChangeExtension(wordPath, "pdf");
                }

                //用wps 打开word不显示界面
                dynamic doc = wps.Documents.Open(wordPath, Visible: false);
                //doc 转pdf 
                doc.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF);
                //设置隐藏菜单栏和工具栏
                //wps.setViewerPreferences(PdfWriter.HideMenubar | PdfWriter.HideToolbar);
                doc.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void Dispose()
        {
            if (wps != null) { wps.Quit(); }
        }

        /// <summary>
        /// Word转换成PDF(单个文件转换推荐使用)
        /// </summary>
        /// <param name="inputPath">载入完整路径</param>
        /// <param name="outputPath">保存完整路径</param>
        /// <param name="startPage">初始页码（默认为第一页[0]）</param>
        /// <param name="endPage">结束页码（默认为最后一页）</param>
        public static bool WordToPDF(string inputPath, string outputPath, int startPage = 0, int endPage = 0)
        {
            bool b = true;

            #region 初始化
            //初始化一个application
            Application wordApplication = new Application();
            //初始化一个document
            Document wordDocument = null;
            #endregion

            #region 参数设置~~我去累死宝宝了~~（所谓的参数都是根据这个方法来的:ExportAsFixedFormat）
            //word路径
            object wordPath = Path.GetFullPath(inputPath);

            //输出路径
            string pdfPath = Path.GetFullPath(outputPath);

            //导出格式为PDF
            WdExportFormat wdExportFormat = WdExportFormat.wdExportFormatPDF;

            //导出大文件
            WdExportOptimizeFor wdExportOptimizeFor = WdExportOptimizeFor.wdExportOptimizeForPrint;

            //导出整个文档
            WdExportRange wdExportRange = WdExportRange.wdExportAllDocument;

            //开始页码
            int startIndex = startPage;

            //结束页码
            int endIndex = endPage;

            //导出不带标记的文档（这个可以改）
            WdExportItem wdExportItem = WdExportItem.wdExportDocumentContent;

            //包含word属性
            bool includeDocProps = true;

            //导出书签
            WdExportCreateBookmarks paramCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks;

            //默认值
            object paramMissing = Type.Missing;

            #endregion

            #region 转换
            try
            {
                //打开word
                wordDocument = wordApplication.Documents.Open(ref wordPath, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing, ref paramMissing);
                //转换成指定格式
                if (wordDocument != null)
                {
                    wordDocument.ExportAsFixedFormat(pdfPath, wdExportFormat, false, wdExportOptimizeFor, wdExportRange, startIndex, endIndex, wdExportItem, includeDocProps, true, paramCreateBookmarks, true, true, false, ref paramMissing);
                }
            }
            catch (Exception ex)
            {
                b = false;
            }
            finally
            {
                //关闭
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }

                //退出
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
            }

            return b;
            #endregion
        }
    }

    public class WordToPdfHelper : IDisposable
    {
        dynamic wps;

        public WordToPdfHelper()
        {
            //创建wps实例，需提前安装wps
            Type type = Type.GetTypeFromProgID("KWps.Application");
            wps = Activator.CreateInstance(type);
        }

        /// <summary>
        /// Word转PDF
        /// </summary>
        /// <param name="wordPath">Wps文件路径</param>
        /// <param name="pdfPath">Pdf文件路径</param>
        /// <returns></returns>
        public void ToPdf(string wordPath, string pdfPath = null)
        {
            try
            {
                if (wordPath == null)
                {
                    throw new ArgumentNullException("wpsFilename");
                }
                if (pdfPath == null)
                {
                    pdfPath = Path.ChangeExtension(wordPath, "pdf");
                }
                //用wps 打开word不显示界面
                dynamic doc = wps.Documents.Open(wordPath, Visible: false);
                //doc 转pdf 
                doc.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF);
                //设置隐藏菜单栏和工具栏
                //wps.setViewerPreferences(PdfWriter.HideMenubar | PdfWriter.HideToolbar);
                doc.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void Dispose()
        {
            if (wps != null) { wps.Quit(); }
        }
    }
}
