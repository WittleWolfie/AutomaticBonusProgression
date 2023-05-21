using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Spellbook.KnownSpells;
using Kingmaker.UI.MVVM._VM.Tooltip.Utils;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI.Attunement
{
  /// <summary>
  /// Partial copy of SpellbookKnownSpellPCView
  /// </summary>
  internal class EnchantmentView : ViewBase<EnchantmentVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnchantmentView));

    internal static EnchantmentView Instantiate()
    {
      var transform = GameObject.Instantiate(Prefabs.Enchantment).transform;

      var spellView = transform.GetComponent<SpellbookKnownSpellPCView>();
      var view = transform.gameObject.CreateComponent<EnchantmentView>(
        view =>
        {
          view.Icon = spellView.m_Icon;
          view.Background = spellView.m_BackgroundImage;
          view.Button = spellView.m_Button;

          view.Name = spellView.m_SpellNameLabel;
          view.Requirements = spellView.m_SchoolNameLabel;
          view.Requirements.enableWordWrapping = false;
        });

      transform.gameObject.DestroyComponents<SpellbookKnownSpellPCView>();
      return view;
    }

    private Image Icon;
    private Image Background;
    private OwlcatButton Button;

    private TextMeshProUGUI Name;
    private TextMeshProUGUI Requirements;

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);

      AddDisposable(Button.OnLeftClickAsObservable().Subscribe(_ => OnClick()));
      AddDisposable(ViewModel.CurrentState.Subscribe(_ => UpdateState()));

      Icon.sprite = ViewModel.Icon;

      Name.SetText(ViewModel.Name);
      Requirements.SetText(ViewModel.Requirements);

      this.SetTooltip(ViewModel.Tooltip);
    }

    public override void DestroyViewImplementation()
    {
      gameObject.SetActive(false);
    }

    private void OnClick()
    {
      ViewModel.Select();
    }

    private void UpdateState()
    {
      switch (ViewModel.CurrentState.Value)
      {
        case EnchantmentVM.State.Available:
          Background.color = UIRoot.Instance.SpellBookColors.DefaultSlotColot;
          break;
        case EnchantmentVM.State.Active:
          Background.color = UIRoot.Instance.SpellBookColors.FavoriteSlotColor;
          break;
        case EnchantmentVM.State.Unaffordable:
        case EnchantmentVM.State.Unavailable:
          Background.color = UIRoot.Instance.SpellBookColors.OppositeSlotcolor;
          break;
      }
    }
  }

  internal class EnchantmentVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnchantmentVM));

    private static readonly Dictionary<string, TooltipBaseTemplate> Tooltips = new();

    internal enum State
    {
      Available,
      Active,
      Unaffordable, // i.e. Not enough enhancement bonus
      Unavailable, // i.e. Can't be used on the equipped item type
    }

    internal EnchantmentVM(
      BlueprintBuff enchantment, AttunementEffect effect, UnitEntityData unit)
    {
      Enchant = enchantment;
      Icon = Enchant.Icon;
      Name = Enchant.Name;
      Cost = effect.Cost;
      Unit = unit;

      var key = Enchant.AssetGuid.ToString();
      if (!Tooltips.TryGetValue(key, out Tooltip))
      {
        Tooltip = new TooltipTemplateEnchantment(Enchant);
        Tooltips[key] = Tooltip;
      }

      EffectComponent = Enchant.GetComponent<AttunementEffect>();
      var requirementsList = EffectComponent.GetRequirements();
      if (!string.IsNullOrEmpty(requirementsList))
        Requirements = requirementsList;

      if (!EffectComponent.IsAvailable(Unit))
        CurrentState.Value = State.Unavailable;
      else if (unit.HasFact(Enchant))
        TempApply(active: true);
      else if (!Unit.Ensure<UnitPartEnhancement>().CanAddTemp(Cost))
        CurrentState.Value = State.Unaffordable;
      else
        CurrentState.Value = State.Available;

      if (CurrentState.Value != State.Unavailable)
        AddDisposable(Unit.Ensure<UnitPartEnhancement>().TempEnhancement.Subscribe(_ => Refresh()));
    }

    public override void DisposeImplementation() { }

    internal void Select()
    {
      if (CurrentState.Value == State.Available)
        TempApply();
      else if (CurrentState.Value == State.Active)
        TempRemove();
    }

    private void Refresh()
    {
      if (CurrentState.Value == State.Active || CurrentState.Value == State.Unavailable)
        return;

      // Only update the effect, nothing else should change on refresh
      var canAfford = Unit.Ensure<UnitPartEnhancement>().CanAddTemp(Cost);
      if (canAfford && CurrentState.Value == State.Unaffordable)
        CurrentState.Value = State.Available;
      else if (!canAfford && CurrentState.Value == State.Available)
        CurrentState.Value = State.Unaffordable;
    }

    private void TempApply(bool active = false)
    {
      Unit.Ensure<UnitPartEnhancement>().AddTemp(Cost, active);
      CurrentState.Value = State.Active;
    }

    private void TempRemove()
    {
      Unit.Ensure<UnitPartEnhancement>().RemoveTemp(Cost);
      CurrentState.Value = State.Available;
    }

    internal Sprite Icon;

    internal string Name;
    internal string Requirements;

    internal int Cost;

    internal TooltipBaseTemplate Tooltip;

    internal readonly ReactiveProperty<State> CurrentState = new();

    private readonly AttunementEffect EffectComponent;
    private readonly UnitEntityData Unit;
    internal readonly BlueprintBuff Enchant;
  }
}
