using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDestinationPath(Vector3 destination)
    {
        GameObject unit = transform.parent.parent.gameObject;
        lineRenderer.SetPosition(0, unit.transform.position);
        lineRenderer.SetPosition(1, destination);
    }
}
