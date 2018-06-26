using Rocket.Core.Plugins;
using Rocket.API.DependencyInjection;
using Rocket.Core.Logging;
using Rocket.API.Player;
using Rocket.API.User;
using Rocket.Core.User;
using Rocket.Unturned.Player;
using Steamworks;

namespace SDPlugins.OwnerChecker
{
    public class OwnerCheckerPlugin : Plugin<OwnerCheckConfig>
    {
		private readonly IPlayerManager _playerManager;

		public OwnerCheckerPlugin (IDependencyContainer container, IPlayerManager playerManager) : base ("Owner Checker", container)
		{
			_playerManager = playerManager;
		}

		protected override void OnLoad(bool isFromReload)
        {
            Logger.LogInformation("[SDPlugins] Owner check loaded!");
        }

		protected override void OnUnload ()
		{
			Logger.LogInformation("[SDPlugins] Owner check unloaded!");
		}

        public void TellInfo(IUser caller, CSteamID ownerid, CSteamID group)
        {
            if (!(_playerManager.GetPlayer(ownerid.ToString()) is UnturnedPlayer owner))
                return;

            if (ConfigurationInstance.SayPlayerID)
                caller.SendMessage($"Owner ID: {ownerid}");
            if (ConfigurationInstance.SayPlayerCharacterName)
                caller.SendMessage($"Character Name: {owner.CharacterName}");
            if (ConfigurationInstance.SayPlayerSteamName)
                caller.SendMessage($"Steam Name: {owner.SteamName}");
            if (group != CSteamID.Nil)
            {
                if (ConfigurationInstance.SayGroupID)
                    caller.SendMessage($"Group ID: {owner.SteamGroupID}");
                if (ConfigurationInstance.SayGroupName)
                    caller.SendMessage($"Group Name: {owner.SteamGroupName}");
            }
        }
    }
}
