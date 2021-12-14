using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    [SerializeField] MovingComponent movingComponent;
    [SerializeField] SkeletonAnimator skeletonAnimator;
    [SerializeField] PhysicalDamager physicalDamager;
    [SerializeField] HealthComponent health;
    public bool attackingNow = false;
    void Start()
    {
        health.ImDeadEvent += Health_ImDeadEvent;
    }

    private void Health_ImDeadEvent()
    {
        Destroy(gameObject);
    }

    public void GoToPoint(Vector2 point)
    {
        movingComponent.Rotate(point);
        movingComponent.Move(MovingComponent.Direction.Forward);
    }
    
    private IEnumerator AttackingTimer()
    {
        skeletonAnimator.PlayAttackAnimation();
        physicalDamager.gameObject.SetActive(true);
        attackingNow = true;
        yield return new WaitForSeconds(0.5f);
        physicalDamager.gameObject.SetActive(false);
        attackingNow = false;
        yield return null;
    }
    public void Attack()
    {
        StartCoroutine(AttackingTimer());
    }

}
