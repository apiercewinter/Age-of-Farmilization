using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UIPathSubscriber deals with the line indicator that shows the path that units is walking
public class UIPathSubscriber : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToSetDestinationPath(setDestinationPath);
        // subscribe to enable and disable will all notify UIPathSubscriber when the unit is selected or deselected
        publisher.subscribeToEnable(enableDestinationPath);
        publisher.subscribeToDisable(disableDestinationPath);

        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        lineRenderer.SetPosition(0, unitPos);
        lineRenderer.SetPosition(1, unitPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.GetPosition(0) != lineRenderer.GetPosition(1))
        {
            lineRenderer.SetPosition(0, transform.parent.parent.gameObject.transform.position);
        }
    }

    public void setDestinationPath(Vector3 destination)
    {
        Vector3 unitPos = transform.parent.parent.gameObject.transform.position;
        destination.y = unitPos.y;
        lineRenderer.SetPosition(0, transform.parent.parent.gameObject.transform.position);
        lineRenderer.SetPosition(1, destination);
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
