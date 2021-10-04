using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeathBlock : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void HitDeathParts()
        {
            GameManager.gameManager.RestartLevel();
        }
    }
}