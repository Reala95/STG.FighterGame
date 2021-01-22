using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Common_FighterObject : MonoBehaviour
{
    // Super class for all fighterObjects
    // This class contains all basic variables and mehtods for both player's and enemy's fighterObject

    public TextAsset fighterJson;
    public TextAsset fighterCrashWeaponJson;
    public string crashTarget;

    protected Fighter fighterData = new Fighter();
    protected DamageSource fighterCrashWeaponData;

    // Common Awake method that may need to be called in the child class's Awake (Some fighterObject may not need this)
    protected void FighterObjectAwake()
    {
        fighterData = JsonUtility.FromJson<Fighter>(fighterJson.text);
        fighterData.initialize();
        fighterCrashWeaponData = JsonUtility.FromJson<DamageSource>(fighterCrashWeaponJson.text);
    }

    // Common FixedUpdate method that may need to be called in the child class's FixedUpdate (Some fighterObject may not need this)
    protected void FighterObjectFixedUpdate()
    {
        if (fighterData.isAlive())
        {
            fighterData.countDamageCD();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // The CollisionStay method for making crash damage when two fighter are colliding each others
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if(target.tag == crashTarget)
        {
            fighterCrashWeaponData.MakeDamage(target);
        }
    }

    // Getter for other objects that need to interactive with the backend fighter data
    public Fighter getFighterData()
    {
        return fighterData;
    }
}
