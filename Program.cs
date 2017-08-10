using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fileRandom
{
	class FileRandomier
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("USAGE: " + System.AppDomain.CurrentDomain.FriendlyName + " orig_folder new_folder");
				Environment.Exit(1);
			}

			if (args[1] == args[0])
			{
				Console.WriteLine("Original and new folder must not be the same");
				Environment.Exit(1);
			}

			var fr = new FileRandomier(args[0], args[1]);

			fr.Start();
		}

		public string OriginalFolder { get; set; }
		public string NewFolder { get; set; }

		public FileRandomier(string origFoler, string newFolder)
		{
			OriginalFolder = origFoler;
			NewFolder = newFolder;
		}

		public void Start()
		{
			var filesInOrigFolder = new List<string>(Directory.EnumerateFiles(OriginalFolder));
			var fileCount = filesInOrigFolder.Count();
			
			Random rnd = new Random();

			for (int i = 0; i < fileCount; i++)
			{
				int randomFileIndex = rnd.Next(0, fileCount - i);
				string filePath = filesInOrigFolder[randomFileIndex];
				filesInOrigFolder.RemoveAt(randomFileIndex);
				
				string fileName = getNameFromPath(filePath);
				string newFilePath = $"{NewFolder}\\{i}_{fileName}";

				File.Copy(filePath, newFilePath);

				Console.WriteLine($"{ filePath } => { newFilePath }");
			}
		}

		private string getNameFromPath(string path)
		{
			var splitted = path.Split(Path.DirectorySeparatorChar);
			return splitted.Last();
		}
	}
}
