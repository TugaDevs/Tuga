using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ds
{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        AnimatorHandler animatorHandler;

        public float staminaRegenerationAmount = 25;
        public float staminaRegenerationTimer = 0;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealht = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealt(currentHealht);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            if (playerManager.isInvulnerable)
            {
                return;
            }

            if (isDead)
            {
                return;
            }

            currentHealht = currentHealht - damage;

            healthBar.SetCurrentHealt(currentHealht);

            animatorHandler.PlayerTargetAnimation("TakingDamage2", true);

            if(currentHealht <= 0)
            {
                currentHealht = 0;
                animatorHandler.PlayerTargetAnimation("Death", true);
                isDead = true;
            }
        }

        public void TakeStaminaDamage(int damage) 
        { 
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenerationTimer = 0;
            }
            else
            {
                staminaRegenerationTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenerationTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }

        public void HealPlayer(int healAmount)
        {
            currentHealht = currentHealht + healAmount;

            if (currentHealht > maxHealth)
            {
                currentHealht = maxHealth;
            }

            healthBar.SetCurrentHealt(currentHealht);
        }

    }
}
