// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Customization.CustomizeNumberView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProEra.Game.UI.Screens.Shared.Customization
{
  public class CustomizeNumberView : MonoBehaviour
  {
    [SerializeField]
    private bool _saveInstantly;
    [SerializeField]
    private TMP_InputField m_numberTextInput;

    private PlayerProfile _playerProfile => SaveManager.GetPlayerProfile();

    private bool _isValid => !((Object) this._playerProfile == (Object) null) && !((Object) this.m_numberTextInput == (Object) null);

    private void Start()
    {
      if (!this._saveInstantly)
        return;
      this.m_numberTextInput.onValueChanged.AddListener(new UnityAction<string>(this.SaveUniformNumber));
    }

    private void OnEnable()
    {
      if (!this._isValid)
        return;
      this.m_numberTextInput.text = this._playerProfile.Customization.UniformNumber.Value.ToString();
    }

    private void OnDisable() => this.SaveUniformNumber(this.m_numberTextInput.text);

    private void OnDestroy() => this.m_numberTextInput.onValueChanged.RemoveAllListeners();

    public void SaveUniformNumber(string n)
    {
      string s = n;
      if (!this._isValid || s.Length <= 0)
        return;
      this._playerProfile.Customization.UniformNumber.SetValue(int.Parse(s));
      this._playerProfile.Customization.SetDirty();
    }
  }
}
