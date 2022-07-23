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
 
    public float angle = 0f;
 
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float frequency = 1f;
 
    private void Start() {
        frequency = GameManager.Instance.enemyEffects.fireRate / 100f;
        InvokeRepeating(nameof(Fire), 0f, frequency);
    }
 
    private void Fire ()
    {
        var angleStep = (endAngle - startAngle) / bulletsAmount;
         
        for (var i = 0; i < bulletsAmount + 1; i++)
        {
            var position = transform.position;
            var bulletDirX = position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            var bulletDirY = position.y + Mathf.Cos((angle * Mathf.PI) / 180);
             
            var bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - position).normalized;
 
            var bullet = Instantiate(bulletPrefab,position, Quaternion.Euler(0f, 0f, 90f - angle));
            //bullet.transform.rotation =  Quaternion.Euler(0f,0f,90f-angle);
            bullet.GetComponent<Rigidbody2D>().AddForce((bulletDir * speed),ForceMode2D.Impulse);
             
 
            angle += angleStep;
        }
    }
}