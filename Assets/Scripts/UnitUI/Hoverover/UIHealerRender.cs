// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UIHealerRender renders the healer unit's attacking range indicator
// this ring is rendered in orange color.
public class UIHealerRender : MonoBehaviour
{
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void renderRange(float moveDistance, Vector3 center)
    {
        lineRenderer.positionCount = 51;
        float angle = 20f;
        for (int i = 0; i < 51; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * moveDistance;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * moveDistance;

            lineRenderer.SetPosition(i, new Vector3(x, 0, z) + center);

            angle += (360f / 50);
        }
        Color orange = new Color(1.0f, 0.64f, 0.0f);
        lineRenderer.startColor = orange;
        lineRenderer.endColor = orange;
    }

    public void cancelRender()
    {
        if (lineRenderer)
        {
            lineRenderer.positionCount = 0;
        }
    }
}
