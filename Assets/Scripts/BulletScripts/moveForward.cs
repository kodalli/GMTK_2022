using UnityEngine;
using UnityEngine.Serialization;

namespace BulletScripts {
    public class moveForward : MonoBehaviour
    {
        [SerializeField]
        private float speed = 4f;
        [FormerlySerializedAs("Dir")] [SerializeField]
        private Vector2 direction = new Vector2(-1f, 0f);

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Rigidbody2D>().AddForce(direction*speed,ForceMode2D.Impulse);
        }

    }
}


