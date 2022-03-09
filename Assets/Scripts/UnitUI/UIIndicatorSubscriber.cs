// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UIIndicatorSubscriber deals with circle below the unit that indicates that unit is currently selected
public class UIIndicatorSubscriber : MonoBehaviour, ISubscriber
{
    void Start()
    {
        subscribe();

        disableSelectionIndicator();
    }

    public void subscribe()
    {
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToEnable(enableSelectionIndicator);
        publisher.subscribeToDisable(disableSelectionIndicator);
    }

    public void enableSelectionIndicator()
    {
        gameObject.SetActive(true);
    }

    public void disableSelectionIndicator()
    {
        gameObject.SetActive(false);
    }
}
