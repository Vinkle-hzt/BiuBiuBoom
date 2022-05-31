using UnityEngine;
using UnityEngine.UI;

public class GradientColor : MonoBehaviour
{
    public Gradient Color;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = Color.Evaluate(Mathf.Abs(Mathf.Cos(Time.time)));
    }
}