using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void HealthBarDel(float amount);

public class UIUnitCentralPublisher : MonoBehaviour
{
    // Indicator Delegate
    public Action EnableSelectionIndicatorDel;
    private Action DisableSelectionIndicatorDel;

    // UI Delegate
    private HealthBarDel AddHealthDel;
    private HealthBarDel SubstractHealthDel;
    private HealthBarDel SetMaxHealthDel;

    // Start is called before the first frame update
    void Start()
    {
        helpIndicatorSubscribe();
    }

    public void enableSelectionIndicator()
    {
        EnableSelectionIndicatorDel();
    }

    public void disableSelectionIndicator()
    {
        DisableSelectionIndicatorDel();
    }

    public void addHealth(float amount)
    {
        AddHealthDel(amount);
    }

    public void substractHealth(float amount)
    {
        SubstractHealthDel(amount);
    }

    public void setMaxHealth(float amount)
    {
        SetMaxHealthDel(amount);
    }

    // ===============================================
    // Below are all subscribing method
    public void subscribeToEnable(Action action)
    {
        EnableSelectionIndicatorDel += action;
    }

    public void subscribeToDisable(Action action)
    {
        DisableSelectionIndicatorDel += action;
    }

    public void subscribeToAddHealth(HealthBarDel del)
    {
        AddHealthDel += del;
    }

    public void subscribeToSubstractHealth(HealthBarDel del)
    {
        SubstractHealthDel += del;
    }

    public void subscribeToSetMaxHealth(HealthBarDel del)
    {
        SetMaxHealthDel += del;
    }

    // As described in UIIndicatorSubscriber() -> manulSubscribe(), Start(), Awake() will never be called 
    // unless the gameObject is enabled, this method is used to manully subscribe to this publisher.
    public void helpIndicatorSubscribe()
    {
        transform.GetChild(0).GetChild(0).GetComponent<UIIndicatorSubscriber>().manulSubscribe();
    }
}
