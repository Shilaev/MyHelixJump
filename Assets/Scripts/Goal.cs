using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Goal : MonoBehaviour
    {
        public void OnCollisionEnter(Collision other)
        {
            GameManager.gameManager.GoNextLevel();
        }
    }
}