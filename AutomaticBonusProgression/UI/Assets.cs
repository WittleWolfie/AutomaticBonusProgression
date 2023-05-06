using AutomaticBonusProgression.Util;
using Kingmaker.BundlesLoading;
using System.Collections.Generic;
using UnityEngine;

namespace AutomaticBonusProgression.UI
{
  internal class Assets
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Assets));

    private static Sprite _okayIcon;
    internal static Sprite OkayIcon
    {
      get
      {
        _okayIcon ??= LoadAsset<Sprite>("ui", "UI_QuestNotification_IconOk");
        return _okayIcon;
      }
    }

    private static readonly Dictionary<string, UnityEngine.Object> LoadedAssets = new();

    private static T LoadAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
      var key = $"{bundleName}-{assetName}";
      if (LoadedAssets.ContainsKey(key))
        return LoadedAssets[key] as T;

      Logger.Verbose(() => $"Attempting to load {assetName} from {bundleName}");

      var bundle = BundlesLoadService.Instance.RequestBundle(bundleName);

      foreach (var asset in bundle.LoadAllAssets())
        LoadedAssets[$"{bundleName}-{asset.name}"] = asset;

      return LoadedAssets[key] as T;
    }
  }
}
