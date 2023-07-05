// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.TextureBlender
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public interface TextureBlender
  {
    bool DoesShaderNameMatch(string shaderName);

    void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName);

    Color OnBlendTexturePixel(string shaderPropertyName, Color pixelColor);

    bool NonTexturePropertiesAreEqual(Material a, Material b);

    void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial);

    Color GetColorIfNoTexture(Material m, ShaderTextureProperty texPropertyName);
  }
}
