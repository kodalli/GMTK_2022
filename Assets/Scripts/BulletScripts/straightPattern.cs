using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace BulletScripts {
    public class straightPattern : MonoBehaviour
    {
        public GameObject homingProj1;
        [SerializeField]
        public AudioClip pewSound;

        private float timer = 0f;
        public float timerEnd = 10f;

        [FormerlySerializedAs("projs")] [SerializeField]
        private int projectiles = 3;
        private void Update()
        {
            if (timer<timerEnd)
            {
                timer += Time.deltaTime;
            }
            else{
                timer = 0f;
                StartCoroutine(Spawn());
            }
        }
        private IEnumerator Spawn()
        {
            for (var i = 0; i < projectiles; i++)
            {
                var transform1 = transform;
                Instantiate(homingProj1, transform1.position,transform1.rotation);
                SoundManager.Instance.PlaySound(pewSound);
                yield return new WaitForSeconds(.2f);
            }
        }
    }
}
 
