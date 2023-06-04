using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ds
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealht;


        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealht = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealht = currentHealht - damage;


            animator.Play("TakingDamage2");

            if (currentHealht <= 0)
            {
                currentHealht = 0;
                animator.Play("Death");
            }
        }

    }
}
