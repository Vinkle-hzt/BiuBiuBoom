abstract public class BuffDetail
{
    protected float lastTime; // buff 持续时间

    abstract public void Apply(CharacterInfo info);
}