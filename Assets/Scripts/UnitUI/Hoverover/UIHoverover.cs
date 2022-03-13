// Boyuan Huang
// Daniel Zhang (only the OnMouseDown & related variables)
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
    private UnitHealer myHealer;
    private UnitBase myBase;

    private int segments = 50;
    private float angle = 20f;

    [SerializeField] private bool selected = false;
    [SerializeField] private bool isHovering = false;

    private UICollectorRender collectorRender;
    private UIAttackerRender attackerRender;
    private UIHealerRender healerRender;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;

        myMover = GetComponent<UnitMover>();
        myCollector = GetComponent<UnitCollector>();
        myAttacker = GetComponent<UnitAttacker>();
        myHealer = GetComponent<UnitHealer>();
        
        myBase = GetComponent<UnitBase>();

        collectorRender = transform.GetChild(1).gameObject.GetComponent<UICollectorRender>();
        attackerRender = transform.GetChild(2).gameObject.GetComponent<UIAttackerRender>();
        healerRender = transform.GetChild(3).gameObject.GetComponent<UIHealerRender>();
    }

    void Update()
    {
        GameObject selectedObj = SelectedObject.getSelected();
        if (selectedObj != null && selectedObj.GetInstanceID() == this.gameObject.GetInstanceID())
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
        if (selected == false && isHovering == false)
        {
            lineRenderer.positionCount = 0;
            collectorRender.cancelRender();
            attackerRender.cancelRender();
            healerRender.cancelRender();
        }
        else if (selected)
        {
            if (myBase.canTakeAction())
            {
                if (myCollector)
                {
                    collectorRender.renderRange(myCollector.getRange(), transform.position);
                }
                if (myAttacker)
                {
                    attackerRender.renderRange(myAttacker.getRange(), transform.position);
                }
                if (myHealer)
                {
                    healerRender.renderRange(myHealer.getRange(), transform.position);
                }
            }
            else
            {
                collectorRender.cancelRender();
                attackerRender.cancelRender();
                healerRender.cancelRender();
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
        collectorRender.cancelRender();
        attackerRender.cancelRender();
        healerRender.cancelRender();
    }

    private void RenderRadii()
    {
        if (!myMover) return;
        Vector3 roundStartLocation = myMover.getRoundStartLocation();
        Vector3 currentPos = transform.position;
        currentPos.y = roundStartLocation.y;
        radius = myMover.getMoveDistance();

        angle = 20f;

        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0.4f, z) + roundStartLocation);

            angle += (360f / segments);
        }
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;

        if (myBase.canTakeAction())
        {
            if (myCollector)
            {
                collectorRender.renderRange(myCollector.getRange(), transform.position);
            }
            if (myAttacker)
            {
                attackerRender.renderRange(myAttacker.getRange(), transform.position);
            }
            if (myHealer)
            {
                healerRender.renderRange(myHealer.getRange(), transform.position);
            }
        }
    }
}
