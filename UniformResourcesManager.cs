// Decompiled with JetBrains decompiler
// Type: UniformResourcesManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using UnityEngine;

public class UniformResourcesManager : MonoBehaviour
{
  internal static UniformSet LoadUniformSet(int teamIndex)
  {
    string assetKey = AddressablesData.CorrectingAssetKey("teamuniforms/team_" + teamIndex.ToString());
    return new UniformSet(AddressablesData.instance.LoadAssetsSync<TextAsset>(assetKey), AddressablesData.instance.LoadAssetsSync<Texture2D>(assetKey), teamIndex);
  }

  public static void UnloadUniform(int teamIndex) => AssetBundle.UnloadAllAssetBundles(false);

  public static string GetPieceNamePrefix(UniformPiece uniType)
  {
    string pieceNamePrefix = "";
    switch (uniType)
    {
      case UniformPiece.Pants:
        pieceNamePrefix = "pants";
        break;
      case UniformPiece.Jerseys:
        pieceNamePrefix = "jerseys";
        break;
      case UniformPiece.Helmets:
        pieceNamePrefix = "helmets";
        break;
    }
    return pieceNamePrefix;
  }
}
