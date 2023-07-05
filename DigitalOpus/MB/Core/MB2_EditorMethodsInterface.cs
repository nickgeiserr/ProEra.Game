// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB2_EditorMethodsInterface
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public interface MB2_EditorMethodsInterface
  {
    void Clear();

    void RestoreReadFlagsAndFormats(ProgressUpdateDelegate progressInfo);

    void SetReadWriteFlag(Texture2D tx, bool isReadable, bool addToList);

    void AddTextureFormat(Texture2D tx, bool isNormalMap);

    void SaveAtlasToAssetDatabase(
      Texture2D atlas,
      ShaderTextureProperty texPropertyName,
      int atlasNum,
      Material resMat);

    void SetMaterialTextureProperty(
      Material target,
      ShaderTextureProperty texPropName,
      string texturePath);

    void SetNormalMap(Texture2D tx);

    bool IsNormalMap(Texture2D tx);

    string GetPlatformString();

    void SetTextureSize(Texture2D tx, int size);

    bool IsCompressed(Texture2D tx);

    void CheckBuildSettings(long estimatedAtlasSize);

    bool CheckPrefabTypes(MB_ObjsToCombineTypes prefabType, List<GameObject> gos);

    bool ValidateSkinnedMeshes(List<GameObject> mom);

    void CommitChangesToAssets();

    void OnPreTextureBake();

    void OnPostTextureBake();

    void Destroy(Object o);
  }
}
