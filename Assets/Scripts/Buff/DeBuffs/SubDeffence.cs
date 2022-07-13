public class SubDefence : DeBuff
{
    float defenceDown = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="defenceDown">减防的防御</param>
    /// <param name="time">减防时间</param>
    public SubDefence(float defenceDown, float time)
    {
        this.defenceDown = defenceDown;
        LastTime = time;
    }

    override public void Apply(CharacterInfo info)
    {
        info.defence -= this.defenceDown;
    }
}