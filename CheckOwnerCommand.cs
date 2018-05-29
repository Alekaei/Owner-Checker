using Rocket.API.Commands;
using Rocket.API.User;
using Rocket.Core.User;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SDPlugins
{
	public class CheckOwner : ICommand
	{
		public string Name => "checkowner";

		public string [] Aliases => new string [0];

		public string Summary => "Check the owner of a certain object.";

		public string Description => throw new NotImplementedException ();

		public string Permission => "checkowner";

		public string Syntax => "";

		public IChildCommand [] ChildCommands => throw new NotImplementedException ();

		public void Execute (ICommandContext context)
		{
			UnturnedPlayer player = (context.User as UnturnedUser)?.Player;
			if (player == null)
				return;
			if (Physics.Raycast (player.NativePlayer.look.aim.position, player.NativePlayer.look.aim.forward, out RaycastHit hit, 10, RayMasks.BARRICADE_INTERACT))
			{
				byte x;
				byte y;

				ushort plant;
				ushort index;

				BarricadeRegion r;
				StructureRegion s;

				Transform transform = hit.transform;
				InteractableVehicle vehicle = transform.gameObject.GetComponent<InteractableVehicle> ();

				if (transform.GetComponent<InteractableDoorHinge> () != null)
				{
					transform = transform.parent.parent;
				}

				if (BarricadeManager.tryGetInfo (transform, out x, out y, out plant, out index, out r))
				{

					var bdata = r.barricades [index];

					Library.TellInfo (context.User, (CSteamID) bdata.owner, (CSteamID) bdata.group);
				}

				else if (StructureManager.tryGetInfo (transform, out x, out y, out index, out s))
				{
					var sdata = s.structures [index];

					Library.TellInfo (context.User, (CSteamID) sdata.owner, (CSteamID) sdata.group);
				}

				else if (vehicle != null)
				{
					if (vehicle.lockedOwner != CSteamID.Nil)
					{
						Library.TellInfo (context.User, vehicle.lockedOwner, vehicle.lockedGroup);
						return;
					}
					context.User.SendMessage ("Vehicle does not have an owner.");
				}
			}
		}

		public bool SupportsUser (Type user) => true;
	}
}
