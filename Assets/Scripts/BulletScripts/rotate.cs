using System.Collections;
using UnityEngine;

namespace BulletScripts {
    public class rotate : MonoBehaviour
    {
        [SerializeField]
        private int rotationSpeed = 20;

        public bool updateOn = true;

        private void Start()
        {
            StartCoroutine(UpdateOff());
        }

        private void Update()
        {
            if (updateOn == true)
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                //whatever you want update to do.
            }
            // if you want certain parts of update to work at all times write them here.
        }

        private IEnumerator UpdateOff()
        {
            yield return new WaitForSeconds(8.0f);
            updateOn = false;
        }
    }
}
