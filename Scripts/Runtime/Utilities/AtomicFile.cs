// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.IO;

namespace Bodix.Evolunity.Utilities
{
	/// <summary>
	/// Writes a file so that a crash or a killed app never leaves it half-written.
	/// The data goes to a temporary file first, then replaces the old file. The old file is kept as a backup.
	/// </summary>
	public static class AtomicFile
	{
		private const string TemporarySuffix = ".tmp";
		private const string BackupSuffix = ".bak";

		/// <summary>
		/// The previous version of the file. Read it when the main file is missing or broken.
		/// </summary>
		public static string GetBackupPath(string path)
		{
			return path + BackupSuffix;
		}

		public static void WriteAllText(string path, string contents)
		{
			Write(path, temporaryPath => File.WriteAllText(temporaryPath, contents));
		}

		/// <summary>
		/// For writers that need a path, like a serializer: <paramref name="write"/> gets a temporary path to write to.
		/// </summary>
		public static void Write(string path, Action<string> write)
		{
			string directory = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			string temporaryPath = path + TemporarySuffix;
			write(temporaryPath);

			// File.Replace is not available on every platform, so the swap is done with moves.
			// If the app dies between the two moves, the backup still holds the previous version.
			if (File.Exists(path))
			{
				string backupPath = GetBackupPath(path);

				if (File.Exists(backupPath))
					File.Delete(backupPath);

				File.Move(path, backupPath);
			}

			File.Move(temporaryPath, path);
		}
	}
}
