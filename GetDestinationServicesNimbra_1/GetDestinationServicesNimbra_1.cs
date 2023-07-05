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
	using System.Linq;
	using Skyline.DataMiner.Analytics.GenericInterface;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Messages;

	[GQIMetaData(Name = "NimbraDestinations")]
	public class MyDataSource : IGQIDataSource, IGQIInputArguments, IGQIOnInit
	{
		private GQIStringArgument _argument = new GQIStringArgument("Purpose Filter") { IsRequired = true };
		private string _purposeFilter = string.Empty;
		private IDms _dms;

		public GQIColumn[] GetColumns()
		{
			return new GQIColumn[]
			{
			new GQIStringColumn("ID"),
			new GQIStringColumn("Src Node"),
			new GQIStringColumn("Src TTP Purpose"),
			new GQIStringColumn("Src DSTI"),
			new GQIIntColumn("Src Customer ID"),
			new GQIStringColumn("Type"),
			new GQIStringColumn("Oper Status"),
			new GQIStringColumn("Dest Node"),
			new GQIStringColumn("Dest TTP "),
			};
		}

		public GQIArgument[] GetInputArguments()
		{
			return new GQIArgument[] { _argument };
		}

		public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
		{
			_purposeFilter = args.GetArgumentValue(_argument);
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

			var filteredRows = rows.Where(row => (int)row.Cells[1].Value > 12).ToArray();

			return new GQIPage(filteredRows)
			{
				HasNextPage = false
			};
		}

		public OnInitOutputArgs OnInit(OnInitInputArgs args)
		{
			_dms = DmsFactory.CreateDms(new GqiConnection(args.DMS));
			return new OnInitOutputArgs();
		}

		public class GqiConnection : ICommunication
		{
			private readonly GQIDMS _gqiDms;

			public GqiConnection(GQIDMS gqiDms)
			{
				_gqiDms = gqiDms ?? throw new ArgumentNullException(nameof(gqiDms));
			}

			public DMSMessage[] SendMessage(DMSMessage message)
			{
				return new[] { _gqiDms.SendMessage(message) };
			}

			public DMSMessage SendSingleResponseMessage(DMSMessage message)
			{
				return _gqiDms.SendMessage(message);
			}

			public DMSMessage SendSingleRawResponseMessage(DMSMessage message)
			{
				return _gqiDms.SendMessage(message);
			}

			public void AddSubscriptionHandler(NewMessageEventHandler handler)
			{
				throw new NotImplementedException();
			}

			public void AddSubscriptions(NewMessageEventHandler handler, string handleGuid, SubscriptionFilter[] subscriptions)
			{
				throw new NotImplementedException();
			}

			public void ClearSubscriptionHandler(NewMessageEventHandler handler)
			{
				throw new NotImplementedException();
			}

			public void ClearSubscriptions(NewMessageEventHandler handler, string handleGuid, bool replaceWithEmpty = false)
			{
				throw new NotImplementedException();
			}
		}
	}
}