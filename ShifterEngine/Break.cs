using System;


namespace ShifterEngine {

	/// <summary>
	/// This class represents a break time.
	/// It inherits from Shift because break time is defined by the same paramters as a Shift.
	/// </summary>
	[Serializable()]
	public class Break : Shift {

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Break"/> class.
		/// Note: default constructor initializes a break with duration 0 seconds.
		/// </summary>
		public Break() : base(DateTime.Now, DateTime.Now) {
		}



		#endregion
	}
}

