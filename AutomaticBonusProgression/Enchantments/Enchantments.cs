using AutomaticBonusProgression.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Enchantments
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Enchantments));

    internal static void Configure()
    {
      Logger.Log($"Configuring enchantments");

      EnergyResistance.Configure();
    }
  }
}
