using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDamager : MonoBehaviour
{
    [SerializeField] float damage = 10;
    public void DoDamage(PhysicalAcceptor physicalAcceptor)
    {
        var rbodyOfAcceptor = physicalAcceptor.gameObject.GetComponent<Rigidbody2D>();
        if (rbodyOfAcceptor != null)
        {
            rbodyOfAcceptor.AddForce((rbodyOfAcceptor.gameObject.transform.position - gameObject.transform.position).normalized * 5000f);
        }
        physicalAcceptor.AcceptDamage(damage);

    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform == transform.parent)
        {
            return;
        }
        Debug.Log(collision.gameObject);
        var physicalAcceptor = collision.gameObject.GetComponent<PhysicalAcceptor>();
        if (physicalAcceptor != null)
        {
            DoDamage(physicalAcceptor);
        }
    }



}
