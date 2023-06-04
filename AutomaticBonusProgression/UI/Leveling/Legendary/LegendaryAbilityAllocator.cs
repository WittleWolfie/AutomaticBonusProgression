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
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
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
      Logger.Log($"Binding {ViewModel.Name}");
      Allocator.m_LongName.SetText(ViewModel.Name);
      Allocator.m_ShortName.SetText(ViewModel.ShortName);

      Allocator.AddDisposable(
        ViewModel.StatValue.Subscribe(value => Allocator.m_Value.SetText(value.ToString())));
      Allocator.AddDisposable(
        ViewModel.Modifier.Subscribe(value => Allocator.m_Modifier.SetText(UIUtility.AddSign(value))));
      Allocator.AddDisposable(ViewModel.CanAdd.Subscribe(UpdateCanAdd));
      Allocator.AddDisposable(ViewModel.CanRemove.Subscribe(UpdateCanRemove));
      Allocator.AddDisposable(ViewModel.Recommendation.Subscribe(Allocator.m_RecommendationMark.Bind));
      Allocator.AddDisposable(
        Allocator.UpButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryIncreaseValue()));
      Allocator.AddDisposable(
        Allocator.DownButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryDecreaseValue()));
    }

    public override void DestroyViewImplementation() { }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return ViewModel.TooltipTemplate();
    }

    private void UpdateCanAdd(bool canAdd)
    {
      Allocator.UpButton.SetInteractable(canAdd);
      Allocator.m_AddCost.SetText(canAdd ? "-1" : string.Empty, syncTextInputBox: true);
    }

    private void UpdateCanRemove(bool canRemove)
    {
      Allocator.DownButton.SetInteractable(canRemove);
      Allocator.m_RemoveCost.SetText(canRemove ? "+1" : string.Empty, syncTextInputBox: true);
    }
  }

  /// <summary>
  /// Replacement for the in-game CharGenAbilityScoreAllocatorVM, since that one is patched to work w/ Prowess which
  /// conflicts with the logic for Legendary Ability.
  /// </summary>
  internal class LegendaryAbilityScoreAllocatorVM : BaseDisposable, IViewModel, IHasTooltipTemplate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryAbilityScoreAllocatorVM));

    private readonly ReactiveProperty<ModifiableValue> Stat = new();
    private readonly StatType Type;

    private readonly LegendaryGiftState State;
    private readonly InfoSectionVM InfoVM;

    public LegendaryAbilityScoreAllocatorVM(StatType type, LegendaryGiftState state, InfoSectionVM infoVM)
    {
      Type = type;
      State = state;
      InfoVM = infoVM;

      Stat.ToSequentialReadOnlyReactiveProperty();
      Stat.Value = State.Controller.Preview.Stats.GetStat(Type);

      AddDisposable(Stat.Subscribe(_ => UpdateValues()));
      AddDisposable(State.AvailableGifts.Subscribe(_ => UpdateCanAddRemove()));

      Name = LocalizedTexts.Instance.Stats.GetText(Type);
      ShortName = UIUtilityTexts.GetStatShortName(Type);
    }

    public override void DisposeImplementation() { }

    // TODO: Somehow when you add two stats at once, then remove one it doesn't remove the actions but it _does_
    // remove the fact from the character. Maybe I need to refactor to a single action that applies X.
    internal void UpdateStat()
    {
      var stat = State.Controller.Preview.Stats.GetStat(Type);
      string log = $"Updating {Type}: ";
      foreach (var mod in stat.Modifiers)
        log += $"[{mod.ModValue}:{mod.ModDescriptor}]";
      Logger.Log(log);
      Stat.SetValueAndForceNotify(State.Controller.Preview.Stats.GetStat(Type));
    }

    internal void SetRecommendationsForClass(ClassData classData)
    {
      var recommend = classData is not null && classData.RecommendedAttributes.Contains(Type);
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
      if (State.Controller.State.SelectedClass != null)
      {
        var classData = State.Controller.Preview.Progression.GetClassData(State.Controller.State.SelectedClass);
        archetype = classData?.Archetypes.FirstOrDefault();
      }
      Kingmaker.UI.MVVM._VM.Tooltip.Templates.ClassInformation classInformation = new();
      classInformation.Class = State.Controller.State.SelectedClass;
      classInformation.Unit = State.Controller.Preview.Descriptor;
      classInformation.Archetype = archetype;
      StatTooltipData statData = new StatTooltipData(State.Controller.Preview.Stats.GetStat(Type));
      return new TooltipTemplateAbilityScoreAllocator(classInformation, statData);
    }

    internal void TryIncreaseValue()
    {
      State.TryAddLegendaryAbility(Type);
    }

    internal void TryDecreaseValue()
    {
      State.TryRemoveLegendaryAbility(Type);
    }

    private void UpdateValues()
    {
      StatValue.Value = Stat.Value.PermanentValue + ProwessPhaseVM.GetProwessBonus(Stat.Value) + GetLegendaryBonus(Stat.Value);
      Modifier.Value = (StatValue.Value - 10) / 2;
    }

    private void UpdateCanAddRemove()
    {
      CanAdd.Value = State.CanAddLegendaryAbility(Type);
      CanRemove.Value = State.CanRemoveLegendaryAbility(Type);
    }

    internal static int GetLegendaryBonus(ModifiableValue stat)
    {
      var inherent = stat.GetModifiers(ModifierDescriptor.Inherent);
      Logger.Log($"Legendary Bonus {stat.Type}: {inherent?.Count() ?? 0}");
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
    internal readonly IntReactiveProperty StatValue = new();
    internal readonly IntReactiveProperty Modifier = new();
    internal readonly BoolReactiveProperty CanAdd = new();
    internal readonly BoolReactiveProperty CanRemove = new();
    internal readonly ReactiveProperty<RecommendationMarkerVM> Recommendation = new();
  }
}
