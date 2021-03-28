using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Common_WeaponObject : MonoBehaviour
{
    // Class for all normal weaponObjects
    // This class can directly used into a normal weaponObject

    public GameObject[] bulletPrefabs;
    public TextAsset [] WeaponJsons;
    public int maxLevel;
    public bool isFireWithRotation = false;

    protected int weaponLevel = 0;
    protected Weapon[] weaponDatas;

    private void Awake()
    {
        WeaponObjectAwake();
    }

    private void FixedUpdate()
    {
        WeaponObjectFixedUpdate();
    }

    // Common Awake method that may need to be called in the child class's Awake (Some weaponObject may not need this)
    protected void WeaponObjectAwake()
    {
        weaponDatas = new Weapon[maxLevel];
        for (int i = 0; i < weaponDatas.Length; i++)
        {
            weaponDatas[i] = JsonUtility.FromJson<Weapon>(WeaponJsons[i].text);
        }
        initializeWeapons();
    }

    // Common FixedUpdate method that may need to be called in the child class's FixedUpdate (Some weaponObject may not need this)
    protected void WeaponObjectFixedUpdate()
    {
        if (!weaponDatas[weaponLevel].isOnFire())
        {
            weaponDatas[weaponLevel].countWeapon();   
        }
        else
        {
            if (isFireWithRotation)
            {
                Instantiate(bulletPrefabs[weaponLevel], transform.position, transform.rotation);
            }
            else
            {
                Instantiate(bulletPrefabs[weaponLevel], transform.position, Quaternion.identity);
            }
            weaponDatas[weaponLevel].Fire();
        }
    }

    // Setter to set the disabled bool whitin the backend weapon data
    public void setWeaponDisabled(bool disabled)
    {
        foreach(Weapon w in weaponDatas)
        {
            w.disabled = disabled;
        }
    }

    // Methods for level up the weapon
    public void weaponUp()
    {
        weaponLevel = Mathf.Min(maxLevel - 1, weaponLevel + 1);
        initializeWeapons();
    }

    // Methods for refresh all weapon datas in the list
    public void initializeWeapons()
    {
        foreach(Weapon w in weaponDatas)
        {
            w.initialize();
        }
    }

    // Getter for returning the current weaponLevel
    public int getWeaponLevel()
    {
        return weaponLevel;
    }

    public Weapon getCurWeaponData()
    {
        return weaponDatas[weaponLevel];
    }
}
