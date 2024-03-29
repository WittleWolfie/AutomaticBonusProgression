﻿using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace AutomaticBonusProgression.UI
{
  /// <summary>
  /// General utilities for working w/ Unity UI, partially copied from BubbleGauntlet:
  /// https://github.com/factubsio/BubbleGauntlet/blob/main/BubbleGauntlet/UIHelpers.cs
  /// </summary>
  public static class UITool
  {
    /// <summary>
    /// Root game object for the static UI elements.
    /// </summary>
    public static GameObject StaticCanvas => Game.Instance.UI.Canvas.gameObject;

    /// <summary>
    /// Root game object for the main menu UI elements.
    /// </summary>
    public static GameObject MainMenu => Game.Instance.UI.MainMenu.gameObject;

    /// <summary>
    /// Root game object for FadeCanvas UI elements, e.g. Tooltips.
    /// </summary>
    public static GameObject FadeCanvas => Kingmaker.UI.FadeCanvas.Instance.gameObject;

    /// <summary>
    /// Returns strings for use w/ the UI (i.e. preffixed w/ "ABP.UI")
    /// </summary>
    public static string GetString(string key)
    {
      return LocalizationTool.GetString($"ABP.UI.{key}");
    }

    private static readonly string TitleDecoration =
      "<voffset=0em><font=\"Saber_Dist32\"><color=#672B31><size=140%>{0}</size></color></font></voffset>{1}";
    /// <summary>
    /// Decorates a title so the first text is red and stylized.
    /// </summary>
    public static void DecorateTitle(TextMeshProUGUI title)
    {
      title.text = string.Format(TitleDecoration, title.text.First(), title.text.Substring(1));
    }

    /// <summary>
    /// Returns the Transform matching the specified <paramref name="path"/>
    /// </summary>
    public static Transform ChildTransform(this GameObject obj, string path)
    {
      return obj.transform.Find(path);
    }

    /// <summary>
    /// Returns the GameObject matching the specified <paramref name="path"/>
    /// </summary>
    public static GameObject ChildObject(this GameObject obj, string path)
    {
      return obj.ChildTransform(path)?.gameObject;
    }

    /// <summary>
    /// Returns a list of child GameObjects matching the provided <paramref name="paths"/>.
    /// </summary>
    public static List<GameObject> ChildObjects(this GameObject obj, params string[] paths)
    {
      return paths.Select(p => obj.transform.Find(p)?.gameObject).ToList();
    }

    /// <summary>
    /// Calls <see cref="GameObject.Destroy"/> on all GameObjects matching the provided <paramref name="paths"/>
    /// </summary>
    public static void DestroyChildren(this GameObject obj, params string[] paths)
    {
      obj.ChildObjects(paths).ForEach(GameObject.Destroy);
    }

    /// <summary>
    /// Calls <see cref="GameObject.DestroyImmediate"/> on all GameObjects matching the provided <paramref name="paths"/>
    /// </summary>
    public static void DestroyChildrenImmediate(this GameObject obj, params string[] paths)
    {
      obj.ChildObjects(paths).ForEach(GameObject.DestroyImmediate);
    }

    /// <summary>
    /// Calls <see cref="GameObject.DestroyImmediate"/> on all components of type <typeparamref name="T"/>
    /// </summary>
    public static void DestroyComponents<T>(this GameObject obj) where T : UnityEngine.Object
    {
      obj.GetComponents<T>().ForEach(c => GameObject.DestroyImmediate(c));
    }

    /// <summary>
    /// Calls <see cref="GameObject.AddComponent(Type)"/> then executes <paramref name="build"/> on the new component
    /// </summary>
    public static T CreateComponent<T>(this GameObject obj, Action<T> build) where T : Component
    {
      var component = obj.AddComponent<T>();
      build(component);
      return component;
    }

    /// <summary>
    /// Invokes <paramref name="build"/> on the first component of type <typeparamref name="T"/> and returns it
    /// </summary>
    public static T EditComponent<T>(this GameObject obj, Action<T> build) where T : Component
    {
      var component = obj.GetComponent<T>();
      build(component);
      return component;
    }

    /// <summary>
    /// Returns the <see cref="RectTransform"/> of the GameObject matching the provided <paramref name="path"/>
    /// </summary>
    public static RectTransform ChildRect(this GameObject obj, string path)
    {
      return obj.ChildTransform(path) as RectTransform;
    }

    /// <summary>
    /// Returns the <see cref="RectTransform"/> of the GameObject
    /// </summary>
    public static RectTransform Rect(this GameObject obj)
    {
      return obj.transform as RectTransform;
    }

    /// <summary>
    /// Returns the <see cref="RectTransform"/> of the Transform
    /// </summary>
    public static RectTransform Rect(this Transform obj)
    {
      return obj as RectTransform;
    }

    /// <summary>
    /// Adds <paramref name="obj"/> as a child of <paramref name="parent"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// Resets the position, scale, and rotation. Usually this is what you want, but if you set something custom be
    /// sure to set after calling this.
    /// </remarks>
    public static void AddTo(this Transform obj, Transform parent)
    {
      obj.SetParent(parent);
      obj.localPosition = Vector3.zero;
      obj.localScale = Vector3.one;
      obj.localRotation = Quaternion.identity;
      obj.Rect().anchoredPosition = Vector3.zero;
    }

    /// <summary>
    /// Sets the sibling index to match the specified <paramref name="path"/> in the parent object
    /// </summary>
    public static void MakeSibling(this GameObject obj, string path)
    {
      obj.transform.SetSiblingIndex(obj.transform.parent.gameObject.ChildObject(path).transform.GetSiblingIndex());
    }

    /// <summary>
    /// Truncates strings and ends w/ ellipses
    /// </summary>
    public static string Truncate(this string value, int maxLength)
    {
      return value.Length <= maxLength ? value : $"{value.Substring(0, maxLength)}..."; 
    }
  }
}
