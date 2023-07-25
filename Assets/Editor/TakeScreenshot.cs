using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Farm.Editor
{
    public static class Screenshot
    {
        [MenuItem("Project Tools/Take Screenshot")]
        public static void TakeScreenshot()
        {
            var path = Path.Combine(Application.dataPath, "Screenshots");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dateTimeNow = DateTime.Now.ToString("MM-dd-yyyy (HH-mm-ss)");
            ScreenCapture.CaptureScreenshot(Path.Combine(path, $"Screenshot_{dateTimeNow}.png"), 2);
            AssetDatabase.Refresh();

            Debug.Log("Screenshot captured!");
        }
    }
}