// Decompiled with JetBrains decompiler
// Type: PortraitManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.IO;
using UnityEngine;

public class PortraitManager : MonoBehaviour
{
  public static PortraitManager self;
  public static int NUMBER_OF_COACH_PORTRAITS_PER_SKIN_TYPE = 65;
  public static int NUMBER_OF_PLAYER_PORTRAITS_PER_SKIN_TYPE = 99;
  public Sprite[] playerPortraits;
  public Sprite[] userTeamPortraits;
  public Sprite[] compTeamPortraits;
  private Sprite _sprite;

  private void Awake()
  {
    if ((Object) PortraitManager.self == (Object) null)
    {
      this.ClearTeamPlayerPortraits();
      Object.DontDestroyOnLoad((Object) this.gameObject);
      PortraitManager.self = this;
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  private void Start()
  {
  }

  public void ClearTeamPlayerPortraits()
  {
    this.userTeamPortraits = new Sprite[TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER];
    this.compTeamPortraits = new Sprite[TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER];
  }

  public Sprite GetPlayerPortrait(PlayerData player, bool onUserTeam)
  {
    int indexOnTeam = player.IndexOnTeam;
    Mathf.Clamp(player.SkinColor, 1, 6);
    int num = player.PortraitID % PortraitManager.NUMBER_OF_PLAYER_PORTRAITS_PER_SKIN_TYPE;
    if (onUserTeam)
    {
      if ((Object) this.userTeamPortraits[indexOnTeam] != (Object) null)
        return this.userTeamPortraits[indexOnTeam];
    }
    else if ((Object) this.compTeamPortraits[indexOnTeam] != (Object) null)
      return this.compTeamPortraits[indexOnTeam];
    Sprite playerPortrait = this.LoadPlayerPortrait(player);
    if (onUserTeam)
      this.userTeamPortraits[indexOnTeam] = playerPortrait;
    else
      this.compTeamPortraits[indexOnTeam] = playerPortrait;
    return playerPortrait;
  }

  private Sprite LoadPlayerPortraitFromAssetBundle(int skinValue, int portraitID)
  {
    skinValue = Mathf.Clamp(skinValue, 1, 6);
    portraitID %= PortraitManager.NUMBER_OF_PLAYER_PORTRAITS_PER_SKIN_TYPE;
    this._sprite = AddressablesData.instance.LoadAssetSync<Sprite>(AddressablesData.CorrectingAssetKey("player_portraits/skin_tone_" + skinValue.ToString()), portraitID.ToString());
    return this._sprite;
  }

  public Sprite LoadPlayerPortrait(PlayerData player) => this.LoadPlayerPortrait(Mathf.Clamp(player.SkinColor, 1, 6), player.PortraitID);

  public Sprite LoadPlayerPortrait(int skinValue, int portraitID)
  {
    string str1 = ModManager.appPath + ModManager.modPath + "Player Portraits/Skin Tone " + skinValue.ToString() + "/";
    string str2 = portraitID.ToString() + ".png";
    if (!AssetManager.UseModsAssets || !File.Exists(str1 + str2))
      return this.LoadPlayerPortraitFromAssetBundle(skinValue, portraitID);
    Texture2D texture2D = new Texture2D(128, 128, TextureFormat.DXT5, false);
    byte[] data = File.ReadAllBytes(str1 + str2);
    texture2D.filterMode = FilterMode.Bilinear;
    texture2D.LoadImage(data);
    texture2D.Compress(true);
    texture2D.anisoLevel = 0;
    return Sprite.Create(texture2D, new Rect(0.0f, 0.0f, 128f, 128f), new Vector2(0.0f, 1f), 1f);
  }

  public Sprite LoadCoachPortrait(int skinValue, int portraitID, int age)
  {
    skinValue = skinValue > 3 ? 2 : 1;
    portraitID %= PortraitManager.NUMBER_OF_COACH_PORTRAITS_PER_SKIN_TYPE;
    int num = Mathf.Clamp((age - 30) / 10, 0, 3);
    string str1 = ModManager.appPath + ModManager.modPath + "Coach Portraits/Skin Tone " + skinValue.ToString() + "/";
    string str2 = portraitID.ToString() + "_" + num.ToString() + ".png";
    if (AssetManager.UseModsAssets && File.Exists(str1 + str2))
    {
      Texture2D texture2D = new Texture2D(128, 128, TextureFormat.DXT5, false);
      byte[] data = File.ReadAllBytes(str1 + str2);
      texture2D.filterMode = FilterMode.Bilinear;
      texture2D.LoadImage(data);
      texture2D.Compress(true);
      texture2D.anisoLevel = 0;
      return Sprite.Create(texture2D, new Rect(0.0f, 0.0f, 128f, 128f), new Vector2(0.0f, 1f), 1f);
    }
    string original = "coach_portraits/skin_tone_" + skinValue.ToString();
    string filename = portraitID.ToString() + "_" + num.ToString();
    return AddressablesData.instance.LoadAssetSync<Sprite>(AddressablesData.CorrectingAssetKey(original), filename);
  }
}
