using System;
using System.Xml.Serialization;
using System.IO;
using NameParsing;

namespace ShifterEngine {

	class MainClass {
		public static void Main(string[] args) {

			var profile = new Profile("Jonathan Rubin Yaniv", "Microsoft Inc.", "Software Engineer Intern", 85, 15);
			
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//file.xml";
			FileStream file = new FileStream(path, FileMode.Create);
			XmlSerializer writer = new XmlSerializer(typeof(Profile));

			writer.Serialize(file, profile);

			file = new FileStream(path, FileMode.Open);
			var deprofile = writer.Deserialize(file);
			Console.WriteLine(deprofile);
		}
	}
}
