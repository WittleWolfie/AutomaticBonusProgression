using AutomaticBonusProgression.Util;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryGifts
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGifts));

    internal static void Configure()
    {
      Logger.Log("Configuring Legendary Gifts");

      LegendaryAbility.Configure();
      LegendaryShieldmaster.Configure();
      LegendaryTwinWeapons.Configure();
      LegendaryArmor.Configure();
      LegendaryArmor.ConfigureShield();
      LegendaryWeapon.Configure();
      LegendaryWeapon.ConfigureOffHand();
    }
  }
}
