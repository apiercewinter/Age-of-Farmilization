// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoverover : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private float ThetaScale = 0.01f;
    private float radius;
    private int Size;
    private float Theta = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, gameObject.transform.position);
        lineRenderer.useWorldSpace = true;
    }

    private void OnMouseOver()
    {
        if (gameObject.tag == TeamManager.getCurrentTeamTag())
        {
            UnitMover myMover = GetComponent<UnitMover>();
            Vector3 roundStartLocation = myMover.getRoundStartLocation();
            Vector3 currentPos = transform.position;
            currentPos.y = roundStartLocation.y;
            radius = myMover.getMoveDistance();
            Theta = 0f;
            Size = (int)((1f / ThetaScale) + 1f);
            lineRenderer.positionCount = Size;
            for (int i = 0; i < Size; i++)
            {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = radius * Mathf.Cos(Theta);
                float z = radius * Mathf.Sin(Theta);
                lineRenderer.SetPosition(i, new Vector3(x, 0, z) + roundStartLocation);
            }
        }
    }

    private void OnMouseExit()
    {
        lineRenderer.positionCount = 0;
    }
}
