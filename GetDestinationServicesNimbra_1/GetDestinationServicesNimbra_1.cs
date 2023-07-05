/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

05-07-2023	1.0.0.1		MichielSA, Skyline	Initial version
****************************************************************************
*/

namespace GetDestinationServicesNimbra_1
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using Skyline.DataMiner.Automation;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Analytics.GenericInterface;
	using SLDataGateway.API.Types.Results.Paging;
	using System.Xml.Linq;

	[GQIMetaData(Name = "NimbraDestinations")]
	public class MyDataSource : IGQIDataSource, IGQIInputArguments
	{
		private GQIDoubleArgument _argument = new GQIDoubleArgument("Age") { IsRequired = true };
		private double _minimumAge;

		public GQIColumn[] GetColumns()
		{
			return new GQIColumn[]
			{
			new GQIStringColumn("Name"),
			new GQIIntColumn("Age"),
			new GQIDoubleColumn("Height (m)"),
			new GQIDateTimeColumn("Birthday"),
			new GQIBooleanColumn("Likes apples")
			};
		}

		public GQIArgument[] GetInputArguments()
		{
			return new GQIArgument[] { _argument };
		}

		public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
		{
			_minimumAge = args.GetArgumentValue(_argument);
			return new OnArgumentsProcessedOutputArgs();
		}

		public GQIPage GetNextPage(GetNextPageInputArgs args)
		{
			var rows = new List<GQIRow>() {
			new GQIRow(
				new GQICell[]
					{
						new GQICell() { Value = "Alice" },
						new GQICell() { Value = 32 },
						new GQICell() { Value = 1.74, DisplayValue = "1.74 m" },
						new GQICell() { Value = new DateTime(1990, 5, 12) },
						new GQICell() { Value = true }
					}),
			new GQIRow(
				new GQICell[]
					{
						new GQICell() { Value = "Bob" },
						new GQICell() { Value = 22 },
						new GQICell() { Value = 1.85, DisplayValue = "1.85 m" },
						new GQICell() { Value = new DateTime(2000, 1, 22) },
						new GQICell() { Value = true }
					}),
			new GQIRow(
				new GQICell[]
					{
						new GQICell() { Value = "Carol" },
						new GQICell() { Value = 27 },
						new GQICell() { Value = 1.67, DisplayValue = "1.67 m" },
						new GQICell() { Value = new DateTime(1995, 10, 3) },
						new GQICell() { Value = false }
					})
			};

			var filteredRows = rows.Where(row => (int)row.Cells[1].Value > _minimumAge).ToArray();

			return new GQIPage(filteredRows)
			{
				HasNextPage = false
			};
		}
	}
}