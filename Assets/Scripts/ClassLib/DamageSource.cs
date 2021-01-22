using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFighter
{
    public class DamageSource
    {
        // Backend code for process data for a damage source like bullets, this class can only initiated with json.

        // Enum for damage type
        public enum DamageType
        {
            bullet, // Basic damage type of bullets
            crash, // Damage type for one fighter collide with another fighter (Used collisionStay)
            laser // Damage type for laser, a bullet that us the collsionStay
        }

        public DamageType damageType = DamageType.bullet;
        public int damage = 0;

        // Method for making damage to a target fighter object (usually called on collision methods)
        public int MakeDamage(GameObject target)
        {
            Fighter targetData = target.GetComponent<Common_FighterObject>().getFighterData();
            return targetData.takeDamage(damage, damageType);
        }
    }
}