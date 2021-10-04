using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PassCheck : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameManager.gameManager.AddScore(1);
        }
    }
}