using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public FartPattern fart;
    public GameObject fartCannon;
    public GameObject fartBullet;

    public void fire()
    {
        if (fart.isShotgun && fart.spread > 1)
        {
            shotGunFire();
        }
        else
        {
            float thisRot;
            float angle;
            if (fart.followsPlayer) thisRot = fartCannon.transform.rotation.eulerAngles.z;
            else thisRot = Random.Range(360f, 0f);
            if (fart.spread < 1) angle = thisRot;
            else
            {
                float upperBound = thisRot - (fart.spread / 2);
                float lowerBound = thisRot + (fart.spread / 2);
                angle = Random.Range(lowerBound, upperBound);
            }
            makeFart(angle);
        }
    }

    private void shotGunFire()
    {
        if (!fart.isShotgun) return;
        float thisRot;
        float angle;
        if (fart.followsPlayer) thisRot = fartCannon.transform.rotation.eulerAngles.z;
        else thisRot = Random.Range(360f, 0f);
        float increment = fart.spread / fart.bulletCount;
        angle = thisRot - (fart.spread / 2);

        for (int i = 0; i < fart.bulletCount; i++)
        {
            makeFart(angle);
            angle += increment;
        }
    }

    private void makeFart(float angle)
    {
        GameObject bullet = Instantiate(fartBullet, fartCannon.transform.position, Quaternion.Euler(0, 0, angle));
        FartController controller = bullet.GetComponent<FartController>();
        SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();

        bullet.transform.localScale *= fart.fartSize;
        sprite.color = fart.fartColor;
        controller.speed = fart.bulletSpeed;

    }
}
