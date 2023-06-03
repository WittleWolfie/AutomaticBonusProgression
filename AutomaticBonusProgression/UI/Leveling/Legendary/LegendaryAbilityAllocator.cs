using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._VM.InfoWindow;
using Kingmaker.UI.MVVM._VM.Other;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using System;
using System.Linq;
using UniRx;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Wrapper view around CharGenAbilityScoreAllocatorPCView
  /// </summary>
  internal class LegendaryAbilityAllocatorView : ViewBase<LegendaryAbilityScoreAllocatorVM>, IHasTooltipTemplate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryAbilityAllocatorView));

    private CharGenAbilityScoreAllocatorPCView Allocator;

    internal void Init(CharGenAbilityScoreAllocatorPCView source)
    {
      Allocator = source;
    }

    public override void BindViewImplementation()
    {
      Allocator.m_LongName.SetText(ViewModel.Name);
      Allocator.m_ShortName.SetText(ViewModel.ShortName);

      Allocator.AddDisposable(ViewModel.Value.Subscribe(_ => UpdateAllocator()));
      Allocator.AddDisposable(ViewModel.CanAdd.Subscribe(Allocator.UpButton.SetInteractable));
      Allocator.AddDisposable(ViewModel.CanRemove.Subscribe(Allocator.DownButton.SetInteractable));
      Allocator.AddDisposable(ViewModel.Recommendation.Subscribe(Allocator.m_RecommendationMark.Bind));
      Allocator.AddDisposable(Allocator.UpButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryIncreaseValue()));
      Allocator.AddDisposable(Allocator.DownButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryDecreaseValue()));
    }

    public override void DestroyViewImplementation() { }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return ViewModel.TooltipTemplate();
    }

    private void UpdateAllocator()
    {
      Logger.Verbose(() => $"Updating allocator: {ViewModel.Name}");
      Allocator.m_Value.SetText(ViewModel.Value.Value.ToString());
      Allocator.m_Modifier.SetText(UIUtility.AddSign(ViewModel.Modifier.Value));
      ViewModel.TryShowTooltip();
    }
  }

  /// <summary>
  /// Replacement for the in-game CharGenAbilityScoreAllocatorVM, since that one is patched to work w/ Prowess which
  /// conflicts with the logic for Legendary Ability.
  /// </summary>
  internal class LegendaryAbilityScoreAllocatorVM : BaseDisposable, IViewModel, IHasTooltipTemplate
  {
    private readonly IntReactiveProperty AvailableGifts;
    private readonly InfoSectionVM InfoVM;
    private readonly LevelUpController LevelUpController;
    private readonly ReactiveProperty<ModifiableValue> Stat = new();
    private StatType Type => Stat.Value.Type;

    private int SpentGifts = 0;

    public LegendaryAbilityScoreAllocatorVM(
      StatType type,
      IntReactiveProperty availableGifts,
      InfoSectionVM infoVM,
      LevelUpController levelUpController)
    {
      AvailableGifts = availableGifts;
      InfoVM = infoVM;
      LevelUpController = levelUpController;
      Stat.ToSequentialReadOnlyReactiveProperty();
      Stat.Value = LevelUpController.Unit.Stats.GetStat(type);

      AddDisposable(Stat.Subscribe(_ => UpdateStats()));
      AddDisposable(AvailableGifts.Subscribe(_ => UpdateStats()));

      Name = LocalizedTexts.Instance.Stats.GetText(Type);
      ShortName = UIUtilityTexts.GetStatShortName(Type);
    }

    public override void DisposeImplementation() { }

    internal void SetRecommendationsForClass(ClassData classData)
    {
      var recommend = classData is not null && classData.RecommendedAttributes.Contains(Stat.Value.Type);
      if (Recommendation.Value is null)
        AddDisposable(Recommendation.Value = new(recommend));
      else
        Recommendation.Value.ChangeRecommendation(recommend);
    }

    internal void TryShowTooltip()
    {
      InfoVM.SetTemplate(TooltipTemplate());
    }

    // At least for now, copied from CharGenAbilityScoreAllocatorVM
    public TooltipBaseTemplate TooltipTemplate()
    {
      BlueprintArchetype archetype = null;
      if (LevelUpController.State.SelectedClass != null)
      {
        var classData = LevelUpController.Preview.Progression.GetClassData(LevelUpController.State.SelectedClass);
        archetype = classData?.Archetypes.FirstOrDefault();
      }
      Kingmaker.UI.MVVM._VM.Tooltip.Templates.ClassInformation classInformation = new();
      classInformation.Class = LevelUpController.State.SelectedClass;
      classInformation.Unit = LevelUpController.Preview.Descriptor;
      classInformation.Archetype = archetype;
      StatTooltipData statData = new StatTooltipData(Stat.Value);
      return new TooltipTemplateAbilityScoreAllocator(classInformation, statData);
    }

    internal void TryIncreaseValue()
    {
      if (!CanAdd.Value)
        return;

      // TODO: This way of tracking doesn't work, I do need to maintain some steady State.
      SpentGifts++;
      AvailableGifts.Value--;
      LevelUpController.AddAction(new SelectLegendaryAbility(Type));
    }

    internal void TryDecreaseValue()
    {
      if (!CanRemove.Value)
        return;

      SpentGifts--;
      AvailableGifts.Value++;
      LevelUpController.RemoveAction<SelectLegendaryAbility>(a => a.Attribute == Type);
    }

    private void UpdateStats()
    {
      CanAdd.Value = AvailableGifts.Value > 0;
      CanRemove.Value = SpentGifts > 0;

      Value.Value =
        Stat.Value.PermanentValue + ProwessPhaseVM.GetProwessBonus(Stat.Value) + GetLegendaryBonus(Stat.Value);
      Modifier.Value = (Value.Value - 10) / 2;
    }

    internal static int GetLegendaryBonus(ModifiableValue stat)
    {
      var inherent = stat.GetModifiers(ModifierDescriptor.Inherent);
      if (inherent is null)
        return 0;

      switch (stat.Type)
      {
        case StatType.Strength:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryStr).Sum(mod => mod.ModValue);
        case StatType.Dexterity:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryDex).Sum(mod => mod.ModValue);
        case StatType.Constitution:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryCon).Sum(mod => mod.ModValue);
        case StatType.Intelligence:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryInt).Sum(mod => mod.ModValue);
        case StatType.Wisdom:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryWis).Sum(mod => mod.ModValue);
        case StatType.Charisma:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryCha).Sum(mod => mod.ModValue);
      }
      return 0;
    }

    internal readonly string Name;
    internal readonly string ShortName;
    internal readonly IntReactiveProperty Value = new();
    internal readonly IntReactiveProperty Modifier = new();
    internal readonly BoolReactiveProperty CanAdd = new();
    internal readonly BoolReactiveProperty CanRemove = new();
    internal readonly ReactiveProperty<RecommendationMarkerVM> Recommendation = new();
  }
}
