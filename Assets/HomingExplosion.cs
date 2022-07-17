using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingExplosion : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    [SerializeField]
    private int bulletsAmount = 10;
    [SerializeField]
    private float delay = 2.5f;

    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;
    [SerializeField]
    private AudioClip explosion;
    


    public float angle = 0f;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 8f;
    private GameObject player;
    public bool updateOn = true;
    void Start()
    {

        StartCoroutine(updateOff());
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 face = (player.transform.position - transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(face * speed, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updateOn == false)
        {
            Invoke("Explode", 0f);
        }
    }
    private void Explode()
    {
        SoundManager.Instance.PlaySound(explosion);
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, 90f - angle));
            bullet.GetComponent<Rigidbody2D>().AddForce((bulletDir * 4f), ForceMode2D.Impulse);


            angle += angleStep;
        }
        Destroy(this.gameObject,0f);

    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(delay);
        updateOn = false;
    }
}
