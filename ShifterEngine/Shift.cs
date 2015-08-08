﻿using System;
using System.Runtime.Serialization;


namespace ShifterEngine {

	[Serializable()]
	public class Shift : IComparable, ISerializable {

		#region Properties

		/// <summary>
		/// The beginning time of this Shift.
		/// </summary>
		public DateTime Start { get; private set; }

		/// <summary>
		/// The finishing time of this Shift.
		/// </summary>
		public DateTime End { get; private set; }

		/// <summary>
		/// The duration of this Shift.
		/// </summary>
		public TimeSpan Duration { get { return this.End - this.Start; } set { } }

		/// <summary>
		/// The amount of hours passed during this Shift.
		/// </summary>
		public double Hours { get { return this.Duration.TotalHours; } set { } }

		/// <summary>
		/// The amount of minutes passed during this Shift.
		/// </summary>
		public double Minutes { get { return this.Duration.TotalMinutes; } set { } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Shift"/> class.
		/// A default Shift is a shift that starts Now, and lasts 1 hour.
		/// </summary>
		public Shift() {
			this.Start = DateTime.Now;
			this.End = this.Start.AddHours(1);
		}

		/// <summary>
		/// Creates a new Shift instance according to the given Start and End times.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="duration"></param>
		public Shift(DateTime start, DateTime end) {
			this.Start = start;
			this.End = end;
		}

		/// <summary>
		/// Creates a new Shift instance according to the given Start and Duration times.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="duration"></param>
		public Shift(DateTime start, TimeSpan duration) : this(start, start + duration) {
		}

		/// <summary>
		/// Creates a new Shift instance according to the Duration and End times.
		/// </summary>
		/// <param name="duration"></param>
		/// <param name="end"></param>
		public Shift(TimeSpan duration, DateTime end) : this(end - duration, end) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Shift"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="context">Context.</param>
		public Shift(SerializationInfo info, StreamingContext context) : this(info.GetDateTime("start"), info.GetDateTime("end")) {
		}

		#endregion

		#region ISerializable implementation

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("start", this.Start);
			info.AddValue("end", this.End);
			info.AddValue("duration", this.Duration.ToString());
			info.AddValue("hours", this.Hours);
			info.AddValue("minutes", this.Minutes);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Creates a textual representation of this Shift.
		/// </summary>
		/// <returns>A string representation of the Shift's details.</returns>
		public override string ToString() {
			return string.Format("Took place at:\t{0}\nUntil:\t{1}\nDuration:\t{2}\n", this.Start, this.End, this.Duration);
		}

		/// <summary>
		/// Two Shifts are equal iff Start and End times are equal.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>True if the given Shift equals this Shift, false otherwise.</returns>
		public override bool Equals(object obj) {
			return this.CompareTo(obj) == 0;
		}

		/// <summary>
		/// Default GetHashCode inherited from Object class.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return base.GetHashCode();
		}

		#endregion

		#region IComparable implementation

		public int CompareTo(object obj) {
			if (obj == null) {
				throw new NullReferenceException("obj is null");
			}

			if (!(obj is Shift)) {
				throw new ArgumentException("Invalid argument", "Parameter obj is of type:" + obj.GetType());
			}

			Shift other = obj as Shift;
			int result = 0;
			if (this.Start < other.Start) {
				result = -1;
			} else if (this.Start > other.Start) {
				result = 1;
			} else if (this.End == other.End) {
				result = 0;
			} else {
				result = this.End <= other.End ? -1 : 1;
			}

			return result;
		}

		#endregion
	}
}
