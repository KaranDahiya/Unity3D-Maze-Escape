using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float damage = 2f;
    public float range = 1000f;
    public float cooldown = 0.5f;
    public Camera fpsCam;
    float shotTime;
    public ParticleSystem shootEffect;
    public GameObject hitFleshEffect;
    public GameObject hitObjectEffect;
    public LayerMask mask;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey("mouse 0") && shotTime < Time.time)
        {
            Shoot();
            shotTime = Time.time + cooldown;
        }
	}

    //shoots using Raycasts
    void Shoot()
    {
        //plays effect when shot
        shootEffect.Play();
        //casts ray to all layers except PlayerLayer
        RaycastHit target;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range, ~mask))
        {
            if (target.transform.tag == "Environment")
            {
                //plays effect on impact
                GameObject impact = Instantiate(hitObjectEffect, target.point, Quaternion.LookRotation(target.normal));
                Destroy(impact, 2f);
            }
            else if (target.transform.tag == "Enemy")
            {
                //plays effect on impact
                GameObject impact = Instantiate(hitFleshEffect, target.point, Quaternion.LookRotation(target.normal));
                Destroy(impact, 2f);
                //deals damage
                GameObject enemyHit = target.collider.gameObject;
                enemyHit.GetComponent<Enemy>().enemyTakeDamage(damage);
            }
        }
    }
}
