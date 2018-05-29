using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPlugins
{
    public class OwnerCheckConfig
    {
		public bool SayPlayerID { get; set; } = true;
		public bool SayPlayerCharacterName { get; set; } = true;
		public bool SayPlayerSteamName { get; set; } = true;
		public bool SayGroupID { get; set; } = true;
		public bool SayGroupName { get; set; } = true;
    }
}
