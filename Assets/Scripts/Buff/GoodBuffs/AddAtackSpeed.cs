/// <summary>
/// 加速 buff
/// </summary>
public class AddAttackSpeed : GoodBuff
{
    float speedUp = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="speedUp">加速的速度</param>
    /// <param name="time">加速时间</param>
    public AddAttackSpeed(float speedUp, float time)
    {
        this.speedUp = speedUp;
        LastTime = time;
    }

    override public void Apply(CharacterInfo info)
    {
        info.attackSpeed += this.speedUp;
    }
}