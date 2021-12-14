using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour //Bad
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] RectTransform healthBar;

    private void Start()
    {
        healthComponent.HealthChangedEvent += HealthComponent_HealthChangedEvent;
    }

    private void HealthComponent_HealthChangedEvent()
    {
        healthBar.localScale = new Vector3((1 * (healthComponent.health / healthComponent.maxHealth)),1,1);
    }

    private void Update()
    {
        healthBar.transform.parent.rotation = new Quaternion(0,0, -gameObject.transform.rotation.z,0);
        var gameobjpos = gameObject.transform.position;
        healthBar.transform.parent.position = new Vector3(gameobjpos.x,gameobjpos.y + 1.5f, gameobjpos.z );
    }



}
