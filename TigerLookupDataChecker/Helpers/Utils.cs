using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TigerLookupDataChecker.Models;

namespace TigerLookupDataChecker.Helpers
{
    public class Utils
    {
        public static void SerializeAndSaveToFile<T>(string fileName, T param, bool openTargetDirectory)
        {
            StreamReader sr = null;
            try
            {
                var directory = Models.Constants.OutputsFolder;
                var filePath = directory + @"\" + fileName;

                var xns = new XmlSerializerNamespaces();



                var serializer = new XmlSerializer(param.GetType());
                xns.Add(string.Empty, string.Empty);

                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    Encoding = System.Text.Encoding.UTF8
                };

                var fileContent = "";
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
                    {
                        var x = new System.Xml.Serialization.XmlSerializer(param.GetType());
                        x.Serialize(xmlWriter, param, xns);

                        // we just output back to the console for this demo.
                        memoryStream.Position = 0; // rewind the stream before reading back.
                        sr = new StreamReader(memoryStream);
                        fileContent = sr.ReadToEnd();
                        System.IO.File.WriteAllText(filePath, fileContent);
                    }
                }

                if (openTargetDirectory)
                    Process.Start(directory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }
        }

        public static T DeSerialize<T>(string fileName) where T : class
        {
            T type;
            var serializer = new XmlSerializer(typeof(T));
            using (StreamReader stream = new StreamReader(Constants.InputFolder + @"\" + fileName,
                                    System.Text.Encoding.GetEncoding(Constants.FileEncoding)))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    type = serializer.Deserialize(reader) as T;
                }
            }
            return type;
        }

        public static bool ConvertXlsToCsv(string inputFileFullPath, string outputFileFullPath)
        {
            Console.WriteLine(inputFileFullPath + "  => " + outputFileFullPath);
            if (!File.Exists(inputFileFullPath))
            {
                throw new System.IO.FileNotFoundException("File specified in command line not found", inputFileFullPath);
            }

            Type officeType = Type.GetTypeFromProgID("Excel.Application");

            if (officeType == null)
            {
                Console.WriteLine("Excel is not installed");
                throw new ApplicationException("Excel is not installed");
            }
            else
            {
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;
                var app = new Microsoft.Office.Interop.Excel.Application() { DisplayAlerts = false };
                try
                {
                    excelWorkbook = app.Workbooks.Open(inputFileFullPath);
                    excelWorkbook.WebOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
                    excelWorkbook.SaveAs(outputFileFullPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                    excelWorkbook.Close();
                    Console.WriteLine(inputFileFullPath + " OK.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    app.Quit();


                    Marshal.ReleaseComObject(excelWorkbook);
                    Marshal.ReleaseComObject(app);


                    Marshal.FinalReleaseComObject(excelWorkbook);
                    Marshal.FinalReleaseComObject(app);

                    excelWorkbook = null;
                    app = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            return true;
        }
    }
}
