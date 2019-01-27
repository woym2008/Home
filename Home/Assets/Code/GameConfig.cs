using UnityEngine;
using System.Collections;

public class GameConfig
{
    //子弹开始速度
    public const float BulletBeginSpeed = 2;
    //每次变房子后子弹增加的开始速度
    public const float BulletPowerUpSpeed = 0.5f;
    //子弹随时间增速参数
    public const float TimeAddSpeed = 0.2f;
    //子弹生成速度
    public const float AddBulletSpeed = 1;
    //玩家移动速度
    public const float PlayerMoveSpeed = 5;
    //再合体需要的冷却时间
    public const float BecameHomeCooldown = 10;
    //合体时保持的最大时间
    public const float HomeLifeTile = 5;


    public static float s_BulletBeginSpeed = 3;
}
