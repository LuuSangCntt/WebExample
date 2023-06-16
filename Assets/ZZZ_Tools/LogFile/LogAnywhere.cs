using System;
using System.IO;
using UnityEngine;

namespace S_Tools
{
    public class LogAnywhere : MonoBehaviour
    {
        public string Filename = "Log";
        private string path;
        public bool Enable = true;

        void Awake()
        {
            if (Enable == false) return;
            try
            {
                var now = DateTime.Now;
                Filename = Filename + "_" + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".txt";
                //string directory = Path.Combine(Application.persistentDataPath, "LogFiles");
                string directory = Path.Combine(Application.streamingAssetsPath, "LogFiles");
                Directory.CreateDirectory(directory);
                path = Path.Combine(directory, Filename);
                System.IO.File.AppendAllText(path, "\n" + "========================================================" + DateTime.Now.ToString() + "\n");
            }
            catch (Exception e)
            { Debug.LogException(e); }

            Application.logMessageReceived += Log;
        }
        void OnDisable()
        {
            if (Enable == false) return;
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            try
            {
                System.IO.File.AppendAllText(path, type + "  " + DateTime.Now.ToString() + "\n");
                System.IO.File.AppendAllText(path, logString + "\n");
                if (type == LogType.Error)
                {
                    System.IO.File.AppendAllText(path, stackTrace + "\n");
                }
            }
            catch { }
        }
    }
}