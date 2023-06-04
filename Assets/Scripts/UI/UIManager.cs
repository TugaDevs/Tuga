using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ds
{
    public class UIManager : MonoBehaviour
    {

        public PlayerInventory playerInventory;
        EquipmentWindowUi equipmentWindowUi;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject weaponInventoryWindow;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParnet;
        WeaponInventorySlot[] weaponInventorySlots;

        private void Awake()
        {
            equipmentWindowUi = FindObjectOfType<EquipmentWindowUi>();
        }
        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotsParnet.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUi.LoadWeaponsOnEquipmentScreen(playerInventory);
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParnet);
                        weaponInventorySlots = weaponInventorySlotsParnet.GetComponentsInChildren<WeaponInventorySlot>();
                    }

                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);

                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            weaponInventoryWindow.SetActive(false);
        }
    }
}

