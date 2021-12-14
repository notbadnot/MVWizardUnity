using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    [SerializeField]private Vector2 targetpoint = new Vector2(0,0);
    [SerializeField] public Wizard myTarget;
    Skeleton skeleton;

    [SerializeField] private AIMapComponent mapComponent;
    [SerializeField] private AngryVision vision;
    Vector2 initialposition;
    [SerializeField] bool patroling = false;
    [SerializeField] Vector2 deltapatrolingPoint; //delta position from initial(start) skeleton to patrol
    bool goTopatrolPoint = true;
    bool waiting = false;
    void Start()
    {
        skeleton = gameObject.GetComponent<Skeleton>();
        mapComponent.BuildMap();
        initialposition = gameObject.transform.position;
    }


    public void GoToPosition(Vector2 whereToGo)
    {
        var path = mapComponent.FindWay((Vector2)gameObject.transform.position, targetpoint);
        if (path == null)
        {
            targetpoint = gameObject.transform.position;
            path = mapComponent.FindWay((Vector2)gameObject.transform.position, targetpoint);
        }
        var point = mapComponent.GetNextPoint(path, whereToGo);
        skeleton.GoToPoint(point);
    }
    public void AttackBehavior(Wizard target)
    {
        if ((target.transform.position - gameObject.transform.position).magnitude <2.2f) 
        {
            if (!skeleton.attackingNow)
            {
                skeleton.Attack();
            }
        }
        else
        {
            skeleton.GoToPoint(target.transform.position);
        }
    }

    private IEnumerator WaitingAtPoint()
    {
        yield return new WaitForSeconds(1f);
        waiting = false;
        yield return null;
    }

    public void PatrolingBehaviour()
    {
        if (waiting)
        {
            return;
        }
        Vector2 myPointToGo; 

        if (goTopatrolPoint)
        {
            myPointToGo  =initialposition + deltapatrolingPoint;

        }
        else
        {
            myPointToGo = initialposition;
        }
        GoToPosition(myPointToGo);
        if (((Vector2)gameObject.transform.position - myPointToGo).magnitude < 0.05f)
        {
            waiting = true;
            goTopatrolPoint = !goTopatrolPoint;
            StartCoroutine(WaitingAtPoint());
        }
    }

    public void PeacefulBehaviour()
    {
        if ((targetpoint - (Vector2)gameObject.transform.position).magnitude < 4f )
        {
            List<Vector2Int> path = null;
            while(path == null)
            {
                targetpoint = (Vector2)gameObject.transform.position + Vector2.up * Random.Range(-3, 3) * 3 + Vector2.right * Random.Range(-3, 3) * 3;
                path = mapComponent.FindWay((Vector2)gameObject.transform.position, targetpoint);
                if ((targetpoint - (Vector2)gameObject.transform.position).magnitude <1f)
                {
                    targetpoint = initialposition;
                }
            }
        }
        GoToPosition(targetpoint);
    }
    void Update()
    {

        var tempTarget = vision.myTarget;
        if (tempTarget != null && tempTarget.GetType() == typeof(Wizard))
        {
            myTarget = (Wizard)tempTarget;
        }
        if (myTarget != null && myTarget.alive)
        {
            AttackBehavior(myTarget);
        }
        else if (patroling)
        {
            PatrolingBehaviour();
        }
        else
        {
            PeacefulBehaviour();
        }
    }


}
