using Rocket.Core.Plugins;
using Rocket.API.DependencyInjection;
using Rocket.Core.Logging;
using Rocket.API.Player;

namespace SDPlugins
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
			Library.Init (_playerManager, ConfigurationInstance);
            Logger.LogInformation("[SDPlugins] Owner check loaded!");
        }

		protected override void OnUnload ()
		{
			Logger.LogInformation("[SDPlugins] Owner check unloaded!");
		}
	}
}
