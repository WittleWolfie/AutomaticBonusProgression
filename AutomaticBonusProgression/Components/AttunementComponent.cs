using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using System.Collections.Generic;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Lists the enchantments granted by this attunement.
  /// </summary>
  [TypeId("7c64b999-347e-4b62-b4f5-4f459b37276e")]
  [AllowedOn(typeof(BlueprintFeature))]
  internal class AttunementComponent : UnitFactComponentDelegate
  {
    internal readonly List<(BlueprintBuffReference buff, int ranks)> Buffs = new();

    internal AttunementComponent(params (Blueprint<BlueprintBuffReference> buff, int ranks)[] buffs)
    {
      foreach ((var buff, int ranks) in buffs)
        Buffs.Add(new(buff.Reference, ranks));
    }
  }
}
