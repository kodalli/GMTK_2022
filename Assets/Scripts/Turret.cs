using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;

    

    void Start()
    {
        InvokeRepeating("Fire", 0f, 1f);
    }
    
    private void Fire ()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab,transform.position, Quaternion.Euler(0f, 0f, 90f - angle));
            //bullet.transform.rotation =  Quaternion.Euler(0f,0f,90f-angle);
            bullet.GetComponent<Rigidbody2D>().AddForce((bulletDir * 4f),ForceMode2D.Impulse);
            Destroy(bullet,2f);

            angle += angleStep;
        }
    }
}
