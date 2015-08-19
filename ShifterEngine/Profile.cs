using System;
using System.Collections.Generic;
using money;
using NameParsing;
using System.Runtime.Serialization;
using System.Text;


namespace ShifterEngine {

	[Serializable]
	public class Profile : IComparable {

		#region Messages

		const string InvalidDay = "Month's end day must be between 1 and 28.";
		const string InvalidPercentage = "Overtime percentage cannot be lower than 100.";

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value>The full name.</value>
		public NameParts FullName { get; set; }

		/// <summary>
		/// Gets or sets the company name.
		/// </summary>
		/// <value>The company.</value>
		public NameParts CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>The role.</value>
		public NameParts RoleName { get; set; }

		/// <summary>
		/// Gets or sets the hour wage.
		/// </summary>
		/// <value>The hour wage.</value>
		public Money HourWage { get; set; }

		/// <summary>
		/// Gets or sets the month end date.
		/// </summary>
		/// <value>The month end date.</value>
		public int MonthEndDay { get; set; }

		/// <summary>
		/// Gets or sets the over time1 (in percentage).
		/// </summary>
		/// <value>The over time1.</value>
		public double OverTime1 { get; set; }

		/// <summary>
		/// Gets or sets the over time2 (in percentage).
		/// </summary>
		/// <value>The over time2.</value>
		public double OverTime2 { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		private Profile() {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		/// <param name="fullName">Full name.</param>
		/// <param name="companyName">Company name.</param>
		/// <param name="roleName">Role name.</param>
		/// <param name="hourWage">Hour wage.</param>
		/// <param name="monthEndDay">Month end day.</param>
		/// <param name="overtime1">Overtime1.</param>
		/// <param name="overtime2">Overtime2.</param>
		public Profile(string fullName, string companyName, string roleName,
		               double hourWage, int monthEndDay, double overtime1,
		               double overtime2) {

			if (monthEndDay < 1 || monthEndDay > 28) {
				throw new ArgumentOutOfRangeException(InvalidDay);
			}

			if (overtime1 < 100 || overtime2 < 100) {
				throw new ArgumentOutOfRangeException(InvalidPercentage);
			}
			
			this.FullName = fullName.ParseName();
			this.CompanyName = companyName.ParseName();
			this.RoleName = roleName.ParseName();
			this.HourWage = new Money(hourWage);
			this.MonthEndDay = monthEndDay;
			this.OverTime1 = overtime1;
			this.OverTime2 = overtime2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		/// <param name="hourWage">Hour wage.</param>
		/// <param name="monthEndDay">Month end day.</param>
		public Profile(double hourWage, int monthEndDay) :
			this(null, null, null, hourWage, monthEndDay, 100, 100) {
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		/// <param name="fullName">Full name.</param>
		/// <param name="companyName">Company name.</param>
		/// <param name="roleName">Role name.</param>
		/// <param name="hourWage">Hour wage.</param>
		/// <param name="monthEndDay">Month end day.</param>
		public Profile(string fullName, string companyName, string roleName, 
		               double hourWage, int monthEndDay) :
			this(fullName, companyName, roleName, hourWage, monthEndDay, 100, 100) {
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		/// <param name="fullName">Full name.</param>
		/// <param name="hourWage">Hour wage.</param>
		/// <param name="monthEndDay">Month end day.</param>
		public Profile(string fullName, double hourWage, int monthEndDay) :
			this(fullName, null, null, hourWage, monthEndDay, 100, 100) {
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShifterEngine.Profile"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="context">Context.</param>
		public Profile(SerializationInfo info, StreamingContext context) :
			this(info.GetString("fullName"), info.GetString("companyName"), info.GetString("roleName"),
			     info.GetDouble("hourWage"), info.GetInt32("monthEndDate"), info.GetDouble("overtime1"),
			     info.GetDouble("overtime2")) {
			
		}

		#endregion

		#region ISerializable implementation

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			// TODO: finish implement, investigate [Serializable] attribute. 
			// Maybe I don't have to override the serialization procedure.
			info.AddValue("fullName", this.FullName.GetWholeName());
			info.AddValue("companyName", this.CompanyName.GetWholeName());
			info.AddValue("roleName", this.RoleName.GetWholeName());
			info.AddValue("hourWage", this.HourWage.GetLitralValue());
			info.AddValue("monthEndDate", this.MonthEndDay);
			info.AddValue("overtime1", this.OverTime1);
			info.AddValue("overtime2", this.OverTime2);
		}

		#endregion

		#region IComparable implementation

		/// <summary>
		/// Compares to another profile.
		/// Returns -1 value 
		/// Returns 1 value iff this profile earns more than the other profile.
		/// Returns 0 otherwise.
		/// </summary>
		/// <returns>0 iff both profiles earn the same, -1 iff this profile earns less than the other profile and 1 otherwise.</returns>
		/// <param name="other">Other profile to be compared with.</param>
		public int CompareTo(object other) {
			if (!(other is Profile)) {
				throw new InvalidCastException("The provided 'other' parameter is not a Profile.");
			}

			var result = 0;
			var otherProfile = other as Profile;

			if (this.HourWage < otherProfile.HourWage) {
				result = -1;
			} else if (this.HourWage > otherProfile.HourWage) {
				result = 1;
			}

			return result;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Serves as a hash function for a <see cref="ShifterEngine.Profile"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode() {
			return this.FullName.GetHashCode() * this.CompanyName.GetHashCode() * (int)this.HourWage.GetLitralValue() * 397;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ShifterEngine.Profile"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ShifterEngine.Profile"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="ShifterEngine.Profile"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj) {
			if (!(obj is Profile)) {
				throw new InvalidCastException("The provided 'other' parameter is not a Profile.");
			}

			return this.GetHashCode() == ((Profile)obj).GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="ShifterEngine.Profile"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="ShifterEngine.Profile"/>.</returns>
		public override string ToString() {
			return string.Format("[Profile: FullName={0}, CompanyName={1}, RoleName={2}\nHourWage={3}, MonthEndDay={4}, OverTime1={5}, OverTime2={6}]", FullName, CompanyName, RoleName, HourWage, MonthEndDay, OverTime1, OverTime2);
		}

		#endregion
	}


	public static class Extensions {

		#region NameParts extensions

		/// <summary>
		/// Returns the whole name that was parsed inorder to create this <see cref="NameParts"/> object.
		/// </summary>
		/// <returns>The whole name.</returns>
		/// <param name="name">Name.</param>
		public static string GetWholeName(this NameParts name) {
			return string.Format("{0} {1} {2} {3}", name.Prefix, name.GivenName, name.MiddleName, name.Surname);
		}
			
		#endregion

		#region Money extensions

		/// <summary>
		/// Gets the litral value of this <see cref="money.Money"/>.
		/// </summary>
		/// <returns>The litral value.</returns>
		/// <param name="money">Money.</param>
		public static double GetLitralValue(this Money money) {
			var sb = new StringBuilder(money.DisplayNative());
			sb.Remove(0, 1);

			return Convert.ToDouble(sb.ToString());
		}

		#endregion

	}
}

