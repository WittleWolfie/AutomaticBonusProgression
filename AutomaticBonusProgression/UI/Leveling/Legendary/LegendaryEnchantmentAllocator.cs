using AutomaticBonusProgression.Util;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._VM.InfoWindow;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using TMPro;
using UniRx;
using UnityEngine.EventSystems;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Wrapper view around CharGenAbilityScoreAllocatorPCView for legendary armor / weapon gifts.
  /// </summary>
  internal class LegendaryEnchantmentAllocatorView : ViewBase<LegendaryEnchantmentScoreAllocatorVM>, IPointerEnterHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryEnchantmentAllocatorView));

    private CharGenAbilityScoreAllocatorPCView Allocator;
    private TextMeshProUGUI CostLabel;

    internal void Init(CharGenAbilityScoreAllocatorPCView source)
    {
      Allocator = source;
    }

    public override void BindViewImplementation()
    {
      Allocator.m_LongName.SetText(ViewModel.Name);
      Allocator.m_ShortName.SetText(ViewModel.ShortName);
      
      AddDisposable(
        ViewModel.MaxEnchantment.Subscribe(value => Allocator.m_Value.SetText(value.ToString())));

      AddDisposable(ViewModel.CanAddEnchantment.Subscribe(UpdateCanAdd));
      AddDisposable(ViewModel.CanRemoveEnchantment.Subscribe(UpdateCanRemove));

      AddDisposable(
        Allocator.UpButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryIncreaseEnchantment()));
      AddDisposable(
        Allocator.DownButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryDecreaseEnchantment()));
    }

    public override void DestroyViewImplementation() { }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return null;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
      ViewModel.TryShowTooltip();
    }
  }

  internal class LegendaryEnchantmentScoreAllocatorVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryEnchantmentScoreAllocatorVM));

    private readonly EnchantmentType Type;
    private readonly LegendaryGiftState State;
    private readonly InfoSectionVM InfoVM;

    public LegendaryEnchantmentScoreAllocatorVM(EnchantmentType type, LegendaryGiftState state, InfoSectionVM infoVM)
    {
      Type = type;
      State = state;
      InfoVM = infoVM;

      AddDisposable(State.Controller.UpdateCommand.Subscribe(_ => UpdateValues()));
      // Unclear why this is needed but it is
      UpdateValues();
      AddDisposable(State.AvailableGifts.Subscribe(_ => UpdateEligibility()));

      Name = UITool.GetString($"Legendary.{type}");
      ShortName = UITool.GetString($"{type}");
    }

    public override void DisposeImplementation() { }

    internal void TryShowTooltip()
    {
      InfoVM.SetTemplate(TooltipTemplate());
    }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return new TooltipTemplateLegendaryEnchantment(Type);
    }

    internal void TryIncreaseEnchantment()
    {
      State.TryAddLegendaryEnchantment(Type);
    }

    internal void TryDecreaseEnchantment()
    {
      State.TryRemoveLegendaryEnchantment(Type);
    }

    private void UpdateValues()
    {
      MaxEnchantment.Value = State.GetMaxEnchantment(Type);
    }

    private void UpdateEligibility()
    {
      CanAddEnchantment.Value = State.CanAddLegendaryEnchantment(Type);
      CanRemoveEnchantment.Value = State.CanRemoveLegendaryEnchantment(Type);
    }

    internal readonly string Name;
    internal readonly string ShortName;

    internal readonly IntReactiveProperty MaxEnchantment = new();
    internal readonly BoolReactiveProperty CanAddEnchantment = new();
    internal readonly BoolReactiveProperty CanRemoveEnchantment = new();
  }
}
