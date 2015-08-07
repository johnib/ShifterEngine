using NUnit.Framework;
using System;
using ShifterEngine;


namespace Test {

	[TestFixture()]
	public class ShiftTests {
		[Test()]
		public void TestEquals() {
			var shift1 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));
			var shift2 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 0));
			Assert.AreEqual(shift1, shift2, string.Format("Shift1: {0}\nShift2: {1}", shift1, shift2));
		}

		[Test()]
		public void TestEquals2() {
			var shift1 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));
			var shift2 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 1));
			Assert.AreNotEqual(shift1, shift2, string.Format("Shift1: {0}\nShift2: {1}", shift1, shift2));
		}

		[Test()]
		public void TestCompare() {
			var shift1 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));
			var shift2 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new DateTime(2015, 8, 8, 19, 22, 1));
			Assert.AreEqual(-1, shift1.CompareTo(shift2));
		}

		[Test()]
		public void TestCompareNull() {
			var shift1 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));
			Assert.Throws<NullReferenceException>(delegate {
				shift1.CompareTo(null);
			});
		}

		[Test()]
		public void TestCompareDiffObj() {
			var shift1 = new Shift(new DateTime(2015, 8, 8, 9, 10, 0), new TimeSpan(10, 12, 0));
			Assert.Throws<ArgumentException>(delegate {
				shift1.CompareTo(new object());
			});
		}
	}
}

