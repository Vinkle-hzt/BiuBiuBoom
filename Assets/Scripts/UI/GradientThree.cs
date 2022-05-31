using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientThree : BaseMeshEffect
{
    public Color32 StartColor = Color.white;
    public Color32 MidColor = Color.white;
    public Color32 EndColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
        {
            return;
        }
        var vertexList = new List<UIVertex>();
        vh.GetUIVertexStream(vertexList);
        int count = vertexList.Count;
        ApplyGradient(vertexList, 0, count);
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }

    private void ApplyGradient(List<UIVertex> vertexList, int startIndex, int endIndex)
    {
        float start;
        float end;
        float tmp;
        float distance;
        float mid;
        float pos;
        start = vertexList[0].position.y;
        end = vertexList[0].position.y;
        for (int i = startIndex; i < endIndex; ++i)
        {
            tmp = vertexList[i].position.x;
            if (tmp > end) end = tmp;
            else if (tmp < start) start = tmp;
        }
        distance = end - start;
        mid = (start + end) * 0.5f;
        for (int i = startIndex; i < endIndex; ++i)
        {
            UIVertex uiVertex = vertexList[i];
            pos = vertexList[i].position.x;
            if (pos <= mid) uiVertex.color = Color32.Lerp(StartColor, MidColor, (pos - start) / (mid - start));
            else uiVertex.color = Color32.Lerp(MidColor, EndColor, (pos - mid) / (end - mid));
            vertexList[i] = uiVertex;
        }
    }
}