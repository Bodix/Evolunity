// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;

namespace Evolutex.Evolunity.Utilities
{
    public static class PathUtility
    {
        public static string ChangeFileName(string path, string fileName)
        {
            return path.Replace(Path.GetFileName(path), fileName);
        }

        public static string ChangeFileNameWithoutExtension(string path, string fileNameWithoutExtension)
        {
            return path.Replace(Path.GetFileNameWithoutExtension(path), fileNameWithoutExtension);
        }

        public static string FixSlashes(string path, SlashStyle style = SlashStyle.AutoByOS)
        {
            switch (style)
            {
                case SlashStyle.ForceSlash:
                    return path.Replace("\\", "/");
                case SlashStyle.ForceBackslash:
                    return path.Replace("/", "\\");
                case SlashStyle.AutoByOS:
                default:
                    char correctSlash = Path.DirectorySeparatorChar;
                    char wrongSlash = correctSlash == '\\' ? '/' : '\\';
                    return path.Replace(wrongSlash, correctSlash);
            }
        }

        public enum SlashStyle
        {
            AutoByOS,
            ForceSlash,
            ForceBackslash
        }
    }
}