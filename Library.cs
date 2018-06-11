using System;
using System.IO;
using System.Net;
using Rocket.Unturned.Player;
using Steamworks;
using Rocket.API.User;
using Rocket.API.Player;
using Rocket.Core.User;

namespace SDPlugins
{
    public class Library
    {
		private static OwnerCheckConfig _config;
		private static IPlayerManager _playerManager;

		public static void Init (IPlayerManager playerManager, OwnerCheckConfig config)
		{
			_playerManager = playerManager;
		    _config = config;
		}

        public static void TellInfo(IUser caller, CSteamID ownerid, CSteamID group)
        {
            if (!(_playerManager.GetPlayer (ownerid.ToString ()) is UnturnedPlayer owner))
                return;

			if (_config.SayPlayerID)
				caller.SendMessage ($"Owner ID: {ownerid}");
            if (_config.SayPlayerCharacterName)
				caller.SendMessage ($"Character Name: {owner.CharacterName}");
            if (_config.SayPlayerSteamName)
				caller.SendMessage ($"Steam Name: {owner.SteamName}");
            if (group != CSteamID.Nil)
            {
                if (_config.SayGroupID)
					caller.SendMessage ($"Group ID: {owner.SteamGroupID}");
                if (_config.SayGroupName)
					caller.SendMessage ($"Group Name: {owner.SteamGroupName}");
            }
        }
    }
}
