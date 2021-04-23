using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;



namespace SJBD.Convert.Services
{
    public class WordToPdfService
    {
        public string WordToPdf(string wordUri)
        {
            return Convert(wordUri);
        }

        string wordPath = null;
        string PDFPath = null;
        public string Convert(string wordUri)
        {
            //try
            //{
                SaveToLocal();


                //word文档路径 = "D:\\Jin\\Desktop\\直接下载到本地的崔家辉写的票.docx";
                PDFPath = System.Environment.CurrentDirectory + "\\Ticket\\ticket.pdf";

                // public enum WdExportFormat
                //{
                //    wdExportFormatPDF = 17,
                //    wdExportFormatXPS = 18
                //}
                WordConvertPDF(WdExportFormat.wdExportFormatPDF);

                //if (resultFlag)
                //{
                //    this.pdfViewer1.LoadDocument(PDF文档路径);
                //}
            //}
            //catch
            //{

            //}
            return PDFPath;
        }
        private void SaveToLocal()
        {

            using (WebClient client = new WebClient())
            {
                string uri = "http://124.163.250.247:8881/static/fileserver/files/ticket/outputs/D2GCSB2103010.docx";

                wordPath = System.Environment.CurrentDirectory + "\\Ticket\\ticket.docx";
                // address：从中下载数据的 URI，www.xxxx.com/xx.jpg
                // filePath：从中下载数据的 URI，C:\\tempFile\\xx.jpg
                client.DownloadFile(uri, wordPath);
            }
        }
        bool resultFlag = false;
        private bool WordConvertPDF(Word.WdExportFormat exportFormat)
        {
            object paramMissing = Type.Missing;
            Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            Word.Document wordDocument = null;
            if (File.Exists(wordPath))
            {
                try
                {
                    object paramSourceDocPath = wordPath;
                    string paramExportFilePath = PDFPath;

                    Word.WdExportFormat paramExportFormat = exportFormat;
                    bool paramOpenAfterExport = false;
                    Word.WdExportOptimizeFor paramExportOptimizeFor =
                            Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                    Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                    int paramStartPage = 0;
                    int paramEndPage = 0;
                    Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                    bool paramIncludeDocProps = true;
                    bool paramKeepIRM = true;
                    Word.WdExportCreateBookmarks paramCreateBookmarks =
                            Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                    bool paramDocStructureTags = true;
                    bool paramBitmapMissingFonts = true;
                    bool paramUseISO19005_1 = false;

                    wordDocument = wordApplication.Documents.Open(
                            ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing);

                    if (wordDocument != null)
                        wordDocument.ExportAsFixedFormat(paramExportFilePath,
                                paramExportFormat, paramOpenAfterExport,
                                paramExportOptimizeFor, paramExportRange, paramStartPage,
                                paramEndPage, paramExportItem, paramIncludeDocProps,
                                paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                                paramBitmapMissingFonts, paramUseISO19005_1,
                                ref paramMissing);
                    resultFlag = true;
                }
                catch (System.Runtime.InteropServices.COMException eee)
                {
                    resultFlag = false;
                    throw (eee);
                    //System.Runtime.InteropServices.COMException (Ox800A1436):很抱歉，找不到您的文件。该项目是否已移动、重命名或删除?
                    //MessageBox.Show(eee.ToString() + " " + eee.ErrorCode.ToString());
                }
                finally
                {
                    if (wordDocument != null)
                    {
                        wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                        wordDocument = null;
                    }
                    if (wordApplication != null)
                    {
                        wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                        wordApplication = null;
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            else
            {
                resultFlag = false;

            }
            return resultFlag;
        }
    }

}