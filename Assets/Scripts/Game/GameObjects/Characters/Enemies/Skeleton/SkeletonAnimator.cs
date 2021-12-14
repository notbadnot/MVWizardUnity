using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkeletonAnimator : MonoBehaviour
{
    Vector2 firstPosition = Vector2.zero;
    Vector2 secondPosition = Vector2.zero;
    Vector2 moveVector;
    [SerializeField] private Animator skeletonAnimator;


  

    private void CalculateMoveAnimation()
    {
        skeletonAnimator.SetFloat("Speed", moveVector.magnitude * 100);
    }

    public void PlayAttackAnimation()
    {
        skeletonAnimator.SetTrigger("Attack");
    } 

    void FixedUpdate()
    {
        secondPosition = gameObject.transform.position;
        moveVector = secondPosition - firstPosition;
        firstPosition = gameObject.transform.position;
        CalculateMoveAnimation();

    }
}
