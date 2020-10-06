using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USB
{
    public class Attack : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            gameObject.SetActive(false);
        }
    }
}
