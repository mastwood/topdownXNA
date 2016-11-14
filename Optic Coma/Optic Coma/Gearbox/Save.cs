﻿using System;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Optic_Coma
{

    public class SaveFileSerializer
    {
        public SaveFileSerializer()
        {

        }
        public static void DumpException(Exception ex)
        {
            Console.WriteLine("--------- Outer Exception Data ---------");
            WriteExceptionInfo(ex);
            ex = ex.InnerException;
            if (null != ex)
            {
                Console.WriteLine("--------- Inner Exception Data ---------");
                WriteExceptionInfo(ex.InnerException);
                ex = ex.InnerException;
            }
        }
        public static void WriteExceptionInfo(Exception ex)
        {
            Console.WriteLine("Message: {0}", ex.Message);
            Console.WriteLine("Exception Type: {0}", ex.GetType().FullName);
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("StrackTrace: {0}", ex.StackTrace);
            Console.WriteLine("TargetSite: {0}", ex.TargetSite);
        }

        public static void Save(XmlSerializer xml, float[] i)
        {
            try
            {
                using (TextWriter f = new StreamWriter(@"Content\save1.xml"))
                {
                    xml.Serialize(f, i);
                }
            }
            catch(Exception ex)
            {
                DumpException(ex);
            }
        }
        public static float[] Load(XmlSerializer xml)
        {
            var i = new float[3];

            try
            {
                using(var f = new FileStream(@"Content\save1.xml", FileMode.Open))
                    i = (float[])xml.Deserialize(f);

                return i;
            }
            catch (FileNotFoundException ex)
            {
                return null;
            }
        }
    }
   
    public class LogWriter
    {
        public string[] Lines;
        public static string Path = "";
        public DateTime DateTime = DateTime.Now;

        public static void Write(string message, string stackTrace, string errorCode)
        {
            if (!File.Exists(Path))
            {
                // Create a file to write to.
                using (var sw = File.CreateText(Path))
                {
                    sw.WriteLine("ERROR LOG {dateTime} \r \r");

                    sw.Write(message);
                    sw.Write(stackTrace);
                }
            }
        }
    }
}
