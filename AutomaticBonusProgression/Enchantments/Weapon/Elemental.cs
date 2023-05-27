using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Elemental
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Elemental));

    #region Too Many Constants
    private const string CorrosiveEnchantCopy = "LW.Elemental.Corrosive.Enchant";
    private const string CorrosiveName = "LW.Elemental.Corrosive.Name";
    private const string CorrosiveBuff = "LW.Elemental.Corrosive.Buff";
    private const string CorrosiveEffect = "LW.Elemental.Corrosive.Effect";
    private const string CorrosiveOffHandBuff = "LW.Elemental.Corrosive.OffHand.Buff";
    private const string CorrosiveOffHandEffect = "LW.Elemental.Corrosive.OffHand.Effect";

    private const string CorrosiveBurstEnchantCopy = "LW.Elemental.Corrosive.Burst.Enchant";
    private const string CorrosiveBurstName = "LW.Elemental.Corrosive.Burst.Name";
    private const string CorrosiveBurstBuff = "LW.Elemental.Corrosive.Burst.Buff";
    private const string CorrosiveBurstEffect = "LW.Elemental.Corrosive.Burst.Effect";
    private const string CorrosiveBurstOffHandBuff = "LW.Elemental.Corrosive.Burst.OffHand.Buff";
    private const string CorrosiveBurstOffHandEffect = "LW.Elemental.Corrosive.Burst.OffHand.Effect";

    private const string FlamingEnchantCopy = "LW.Elemental.Flaming.Enchant";
    private const string FlamingName = "LW.Elemental.Flaming.Name";
    private const string FlamingBuff = "LW.Elemental.Flaming.Buff";
    private const string FlamingEffect = "LW.Elemental.Flaming.Effect";
    private const string FlamingOffHandBuff = "LW.Elemental.Flaming.OffHand.Buff";
    private const string FlamingOffHandEffect = "LW.Elemental.Flaming.OffHand.Effect";

    private const string FlamingBurstEnchantCopy = "LW.Elemental.Flaming.Burst.Enchant";
    private const string FlamingBurstName = "LW.Elemental.Flaming.Burst.Name";
    private const string FlamingBurstBuff = "LW.Elemental.Flaming.Burst.Buff";
    private const string FlamingBurstEffect = "LW.Elemental.Flaming.Burst.Effect";
    private const string FlamingBurstOffHandBuff = "LW.Elemental.Flaming.Burst.OffHand.Buff";
    private const string FlamingBurstOffHandEffect = "LW.Elemental.Flaming.Burst.OffHand.Effect";

    private const string FrostEnchantCopy = "LW.Elemental.Frost.Enchant";
    private const string FrostName = "LW.Elemental.Frost.Name";
    private const string FrostBuff = "LW.Elemental.Frost.Buff";
    private const string FrostEffect = "LW.Elemental.Frost.Effect";
    private const string FrostOffHandBuff = "LW.Elemental.Frost.OffHand.Buff";
    private const string FrostOffHandEffect = "LW.Elemental.Frost.OffHand.Effect";

    private const string FrostBurstEnchantCopy = "LW.Elemental.Frost.Burst.Enchant";
    private const string FrostBurstName = "LW.Elemental.Frost.Burst.Name";
    private const string FrostBurstBuff = "LW.Elemental.Frost.Burst.Buff";
    private const string FrostBurstEffect = "LW.Elemental.Frost.Burst.Effect";
    private const string FrostBurstOffHandBuff = "LW.Elemental.Frost.Burst.OffHand.Buff";
    private const string FrostBurstOffHandEffect = "LW.Elemental.Frost.Burst.OffHand.Effect";

    private const string ShockingEnchantCopy = "LW.Elemental.Shocking.Enchant";
    private const string ShockingName = "LW.Elemental.Shocking.Name";
    private const string ShockingBuff = "LW.Elemental.Shocking.Buff";
    private const string ShockingEffect = "LW.Elemental.Shocking.Effect";
    private const string ShockingOffHandBuff = "LW.Elemental.Shocking.OffHand.Buff";
    private const string ShockingOffHandEffect = "LW.Elemental.Shocking.OffHand.Effect";

    private const string ShockingBurstEnchantCopy = "LW.Elemental.Shocking.Burst.Enchant";
    private const string ShockingBurstName = "LW.Elemental.Shocking.Burst.Name";
    private const string ShockingBurstBuff = "LW.Elemental.Shocking.Burst.Buff";
    private const string ShockingBurstEffect = "LW.Elemental.Shocking.Burst.Effect";
    private const string ShockingBurstOffHandBuff = "LW.Elemental.Shocking.Burst.OffHand.Buff";
    private const string ShockingBurstOffHandEffect = "LW.Elemental.Shocking.Burst.OffHand.Effect";

    private const string ThunderingEnchantCopy = "LW.Elemental.Thundering.Enchant";
    private const string ThunderingName = "LW.Elemental.Thundering.Name";
    private const string ThunderingBuff = "LW.Elemental.Thundering.Buff";
    private const string ThunderingEffect = "LW.Elemental.Thundering.Effect";
    private const string ThunderingOffHandBuff = "LW.Elemental.Thundering.OffHand.Buff";
    private const string ThunderingOffHandEffect = "LW.Elemental.Thundering.OffHand.Effect";

    private const string ThunderingBurstEnchantCopy = "LW.Elemental.Thundering.Burst.Enchant";
    private const string ThunderingBurstName = "LW.Elemental.Thundering.Burst.Name";
    private const string ThunderingBurstBuff = "LW.Elemental.Thundering.Burst.Buff";
    private const string ThunderingBurstEffect = "LW.Elemental.Thundering.Burst.Effect";
    private const string ThunderingBurstOffHandBuff = "LW.Elemental.Thundering.Burst.OffHand.Buff";
    private const string ThunderingBurstOffHandEffect = "LW.Elemental.Thundering.Burst.OffHand.Effect";
    #endregion

    private const int EnhancementCost = 1;
    private const int BurstEnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Elemental");

      // Corrosive
      var corrosive = WeaponEnchantmentRefs.Corrosive.Reference.Get();
      var corrosiveEnchantInfo = new WeaponEnchantInfo(CorrosiveName, corrosive.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        corrosiveEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(CorrosiveEffect, Guids.CorrosiveEffect, Guids.CorrosiveEnchantCopy),
        parentBuff: new(CorrosiveBuff, Guids.CorrosiveBuff));
      EnchantTool.CreateVariantEnchant(
        corrosiveEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          CorrosiveOffHandEffect,
          Guids.CorrosiveOffHandEffect,
          Guids.CorrosiveEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(CorrosiveOffHandBuff, Guids.CorrosiveOffHandBuff));

      // Corrosive Burst
      var corrosiveBurst = WeaponEnchantmentRefs.CorrosiveBurst.Reference.Get();
      var corrosiveBurstEnchantInfo =
        new WeaponEnchantInfo(CorrosiveBurstName, corrosiveBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        corrosiveBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          CorrosiveBurstEffect,
          Guids.CorrosiveBurstEffect,
          enchantments: new() { Guids.CorrosiveEnchantCopy, Guids.CorrosiveBurstEnchantCopy }),
        parentBuff: new(CorrosiveBurstBuff, Guids.CorrosiveBurstBuff));
      EnchantTool.CreateVariantEnchant(
        corrosiveBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          CorrosiveBurstOffHandEffect,
          Guids.CorrosiveBurstOffHandEffect,
          enchantments: new() { Guids.CorrosiveEnchantCopy, Guids.CorrosiveBurstEnchantCopy },
          toPrimaryWeapon: false),
        variantBuff: new(CorrosiveBurstOffHandBuff, Guids.CorrosiveBurstOffHandBuff));

      // Flaming
      var flaming = WeaponEnchantmentRefs.Flaming.Reference.Get();
      var flamingEnchantInfo = new WeaponEnchantInfo(FlamingName, flaming.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        flamingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(FlamingEffect, Guids.FlamingEffect, Guids.FlamingEnchantCopy),
        parentBuff: new(FlamingBuff, Guids.FlamingBuff));
      EnchantTool.CreateVariantEnchant(
        flamingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FlamingOffHandEffect,
          Guids.FlamingOffHandEffect,
          Guids.FlamingEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(FlamingOffHandBuff, Guids.FlamingOffHandBuff));

      // Flaming Burst
      var flamingBurst = WeaponEnchantmentRefs.FlamingBurst.Reference.Get();
      var flamingBurstEnchantInfo =
        new WeaponEnchantInfo(FlamingBurstName, flamingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        flamingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FlamingBurstEffect,
          Guids.FlamingBurstEffect,
          enchantments: new() { Guids.FlamingEnchantCopy, Guids.FlamingBurstEnchantCopy }),
        parentBuff: new(FlamingBurstBuff, Guids.FlamingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        flamingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FlamingBurstOffHandEffect,
          Guids.FlamingBurstOffHandEffect,
          enchantments: new() { Guids.FlamingEnchantCopy, Guids.FlamingBurstEnchantCopy },
          toPrimaryWeapon: false),
        variantBuff: new(FlamingBurstOffHandBuff, Guids.FlamingBurstOffHandBuff));

      // Frost
      var frost = WeaponEnchantmentRefs.Frost.Reference.Get();
      var frostEnchantInfo = new WeaponEnchantInfo(FrostName, frost.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        frostEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(FrostEffect, Guids.FrostEffect, Guids.FrostEnchantCopy),
        parentBuff: new(FrostBuff, Guids.FrostBuff));
      EnchantTool.CreateVariantEnchant(
        frostEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FrostOffHandEffect,
          Guids.FrostOffHandEffect,
          Guids.FrostEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(FrostOffHandBuff, Guids.FrostOffHandBuff));

      // Frost Burst
      var frostBurst = WeaponEnchantmentRefs.IcyBurst.Reference.Get();
      var frostBurstEnchantInfo =
        new WeaponEnchantInfo(FrostBurstName, frostBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        frostBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FrostBurstEffect,
          Guids.FrostBurstEffect,
          enchantments: new() { Guids.FrostEnchantCopy, Guids.FrostBurstEnchantCopy }),
        parentBuff: new(FrostBurstBuff, Guids.FrostBurstBuff));
      EnchantTool.CreateVariantEnchant(
        frostBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FrostBurstOffHandEffect,
          Guids.FrostBurstOffHandEffect,
          enchantments: new() { Guids.FrostEnchantCopy, Guids.FrostBurstEnchantCopy },
          toPrimaryWeapon: false),
        variantBuff: new(FrostBurstOffHandBuff, Guids.FrostBurstOffHandBuff));

      // Shocking
      var shocking = WeaponEnchantmentRefs.Shock.Reference.Get();
      var shockingEnchantInfo = new WeaponEnchantInfo(ShockingName, shocking.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        shockingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(ShockingEffect, Guids.ShockingEffect, Guids.ShockingEnchantCopy),
        parentBuff: new(ShockingBuff, Guids.ShockingBuff));
      EnchantTool.CreateVariantEnchant(
        shockingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ShockingOffHandEffect,
          Guids.ShockingOffHandEffect,
          Guids.ShockingEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(ShockingOffHandBuff, Guids.ShockingOffHandBuff));

      // Shocking Burst
      var shockingBurst = WeaponEnchantmentRefs.ShockingBurst.Reference.Get();
      var shockingBurstEnchantInfo =
        new WeaponEnchantInfo(ShockingBurstName, shockingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        shockingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ShockingBurstEffect,
          Guids.ShockingBurstEffect,
          enchantments: new() { Guids.ShockingEnchantCopy, Guids.ShockingBurstEnchantCopy }),
        parentBuff: new(ShockingBurstBuff, Guids.ShockingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        shockingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ShockingBurstOffHandEffect,
          Guids.ShockingBurstOffHandEffect,
          enchantments: new() { Guids.ShockingEnchantCopy, Guids.ShockingBurstEnchantCopy },
          toPrimaryWeapon: false),
        variantBuff: new(ShockingBurstOffHandBuff, Guids.ShockingBurstOffHandBuff));

      // Thundering
      var thundering = WeaponEnchantmentRefs.Thundering.Reference.Get();
      var thunderingEnchantInfo = new WeaponEnchantInfo(ThunderingName, thundering.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        thunderingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(ThunderingEffect, Guids.ThunderingEffect, Guids.ThunderingEnchantCopy),
        parentBuff: new(ThunderingBuff, Guids.ThunderingBuff));
      EnchantTool.CreateVariantEnchant(
        thunderingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ThunderingOffHandEffect,
          Guids.ThunderingOffHandEffect,
          Guids.ThunderingEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(ThunderingOffHandBuff, Guids.ThunderingOffHandBuff));

      // Thundering Burst
      var thunderingBurst = WeaponEnchantmentRefs.ThunderingBurst.Reference.Get();
      var thunderingBurstEnchantInfo =
        new WeaponEnchantInfo(ThunderingBurstName, thunderingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        thunderingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ThunderingBurstEffect,
          Guids.ThunderingBurstEffect,
          enchantments: new() { Guids.ThunderingEnchantCopy, Guids.ThunderingBurstEnchantCopy }),
        parentBuff: new(ThunderingBurstBuff, Guids.ThunderingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        thunderingBurstEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ThunderingBurstOffHandEffect,
          Guids.ThunderingBurstOffHandEffect,
          enchantments: new() { Guids.ThunderingEnchantCopy, Guids.ThunderingBurstEnchantCopy },
          toPrimaryWeapon: false),
        variantBuff: new(ThunderingBurstOffHandBuff, Guids.ThunderingBurstOffHandBuff));

      // In-game "X Burst" enchantments don't actually implement X + Burst. All weapons just get X & X Burst. To
      // calculate the bonus correctly then just treat each as a +1.
      EnchantTool.SetUpWeaponEnchant(
        corrosive, new(CorrosiveEnchantCopy, Guids.CorrosiveEnchantCopy), corrosiveEnchantInfo);
      EnchantTool.SetUpWeaponEnchant(
        corrosive, new(CorrosiveBurstEnchantCopy, Guids.CorrosiveBurstEnchantCopy), corrosiveEnchantInfo);

      EnchantTool.SetUpWeaponEnchant(
        flaming, new(FlamingEnchantCopy, Guids.FlamingEnchantCopy), flamingEnchantInfo);
      EnchantTool.SetUpWeaponEnchant(
        flamingBurst, new(FlamingBurstEnchantCopy, Guids.FlamingBurstEnchantCopy), flamingEnchantInfo);

      EnchantTool.SetUpWeaponEnchant(
        frost, new(FrostEnchantCopy, Guids.FrostEnchantCopy), frostEnchantInfo);
      EnchantTool.SetUpWeaponEnchant(
        frostBurst, new(FrostBurstEnchantCopy, Guids.FrostBurstEnchantCopy), frostEnchantInfo);

      EnchantTool.SetUpWeaponEnchant(
        shocking, new(ShockingEnchantCopy, Guids.ShockingEnchantCopy), shockingEnchantInfo);
      EnchantTool.SetUpWeaponEnchant(
        shockingBurst, new(ShockingBurstEnchantCopy, Guids.ShockingBurstEnchantCopy), shockingEnchantInfo);

      EnchantTool.SetUpWeaponEnchant(
        thundering, new(ThunderingEnchantCopy, Guids.ThunderingEnchantCopy), thunderingEnchantInfo);
      EnchantTool.SetUpWeaponEnchant(
        thunderingBurst, new(ThunderingBurstEnchantCopy, Guids.ThunderingBurstEnchantCopy), thunderingEnchantInfo);
    }
  }
}