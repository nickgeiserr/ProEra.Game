// Decompiled with JetBrains decompiler
// Type: TB12.UI.CustomizeTrailView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework;
using System;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class CustomizeTrailView : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private ColorPicker _colorPicker;
    [SerializeField]
    private CircularLayout _scrollLayout;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    private BallTrailStore _store;
    [SerializeField]
    private BallTrail _previewTrail;
    [SerializeField]
    private Transform _ballTrailTx;
    [SerializeField]
    private CircularLayoutItem _itemPrefab;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private Vector2 fixedUpdateDelay = new Vector2(0.0f, 20f);

    public CircularLayoutItem ItemPrefab => this._itemPrefab;

    public int itemCount => this._store.TrailCount;

    private void FixedUpdate()
    {
      ++this.fixedUpdateDelay.x;
      if ((double) this.fixedUpdateDelay.x <= (double) this.fixedUpdateDelay.y)
        return;
      this.fixedUpdateDelay.x = 0.0f;
      this.ShowPreview((EBallTrail) (Variable<EBallTrail>) this._playerProfile.Customization.TrailType);
    }

    private void OnEnable()
    {
      this.WillAppear();
      this.DidAppear();
    }

    private void OnDisable() => this.WillDisappear();

    private void WillAppear()
    {
      this._colorPicker.SetColor((Color) this._playerProfile.Customization.TrailColor);
      this._scrollLayout.Initialize();
      int index = this._store.GetIndex((EBallTrail) (Variable<EBallTrail>) this._playerProfile.Customization.TrailType);
      if (index < 0)
        return;
      this._scrollLayout.CurrentIndex = index;
    }

    private void DidAppear()
    {
      this._colorPicker.OnColorChanged += new Action<Color>(this.HandleColor);
      this._scrollLayout.OnCurrentIndexChanged += new Action<int>(this.HandleCurrentIndexChanged);
      this.ShowPreview((EBallTrail) (Variable<EBallTrail>) this._playerProfile.Customization.TrailType);
    }

    private void WillDisappear()
    {
      this._scrollLayout.OnCurrentIndexChanged -= new Action<int>(this.HandleCurrentIndexChanged);
      this._colorPicker.OnColorChanged -= new Action<Color>(this.HandleColor);
      this._playerProfile.Customization.SetDirty();
    }

    private void HandleCurrentIndexChanged(int currIndex)
    {
      EBallTrail trailType = this._store.GetTrailType(currIndex);
      this._playerProfile.Customization.TrailType.SetValue(trailType);
      this.ShowPreview(trailType);
    }

    private void ShowPreview(EBallTrail trailType)
    {
      if ((UnityEngine.Object) this._previewTrail != (UnityEngine.Object) null)
        this._store.ReturnTrail(this._previewTrail);
      this._previewTrail = this._store.GetTrail(trailType);
      this._previewTrail.transform.SetParent(this._ballTrailTx);
      this._previewTrail.transform.ResetTransform(true, false);
      this._previewTrail.TrailColor = (Color) this._playerProfile.Customization.TrailColor;
      this._previewTrail.TrailRenderer.Clear();
      this._previewTrail.TrailRenderer.time = 200000f;
      this._previewTrail.TrailEnabled = true;
      Vector3 position = this._previewTrail.transform.position;
      Vector3 vector3 = position + this._previewTrail.transform.forward * 0.5f;
      this._previewTrail.TrailRenderer.Clear();
      this._previewTrail.TrailRenderer.AddPositions(new Vector3[2]
      {
        vector3,
        position
      });
      this._previewTrail.TrailRenderer.sortingOrder = 2;
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      EBallTrail trailType = this._store.GetTrailType(itemIndex);
      CircularTextItem circularTextItem = (CircularTextItem) item;
      circularTextItem.IsLocalized = true;
      circularTextItem.localizationText = trailType.ToString();
    }

    private void HandleColor(Color color) => this._playerProfile.Customization.TrailColor.SetValue(color);
  }
}
