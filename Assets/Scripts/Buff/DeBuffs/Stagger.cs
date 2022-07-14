/// <summary>
/// 大硬直
/// </summary>
public class Stagger : DeBuff
{
    public Stagger(float time)
    {
        LastTime = time;
    }

    public override void Apply(CharacterInfo info)
    {
        info.isStagger = true;
    }
}