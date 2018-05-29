using Rocket.Core.Plugins;
using Rocket.API.DependencyInjection;
using Rocket.Core.Logging;
using Rocket.API.Player;

namespace SDPlugins
{
    public class Init : Plugin<OwnerCheckConfig>
    {
		private IPlayerManager playerManager;

		protected Init (IDependencyContainer container, IPlayerManager playerManager) : base ("Owner Checker", container)
		{
			this.playerManager = playerManager;
		}

		protected override void OnLoad(bool isFromReload)
        {
			Library.Init (playerManager);
            Logger.LogWarning("[SDPlugins] Owner check loaded!");
        }

		protected override void OnUnload ()
		{
			Logger.LogWarning ("[SDPlugins] Owner check unloaded!");
		}
	}
}
