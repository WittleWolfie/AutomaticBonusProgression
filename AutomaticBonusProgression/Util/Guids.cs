using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// List of new and mod Guids.
  /// </summary>
  internal class Guids
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Guids));

    // Feature used to calculate armor / weapon / shield enhancement bonuses
    internal const string EnhancementCalculator = "945f05cf-ffb5-489d-b4d8-2687d1f1b835";

    internal const string ArmorSelection = "ae78a80c-f6ba-4c42-962f-2b0ac908d25a";
    internal const string ArmorAttunement = "24af67c6-bc58-4dba-a9bf-d7d00925e7c7";
    internal const string ShieldAttunement = "58b87d99-125c-4300-9c87-ba0d90168ca2";
    internal const string LegendaryArmorAttunement = "8b361c98-2aa0-4109-9a11-33a9fc997793";
    internal const string LegendaryShieldAttunement = "5022aa7f-3e42-4540-a8b9-5fac720793e9";
  }
}
