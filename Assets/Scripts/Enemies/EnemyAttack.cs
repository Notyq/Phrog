using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        public float damage = 1f;
        private bool isColliding;

        private void Update()
        {
            isColliding = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(isColliding) return;
            isColliding = true;
            
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
