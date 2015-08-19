using System;
using System.Collections;
using System.Collections.Generic;
using money;
using C5;
using System.Linq;


namespace ShifterEngine {

	/// <summary>
	/// Shift manager handles mutliple shifts, export logs and calculates data such as expected salary etc.
	/// </summary>
	public class ShiftManager {

		#region Properties

		/// <summary>
		/// The profile that the calculations will be made according to.
		/// </summary>
		public Profile Profile { get; set; }

		/// <summary>
		/// Gets or sets the shifts.
		/// </summary>
		/// <value>The shifts.</value>
		private ArrayList<Shift> Shifts { get; set; }

		/// <summary>
		/// Gets the Shifts count.
		/// </summary>
		/// <value>The count.</value>
		public int ShiftCount { get { return this.Shifts.Count; } set { } }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.ShiftManager"/> class.
		/// </summary>
		/// <param name="profile">Profile.</param>
		public ShiftManager(Profile profile) {
			this.Shifts = new ArrayList<Shift>();
			this.Profile = profile;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Adds the shift.
		/// </summary>
		/// <param name="shift">Shift.</param>
		public void AddShift(Shift shift) {
			this.Shifts.Add(shift);
		}

		/// <summary>
		/// Edits the shift.
		/// </summary>
		/// <param name="shift">Shift.</param>
		public void EditShift(Shift shift) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes the shift.
		/// </summary>
		/// <param name="shift">Shift.</param>
		public void RemoveShift(Shift shift) {
			if (!this.Shifts.Remove(shift)) {
				throw new KeyNotFoundException("The Shift does not exist");
			}
		}

		/// <summary>
		/// Gets the shifts on date.
		/// </summary>
		/// <returns>The shifts on date.</returns>
		/// <param name="date">Date.</param>
		public IEnumerable<Shift> GetShiftsOnDate(DateTime date) {
			return this.GetShiftsBetweenDates(date, date);
		}

		/// <summary>
		/// Gets the shifts between dates.
		/// </summary>
		/// <returns>The shifts between dates.</returns>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		public IEnumerable<Shift> GetShiftsBetweenDates(DateTime start, DateTime end) {
			var theShifts = from shift in this.Shifts
			                where shift.Start >= start && shift.End <= end
			                select shift;

			return theShifts;
		}

		/// <summary>
		/// Gets the salary on date.
		/// </summary>
		/// <returns>The salary on date.</returns>
		/// <param name="date">Date.</param>
		public Money GetSalaryOnDate(DateTime date) {
			return this.GetSalaryBetweenDates(date, date);
		}

		/// <summary>
		/// Gets the salary between dates.
		/// </summary>
		/// <returns>The salary between dates.</returns>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		public Money GetSalaryBetweenDates(DateTime start, DateTime end) {
			throw new NotImplementedException();
		}

		#endregion

		#region Static methods

		public static Money CalcSalaryOf(IEnumerable<Shift> shifts, Profile profile) {
			throw new NotImplementedException();
		}

		#endregion
	}
}

