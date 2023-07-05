// Decompiled with JetBrains decompiler
// Type: FootballWorld.BakeUniformsTexts
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace FootballWorld
{
  [ExecuteInEditMode]
  public class BakeUniformsTexts : MonoBehaviour
  {
    private UniformStore uniformStore;
    [Space(10f)]
    [SerializeField]
    private AtlasPattern[] _atlasPatterns;
    private int ps4_TextureRes = 512;
    private int default_TextureRes = 2048;
    private int android_TextureRes = 2048;

    public void Awake()
    {
      this.uniformStore = SaveManager.GetUniformStore();
      for (int index = 0; index < this._atlasPatterns.Length; ++index)
      {
        if ((Object) this._atlasPatterns[index].GetCamera() != (Object) null)
        {
          this._atlasPatterns[index].GetCamera().targetTexture.width = this.android_TextureRes;
          this._atlasPatterns[index].GetCamera().targetTexture.height = this.android_TextureRes;
        }
      }
    }

    public void CreateLabels(AtlasType value) => this.CreateLabels(this.GetAtlasPattern(value));

    public void CreateLabels(AtlasPattern targetAtlasPattern)
    {
      this.ResetLabels(targetAtlasPattern);
      TextMeshProUGUI templateLabel = targetAtlasPattern.GetTemplateLabel();
      List<TextMeshProUGUI> preparedLabels = targetAtlasPattern.GetPreparedLabels();
      templateLabel.enabled = true;
      for (int index1 = 0; (double) index1 < (double) targetAtlasPattern.GetSheetSize().x; ++index1)
      {
        for (int index2 = 0; (double) index2 < (double) targetAtlasPattern.GetSheetSize().y; ++index2)
        {
          TextMeshProUGUI textMeshProUgui = Object.Instantiate<TextMeshProUGUI>(templateLabel);
          Transform transform = textMeshProUgui.transform;
          transform.parent = templateLabel.transform.parent;
          transform.localScale = Vector3.one;
          transform.localRotation = Quaternion.Euler(Vector3.zero);
          transform.localPosition = Vector3.zero;
          textMeshProUgui.rectTransform.anchoredPosition = new Vector2((float) index2 * templateLabel.rectTransform.sizeDelta.x, (float) -index1 * templateLabel.rectTransform.sizeDelta.y);
          preparedLabels.Add(textMeshProUgui);
        }
      }
      templateLabel.enabled = false;
    }

    private void SetupLabelValue(
      AtlasType atlasType,
      int index,
      string value,
      UniformFontConfig config)
    {
      AtlasPattern atlasPattern = this.GetAtlasPattern(atlasType);
      if (atlasPattern == null)
        return;
      Color fillColor = config.FillColor.Item1 ? config.FillColor.Item2 : Color.white;
      Color outlineColor = config.OutlineColor.Item1 ? config.OutlineColor.Item2 : Color.white;
      this.SetColorsForLabel(atlasPattern.GetPreparedLabels()[index], fillColor, outlineColor);
      atlasPattern.GetPreparedLabels()[index].SetText(value.ToUpper());
    }

    public void PrepareLabel(
      AtlasType atlasType,
      int index,
      string value,
      UniformFontConfig config)
    {
      AtlasPattern atlasPattern = this.GetAtlasPattern(atlasType);
      if (atlasPattern == null)
        return;
      TextMeshProUGUI templateLabel = atlasPattern.GetTemplateLabel();
      TextMeshProUGUI preparedLabel = atlasPattern.GetPreparedLabels()[index];
      preparedLabel.margin = new Vector4(templateLabel.margin.x + config.PixelsOffset.x, templateLabel.margin.y - config.PixelsOffset.y, templateLabel.margin.y, templateLabel.margin.w);
      preparedLabel.fontSize = templateLabel.fontSize * config.SizeMultiplier;
      if ((Object) config.Font == (Object) null)
      {
        if ((Object) preparedLabel.font != (Object) templateLabel.font)
          preparedLabel.font = templateLabel.font;
      }
      else
        atlasPattern.GetPreparedLabels()[index].font = config.Font;
      this.SetupLabelValue(atlasType, index, value, config);
    }

    private void SetColorsForLabel(TextMeshProUGUI target, Color fillColor, Color outlineColor)
    {
      if (!((Object) target != (Object) null))
        return;
      if (target.fontMaterial.HasProperty(ShaderUtilities.ID_OutlineColor))
        target.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, outlineColor);
      if (!target.fontMaterial.HasProperty(ShaderUtilities.ID_FaceColor))
        return;
      target.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, fillColor);
    }

    private AtlasPattern GetAtlasPattern(AtlasType type)
    {
      foreach (AtlasPattern atlasPattern in this._atlasPatterns)
      {
        if (atlasPattern != null && atlasPattern.GetAtlasType() == type)
          return atlasPattern;
      }
      return (AtlasPattern) null;
    }

    public Texture2D[] PrepareTextures()
    {
      Texture2D[] texture2DArray = new Texture2D[this._atlasPatterns.Length];
      for (int index = 0; index < texture2DArray.Length; ++index)
      {
        int defaultTextureRes = this.default_TextureRes;
        int mipCount = 12;
        texture2DArray[index] = new Texture2D(defaultTextureRes, defaultTextureRes, GraphicsFormat.R8G8B8A8_UNorm, mipCount, TextureCreationFlags.MipChain);
        texture2DArray[index].anisoLevel = 2;
      }
      return texture2DArray;
    }

    public void BakeTextures(Texture2D[] textures)
    {
      if (textures == null || textures.Length == 0)
        textures = this.PrepareTextures();
      int index = -1;
      foreach (AtlasPattern atlasPattern in this._atlasPatterns)
      {
        ++index;
        if (atlasPattern != null)
          this.BakeTexture(atlasPattern, textures[index]);
      }
    }

    private void BakeTexture(AtlasPattern targetAtlasPattern, Texture2D newTexture)
    {
      if (targetAtlasPattern == null)
        return;
      RenderTexture active = RenderTexture.active;
      Camera camera = targetAtlasPattern.GetCamera();
      RenderTexture.active = camera.targetTexture;
      camera.Render();
      RenderTexture targetTexture = camera.targetTexture;
      targetTexture.GenerateMips();
      Graphics.CopyTexture((Texture) targetTexture, (Texture) newTexture);
      RenderTexture.active = active;
    }

    private void ResetLabels(AtlasPattern targetAtlasPattern)
    {
      foreach (TextMeshProUGUI preparedLabel in targetAtlasPattern.GetPreparedLabels())
      {
        if (!((Object) preparedLabel == (Object) null))
        {
          if (Application.isPlaying)
            Object.Destroy((Object) preparedLabel);
          else
            Object.DestroyImmediate((Object) preparedLabel);
        }
      }
      targetAtlasPattern.Reset();
    }
  }
}
