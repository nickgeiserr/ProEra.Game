// Decompiled with JetBrains decompiler
// Type: UDB.TKSwipeDirection
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

namespace UDB
{
  [Flags]
  public enum TKSwipeDirection
  {
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
    UpLeft = 16, // 0x00000010
    DownLeft = 32, // 0x00000020
    UpRight = 64, // 0x00000040
    DownRight = 128, // 0x00000080
    Horizontal = Right | Left, // 0x00000003
    Vertical = Down | Up, // 0x0000000C
    Cardinal = Vertical | Horizontal, // 0x0000000F
    DiagonalUp = UpRight | UpLeft, // 0x00000050
    DiagonalDown = DownRight | DownLeft, // 0x000000A0
    DiagonalLeft = DownLeft | UpLeft, // 0x00000030
    DiagonalRight = DownRight | UpRight, // 0x000000C0
    Diagonal = DiagonalRight | DiagonalLeft, // 0x000000F0
    RightSide = DiagonalRight | Right, // 0x000000C2
    LeftSide = DiagonalLeft | Left, // 0x00000031
    TopSide = DiagonalUp | Up, // 0x00000054
    BottomSide = DiagonalDown | Down, // 0x000000A8
    All = BottomSide | TopSide | Horizontal, // 0x000000FF
  }
}
