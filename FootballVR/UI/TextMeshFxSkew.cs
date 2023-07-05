// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TextMeshFxSkew
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace FootballVR.UI
{
  public class TextMeshFxSkew : MonoBehaviour
  {
    [SerializeField]
    private TouchButton m_touchButton;
    [SerializeField]
    private TMP_Text m_textComponent;
    [SerializeField]
    private float m_skewFactor = 1.2f;
    [SerializeField]
    private float m_effectDuration = 0.2f;
    private EventHandle handle;
    private readonly RoutineHandle m_effectRoutine = new RoutineHandle();

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this.m_touchButton == (UnityEngine.Object) null))
        return;
      this.m_touchButton = this.GetComponent<TouchButton>();
    }

    private void Awake()
    {
      this.m_touchButton.Initialize();
      this.handle = this.m_touchButton.Highlighted.Link<bool>(new Action<bool>(this.HandleHighlight));
    }

    private void OnEnable() => this.handle.SetState(true);

    private void OnDisable() => this.handle.SetState(false);

    private void OnDestroy()
    {
      this.handle.Dispose();
      this.m_effectRoutine.Stop();
    }

    private void HandleHighlight(bool highlighted)
    {
      if (!(bool) this.m_touchButton.Interactable || (UnityEngine.Object) this.m_textComponent == (UnityEngine.Object) null)
        return;
      this.m_effectRoutine.Run(this.EffectRoutine(highlighted));
    }

    private IEnumerator EffectRoutine(bool highlighted)
    {
      float timer = 0.0f;
      while ((double) timer < (double) this.m_effectDuration)
      {
        timer += Time.unscaledDeltaTime;
        TextMeshFxSkew.ApplySkew(this.m_textComponent, this.m_skewFactor * (highlighted ? timer / this.m_effectDuration : (this.m_effectDuration - timer) / this.m_effectDuration));
        yield return (object) null;
      }
      TextMeshFxSkew.ApplySkew(this.m_textComponent, highlighted ? this.m_skewFactor : 0.0f);
    }

    private static void ApplySkew(TMP_Text a_textComponent, float a_skewMod)
    {
      a_textComponent.ForceMeshUpdate();
      UnityEngine.Mesh mesh = a_textComponent.mesh;
      Vector3[] vertices = mesh.vertices;
      TMP_TextInfo textInfo = a_textComponent.textInfo;
      int characterCount = textInfo.characterCount;
      int length = vertices.Length;
      for (int index1 = 0; index1 < characterCount; ++index1)
      {
        TMP_CharacterInfo tmpCharacterInfo = textInfo.characterInfo[index1];
        if (tmpCharacterInfo.isVisible)
        {
          int vertexIndex = tmpCharacterInfo.vertexIndex;
          int index2 = vertexIndex + 1;
          int index3 = vertexIndex + 2;
          Vector3 vector3 = Vector3.right * a_skewMod;
          if (index2 < length)
            vertices[index2] += vector3;
          if (index3 < length)
            vertices[index3] += vector3;
        }
      }
      mesh.vertices = vertices;
      a_textComponent.canvasRenderer.SetMesh(mesh);
    }
  }
}
