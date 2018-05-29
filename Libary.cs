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
		private static OwnerCheckConfig config;
		private static IPlayerManager playerManager;

		public static void Init (IPlayerManager _playerManager)
		{
			playerManager = _playerManager;
		}

        public static void TellInfo(IUser caller, CSteamID ownerid, CSteamID group)
        {
			UnturnedPlayer owner = playerManager.GetPlayer (ownerid.ToString ()) as UnturnedPlayer;
            bool isOnline = owner.NativePlayer != null ? owner.NativePlayer.channel != null ? true : false : false;
			
			if (config.SayPlayerID)
				caller.SendMessage ($"Owner ID: {ownerid.ToString ()}");
            if (config.SayPlayerCharacterName)
				caller.SendMessage ($"Character Name: {owner.CharacterName}");
            if (config.SayPlayerSteamName)
				caller.SendMessage ($"Steam Name: {owner.SteamName}");
            if (group != CSteamID.Nil)
            {
                if (config.SayGroupID)
					caller.SendMessage ($"Group ID: {owner.SteamGroupID}");
                if (config.SayGroupName)
					caller.SendMessage ($"Group Name: {owner.SteamGroupName}");
            }
        }
    }
}
