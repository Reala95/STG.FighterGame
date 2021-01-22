using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFighter
{
    public class Weapon
    {
        // Backend code for process data for a weapon, this class can only initiated with json.
        // !This Class is only for processing data for a weapon, not used for the actual weapon fire action ingame!

        // Enum for weapon state
        enum WeaponState
        {
            onWait, // Waiting state before making the first shot
            onCD, // Fire CD state on each shot
            onFire, // Fire state on firing
            onReload, // Reload CD state when a weapon clip is out of ammo
            onEnd // End state after all clips are used
        }

        public float waitTime = 0f;
        public float fireCD = 0f;
        public float reloadTime = 0f;
        public int ammoAmount = 0; // Infinite ammon when set to -1
        public int clipAmount = 0; // Infinite clip when set to -1
        public bool disabled = false;

        WeaponState state = WeaponState.onWait;
        float curWaitTime = 0f;
        float curFireCD = 0f;
        float curReloadTime = 0f;
        float curAmmoAmount = 0f; 
        float curClipAmount = 0f;

        // Initialize needed non-public variable after object is constructed (!Call this method after JsonUtility!) 
        public void initialize()
        {
            state = WeaponState.onWait;
            curWaitTime = waitTime;
            curFireCD = fireCD;
            curReloadTime = reloadTime;
            curAmmoAmount = ammoAmount;
            curClipAmount = clipAmount;
        }

        // Method called on each fixedUpdate for count down the times for each state
        public void countWeapon()
        {
            // Check if the weapon is disabled before processing datas
            if (!disabled)
            {
                switch (state)
                {
                    case WeaponState.onWait:
                        if(curWaitTime > 0)
                        {
                            curWaitTime -= Time.fixedDeltaTime;
                        }
                        // Start from reload state after wait state
                        else
                        {
                            state = WeaponState.onReload;
                            curReloadTime = reloadTime;
                        }
                        break;
                    case WeaponState.onCD:
                        if (curFireCD > 0)
                        {
                            curFireCD -= Time.fixedDeltaTime;
                        }
                        // Change to fire state after CD state
                        else
                        {
                            state = WeaponState.onFire;
                        }
                        break;
                    case WeaponState.onReload:
                        if (curReloadTime > 0)
                        {
                            curReloadTime -= Time.fixedDeltaTime;
                        }
                        // Change to fire state after reload state
                        else
                        {
                            // Reduce clip amount after finish reload
                            curClipAmount -= 1;
                            curAmmoAmount = ammoAmount;
                            state = WeaponState.onFire;
                        }
                        break;
                }
            }
        }

        public bool isOnFire()
        {
            return state == WeaponState.onFire;
        }

        // Method for weapon fire and change to Reload or CD state dependents on the ammo remain (called after Instaite a bullet object)
        public void Fire()
        {
            // Reduce ammon on each fire
            curAmmoAmount -= 1;
            // If have ammo remain, change to CD state
            if (ammoAmount == -1)
            {
                // Keep curAmmoAmount in -1
                curAmmoAmount = -1;
                state = WeaponState.onCD;
            }
            else if(curAmmoAmount > 0)
            {
                state = WeaponState.onCD;
            }
            // If ammo is run out, change to reload state
            else if (clipAmount == -1)
            {
                curClipAmount = -1;
                state = WeaponState.onReload;
                curReloadTime = reloadTime;
            }
            else if (curClipAmount > 0)
            {
                state = WeaponState.onReload;
                curReloadTime = reloadTime;
            }
            // If clip is run out, change to end state
            else
            {
                state = WeaponState.onEnd;
            }
            // Reset fire CD time
            curFireCD = fireCD;
        }

        public bool isOnEnd()
        {
            return state == WeaponState.onEnd;
        }
    }
}
 