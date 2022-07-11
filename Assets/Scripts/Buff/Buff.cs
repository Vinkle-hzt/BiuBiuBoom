public class Buff : BuffDetail
{
    float curTime; // 当前时间
    Buff(BuffDetail buff) : base(buff)
    {
        curTime = 0;
    }
    bool BuffTimeOut()
    {
        return curTime >= lastTime;
    }

    public void AddTime(float time)
    {
        curTime += time;
    }
}