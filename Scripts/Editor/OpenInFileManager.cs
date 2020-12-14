/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Evolutex.Evolunity.Editor
{
    public static class OpenInFileManager
    {
        private static bool IsMacOS => SystemInfo.operatingSystem.Contains("Mac OS");
        private static bool IsWinOS => SystemInfo.operatingSystem.Contains("Windows");

        public static void Open(string path)
        {
            if (IsWinOS)
                OpenInWin(path);
            else if (IsMacOS)
                OpenInMac(path);
            else
                Debug.LogWarning($"[{nameof(OpenInFileManager)}] OS is not supported");
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