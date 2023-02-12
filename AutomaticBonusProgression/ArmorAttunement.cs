using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression
{
  internal class ArmorAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorAttunement));

    private const string ArmorName = "ArmorAttunement";
    private const string ArmorDisplayName = "ArmorAttunement.Name";
    private const string ArmorDescription = "ArmorAttunement.Description";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log($"Configuring Armor Attunement");

      return FeatureConfigurator.New(ArmorName, Guids.ArmorAttunement)
        .SetIsClassFeature()
        .SetDisplayName(ArmorDisplayName)
        .SetDescription(ArmorDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent<ArmorAttunementComponent>()
        .Configure();
    }

    private const string ShieldName = "ShieldAttunement";
    private const string ShieldDisplayName = "ShieldAttunement.Name";
    private const string ShieldDescription = "ShieldAttunement.Description";

    internal static BlueprintFeature ConfigureShield()
    {
      Logger.Log($"Configuring Shield Attunement");

      return FeatureConfigurator.New(ShieldName, Guids.ShieldAttunement)
        .SetIsClassFeature()
        .SetDisplayName(ShieldDisplayName)
        .SetDescription(ShieldDescription)
        //.SetIcon()
        .SetRanks(4)
        .AddComponent<RecalculateArmorStats>()
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
    private class ArmorAttunementComponent :
      UnitFactComponentDelegate, IUnitActiveEquipmentSetHandler, IUnitEquipmentHandler
    {
      public override void OnTurnOn()
      {
        try
        {
          UpdateUnarmoredBonus();
        } catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          Owner.Stats.AC.RemoveModifiersFrom(Runtime);
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
        if (!Common.IsAffectedByABP(Owner))
          return;

        if (Owner.Body.SecondaryHand.HasShield || Owner.Body.Armor.HasArmor)
        {
          Logger.Verbose(() => $"Removing {Owner} unarmored bonus");
          Owner.Stats.AC.RemoveModifiersFrom(Runtime);
          return;
        }

        Logger.Verbose(() => $"Granting {Owner} attunement while unarmored.");
        Owner.Stats.AC.AddModifier(Fact.GetRank(), source: Runtime, desc: ModifierDescriptor.ArmorEnhancement);
      }
    }
  }
}
