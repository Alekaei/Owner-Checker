using Rocket.API.Commands;
using Rocket.Core.User;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using Rocket.API.Plugins;
using UnityEngine;

namespace SDPlugins.OwnerChecker
{
	public class CheckOwner : ICommand
	{
	    private OwnerCheckerPlugin _plugin;

	    public CheckOwner(IPlugin plugin)
	    {
	        _plugin = (OwnerCheckerPlugin) plugin;
	    }
		public string Name => "checkowner";

	    public string[] Aliases => null;

		public string Summary => "Check the owner of a certain object.";

		public string Description => null;

		public string Permission => "checkowner";

		public string Syntax => "";

	    public IChildCommand[] ChildCommands => null;

		public void Execute (ICommandContext context)
		{
			UnturnedPlayer player = (context.User as UnturnedUser)?.Player;
			if (player == null)
				return;
			if (Physics.Raycast (player.NativePlayer.look.aim.position, player.NativePlayer.look.aim.forward, out RaycastHit hit, 10, RayMasks.BARRICADE_INTERACT))
			{
			    Transform transform = hit.transform;
				InteractableVehicle vehicle = transform.gameObject.GetComponent<InteractableVehicle> ();

				if (transform.GetComponent<InteractableDoorHinge> () != null)
				{
					transform = transform.parent.parent;
				}

				if (BarricadeManager.tryGetInfo (transform, out _, out _, out _, out var index, out var r))
				{

					var bdata = r.barricades [index];

				    _plugin.TellInfo (context.User, (CSteamID) bdata.owner, (CSteamID) bdata.group);
				}

				else if (StructureManager.tryGetInfo (transform, out _, out _, out index, out var s))
				{
					var sdata = s.structures [index];

				    _plugin.TellInfo (context.User, (CSteamID) sdata.owner, (CSteamID) sdata.group);
				}

				else if (vehicle != null)
				{
					if (vehicle.lockedOwner != CSteamID.Nil)
					{
					    _plugin.TellInfo (context.User, vehicle.lockedOwner, vehicle.lockedGroup);
						return;
					}
					context.User.SendMessage ("Vehicle does not have an owner.");
				}
			}
		}

		public bool SupportsUser (Type user) => true;
	}
}
