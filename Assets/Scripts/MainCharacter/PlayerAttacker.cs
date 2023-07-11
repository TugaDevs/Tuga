using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ds
{
    public class PlayerAttacker : MonoBehaviour
    {

        PlayerManager playerManager;
        AnimatorHandler animatorHandler;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;

        public string lastAttack;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            animatorHandler = GetComponent<AnimatorHandler>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if (lastAttack == weapon.TH_Light_Attack_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.TH_Light_Attack_2, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            if (inputHandler.twoHandFlag)
            {
                animatorHandler.PlayerTargetAnimation(weapon.TH_Light_Attack_1, true);
                lastAttack = weapon.TH_Light_Attack_1;
            }
            else
            {            
                animatorHandler.PlayerTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            if (inputHandler.twoHandFlag)
            {
                animatorHandler.PlayerTargetAnimation(weapon.TH_Heavy_Attack, true);
                lastAttack = weapon.TH_Heavy_Attack;
            }
            else
            {
                animatorHandler.PlayerTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                lastAttack = weapon.OH_Heavy_Attack_1;
            }
        }

        #region Input Actions
        public void HandleRBAction()
        {
            if (playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformRBMeleeAction();
            }

            else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
            {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
        }

        #endregion

        #region Attack Action
        public void PerformRBMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }

            else
            {
                if (playerManager.isInteracting)
                {
                    return;
                }
                if (playerManager.canDoCombo)
                {
                    return;
                }

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        public void PerformRBMagicAction(WeaponItem weapon)
        {
            if (weapon.isFaithCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    //chek for fp
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats);
        }

        #endregion
    }
}
