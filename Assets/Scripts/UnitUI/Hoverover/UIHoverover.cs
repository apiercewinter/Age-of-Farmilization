// Boyuan Huang
//Daniel Zhang (only the OnMouseDown & related variables)
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

    [SerializeField] private bool selected = false;
    [SerializeField] private bool isHovering = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHovering == false && selected == true)
        {
            selected = false;
            lineRenderer.positionCount = 0;
        }
    }

    //Allows radius to remain visible after clicked
    private void OnMouseDown()
    {
        selected = !selected;
        if (gameObject.tag == TeamManager.getCurrentTeamTag() && selected == true)
        {
            RenderRadii();
        }
    }

    private void OnMouseOver()
    {
        isHovering = true;
        if (gameObject.tag == TeamManager.getCurrentTeamTag())
        {
            RenderRadii();
        }
    }

    private void OnMouseExit()
    {
        isHovering = false;
        if(selected != true)
            lineRenderer.positionCount = 0;
    }

    private void RenderRadii()
    {
        UnitMover myMover = GetComponent<UnitMover>();
        Vector3 roundStartLocation = myMover.getRoundStartLocation();
        Vector3 currentPos = transform.position;
        currentPos.y = roundStartLocation.y;
        radius = myMover.getMoveDistance();
        int segments = 50;
        float x;
        float z;

        float angle = 20f;
        lineRenderer.positionCount = segments * 2 + 1;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0, z) + roundStartLocation);

            angle += (360f / segments);
        }

        UnitCollector myCollector = GetComponent<UnitCollector>();
        if (myCollector)
        {
            angle = 20f;
            radius = myCollector.getRange();
            for (int i = segments + 1; i < segments * 2 + 1; i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition(i, new Vector3(x, 0, z) + roundStartLocation);

                angle += (360f / segments);
            }
        }
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }
}
