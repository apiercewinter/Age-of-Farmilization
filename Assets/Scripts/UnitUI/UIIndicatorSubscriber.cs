using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// UIIndicatorSubscriber deals with circle below the unit that indicates that unit is currently selected
public class UIIndicatorSubscriber : MonoBehaviour
{
    public void enableSelectionIndicator()
    {
        gameObject.SetActive(true);
    }

    public void disableSelectionIndicator()
    {
        gameObject.SetActive(false);
    }

    // Since the indicator UI is set to be disabled when the game starts, neither Start()
    // nor Awake() will be called when the game starts. So this method is made to maunlly
    // subscribe to the Publisher, and will be called in Publisher's Start() method.
    public void manulSubscribe()
    {
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToEnable(enableSelectionIndicator);
        publisher.subscribeToDisable(disableSelectionIndicator);
    }
}
