// Decompiled with JetBrains decompiler
// Type: ControllerButtonSwapper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ControllerButtonSwapper : MonoBehaviour
{
  public static ControllerButtonSwapper instance;
  [Header("In-Game Textures")]
  [SerializeField]
  private Texture2D controllerSelect;
  [SerializeField]
  private Texture2D bumpBothColored;
  [SerializeField]
  private Texture2D bumpBoth;
  [SerializeField]
  private Texture2D bumpLeftOnField;
  [SerializeField]
  private Texture2D bumpLeftColored;
  [SerializeField]
  private Texture2D bumpLeft;
  [SerializeField]
  private Texture2D bumpRightOnField;
  [SerializeField]
  private Texture2D bumpRightColored;
  [SerializeField]
  private Texture2D bumpRight;
  [SerializeField]
  private Texture2D action1OnField;
  [SerializeField]
  private Texture2D action1Colored;
  [SerializeField]
  private Texture2D action1;
  [SerializeField]
  private Texture2D action2OnField;
  [SerializeField]
  private Texture2D action2Colored;
  [SerializeField]
  private Texture2D action2;
  [SerializeField]
  private Texture2D action3OnField;
  [SerializeField]
  private Texture2D action3Colored;
  [SerializeField]
  private Texture2D action3;
  [SerializeField]
  private Texture2D action4OnField;
  [SerializeField]
  private Texture2D action4Colored;
  [SerializeField]
  private Texture2D action4;
  [SerializeField]
  private Texture2D startColored;
  [SerializeField]
  private Texture2D start;
  [SerializeField]
  private Texture2D triggerBothColored;
  [SerializeField]
  private Texture2D triggerBoth;
  [SerializeField]
  private Texture2D triggerLeftOnField;
  [SerializeField]
  private Texture2D triggerLeftColored;
  [SerializeField]
  private Texture2D triggerLeft;
  [SerializeField]
  private Texture2D triggerRightOnField;
  [SerializeField]
  private Texture2D triggerRightColored;
  [SerializeField]
  private Texture2D triggerRight;
  [SerializeField]
  private Texture2D player1;
  [SerializeField]
  private Texture2D player2;
  [SerializeField]
  private Texture2D controllerLayout_OffBeforeSnap;
  [SerializeField]
  private Texture2D controllerLayout_RunControls;
  [SerializeField]
  private Texture2D controllerLayout_PassControls;
  [SerializeField]
  private Texture2D controllerLayout_DefBeforeSnap;
  [SerializeField]
  private Texture2D controllerLayout_DefActivePlay;
  [Header("Xbox Icons")]
  [SerializeField]
  private Texture2D style1_controllerSelect;
  [SerializeField]
  private Texture2D style1_bumpBothColored;
  [SerializeField]
  private Texture2D style1_bumpBoth;
  [SerializeField]
  private Texture2D style1_bumpLeftOnField;
  [SerializeField]
  private Texture2D style1_bumpLeftColored;
  [SerializeField]
  private Texture2D style1_bumpLeft;
  [SerializeField]
  private Texture2D style1_bumpRightOnField;
  [SerializeField]
  private Texture2D style1_bumpRightColored;
  [SerializeField]
  private Texture2D style1_bumpRight;
  [SerializeField]
  private Texture2D style1_action1OnField;
  [SerializeField]
  private Texture2D style1_action1Colored;
  [SerializeField]
  private Texture2D style1_action1;
  [SerializeField]
  private Texture2D style1_action2OnField;
  [SerializeField]
  private Texture2D style1_action2Colored;
  [SerializeField]
  private Texture2D style1_action2;
  [SerializeField]
  private Texture2D style1_action3OnField;
  [SerializeField]
  private Texture2D style1_action3Colored;
  [SerializeField]
  private Texture2D style1_action3;
  [SerializeField]
  private Texture2D style1_action4OnField;
  [SerializeField]
  private Texture2D style1_action4Colored;
  [SerializeField]
  private Texture2D style1_action4;
  [SerializeField]
  private Texture2D style1_startColored;
  [SerializeField]
  private Texture2D style1_start;
  [SerializeField]
  private Texture2D style1_triggerBothColored;
  [SerializeField]
  private Texture2D style1_triggerBoth;
  [SerializeField]
  private Texture2D style1_triggerLeftOnField;
  [SerializeField]
  private Texture2D style1_triggerLeftColored;
  [SerializeField]
  private Texture2D style1_triggerLeft;
  [SerializeField]
  private Texture2D style1_triggerRightOnField;
  [SerializeField]
  private Texture2D style1_triggerRightColored;
  [SerializeField]
  private Texture2D style1_triggerRight;
  [SerializeField]
  private Texture2D style1_player1;
  [SerializeField]
  private Texture2D style1_player2;
  [SerializeField]
  private Texture2D style1_controllerLayout_OffBeforeSnap;
  [SerializeField]
  private Texture2D style1_controllerLayout_RunControls;
  [SerializeField]
  private Texture2D style1_controllerLayout_PassControls;
  [SerializeField]
  private Texture2D style1_controllerLayout_DefBeforeSnap;
  [SerializeField]
  private Texture2D style1_controllerLayout_DefActivePlay;
  [Header("PS4 Icons")]
  [SerializeField]
  private Texture2D style2_controllerSelect;
  [SerializeField]
  private Texture2D style2_bumpBothColored;
  [SerializeField]
  private Texture2D style2_bumpBoth;
  [SerializeField]
  private Texture2D style2_bumpLeftOnField;
  [SerializeField]
  private Texture2D style2_bumpLeftColored;
  [SerializeField]
  private Texture2D style2_bumpLeft;
  [SerializeField]
  private Texture2D style2_bumpRightOnField;
  [SerializeField]
  private Texture2D style2_bumpRightColored;
  [SerializeField]
  private Texture2D style2_bumpRight;
  [SerializeField]
  private Texture2D style2_action1OnField;
  [SerializeField]
  private Texture2D style2_action1Colored;
  [SerializeField]
  private Texture2D style2_action1;
  [SerializeField]
  private Texture2D style2_action2OnField;
  [SerializeField]
  private Texture2D style2_action2Colored;
  [SerializeField]
  private Texture2D style2_action2;
  [SerializeField]
  private Texture2D style2_action3OnField;
  [SerializeField]
  private Texture2D style2_action3Colored;
  [SerializeField]
  private Texture2D style2_action3;
  [SerializeField]
  private Texture2D style2_action4OnField;
  [SerializeField]
  private Texture2D style2_action4Colored;
  [SerializeField]
  private Texture2D style2_action4;
  [SerializeField]
  private Texture2D style2_startColored;
  [SerializeField]
  private Texture2D style2_start;
  [SerializeField]
  private Texture2D style2_triggerBothColored;
  [SerializeField]
  private Texture2D style2_triggerBoth;
  [SerializeField]
  private Texture2D style2_triggerLeftOnField;
  [SerializeField]
  private Texture2D style2_triggerLeftColored;
  [SerializeField]
  private Texture2D style2_triggerLeft;
  [SerializeField]
  private Texture2D style2_triggerRightOnField;
  [SerializeField]
  private Texture2D style2_triggerRightColored;
  [SerializeField]
  private Texture2D style2_triggerRight;
  [SerializeField]
  private Texture2D style2_player1;
  [SerializeField]
  private Texture2D style2_player2;
  [SerializeField]
  private Texture2D style2_controllerLayout_OffBeforeSnap;
  [SerializeField]
  private Texture2D style2_controllerLayout_RunControls;
  [SerializeField]
  private Texture2D style2_controllerLayout_PassControls;
  [SerializeField]
  private Texture2D style2_controllerLayout_DefBeforeSnap;
  [SerializeField]
  private Texture2D style2_controllerLayout_DefActivePlay;

  private void Awake()
  {
    if ((Object) ControllerButtonSwapper.instance == (Object) null)
    {
      ControllerButtonSwapper.instance = this;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  public void ChangeToStyle(int style)
  {
    if (style == 1)
      this.ChangeToStyle1();
    else
      this.ChangeToStyle2();
  }

  private void ChangeToStyle1()
  {
    this.controllerSelect.LoadRawTextureData(this.style1_controllerSelect.GetRawTextureData());
    this.bumpBothColored.LoadRawTextureData(this.style1_bumpBothColored.GetRawTextureData());
    this.bumpBoth.LoadRawTextureData(this.style1_bumpBoth.GetRawTextureData());
    this.bumpLeftOnField.LoadRawTextureData(this.style1_bumpLeftOnField.GetRawTextureData());
    this.bumpLeftColored.LoadRawTextureData(this.style1_bumpLeftColored.GetRawTextureData());
    this.bumpLeft.LoadRawTextureData(this.style1_bumpLeft.GetRawTextureData());
    this.bumpRightOnField.LoadRawTextureData(this.style1_bumpRightOnField.GetRawTextureData());
    this.bumpRightColored.LoadRawTextureData(this.style1_bumpRightColored.GetRawTextureData());
    this.bumpRight.LoadRawTextureData(this.style1_bumpRight.GetRawTextureData());
    this.action1OnField.LoadRawTextureData(this.style1_action1OnField.GetRawTextureData());
    this.action1Colored.LoadRawTextureData(this.style1_action1Colored.GetRawTextureData());
    this.action1.LoadRawTextureData(this.style1_action1.GetRawTextureData());
    this.action2OnField.LoadRawTextureData(this.style1_action2OnField.GetRawTextureData());
    this.action2Colored.LoadRawTextureData(this.style1_action2Colored.GetRawTextureData());
    this.action2.LoadRawTextureData(this.style1_action2.GetRawTextureData());
    this.action3OnField.LoadRawTextureData(this.style1_action3OnField.GetRawTextureData());
    this.action3Colored.LoadRawTextureData(this.style1_action3Colored.GetRawTextureData());
    this.action3.LoadRawTextureData(this.style1_action3.GetRawTextureData());
    this.action4OnField.LoadRawTextureData(this.style1_action4OnField.GetRawTextureData());
    this.action4Colored.LoadRawTextureData(this.style1_action4Colored.GetRawTextureData());
    this.action4.LoadRawTextureData(this.style1_action4.GetRawTextureData());
    this.startColored.LoadRawTextureData(this.style1_startColored.GetRawTextureData());
    this.start.LoadRawTextureData(this.style1_start.GetRawTextureData());
    this.triggerBothColored.LoadRawTextureData(this.style1_triggerBothColored.GetRawTextureData());
    this.triggerBoth.LoadRawTextureData(this.style1_triggerBoth.GetRawTextureData());
    this.triggerLeftOnField.LoadRawTextureData(this.style1_triggerLeftOnField.GetRawTextureData());
    this.triggerLeftColored.LoadRawTextureData(this.style1_triggerLeftColored.GetRawTextureData());
    this.triggerLeft.LoadRawTextureData(this.style1_triggerLeft.GetRawTextureData());
    this.triggerRightOnField.LoadRawTextureData(this.style1_triggerRightOnField.GetRawTextureData());
    this.triggerRightColored.LoadRawTextureData(this.style1_triggerRightColored.GetRawTextureData());
    this.triggerRight.LoadRawTextureData(this.style1_triggerRight.GetRawTextureData());
    this.player1.LoadRawTextureData(this.style1_player1.GetRawTextureData());
    this.player2.LoadRawTextureData(this.style1_player2.GetRawTextureData());
    this.controllerLayout_OffBeforeSnap.LoadRawTextureData(this.style1_controllerLayout_OffBeforeSnap.GetRawTextureData());
    this.controllerLayout_RunControls.LoadRawTextureData(this.style1_controllerLayout_RunControls.GetRawTextureData());
    this.controllerLayout_PassControls.LoadRawTextureData(this.style1_controllerLayout_PassControls.GetRawTextureData());
    this.controllerLayout_DefBeforeSnap.LoadRawTextureData(this.style1_controllerLayout_DefBeforeSnap.GetRawTextureData());
    this.controllerLayout_DefActivePlay.LoadRawTextureData(this.style1_controllerLayout_DefActivePlay.GetRawTextureData());
    this.ApplyNewButtonTextureData();
  }

  private void ChangeToStyle2()
  {
    this.controllerSelect.LoadRawTextureData(this.style2_controllerSelect.GetRawTextureData());
    this.bumpBothColored.LoadRawTextureData(this.style2_bumpBothColored.GetRawTextureData());
    this.bumpBoth.LoadRawTextureData(this.style2_bumpBoth.GetRawTextureData());
    this.bumpLeftOnField.LoadRawTextureData(this.style2_bumpLeftOnField.GetRawTextureData());
    this.bumpLeftColored.LoadRawTextureData(this.style2_bumpLeftColored.GetRawTextureData());
    this.bumpLeft.LoadRawTextureData(this.style2_bumpLeft.GetRawTextureData());
    this.bumpRightOnField.LoadRawTextureData(this.style2_bumpRightOnField.GetRawTextureData());
    this.bumpRightColored.LoadRawTextureData(this.style2_bumpRightColored.GetRawTextureData());
    this.bumpRight.LoadRawTextureData(this.style2_bumpRight.GetRawTextureData());
    this.action1OnField.LoadRawTextureData(this.style2_action1OnField.GetRawTextureData());
    this.action1Colored.LoadRawTextureData(this.style2_action1Colored.GetRawTextureData());
    this.action1.LoadRawTextureData(this.style2_action1.GetRawTextureData());
    this.action2OnField.LoadRawTextureData(this.style2_action2OnField.GetRawTextureData());
    this.action2Colored.LoadRawTextureData(this.style2_action2Colored.GetRawTextureData());
    this.action2.LoadRawTextureData(this.style2_action2.GetRawTextureData());
    this.action3OnField.LoadRawTextureData(this.style2_action3OnField.GetRawTextureData());
    this.action3Colored.LoadRawTextureData(this.style2_action3Colored.GetRawTextureData());
    this.action3.LoadRawTextureData(this.style2_action3.GetRawTextureData());
    this.action4OnField.LoadRawTextureData(this.style2_action4OnField.GetRawTextureData());
    this.action4Colored.LoadRawTextureData(this.style2_action4Colored.GetRawTextureData());
    this.action4.LoadRawTextureData(this.style2_action4.GetRawTextureData());
    this.startColored.LoadRawTextureData(this.style2_startColored.GetRawTextureData());
    this.start.LoadRawTextureData(this.style2_start.GetRawTextureData());
    this.triggerBothColored.LoadRawTextureData(this.style2_triggerBothColored.GetRawTextureData());
    this.triggerBoth.LoadRawTextureData(this.style2_triggerBoth.GetRawTextureData());
    this.triggerLeftOnField.LoadRawTextureData(this.style2_triggerLeftOnField.GetRawTextureData());
    this.triggerLeftColored.LoadRawTextureData(this.style2_triggerLeftColored.GetRawTextureData());
    this.triggerLeft.LoadRawTextureData(this.style2_triggerLeft.GetRawTextureData());
    this.triggerRightOnField.LoadRawTextureData(this.style2_triggerRightOnField.GetRawTextureData());
    this.triggerRightColored.LoadRawTextureData(this.style2_triggerRightColored.GetRawTextureData());
    this.triggerRight.LoadRawTextureData(this.style2_triggerRight.GetRawTextureData());
    this.player1.LoadRawTextureData(this.style2_player1.GetRawTextureData());
    this.player2.LoadRawTextureData(this.style2_player2.GetRawTextureData());
    this.controllerLayout_OffBeforeSnap.LoadRawTextureData(this.style2_controllerLayout_OffBeforeSnap.GetRawTextureData());
    this.controllerLayout_RunControls.LoadRawTextureData(this.style2_controllerLayout_RunControls.GetRawTextureData());
    this.controllerLayout_PassControls.LoadRawTextureData(this.style2_controllerLayout_PassControls.GetRawTextureData());
    this.controllerLayout_DefBeforeSnap.LoadRawTextureData(this.style2_controllerLayout_DefBeforeSnap.GetRawTextureData());
    this.controllerLayout_DefActivePlay.LoadRawTextureData(this.style2_controllerLayout_DefActivePlay.GetRawTextureData());
    this.ApplyNewButtonTextureData();
  }

  private void ApplyNewButtonTextureData()
  {
    this.controllerSelect.Apply();
    this.bumpBothColored.Apply();
    this.bumpBoth.Apply();
    this.bumpLeftOnField.Apply();
    this.bumpLeftColored.Apply();
    this.bumpLeft.Apply();
    this.bumpRightOnField.Apply();
    this.bumpRightColored.Apply();
    this.bumpRight.Apply();
    this.action1OnField.Apply();
    this.action1Colored.Apply();
    this.action1.Apply();
    this.action2OnField.Apply();
    this.action2Colored.Apply();
    this.action2.Apply();
    this.action3OnField.Apply();
    this.action3Colored.Apply();
    this.action3.Apply();
    this.action4OnField.Apply();
    this.action4Colored.Apply();
    this.action4.Apply();
    this.start.Apply();
    this.triggerBothColored.Apply();
    this.triggerBoth.Apply();
    this.triggerLeftOnField.Apply();
    this.triggerLeftColored.Apply();
    this.triggerLeft.Apply();
    this.triggerRightOnField.Apply();
    this.triggerRightColored.Apply();
    this.triggerRight.Apply();
    this.player1.Apply();
    this.player2.Apply();
    this.controllerLayout_OffBeforeSnap.Apply();
    this.controllerLayout_RunControls.Apply();
    this.controllerLayout_PassControls.Apply();
    this.controllerLayout_DefBeforeSnap.Apply();
    this.controllerLayout_DefActivePlay.Apply();
  }
}
