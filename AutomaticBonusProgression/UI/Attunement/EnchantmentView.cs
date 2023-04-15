using AutomaticBonusProgression.Components;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Root;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Spellbook.KnownSpells;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using System.Linq;
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
    internal static EnchantmentView Instantiate()
    {
      var transform = UnityEngine.Object.Instantiate(Prefabs.Enchantment).transform;

      var spellView = transform.GetComponent<SpellbookKnownSpellPCView>();
      var view = transform.gameObject.CreateComponent<EnchantmentView>(
        view =>
        {
          view.Icon = spellView.m_Icon;
          view.Background = spellView.m_BackgroundImage;
          view.Button = spellView.m_Button;

          view.Name = spellView.m_SpellNameLabel;
          view.Requirements = spellView.m_SchoolNameLabel;

          view.CostLabel = spellView.m_SpellLevelLabel;
        });

      transform.gameObject.DestroyComponents<SpellbookKnownSpellPCView>();
      return view;
    }

    private Image Icon;
    private Image Background;
    private OwlcatButton Button;

    private TextMeshProUGUI Name;
    private TextMeshProUGUI Requirements;

    private TextMeshProUGUI CostLabel;

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);

      AddDisposable(Button.OnLeftClickAsObservable().Subscribe(_ => OnClick()));
      AddDisposable(ViewModel.CurrentState.Subscribe(_ => UpdateState()));

      Icon.sprite = ViewModel.Icon;

      Name.SetText(ViewModel.Name);
      Requirements.SetText(ViewModel.Requirements);

      CostLabel.SetText(ViewModel.Cost.ToString());
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
        case EnchantmentVM.State.Incompatible:
          Background.color = UIRoot.Instance.SpellBookColors.OppositeSlotcolor;
          break;
      }
    }
  }

  internal class EnchantmentVM : BaseDisposable, IViewModel
  {
    internal enum State
    {
      Available,
      Active,
      Unaffordable, // i.e. Not enough enhancement bonus
      Incompatible, // i.e. Can't be used on the equipped item type
    }

    internal EnchantmentVM(BlueprintBuff enchantment, int cost, params ArmorProficiencyGroup[] armorProficiencies)
    {
      Icon = enchantment.Icon;

      Name = enchantment.Name;

      if (armorProficiencies.Any())
      {
        var proficiencyText = armorProficiencies.Select(LocalizedTexts.Instance.Stats.GetText);
        Requirements = string.Format(UITool.GetString("Attunement.Requirements"), string.Join(", ", proficiencyText));
      }

      Cost = cost;
      CurrentState.Value = State.Available;
    }

    public override void DisposeImplementation() { }

    internal void Select()
    {
      if (CurrentState.Value == State.Available)
        CurrentState.Value = State.Active;
      else if (CurrentState.Value == State.Active)
        CurrentState.Value = State.Available;
    }

    internal Sprite Icon;

    internal string Name;
    internal string Requirements;

    internal int Cost;

    internal readonly ReactiveProperty<State> CurrentState = new();
  }
}
