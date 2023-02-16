using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public event UnityAction<TrackPoint> Triggered;
    protected virtual void OnPassed() { }
    protected virtual void OnAssignAsTarget() { }

    public TrackPoint NextPoint;
    public bool IsFirst;
    public bool IsLast;

    protected bool isTarget;
    public bool IsTarget => isTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;

        Triggered?.Invoke(this);
    }

    public void Passed()
    {
        isTarget = false;
        OnPassed();
    }

    public void AssignAsTarget()
    {
        isTarget = true;
        OnAssignAsTarget();
    }

    public void ResetProperties()
    {
        NextPoint = null;
        IsFirst = false;
        IsLast = false;
    }
}
