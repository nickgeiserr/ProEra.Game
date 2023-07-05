// Decompiled with JetBrains decompiler
// Type: PlayerCustomization
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Vars;

[Serializable]
public class PlayerCustomization : ISaveSync, IDisposable
{
  public static string FileName = "SavePlayerCustomization";
  protected SavePlayerCustomization saveData;
  public VariableUniform Uniform = new VariableUniform(ETeamUniformId.Ravens);
  public VariableInt UniformNumber = new VariableInt(5);
  public VariableBool HomeUniform = new VariableBool(true);
  public VariableGloveId GloveId = new VariableGloveId(EGlovesId.Default);
  public VariableInt MultiplayerTeamBallId = new VariableInt(-1);
  public VariableColor TrailColor = new VariableColor(Color.yellow);
  public VariableTrailType TrailType = new VariableTrailType(EBallTrail.CustomizePlayer_Trail_Default);
  public VariableColor NameColor = new VariableColor(Color.white);
  public VariableColor WristbandColor = new VariableColor(new Color(0.28f, 0.28f, 0.28f, 1f));
  public VariableString FirstName = new VariableString("Troy");
  public VariableString LastName = new VariableString("Jones");
  public VariableString UserName = new VariableString(nameof (UserName));
  public VariableFloat BodyHeight = new VariableFloat(1.7f);
  public VariableBool AvatarCustomized = new VariableBool(false);
  public VariableBodyType BodyType = new VariableBodyType(EBodyType.Male);
  public VariableInt BodyModelId = new VariableInt(0);
  public VariableInt HeadModelId = new VariableInt(0);
  public VariableInt FaceModelId = new VariableInt(0);
  public VariableString AvatarPresetId = new VariableString("default");
  public float HeightScale = 1f;
  public VariableBool IsNewCustomization = new VariableBool(true);
  private bool isDirty;

  ~PlayerCustomization()
  {
  }

  public void SetUniform(ETeamUniformId value) => this.Uniform.SetValue(value);

  public async Task Save()
  {
    PlayerCustomization target = this;
    if (target.saveData == null)
      target.saveData = new SavePlayerCustomization();
    target.saveData.ParseData(target);
    SaveSyncUtils.SetSaveTimeCreated(ref target.saveData.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref target.saveData.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref target.saveData.metaData);
    await SaveIO.SaveAsync<SavePlayerCustomization>(target.saveData, SaveIO.GetPath(PlayerCustomization.FileName));
  }

  public async Task Load()
  {
    PlayerCustomization target = this;
    Task<SavePlayerCustomization> loadedObject = SaveIO.LoadAsync<SavePlayerCustomization>(SaveIO.GetPath(PlayerCustomization.FileName));
    SavePlayerCustomization playerCustomization = await loadedObject;
    if (loadedObject.Result != null)
      loadedObject.Result.ParseTarget(target);
    target.saveData = loadedObject.Result;
    loadedObject = (Task<SavePlayerCustomization>) null;
  }

  public void Reset()
  {
    this.saveData = new SavePlayerCustomization();
    this.saveData.ParseTarget(this);
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value = true)
  {
    if (!value)
      return;
    AppEvents.SavePlayerCustomization.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.saveData.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.saveData.metaData);

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.saveData.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.saveData.metaData);

  public void Dispose() => GC.SuppressFinalize((object) this);
}
