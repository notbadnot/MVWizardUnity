using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using Math = System.Math;

public class MovingComponent : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    [SerializeField] float nonForwardMultipluer = 0.5f;

    public enum Direction
    {
        Forward,
        Left,
        Back,
        Right
    }


    Vector2 LeftFromLookVector;


    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        LeftFromLookVector = Vector2.zero;
    }

    private Vector2 FindForwardVector()
    {
        float rotation = rigidbody.rotation;
        LeftFromLookVector = LeftFromLookVector.normalized;
        return (new Vector2(-(float)Math.Sin(rotation * Mathf.Deg2Rad), (float)Math.Cos(rotation * Mathf.Deg2Rad))).normalized;
    }

    public void Move(Direction dir)
    {
        float localSpeed;
        Vector2 directionVector = Vector2.zero;
        var tempVector = FindForwardVector();
        if (dir == Direction.Forward)
        {
            localSpeed = speed;
            directionVector = FindForwardVector();
        }else 
        {   
            localSpeed = speed * nonForwardMultipluer;

            if (dir == Direction.Left)
            {
                directionVector = new Vector2(-tempVector.y, tempVector.x);
            }else if(dir == Direction.Back)
            {
                directionVector = -tempVector;
            }else if (dir == Direction.Right)
            {
                directionVector = new Vector2(tempVector.y, -tempVector.x);
            }

        }
        rigidbody.AddForce(directionVector.normalized * localSpeed);
    }

    public void Rotate(Vector2 point)
    {

        var rotateVector = (point - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
        
        float rotation = rigidbody.rotation;
        LeftFromLookVector.y = -(float)Math.Sin(rotation * Mathf.Deg2Rad);
        LeftFromLookVector.x = -(float)Math.Cos(rotation * Mathf.Deg2Rad);
        LeftFromLookVector = LeftFromLookVector.normalized;

        var dot = Vector2.Dot(rotateVector, LeftFromLookVector);
        
        if (Mathf.Abs(dot) > 0.9999f)
        {
            return;
        }

        float rotateangleasin = (Mathf.Asin(dot)) * Mathf.Rad2Deg;

        rigidbody.rotation = rigidbody.rotation + rotateangleasin;

        if (rigidbody.rotation > 360) { rigidbody.rotation = 0; }
        if (rigidbody.rotation < -360) { rigidbody.rotation = 0; }
    }




}
