// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Evolutex.Evolunity.Editor.Utilities
{
    public static class FileExplorer
    {
        // TODO:
        // 1. Make the opening abort if the folder does not exist, with error logging.
        // 2. Change "openInsideFolder" logic to something more obvious (for example, add logs) or remove completely.
        
        private static bool IsMacOS => SystemInfo.operatingSystem.Contains("Mac OS");
        private static bool IsWinOS => SystemInfo.operatingSystem.Contains("Windows");

        public static void Open(string path)
        {
            if (IsWinOS)
                OpenInWin(path);
            else if (IsMacOS)
                OpenInMac(path);
            else
                Debug.LogWarning($"[{nameof(FileExplorer)}] OS is not supported");
        }

        private static void OpenInMac(string path)
        {
            bool openInsideFolder = false;
            string macPath = path.Replace("\\", "/");

            if (Directory.Exists(macPath))
                openInsideFolder = true;

            if (!macPath.StartsWith("\""))
                macPath = "\"" + macPath;

            if (!macPath.EndsWith("\""))
                macPath += "\"";

            string arguments = (openInsideFolder ? "" : "-R ") + macPath;

            Process.Start("open", arguments);
        }

        private static void OpenInWin(string path)
        {
            bool openInsideFolder = false;
            string winPath = path.Replace("/", "\\");

            if (Directory.Exists(winPath))
                openInsideFolder = true;

            Process.Start("explorer.exe", (openInsideFolder ? "/root," : "/select,") + winPath);
        }
    }
}