// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UIPathSubscriber deals with the line indicator that shows the path that units is walking
public class UIPathSubscriber : MonoBehaviour, ISubscriber
{
    private LineRenderer lineRenderer;
    private Color walkingToDestinationColor;
    private Color attackingEnemyColor;
    private Color gatheringResourcesColor;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        subscribe();

        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        lineRenderer.SetPosition(0, unitPos);
        lineRenderer.SetPosition(1, unitPos);

        // Pure white color for walking
        walkingToDestinationColor = Color.white;
        // Pure red color for attacking
        attackingEnemyColor = Color.red;
        // Pure green color for gathering resource
        gatheringResourcesColor = Color.green;

        target = null;
    }

    public void subscribe()
    {
        lineRenderer = GetComponent<LineRenderer>();
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToSetDestinationPath(setDestinationPath);
        // subscribe to enable and disable will all notify UIPathSubscriber when the unit is selected or deselected
        publisher.subscribeToEnable(enableDestinationPath);
        publisher.subscribeToDisable(disableDestinationPath);
        publisher.subscribeToAttackingEnemyPath(setAttackingPath);
        publisher.subscribeToGatheringResourcePath(setGatheringPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            lineRenderer.SetPosition(1, target.transform.position);
        }
        if (lineRenderer.GetPosition(0) != lineRenderer.GetPosition(1))
        {
            lineRenderer.SetPosition(0, transform.parent.parent.gameObject.transform.position);
        }
    }

    public void setDestinationPath(Vector3 destination)
    {
        target = null;
        lineRenderer.startColor = walkingToDestinationColor;
        lineRenderer.endColor = walkingToDestinationColor;
        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        destination.y = unitPos.y;
        lineRenderer.SetPosition(0, unitPos);
        lineRenderer.SetPosition(1, destination);
    }

    public void setAttackingPath(GameObject target)
    {
        this.target = target;
        lineRenderer.startColor = attackingEnemyColor;
        lineRenderer.endColor = attackingEnemyColor;
        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        Vector3 enemyPos = target.transform.position;
        lineRenderer.SetPosition(0, unitPos);
        lineRenderer.SetPosition(1, enemyPos);
    }

    public void setGatheringPath(GameObject resource)
    {
        lineRenderer.startColor = gatheringResourcesColor;
        lineRenderer.endColor = gatheringResourcesColor;
        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        Vector3 resourcePos = resource.transform.position;
        lineRenderer.SetPosition(0, unitPos);
        lineRenderer.SetPosition(1, resourcePos);
    }

    public void enableDestinationPath()
    {
        lineRenderer.gameObject.SetActive(true);
    }

    public void disableDestinationPath()
    {
        lineRenderer.gameObject.SetActive(false);
    }
}
