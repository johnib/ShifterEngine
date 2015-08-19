using System;
using NameParsing;

namespace Test {

	public class Main {
		public Main() {
			var fullname = "Jonathan Rubin Yaniv".ParseName();

			Console.WriteLine(fullname);
		}
	}
}

