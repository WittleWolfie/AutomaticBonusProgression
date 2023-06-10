using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;

namespace AutomaticBonusProgression.Features
{
  internal class ArmorAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorAttunement));

    private const string ArmorName = "ArmorAttunement";
    private const string ArmorBaseName = "ArmorAttunement.Base";
    private const string ArmorDisplayName = "ArmorAttunement.Name";
    private const string ArmorDescription = "ArmorAttunement.Description";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log($"Configuring Armor Attunement");

      var effect = FeatureConfigurator.New(ArmorName, Guids.ArmorAttunement)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI() // Since the parent will be shown, no need to show both
        .AddComponent<ArmorAttunementComponent>()
        .Configure();
      return FeatureConfigurator.New(ArmorBaseName, Guids.ArmorAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(ArmorDisplayName)
        .SetDescription(ArmorDescription)
        .SetIcon(BuffRefs.MageArmorBuff.Reference.Get().Icon)
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect() // Hides it from enemy inspect dialog
        .Configure();
    }

    private const string ShieldName = "ShieldAttunement";
    private const string ShieldBaseName = "ShieldAttunement.Base";
    private const string ShieldDisplayName = "ShieldAttunement.Name";
    private const string ShieldDescription = "ShieldAttunement.Description";

    internal static BlueprintFeature ConfigureShield()
    {
      Logger.Log($"Configuring Shield Attunement");

      var effect = FeatureConfigurator.New(ShieldName, Guids.ShieldAttunement)
        .SetIsClassFeature()
        .SetRanks(4)
        .SetHideInUI()
        .AddComponent<RecalculateArmorStats>()
        .Configure();
      return FeatureConfigurator.New(ShieldBaseName, Guids.ShieldAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(ShieldDisplayName)
        .SetDescription(ShieldDescription)
        .SetIcon(BuffRefs.MageShieldBuff.Reference.Get().Icon)
        .SetRanks(4)
        .AddComponent(new AddFeatureABP(effect)) 
        .AddHideFeatureInInspect() // Hide in enemy inspect dialog
        .Configure();
    }

    [TypeId("10638b67-0028-4ef8-86f4-0c3354926a79")]
    internal class RecalculateArmorStats : UnitFactComponentDelegate
    {
      private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(RecalculateArmorStats));

      public override void OnTurnOn()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        }
        catch (Exception e)
        {
          Logger.LogException("RecalculateArmorStats.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        }
        catch (Exception e)
        {
          Logger.LogException("RecalculateArmorStats.OnTurnOff", e);
        }
      }
    }

    [TypeId("4c92c283-1d5c-43af-9277-f69332f419ae")]
    private class ArmorAttunementComponent
      : UnitFactComponentDelegate<ArmorAttunementComponent.ComponentData>,
      IUnitActiveEquipmentSetHandler,
      IUnitEquipmentHandler
    {
      public override void OnTurnOn()
      {
        try
        {
          UpdateUnarmoredBonus();
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          RemoveBonus();
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.OnTurnOff", e);
        }
      }

      public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
      {
        try
        {
          if (unit != Owner)
            return;

          UpdateUnarmoredBonus();
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.HandleUnitChangeActiveEquipmentSet", e);
        }
      }

      public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
      {
        try
        {
          if (slot.Owner != Owner)
            return;

          UpdateUnarmoredBonus();
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.HandleUnitChangeActiveEquipmentSet", e);
        }
      }

      private void UpdateUnarmoredBonus()
      {
        if (Owner.Body.SecondaryHand.HasShield || Owner.Body.Armor.HasArmor)
        {
          RemoveBonus();
          return;
        }

        if (Data.AppliedModifier is not null)
          return;

        Logger.Verbose(() => $"Granting {Owner} attunement while unarmored.");
        Data.AppliedModifier =
          Owner.Stats.AC.AddModifier(Fact.GetRank(), source: Runtime, desc: ModifierDescriptor.ArmorEnhancement);
      }

      private void RemoveBonus()
      {
        if (Data.AppliedModifier is null)
          return;

        Logger.Verbose(() => $"Removing {Owner} unarmored bonus");
        Owner.Stats.AC.RemoveModifier(Data.AppliedModifier);
        Data.AppliedModifier = null;
      }

      public class ComponentData
      {
        [JsonProperty]
        public ModifiableValue.Modifier AppliedModifier;
      }
    }
  }
}
