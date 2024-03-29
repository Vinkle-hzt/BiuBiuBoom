abstract public class Buff
{
    float curTime = 0; // 当前时间
    public float LastTime { get; set; } // 持续时间
    public bool BuffTimeOut()
    {
        if (curTime >= LastTime)
        {
            curTime = 0;
            return true;
        }
        return false;
    }

    public void AddTime(float time)
    {
        curTime += time;
    }

    abstract public void Apply(CharacterInfo info);
}