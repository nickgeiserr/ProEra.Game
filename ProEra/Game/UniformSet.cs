// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UniformSet
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ProEra.Game
{
  public class UniformSet
  {
    public int teamIndex;
    private List<Texture2D> helmetTextures;
    private List<Texture2D> jerseyTextures;
    private List<Texture2D> pantTextures;
    private List<string> uniformNames;
    private List<string> helmetNames;
    private List<string> jerseyNames;
    private List<string> pantNames;
    private int chosenUniform;
    private int chosenHelmet;
    private int chosenJersey;
    private int chosenPants;
    private Dictionary<string, UniformConfig> allUniforms;

    public int GetTeamIndex() => this.teamIndex;

    public UniformSet(TextAsset[] uniformTextFiles, Texture2D[] allTextures, int _teamIndex)
    {
      this.teamIndex = _teamIndex;
      this.InitializeAllCollections();
      this.AssignUniformTexts(this.ConvertTextAssetArrayToStringArray(uniformTextFiles));
      this.AssignUniformTextures(allTextures);
      this.LoadCustomBaseUniforms();
    }

    public UniformSet(
      string[] uniformTextFiles,
      Dictionary<string, Texture2D> allTextures,
      int _teamIndex)
    {
      this.teamIndex = _teamIndex;
      this.InitializeAllCollections();
      this.AssignUniformTexts(uniformTextFiles);
      this.AssignUniformTextures(allTextures);
      this.LoadCustomBaseUniforms();
    }

    private void InitializeAllCollections()
    {
      this.helmetTextures = new List<Texture2D>();
      this.jerseyTextures = new List<Texture2D>();
      this.pantTextures = new List<Texture2D>();
      this.uniformNames = new List<string>();
      this.helmetNames = new List<string>();
      this.jerseyNames = new List<string>();
      this.pantNames = new List<string>();
      this.allUniforms = new Dictionary<string, UniformConfig>();
    }

    private void AssignUniformTexts(string[] uniformTextFiles)
    {
      for (int index = 0; index < uniformTextFiles.Length; ++index)
      {
        Dictionary<string, string> dictionary = AssetManager.ReadProperties(uniformTextFiles[index]);
        string key = dictionary["UNIFORM_NAME"];
        if (!this.uniformNames.Contains(key))
        {
          UniformConfig uniformConfig = new UniformConfig();
          uniformConfig.HelmetName = dictionary["HELMET_NAME"];
          uniformConfig.JerseyName = dictionary["JERSEY_NAME"];
          uniformConfig.PantName = dictionary["PANT_NAME"];
          uniformConfig.NumberFontIndex = dictionary["NUMBER_FONT_INDEX"];
          uniformConfig.NumberFillColor = dictionary["NUMBER_FILL_COLOR"];
          uniformConfig.NumberOutline1Color = dictionary["NUMBER_OUTLINE1_COLOR"];
          uniformConfig.NumberOutline2Color = dictionary["NUMBER_OUTLINE2_COLOR"];
          uniformConfig.LetterFontIndex = dictionary["LETTER_FONT_INDEX"];
          uniformConfig.LetterFillColor = dictionary["LETTER_FILL_COLOR"];
          uniformConfig.LetterOutlineColor = dictionary["LETTER_OUTLINE_COLOR"];
          uniformConfig.VisorColor = dictionary["VISOR_COLOR"];
          uniformConfig.ArmSleeveColor = dictionary["ARM_SLEEVE_COLOR"];
          uniformConfig.ArmBandColor = dictionary["ARM_BAND_COLOR"];
          uniformConfig.HasSleeveNumber = dictionary["HAS_SLEEVE_NUMBERS"];
          uniformConfig.HasShoulderNumber = dictionary["HAS_SHOULDER_NUMBERS"];
          uniformConfig.HelmetType = dictionary["HELMET_TYPE"];
          uniformConfig.IsCustomBaseUniform = false;
          this.uniformNames.Add(key);
          this.allUniforms.Add(key, uniformConfig);
        }
      }
    }

    private void LoadCustomBaseUniforms()
    {
      if (this.teamIndex >= TeamAssetManager.NUMBER_OF_BASE_TEAMS)
        return;
      int num = PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets ? 1 : 0;
    }

    private void AssignUniformTextures(Texture2D[] allTextures)
    {
      for (int index = 0; index < allTextures.Length; ++index)
      {
        switch (allTextures[index].name.Substring(0, allTextures[index].name.IndexOf("_")))
        {
          case "helmets":
            this.helmetNames.Add(allTextures[index].name.Substring(allTextures[index].name.IndexOf("_") + 1));
            this.helmetTextures.Add(allTextures[index]);
            break;
          case "jerseys":
            this.jerseyNames.Add(allTextures[index].name.Substring(allTextures[index].name.IndexOf("_") + 1));
            this.jerseyTextures.Add(allTextures[index]);
            break;
          case "pants":
            this.pantNames.Add(allTextures[index].name.Substring(allTextures[index].name.IndexOf("_") + 1));
            this.pantTextures.Add(allTextures[index]);
            break;
        }
      }
    }

    private void AssignUniformTextures(Dictionary<string, Texture2D> allTextures)
    {
      foreach (KeyValuePair<string, Texture2D> allTexture in allTextures)
      {
        switch (allTexture.Key.Substring(0, allTexture.Key.IndexOf("_")))
        {
          case "helmets":
            this.helmetNames.Add(allTexture.Key.Substring(allTexture.Key.IndexOf("_") + 1));
            this.helmetTextures.Add(allTexture.Value);
            continue;
          case "jerseys":
            this.jerseyNames.Add(allTexture.Key.Substring(allTexture.Key.IndexOf("_") + 1));
            this.jerseyTextures.Add(allTexture.Value);
            continue;
          case "pants":
            this.pantNames.Add(allTexture.Key.Substring(allTexture.Key.IndexOf("_") + 1));
            this.pantTextures.Add(allTexture.Value);
            continue;
          default:
            continue;
        }
      }
    }

    public string[] GetNamesOfUniformPieces(UniformPiece type)
    {
      switch (type)
      {
        case UniformPiece.Pants:
          return this.pantNames.ToArray();
        case UniformPiece.Jerseys:
          return this.jerseyNames.ToArray();
        case UniformPiece.Helmets:
          return this.helmetNames.ToArray();
        default:
          return (string[]) null;
      }
    }

    public Texture2D GetHelmetTexture(int pieceIndex) => this.helmetTextures[pieceIndex];

    public Texture2D GetJerseyTexture(int pieceIndex) => this.jerseyTextures[pieceIndex];

    public Texture2D GetPantTexture(int pieceIndex) => this.pantTextures[pieceIndex];

    public int GetNumberOfUniforms() => this.allUniforms.Count;

    public int GetNumberOfUniformPieces(UniformPiece type)
    {
      switch (type)
      {
        case UniformPiece.Pants:
          return this.pantNames.Count;
        case UniformPiece.Jerseys:
          return this.jerseyNames.Count;
        case UniformPiece.Helmets:
          return this.helmetNames.Count;
        default:
          Debug.Log((object) ("Unexpected UniformPiece Type:" + type.ToString()));
          return 0;
      }
    }

    public string GetUniformNameByIndex(int uniformIndex) => this.uniformNames[uniformIndex];

    public int GetUniformIndexByName(string uniformName)
    {
      for (int index = 0; index < this.uniformNames.Count; ++index)
      {
        if (this.uniformNames[index] == uniformName)
          return index;
      }
      return 0;
    }

    public string GetUniformPieceNameByIndex(UniformPiece type, int pieceIndex)
    {
      switch (type)
      {
        case UniformPiece.Pants:
          return this.pantNames[pieceIndex];
        case UniformPiece.Jerseys:
          return this.jerseyNames[pieceIndex];
        case UniformPiece.Helmets:
          return this.helmetNames[pieceIndex];
        default:
          return "UNKNOWN";
      }
    }

    private int GetUniformPieceIndexByName(UniformPiece type, string uniformPieceName)
    {
      switch (type)
      {
        case UniformPiece.Pants:
          for (int index = 0; index < this.pantNames.Count; ++index)
          {
            if (this.pantNames[index].ToUpper() == uniformPieceName.Replace(" ", "_").ToUpper())
              return index;
          }
          break;
        case UniformPiece.Jerseys:
          for (int index = 0; index < this.jerseyNames.Count; ++index)
          {
            if (this.jerseyNames[index].ToUpper() == uniformPieceName.Replace(" ", "_").ToUpper())
              return index;
          }
          break;
        case UniformPiece.Helmets:
          for (int index = 0; index < this.helmetNames.Count; ++index)
          {
            if (this.helmetNames[index].ToUpper() == uniformPieceName.Replace(" ", "_").ToUpper())
              return index;
          }
          break;
      }
      return 0;
    }

    public int GetPieceForUniformSet(int uniformIndex, UniformPiece type)
    {
      string uniformNameByIndex = this.GetUniformNameByIndex(uniformIndex);
      string uniformPieceName = "";
      switch (type)
      {
        case UniformPiece.Pants:
          uniformPieceName = this.allUniforms[uniformNameByIndex].PantName;
          break;
        case UniformPiece.Jerseys:
          uniformPieceName = this.allUniforms[uniformNameByIndex].JerseyName;
          break;
        case UniformPiece.Helmets:
          uniformPieceName = this.allUniforms[uniformNameByIndex].HelmetName;
          break;
      }
      return this.GetUniformPieceIndexByName(type, uniformPieceName);
    }

    public UniformConfig GetUniformConfig(int uniformIndex) => this.allUniforms[this.GetUniformNameByIndex(uniformIndex)];

    private Dictionary<UniformPiece, string> GetDefaultPiecesForUniform(string uniformName) => new Dictionary<UniformPiece, string>()
    {
      {
        UniformPiece.Pants,
        this.allUniforms[uniformName].PantName
      },
      {
        UniformPiece.Jerseys,
        this.allUniforms[uniformName].JerseyName
      },
      {
        UniformPiece.Helmets,
        this.allUniforms[uniformName].HelmetName
      }
    };

    public string[] GetNamesOfUniforms() => this.uniformNames.ToArray();

    public void SetUniformConfig(int uniformIndex, UniformConfig config) => this.allUniforms[this.GetUniformNameByIndex(uniformIndex)] = config;

    public void LockInUniformPieces(
      int uniformIndex,
      int helmetIndex,
      int jerseyIndex,
      int pantIndex)
    {
      this.chosenUniform = uniformIndex;
      this.chosenHelmet = helmetIndex;
      this.chosenJersey = jerseyIndex;
      this.chosenPants = pantIndex;
    }

    public int GetLockedInUniform() => this.chosenUniform;

    public int GetLockedInUniformPiece(UniformPiece type)
    {
      switch (type)
      {
        case UniformPiece.Pants:
          return this.chosenPants;
        case UniformPiece.Jerseys:
          return this.chosenJersey;
        case UniformPiece.Helmets:
          return this.chosenHelmet;
        default:
          return 0;
      }
    }

    public void ClearUnusedTextures(int helmetIndex, int jerseyIndex, int pantIndex)
    {
      if (this.helmetTextures != null)
      {
        for (int index = 0; index < this.helmetTextures.Count; ++index)
        {
          if (index != helmetIndex)
            this.helmetTextures[index] = (Texture2D) null;
        }
      }
      if (this.jerseyTextures != null)
      {
        for (int index = 0; index < this.jerseyTextures.Count; ++index)
        {
          if (index != jerseyIndex)
            this.jerseyTextures[index] = (Texture2D) null;
        }
      }
      if (this.pantTextures != null)
      {
        for (int index = 0; index < this.pantTextures.Count; ++index)
        {
          if (index != pantIndex)
            this.pantTextures[index] = (Texture2D) null;
        }
      }
      Resources.UnloadUnusedAssets();
    }

    public void ClearAllTextures()
    {
      if (this.helmetTextures != null)
      {
        for (int index = 0; index < this.helmetTextures.Count; ++index)
          this.helmetTextures[index] = (Texture2D) null;
      }
      if (this.jerseyTextures != null)
      {
        for (int index = 0; index < this.jerseyTextures.Count; ++index)
          this.jerseyTextures[index] = (Texture2D) null;
      }
      if (this.pantTextures != null)
      {
        for (int index = 0; index < this.pantTextures.Count; ++index)
          this.pantTextures[index] = (Texture2D) null;
      }
      Resources.UnloadUnusedAssets();
    }

    public void ClearNonLockedTextures() => this.ClearUnusedTextures(this.GetLockedInUniformPiece(UniformPiece.Helmets), this.GetLockedInUniformPiece(UniformPiece.Jerseys), this.GetLockedInUniformPiece(UniformPiece.Pants));

    private string[] ConvertTextAssetArrayToStringArray(TextAsset[] texts)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < texts.Length; ++index)
        stringList.Add(texts[index].text);
      return stringList.ToArray();
    }

    public void AppendUniform(UniformConfig u)
    {
      this.allUniforms.Add(u.UniformSetName, u);
      this.uniformNames.Add(u.UniformSetName);
    }
  }
}
