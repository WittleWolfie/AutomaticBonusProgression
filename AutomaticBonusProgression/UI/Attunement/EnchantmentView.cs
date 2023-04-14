using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI.Attunement
{
  internal class EnchantmentView : ViewBase<EnchantmentVM>
  {
    // TODO: For text stuff, Bubbles says I either need to get Font from prefab or load from bundle directly

    private TextMeshProUGUI Name;
    private Image Frame;

    private Image Icon;
    private Image IconBorder;

    private TextMeshProUGUI Cost;
    private Image CostFrame;

    // TODO: Find a texture then highlight it, see bubble buffs highlight
    private Image Highlight;

    public override void BindViewImplementation()
    {
      throw new NotImplementedException();
    }

    public override void DestroyViewImplementation()
    {
      throw new NotImplementedException();
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

    public override void DisposeImplementation()
    {
      throw new NotImplementedException();
    }

    internal Sprite Icon;
    internal string Name;
    internal int Cost;
    internal State CurrentState;
  }
}
