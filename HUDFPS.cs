// Decompiled with JetBrains decompiler
// Type: HUDFPS
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class HUDFPS : MonoBehaviour
{
  private float deltaTime;

  private void Update() => this.deltaTime += (float) (((double) Time.deltaTime - (double) this.deltaTime) * 0.10000000149011612);

  private void OnGUI()
  {
    int width = Screen.width;
    int height = Screen.height;
    GUIStyle guiStyle = new GUIStyle();
    Rect position = new Rect(0.0f, 0.0f, (float) width, (float) (height * 2 / 100));
    guiStyle.alignment = TextAnchor.UpperLeft;
    guiStyle.fontSize = height * 2 / 50;
    guiStyle.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1f);
    string text = string.Format("{0:0.0} ms ({1:0.} fps)", (object) (this.deltaTime * 1000f), (object) (1f / this.deltaTime));
    GUIStyle style = guiStyle;
    GUI.Label(position, text, style);
  }
}
