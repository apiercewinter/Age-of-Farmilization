// Boyuan Huang
//Daniel Zhang (only the OnMouseDown & related variables)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoverover : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private float radius;

    private UnitMover myMover;
    private UnitCollector myCollector;
    private UnitAttacker myAttacker;
    private UnitBase myBase;

    private int segments = 50;
    private float angle = 20f;

    [SerializeField] private bool selected = false;
    [SerializeField] private bool isHovering = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;

        myMover = GetComponent<UnitMover>();
        myCollector = GetComponent<UnitCollector>();
        myAttacker = GetComponent<UnitAttacker>();
        myBase = GetComponent<UnitBase>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHovering == false && selected == true)
        {
            selected = false;
            lineRenderer.positionCount = 0;
        }
        else if (selected)
        {
            if (myBase.canTakeAction())
            {
                if (myCollector)
                {
                    float angle = 20f;
                    radius = myCollector.getRange();
                    for (int i = segments + 1; i < (segments + 1) * 2; i++)
                    {
                        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                        lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);

                        angle += (360f / segments);
                    }
                }

                if (myAttacker)
                {
                    int startIndex, endIndex;
                    if (myCollector)
                    {
                        startIndex = (segments + 1) * 2;
                        endIndex = (segments + 1) * 3;
                    }
                    else
                    {
                        startIndex = segments + 1;
                        endIndex = (segments + 1) * 2;
                    }
                    angle = 20f;
                    radius = myAttacker.getRange();
                    
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                        lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);

                        angle += (360f / segments);
                    }
                }
            }
            else
            {
                lineRenderer.positionCount = segments + 1;
            }
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
        Vector3 roundStartLocation = myMover.getRoundStartLocation();
        Vector3 currentPos = transform.position;
        currentPos.y = roundStartLocation.y;
        radius = myMover.getMoveDistance();

        angle = 20f;

        int ptCount = segments + 1;
        if (myCollector)
        {
            ptCount += segments + 1;
        }
        if (myAttacker)
        {
            ptCount += segments + 1;
        }
        lineRenderer.positionCount = ptCount;

        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0, z) + roundStartLocation);

            angle += (360f / segments);
        }
        if (myBase.canTakeAction())
        {
            if (myCollector)
            {
                angle = 20f;
                radius = myCollector.getRange();
                for (int i = segments + 1; i < (segments + 1) * 2; i++)
                {
                    float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                    float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                    lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);

                    angle += (360f / segments);
                }
            }

            if (myAttacker)
            {
                int startIndex, endIndex;
                if (myCollector)
                {
                    startIndex = (segments + 1) * 2;
                    endIndex = (segments + 1) * 3;
                }
                else
                {
                    startIndex = segments + 1;
                    endIndex = (segments + 1) * 2;
                }
                angle = 20f;
                radius = myAttacker.getRange();
                for (int i = startIndex; i < endIndex; i++)
                {
                    float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                    float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                    lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);

                    angle += (360f / segments);
                }
            }
        }
        else
        {
            lineRenderer.positionCount = segments + 1;
        }

    }
}
