using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Math = System.Math;

public class WizardAnimationScript : MonoBehaviour
{
    Vector2 firstPosition = Vector2.zero;
    Vector2 secondPosition = Vector2.zero;
    Vector2 newMoveVector;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator legsAnimator;



    public void StartAttackingAnimation()
    {
        bodyAnimator.SetBool("Attacking",true);
    }
    public void StopAttackingAnimation()
    {
        bodyAnimator.SetBool("Attacking", false);
    }

    private void CalculateMoveAnimation()
    {
        bodyAnimator.SetFloat("ForwardSpeed", newMoveVector.y * 20);
        bodyAnimator.SetFloat("RightSpeed", newMoveVector.x * 20);
        legsAnimator.SetFloat("ForwardSpeed", newMoveVector.y * 20);
        legsAnimator.SetFloat("RightSpeed", newMoveVector.x * 20);
    }

    public void PlayDeadAnimation()
    {
        bodyAnimator.SetBool ("Dead", true);
        legsAnimator.SetBool("Dead", true);
    }

    void FixedUpdate()
    {
        secondPosition = gameObject.transform.position;
        var moveVector = secondPosition - firstPosition;
        var moveVectorx = moveVector.x;
        var moveVectory = moveVector.y;
        float angleCos = (float)Math.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        float angleSin = (float)Math.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        float newVectorx = moveVectorx * angleCos - moveVectory * angleSin;
        float newVectory = moveVectorx * angleSin + moveVectory * angleCos;
        newMoveVector = new Vector2(newVectorx, newVectory);
        firstPosition = gameObject.transform.position;
        CalculateMoveAnimation();

    }
}
