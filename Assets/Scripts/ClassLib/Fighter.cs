using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFighter
{
    public class Fighter
    {
        // Backend code for process data and state of a fighter object ingame, this class can only initiated with json.

        public int maxHp = 0;
        public int bulletDef = 0;
        public int crashDef = 0;
        public bool isInvincible = false;

        int curHp = 0;

        // Crash and Laser type damage protection time, making damage made by collisionStay from "per frame" to "by some seconds"
        const float crashDamageCD = 0.25f;
        const float laserDamageCD = 0.5f;
        float curCrashDamageCD = 0f;
        float curLaserDamageCD = 0f;

        // Initialize needed non-public variable after object is constructed (!Call this method after JsonUtility!) 
        public void initialize()
        {
            curHp = maxHp;
        }

        // Method for making hp change while taking damage (called on damage source class, can be used on other stage scripts)
        public int takeDamage(int damage, DamageSource.DamageType damageType)
        {
            // Check if the object is invincible before calculating damage
            if (!isInvincible)
            {
                int finalDamage = 0;
                // Check damage type and process damage
                switch (damageType)
                {
                    case DamageSource.DamageType.bullet:
                        // Using bullet def for bullet type damage
                        finalDamage = Mathf.Max(damage - bulletDef, 1);
                        curHp = Mathf.Max(0, curHp - finalDamage);
                        return finalDamage;
                    case DamageSource.DamageType.crash:
                        // Check crash type protection time and using crash def for crash type damage
                        if (curCrashDamageCD <= 0)
                        {
                            finalDamage = Mathf.Max(damage - crashDef, 1);
                            curHp = Mathf.Max(0, curHp - finalDamage);
                            curCrashDamageCD = crashDamageCD;
                        }
                        return finalDamage;
                    case DamageSource.DamageType.laser:
                        // Check laser type protection time and using bullet def for laser type damage
                        if (curLaserDamageCD <= 0)
                        {
                            finalDamage = Mathf.Max(damage - bulletDef, 1);
                            curHp = Mathf.Max(0, curHp - finalDamage);
                            curLaserDamageCD = laserDamageCD;
                        }
                        return finalDamage;
                }
            }
            return 0;
        }

        // Method for making hp change while healing (!Use positive int on argument!)
        public void heal() 
        {
            // Full heal when no arguments assigned
            curHp = maxHp;
        }

        public void heal(int healAmount)
        {
            // Set negetive healAmount to make a real damage
            curHp = Mathf.Clamp(curHp + healAmount, 0, maxHp);
        }

        // Method called on each fixedUpdate for count down the collision damage CDs
        public void countDamageCD ()
        {
            if(curCrashDamageCD > 0)
            {
                curCrashDamageCD -= Time.fixedDeltaTime;
            }
            if(curLaserDamageCD > 0)
            {
                curLaserDamageCD -= Time.fixedDeltaTime;
            }
        }

        // Method for checking the fighter is still alive (usually used for check and destroy died object)
        public bool isAlive()
        {
            return curHp > 0;
        }

        // Method for return the HP percentage  (usually used for HealBar UI)
        public float getHealPercentage()
        {
            return (float)curHp / (float)maxHp;
        }
    }
}