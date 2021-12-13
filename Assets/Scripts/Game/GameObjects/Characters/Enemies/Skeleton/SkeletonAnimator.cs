using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour
{
    Vector2 firstPosition = Vector2.zero;
    Vector2 secondPosition = Vector2.zero;
    Vector2 moveVector;
    [SerializeField] private Animator skeletonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CalculateMoveAnimation()
    {
        skeletonAnimator.SetFloat("Speed", moveVector.magnitude * 100);
    }

    public void PlayAttackAnimation()
    {
        skeletonAnimator.SetTrigger("Attack");
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        secondPosition = gameObject.transform.position;
        moveVector = secondPosition - firstPosition;
        firstPosition = gameObject.transform.position;
        CalculateMoveAnimation();

    }
}
