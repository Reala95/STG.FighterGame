using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Common_BulletObject : MonoBehaviour
{
    // Super class for all bulletObjects
    // This class contains all basic variables and mehtods that every bulletObject needed

    public TextAsset bulletJson;
    public string hitTarget;
    public string breakBullet;
    public bool isPiercing;
    public bool isBulletBreaker;

    public int soundIndex = -1;

    protected Rigidbody2D bullet;
    protected DamageSource bulletData;

    // Common Awake method that may need to be called in the child class's Awake (Some bulletObject may not need this)
    protected void BulletObjectAwake()
    {
        bulletData = JsonUtility.FromJson<DamageSource>(bulletJson.text);
        bullet = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (soundIndex != -1)
        {
            GameObject.FindGameObjectWithTag(uiSEManager).GetComponents<AudioSource>()[soundIndex].Play();
        }
    }

    // Upated Method to delete the bullet if it is out of the screen 
    private void Update()
    {
        if (transform.position.x >= 10 || transform.position.x <= -10 || transform.position.y >= 6 || transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    // Common CollsionEnter method that may need to be called in the child class's COllisionEnter ((Some bulletObject may not need this))
    protected void BulletObjectOnCollisionEnter2D(Collision2D collision, GameObject target)
    {
        // Get the gameObject colliding with this object
        if(target == null)
        {
            target = collision.gameObject;
        }
        // Check if the target object is match with the hitable target tag, then make damage to the target
        if (target.tag == hitTarget)
        {
            bulletData.MakeDamage(target);
            // Check if this bulletObject is a piercing bullet or not, if not then destroy this bullet
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }
        // If the target is not a hitTarget but a bulletOjbect that match with the breakable bullet tag, then destory the target bullet 
        else if (isBulletBreaker && target.tag == breakBullet)
        {
            Destroy(target);
        }
    }
}