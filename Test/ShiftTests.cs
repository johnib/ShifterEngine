using NUnit.Framework;
using System;
using System.IO;
using ShifterEngine;
using System.Xml.Serialization;


namespace Test {

	[TestFixture()]
	public class ShiftTests {

		#region Instances

		Shift mainShift = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));

		#endregion

		#region Test Equals

		[Test()]
		public void TestEquals() {
			var otherShift = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 0));
			Assert.AreEqual(mainShift, otherShift, string.Format("Shift1: {0}\nShift2: {1}", mainShift, otherShift));
		}

		[Test()]
		public void TestEquals2() {
			var otherShift = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 1));
			Assert.AreNotEqual(mainShift, otherShift, string.Format("Shift1: {0}\nShift2: {1}", mainShift, otherShift));
		}

		#endregion

		#region Test CompareTo

		[Test()]
		public void TestCompareTo() {
			var otherShift = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 1));
			Assert.AreEqual(-1, mainShift.CompareTo(otherShift));
		}

		[Test()]
		public void TestCompareToNull() {
			Assert.Throws<NullReferenceException>(delegate {
				mainShift.CompareTo(null);
			});
		}

		[Test()]
		public void TestCompareToDiffObj() {
			Assert.Throws<ArgumentException>(delegate {
				mainShift.CompareTo(new object());
			});
		}

		#endregion

		#region Test Serialization

		[Test()]
		public void TestSerializationWriteReadEquality() {
			XmlSerializer serializer = new XmlSerializer(typeof(Shift));
			var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"/test_shift.xml";
			Stream writer = File.Open(path, FileMode.Create);

			serializer.Serialize(writer, mainShift);
			writer.Dispose();

			Stream reader = File.Open(path, FileMode.Open);
			var deserializedShift = (Shift)serializer.Deserialize(reader);

			Assert.AreEqual(mainShift, deserializedShift, "Deserialized shift is not equal to the serialized main shift");
		}

		#endregion
	}
}

