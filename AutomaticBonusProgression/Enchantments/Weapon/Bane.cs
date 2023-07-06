using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Bane
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bane));

    #region Too Many Constants
    private const string AberrationsEnchantCopy = "LW.Bane.Aberrations.Enchant";
    private const string AberrationsName = "LW.Bane.Aberrations.Name";
    private const string AberrationsEffect = "LW.Bane.Aberrations.Effect";
    private const string AberrationsOffHandEffect = "LW.Bane.Aberrations.OffHand.Effect";
    private const string AberrationsBuff = "LW.Bane.Aberrations.Buff";
    private const string AberrationsOffHandBuff = "LW.Bane.Aberrations.OffHand.Buff";

    private const string AnimalsEnchantCopy = "LW.Bane.Animals.Enchant";
    private const string AnimalsName = "LW.Bane.Animals.Name";
    private const string AnimalsEffect = "LW.Bane.Animals.Effect";
    private const string AnimalsOffHandEffect = "LW.Bane.Animals.OffHand.Effect";
    private const string AnimalsBuff = "LW.Bane.Animals.Buff";
    private const string AnimalsOffHandBuff = "LW.Bane.Animals.OffHand.Buff";

    private const string ConstructsEnchantCopy = "LW.Bane.Constructs.Enchant";
    private const string ConstructsName = "LW.Bane.Constructs.Name";
    private const string ConstructsEffect = "LW.Bane.Constructs.Effect";
    private const string ConstructsOffHandEffect = "LW.Bane.Constructs.OffHand.Effect";
    private const string ConstructsBuff = "LW.Bane.Constructs.Buff";
    private const string ConstructsOffHandBuff = "LW.Bane.Constructs.OffHand.Buff";

    private const string DragonsEnchantCopy = "LW.Bane.Dragons.Enchant";
    private const string DragonsName = "LW.Bane.Dragons.Name";
    private const string DragonsEffect = "LW.Bane.Dragons.Effect";
    private const string DragonsOffHandEffect = "LW.Bane.Dragons.OffHand.Effect";
    private const string DragonsBuff = "LW.Bane.Dragons.Buff";
    private const string DragonsOffHandBuff = "LW.Bane.Dragons.OffHand.Buff";

    private const string FeyEnchantCopy = "LW.Bane.Fey.Enchant";
    private const string FeyName = "LW.Bane.Fey.Name";
    private const string FeyEffect = "LW.Bane.Fey.Effect";
    private const string FeyOffHandEffect = "LW.Bane.Fey.OffHand.Effect";
    private const string FeyBuff = "LW.Bane.Fey.Buff";
    private const string FeyOffHandBuff = "LW.Bane.Fey.OffHand.Buff";

    private const string HumanoidGiantEnchantCopy = "LW.Bane.HumanoidGiant.Enchant";
    private const string HumanoidGiantName = "LW.Bane.Humanoid.Giant.Name";
    private const string HumanoidGiantEffect = "LW.Bane.Humanoid.Giant.Effect";
    private const string HumanoidGiantOffHandEffect = "LW.Bane.Humanoid.Giant.OffHand.Effect";
    private const string HumanoidGiantBuff = "LW.Bane.Humanoid.Giant.Buff";
    private const string HumanoidGiantOffHandBuff = "LW.Bane.Humanoid.Giant.OffHand.Buff";

    private const string HumanoidReptilianEnchantCopy = "LW.Bane.Humanoid.Reptilian.Enchant";
    private const string HumanoidReptilianName = "LW.Bane.Humanoid.Reptilian.Name";
    private const string HumanoidReptilianEffect = "LW.Bane.Humanoid.Reptilian.Effect";
    private const string HumanoidReptilianOffHandEffect = "LW.Bane.Humanoid.Reptilian.OffHand.Effect";
    private const string HumanoidReptilianBuff = "LW.Bane.Humanoid.Reptilian.Buff";
    private const string HumanoidReptilianOffHandBuff = "LW.Bane.Humanoid.Reptilian.OffHand.Buff";

    private const string HumanoidMonstrousEnchantCopy = "LW.Bane.Humanoid.Monstrous.Enchant";
    private const string HumanoidMonstrousName = "LW.Bane.Humanoid.Monstrous.Name";
    private const string HumanoidMonstrousEffect = "LW.Bane.Humanoid.Monstrous.Effect";
    private const string HumanoidMonstrousOffHandEffect = "LW.Bane.Humanoid.Monstrous.OffHand.Effect";
    private const string HumanoidMonstrousBuff = "LW.Bane.Humanoid.Monstrous.Buff";
    private const string HumanoidMonstrousOffHandBuff = "LW.Bane.Humanoid.Monstrous.OffHand.Buff";

    private const string MagicalBeastsEnchantCopy = "LW.Bane.MagicalBeasts.Enchant";
    private const string MagicalBeastsName = "LW.Bane.MagicalBeasts.Name";
    private const string MagicalBeastsEffect = "LW.Bane.MagicalBeasts.Effect";
    private const string MagicalBeastsOffHandEffect = "LW.Bane.MagicalBeasts.OffHand.Effect";
    private const string MagicalBeastsBuff = "LW.Bane.MagicalBeasts.Buff";
    private const string MagicalBeastsOffHandBuff = "LW.Bane.MagicalBeasts.OffHand.Buff";

    private const string OutsiderGoodEnchantCopy = "LW.Bane.Outsider.Good.Enchant";
    private const string OutsiderGoodName = "LW.Bane.Outsider.Good.Name";
    private const string OutsiderGoodEffect = "LW.Bane.Outsider.Good.Effect";
    private const string OutsiderGoodOffHandEffect = "LW.Bane.Outsider.Good.OffHand.Effect";
    private const string OutsiderGoodBuff = "LW.Bane.Outsider.Good.Buff";
    private const string OutsiderGoodOffHandBuff = "LW.Bane.Outsider.Good.OffHand.Buff";

    private const string OutsiderEvilEnchantCopy = "LW.Bane.Outsider.Evil.Enchant";
    private const string OutsiderEvilName = "LW.Bane.Outsider.Evil.Name";
    private const string OutsiderEvilEffect = "LW.Bane.Outsider.Evil.Effect";
    private const string OutsiderEvilOffHandEffect = "LW.Bane.Outsider.Evil.OffHand.Effect";
    private const string OutsiderEvilBuff = "LW.Bane.Outsider.Evil.Buff";
    private const string OutsiderEvilOffHandBuff = "LW.Bane.Outsider.Evil.OffHand.Buff";

    private const string OutsiderLawfulEnchantCopy = "LW.Bane.Outsider.Lawful.Enchant";
    private const string OutsiderLawfulName = "LW.Bane.Outsider.Lawful.Name";
    private const string OutsiderLawfulEffect = "LW.Bane.Outsider.Lawful.Effect";
    private const string OutsiderLawfulOffHandEffect = "LW.Bane.Outsider.Lawful.OffHand.Effect";
    private const string OutsiderLawfulBuff = "LW.Bane.Outsider.Lawful.Buff";
    private const string OutsiderLawfulOffHandBuff = "LW.Bane.Outsider.Lawful.OffHand.Buff";

    private const string OutsiderChaoticEnchantCopy = "LW.Bane.Outsider.Chaotic.Enchant";
    private const string OutsiderChaoticName = "LW.Bane.Outsider.Chaotic.Name";
    private const string OutsiderChaoticEffect = "LW.Bane.Outsider.Chaotic.Effect";
    private const string OutsiderChaoticOffHandEffect = "LW.Bane.Outsider.Chaotic.OffHand.Effect";
    private const string OutsiderChaoticBuff = "LW.Bane.Outsider.Chaotic.Buff";
    private const string OutsiderChaoticOffHandBuff = "LW.Bane.Outsider.Chaotic.OffHand.Buff";

    private const string OutsiderNeutralEnchantCopy = "LW.Bane.Outsider.Neutral.Enchant";
    private const string OutsiderNeutralName = "LW.Bane.Outsider.Neutral.Name";
    private const string OutsiderNeutralEffect = "LW.Bane.Outsider.Neutral.Effect";
    private const string OutsiderNeutralOffHandEffect = "LW.Bane.Outsider.Neutral.OffHand.Effect";
    private const string OutsiderNeutralBuff = "LW.Bane.Outsider.Neutral.Buff";
    private const string OutsiderNeutralOffHandBuff = "LW.Bane.Outsider.Neutral.OffHand.Buff";

    private const string PlantsEnchantCopy = "LW.Bane.Plants.Enchant";
    private const string PlantsName = "LW.Bane.Plants.Name";
    private const string PlantsEffect = "LW.Bane.Plants.Effect";
    private const string PlantsOffHandEffect = "LW.Bane.Plants.OffHand.Effect";
    private const string PlantsBuff = "LW.Bane.Plants.Buff";
    private const string PlantsOffHandBuff = "LW.Bane.Plants.OffHand.Buff";

    private const string UndeadEnchantCopy = "LW.Bane.Plants.Undead";
    private const string UndeadName = "LW.Bane.Undead.Name";
    private const string UndeadEffect = "LW.Bane.Undead.Effect";
    private const string UndeadOffHandEffect = "LW.Bane.Undead.OffHand.Effect";
    private const string UndeadBuff = "LW.Bane.Undead.Buff";
    private const string UndeadOffHandBuff = "LW.Bane.Undead.OffHand.Buff";

    private const string VerminEnchantCopy = "LW.Bane.Vermin.Undead";
    private const string VerminName = "LW.Bane.Vermin.Name";
    private const string VerminEffect = "LW.Bane.Vermin.Effect";
    private const string VerminOffHandEffect = "LW.Bane.Vermin.OffHand.Effect";
    private const string VerminBuff = "LW.Bane.Vermin.Buff";
    private const string VerminOffHandBuff = "LW.Bane.Vermin.OffHand.Buff";
    #endregion

    private const string Description = "LW.Bane.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Bane");

      var icon = BuffRefs.BaneBuff.Reference.Get().Icon;

      // Aberrations
      var aberrationsEnchantInfo = new WeaponEnchantInfo(AberrationsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        aberrationsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(AberrationsEffect, Guids.BaneAberrationsEffect, Guids.BaneAberrationsEnchantCopy),
        parentBuff: new(AberrationsBuff, Guids.BaneAberrationsBuff));
      EnchantTool.CreateVariantEnchant(
        aberrationsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          AberrationsOffHandEffect,
          Guids.BaneAberrationsOffHandEffect,
          Guids.BaneAberrationsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(AberrationsOffHandBuff, Guids.BaneAberrationsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneAberration,
        new(AberrationsEnchantCopy, Guids.BaneAberrationsEnchantCopy),
        aberrationsEnchantInfo);

      // Animals
      var animalsEnchantInfo = new WeaponEnchantInfo(AnimalsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        animalsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(AnimalsEffect, Guids.BaneAnimalsEffect, Guids.BaneAnimalsEnchantCopy),
        parentBuff: new(AnimalsBuff, Guids.BaneAnimalsBuff));
      EnchantTool.CreateVariantEnchant(
        animalsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          AnimalsOffHandEffect,
          Guids.BaneAnimalsOffHandEffect,
          Guids.BaneAnimalsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(AnimalsOffHandBuff, Guids.BaneAnimalsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneAnimal,
        new(AnimalsEnchantCopy, Guids.BaneAnimalsEnchantCopy),
        animalsEnchantInfo);

      // Constructs
      var constructsEnchantInfo = new WeaponEnchantInfo(ConstructsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        constructsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(ConstructsEffect, Guids.BaneConstructsEffect, Guids.BaneConstructsEnchantCopy),
        parentBuff: new(ConstructsBuff, Guids.BaneConstructsBuff));
      EnchantTool.CreateVariantEnchant(
        constructsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ConstructsOffHandEffect,
          Guids.BaneConstructsOffHandEffect,
          Guids.BaneConstructsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(ConstructsOffHandBuff, Guids.BaneConstructsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneConstruct,
        new(ConstructsEnchantCopy, Guids.BaneConstructsEnchantCopy),
        constructsEnchantInfo);

      // Dragons
      var dragonsEnchantInfo = new WeaponEnchantInfo(DragonsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        dragonsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(DragonsEffect, Guids.BaneDragonsEffect, Guids.BaneDragonsEnchantCopy),
        parentBuff: new(DragonsBuff, Guids.BaneDragonsBuff));
      EnchantTool.CreateVariantEnchant(
        dragonsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          DragonsOffHandEffect,
          Guids.BaneDragonsOffHandEffect,
          Guids.BaneDragonsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(DragonsOffHandBuff, Guids.BaneDragonsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneDragon,
        new(DragonsEnchantCopy, Guids.BaneDragonsEnchantCopy),
        dragonsEnchantInfo);

      // Fey
      var feyEnchantInfo = new WeaponEnchantInfo(FeyName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        feyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(FeyEffect, Guids.BaneFeyEffect, Guids.BaneFeyEnchantCopy),
        parentBuff: new(FeyBuff, Guids.BaneFeyBuff));
      EnchantTool.CreateVariantEnchant(
        feyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FeyOffHandEffect,
          Guids.BaneFeyOffHandEffect,
          Guids.BaneFeyEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(FeyOffHandBuff, Guids.BaneFeyOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneFey,
        new(FeyEnchantCopy, Guids.BaneFeyEnchantCopy),
        feyEnchantInfo);

      // HumanoidGiant
      var humanoidGiantEnchantInfo = new WeaponEnchantInfo(HumanoidGiantName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidGiantEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidGiantEffect, Guids.BaneHumanoidGiantEffect, Guids.BaneHumanoidGiantEnchantCopy),
        parentBuff: new(HumanoidGiantBuff, Guids.BaneHumanoidGiantBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidGiantEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidGiantOffHandEffect,
          Guids.BaneHumanoidGiantOffHandEffect,
          Guids.BaneHumanoidGiantEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(HumanoidGiantOffHandBuff, Guids.BaneHumanoidGiantOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneHumanoidGiant,
        new(HumanoidGiantEnchantCopy, Guids.BaneHumanoidGiantEnchantCopy),
        humanoidGiantEnchantInfo);

      // HumanoidReptilian
      var humanoidReptilianEnchantInfo = new WeaponEnchantInfo(HumanoidReptilianName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidReptilianEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidReptilianEffect, Guids.BaneHumanoidReptilianEffect, Guids.BaneHumanoidReptilianEnchantCopy),
        parentBuff: new(HumanoidReptilianBuff, Guids.BaneHumanoidReptilianBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidReptilianEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidReptilianOffHandEffect,
          Guids.BaneHumanoidReptilianOffHandEffect,
          Guids.BaneHumanoidReptilianEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(HumanoidReptilianOffHandBuff, Guids.BaneHumanoidReptilianOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneHumanoidReptilian,
        new(HumanoidReptilianEnchantCopy, Guids.BaneHumanoidReptilianEnchantCopy),
        humanoidReptilianEnchantInfo);

      // HumanoidMonstrous
      var humanoidMonstrousEnchantInfo = new WeaponEnchantInfo(HumanoidMonstrousName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidMonstrousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidMonstrousEffect, Guids.BaneHumanoidMonstrousEffect, Guids.BaneHumanoidMonstrousEnchantCopy),
        parentBuff: new(HumanoidMonstrousBuff, Guids.BaneHumanoidMonstrousBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidMonstrousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HumanoidMonstrousOffHandEffect,
          Guids.BaneHumanoidMonstrousOffHandEffect,
          Guids.BaneHumanoidMonstrousEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(HumanoidMonstrousOffHandBuff, Guids.BaneHumanoidMonstrousOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneMonstrousHumanoid,
        new(HumanoidMonstrousEnchantCopy, Guids.BaneHumanoidMonstrousEnchantCopy),
        humanoidMonstrousEnchantInfo);

      // MagicalBeasts
      var magicalBeastsEnchantInfo = new WeaponEnchantInfo(MagicalBeastsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        magicalBeastsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          MagicalBeastsEffect, Guids.BaneMagicalBeastsEffect, Guids.BaneMagicalBeastsEnchantCopy),
        parentBuff: new(MagicalBeastsBuff, Guids.BaneMagicalBeastsBuff));
      EnchantTool.CreateVariantEnchant(
        magicalBeastsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          MagicalBeastsOffHandEffect,
          Guids.BaneMagicalBeastsOffHandEffect,
          Guids.BaneMagicalBeastsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(MagicalBeastsOffHandBuff, Guids.BaneMagicalBeastsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneMagicalBeast,
        new(MagicalBeastsEnchantCopy, Guids.BaneMagicalBeastsEnchantCopy),
        magicalBeastsEnchantInfo);

      // OutsiderGood
      var outsiderGoodEnchantInfo = new WeaponEnchantInfo(OutsiderGoodName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderGoodEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(OutsiderGoodEffect, Guids.BaneOutsiderGoodEffect, Guids.BaneOutsiderGoodEnchantCopy),
        parentBuff: new(OutsiderGoodBuff, Guids.BaneOutsiderGoodBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderGoodEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderGoodOffHandEffect,
          Guids.BaneOutsiderGoodOffHandEffect,
          Guids.BaneOutsiderGoodEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(OutsiderGoodOffHandBuff, Guids.BaneOutsiderGoodOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneOutsiderGood,
        new(OutsiderGoodEnchantCopy, Guids.BaneOutsiderGoodEnchantCopy),
        outsiderGoodEnchantInfo);

      // OutsiderEvil
      var outsiderEvilEnchantInfo = new WeaponEnchantInfo(OutsiderEvilName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderEvilEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(OutsiderEvilEffect, Guids.BaneOutsiderEvilEffect, Guids.BaneOutsiderEvilEnchantCopy),
        parentBuff: new(OutsiderEvilBuff, Guids.BaneOutsiderEvilBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderEvilEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderEvilOffHandEffect,
          Guids.BaneOutsiderEvilOffHandEffect,
          Guids.BaneOutsiderEvilEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(OutsiderEvilOffHandBuff, Guids.BaneOutsiderEvilOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneOutsiderEvil,
        new(OutsiderEvilEnchantCopy, Guids.BaneOutsiderEvilEnchantCopy),
        outsiderEvilEnchantInfo);

      // OutsiderLawful
      var outsiderLawfulEnchantInfo = new WeaponEnchantInfo(OutsiderLawfulName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderLawfulEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderLawfulEffect, Guids.BaneOutsiderLawfulEffect, Guids.BaneOutsiderLawfulEnchantCopy),
        parentBuff: new(OutsiderLawfulBuff, Guids.BaneOutsiderLawfulBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderLawfulEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderLawfulOffHandEffect,
          Guids.BaneOutsiderLawfulOffHandEffect,
          Guids.BaneOutsiderLawfulEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(OutsiderLawfulOffHandBuff, Guids.BaneOutsiderLawfulOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneOutsiderLawful,
        new(OutsiderLawfulEnchantCopy, Guids.BaneOutsiderLawfulEnchantCopy),
        outsiderLawfulEnchantInfo);

      // OutsiderChaotic
      var outsiderChaoticEnchantInfo = new WeaponEnchantInfo(OutsiderChaoticName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderChaoticEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderChaoticEffect, Guids.BaneOutsiderChaoticEffect, Guids.BaneOutsiderChaoticEnchantCopy),
        parentBuff: new(OutsiderChaoticBuff, Guids.BaneOutsiderChaoticBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderChaoticEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderChaoticOffHandEffect,
          Guids.BaneOutsiderChaoticOffHandEffect,
          Guids.BaneOutsiderChaoticEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(OutsiderChaoticOffHandBuff, Guids.BaneOutsiderChaoticOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneOutsiderChaotic,
        new(OutsiderChaoticEnchantCopy, Guids.BaneOutsiderChaoticEnchantCopy),
        outsiderChaoticEnchantInfo);

      // OutsiderNeutral
      var outsiderNeutralEnchantInfo = new WeaponEnchantInfo(OutsiderNeutralName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderNeutralEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderNeutralEffect, Guids.BaneOutsiderNeutralEffect, Guids.BaneOutsiderNeutralEnchantCopy),
        parentBuff: new(OutsiderNeutralBuff, Guids.BaneOutsiderNeutralBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderNeutralEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OutsiderNeutralOffHandEffect,
          Guids.BaneOutsiderNeutralOffHandEffect,
          Guids.BaneOutsiderNeutralEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(OutsiderNeutralOffHandBuff, Guids.BaneOutsiderNeutralOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneOutsiderNeutral,
        new(OutsiderNeutralEnchantCopy, Guids.BaneOutsiderNeutralEnchantCopy),
        outsiderNeutralEnchantInfo);

      // Plants
      var plantsEnchantInfo = new WeaponEnchantInfo(PlantsName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        plantsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(PlantsEffect, Guids.BanePlantsEffect, Guids.BanePlantsEnchantCopy),
        parentBuff: new(PlantsBuff, Guids.BanePlantsBuff));
      EnchantTool.CreateVariantEnchant(
        plantsEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          PlantsOffHandEffect,
          Guids.BanePlantsOffHandEffect,
          Guids.BanePlantsEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(PlantsOffHandBuff, Guids.BanePlantsOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BanePlant,
        new(PlantsEnchantCopy, Guids.BanePlantsEnchantCopy),
        plantsEnchantInfo);

      // Undead
      var undeadEnchantInfo = new WeaponEnchantInfo(UndeadName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        undeadEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(UndeadEffect, Guids.BaneUndeadEffect, Guids.BaneUndeadEnchantCopy),
        parentBuff: new(UndeadBuff, Guids.BaneUndeadBuff));
      EnchantTool.CreateVariantEnchant(
        undeadEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          UndeadOffHandEffect,
          Guids.BaneUndeadOffHandEffect,
          Guids.BaneUndeadEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(UndeadOffHandBuff, Guids.BaneUndeadOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneUndead,
        new(UndeadEnchantCopy, Guids.BaneUndeadEnchantCopy),
        undeadEnchantInfo);

      // Vermin
      var verminEnchantInfo = new WeaponEnchantInfo(VerminName, Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        verminEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(VerminEffect, Guids.BaneVerminEffect, Guids.BaneVerminEnchantCopy),
        parentBuff: new(VerminBuff, Guids.BaneVerminBuff));
      EnchantTool.CreateVariantEnchant(
        verminEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          VerminOffHandEffect,
          Guids.BaneVerminOffHandEffect,
          Guids.BaneVerminEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(VerminOffHandBuff, Guids.BaneVerminOffHandBuff));
      EnchantTool.SetUpWeaponEnchant(
        WeaponEnchantmentRefs.BaneVermin,
        new(VerminEnchantCopy, Guids.BaneVerminEnchantCopy),
        verminEnchantInfo);
    }
  }
}