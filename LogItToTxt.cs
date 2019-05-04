using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HCD.Logger.Txt
{
    public enum AlertType
    {
        INFO = 0,
        WARN = 1,
        ERROR = 2,
        FATAL = 3,
    }


    public class LogItToTxt
    {
       
        public static void Add(string logText, AlertType alertType, string specialMark = "")
        {
            if (string.IsNullOrEmpty(specialMark) == false)
                logText = string.Format($"{specialMark} => {logText}");

            string logLineText = string.Format($"{alertType} \t {DateTime.Now} \t {logText}");

            WriteToTxtFile(logLineText);
        }

        private static void WriteToTxtFile(string logLineText)
        {
            string basePath =  CreateDirectoryIfNotExist();
            string filePath = CreateFileIfNotExist(basePath);


            string contentOfTxtFile = File.ReadAllText(filePath);
            contentOfTxtFile = string.Format($"{logLineText}{Environment.NewLine}{contentOfTxtFile}");

            try
            {
                File.WriteAllText(filePath, contentOfTxtFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static string CreateFileIfNotExist(string basePath)
        {
            string filePath = Path.Combine(basePath, 
                string.Format($"{DateTime.Today.ToString("dd/MM/yyyy")}.txt"));

            try
            {
                if (File.Exists(filePath) == false)
                {
                    File.Create(filePath).Close();
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filePath;

        }

        private static string CreateDirectoryIfNotExist()
        {
            string basePath = string.Format($"D:\\LogIt\\{AppDomain.CurrentDomain.FriendlyName}");
            try
            {
                Directory.CreateDirectory(basePath);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return basePath;
        }
    }

    
}
