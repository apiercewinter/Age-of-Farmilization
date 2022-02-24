// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface ISubscriber
{
    // All subscriber class must have a subscribe() method that deals with the subscribing business
    void subscribe();
}

public delegate void HealthBarDel(float amount);
public delegate void PathDel(Vector3 destination);
public delegate void GameObjectPathDel(GameObject go);
public delegate void ResourceGatheredDel(string resource, int amount);

// UIUnitCentralPublisher is the central publisher that publishes information to its subscribers

// NOTE: To add more UI and scripts that manage UI's behavior:
// 1. You need to create a new UI-managing class (name it UI..... so we can easily recognize it).

// 2. In the newly created UI-managing class, write method that really manages UI's behavior.

// (3.) If the UI-managing method takes any parameter or returns anything, write a new delegate class here
//      just like the HealthBarDel above. If the UI-managing method does not takes any parameter and returns nothing,
//      skip this step and just use "Action" class, like the EnableSelectionIndicatorDel variable below.

// 4. Create a new variable in the UIUnitCentralPublisher class using newly-created delegate class or Action.

// 5. Write a method in the UIUnitCentralPublisher to help subscribers subscribe to this publisher, it should have
//    a parameter that is the same as the delegate or Action you used in step 4.

// 6. In the UI-managing class, call this newly-created subscribe method in step 5, and pass in the name of the really UI-managing method.

// 7. Write a delegate method in UIUnitCentralPublisher class, and takes the same type of parameter and returns the same
//    thing if there is any. Inside this delegate method, just call the delegate variable created in step 4, pass in parameter if there is any.

// 8. Whenever you need to manage that UI, just GetCompoent of this UIUnitCentralPublisher and call the 
//    delgate method, and it should correly manage the UI according to UI-managing method in the UI-managing class.

public class UIUnitCentralPublisher : MonoBehaviour
{
    // Indicator Delegate
    private Action EnableSelectionIndicatorDel;
    private Action DisableSelectionIndicatorDel;

    // HealthBar Delegate
    private HealthBarDel AddHealthDel;
    private HealthBarDel SubstractHealthDel;
    private HealthBarDel SetMaxHealthDel;

    // Path Delegate
    private PathDel SetDestinationPathDel;
    private GameObjectPathDel AttackingEnemyPathDel;
    private GameObjectPathDel GatheringResourcePathDel;

    // Resource Delegate
    private ResourceGatheredDel GatheringResourceDel;

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

    public void setDestinationPath(Vector3 destination)
    {
        SetDestinationPathDel(destination);
    }

    public void setAttackingEnemyPath(GameObject go)
    {
        AttackingEnemyPathDel(go);
    }

    public void setGatheringResourcePath(GameObject go)
    {
        GatheringResourcePathDel(go);
    }

    public void indicateGatheredResource(string resource, int amount)
    {
        GatheringResourceDel(resource, amount);
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

    public void subscribeToSetDestinationPath(PathDel del)
    {
        SetDestinationPathDel += del;
    }

    public void subscribeToAttackingEnemyPath(GameObjectPathDel del)
    {
        AttackingEnemyPathDel += del;
    }

    // This one is used for the path indicator that turns green when player clicks on a resource
    public void subscribeToGatheringResourcePath(GameObjectPathDel del)
    {
        GatheringResourcePathDel += del;
    }

    // This one is used for the indicator that appears at the top of the healthbar when gathering resources
    public void subscribeToGatheringResource(ResourceGatheredDel del)
    {
        GatheringResourceDel += del;
    }
}
