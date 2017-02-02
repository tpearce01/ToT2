using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

public abstract class Unit : MonoBehaviour
{

    //Resources
    public float health;
	public PAttribute maxHealth = new PAttribute();
    public float resource;
	public PAttribute maxResource = new PAttribute();

    //Secondary stats
    protected PAttribute armor = new PAttribute();
	protected PAttribute criticalChance = new PAttribute();
	protected PAttribute criticalDamage = new PAttribute();
	protected PAttribute multistrikeChance = new PAttribute();
	protected float damageOutModifier;
	protected float damageInModifier;
	protected int intDamageOutModifier;
	protected int intDamageInModifier;
	protected float healModifier;
	protected float resourceModifier;

    //Other
	protected float variance = 0.1f;  //+/-10% variance
    //private List<Skill> skills = new List<Skill>();
	protected bool isDead = false;

    public Unit()
    {
        health = maxHealth.baseValue = 100;
		resource = 0;
		maxResource.baseValue = 100;
        armor.baseValue = armor.modifier = 0;
        criticalChance.baseValue = criticalChance.modifier = 0;
        criticalDamage.baseValue = 1f;
        criticalDamage.modifier = 0;
        multistrikeChance.baseValue = multistrikeChance.modifier = 0;
        damageInModifier = 0;
        damageInModifier = 0;
        healModifier = 0;
        resourceModifier = 0;
    }
		



    public virtual void ModifyHealth(float value)
    {
        //Modify with variance
        value *= 1 + Random.Range(-variance, variance);

        if (value < 0)
        {
            //Perform modifiers;
            value *= 1 + damageInModifier;  //Percent damage intake modifier
            value += intDamageInModifier;   //Flat damage intake modifier
        }
        else
        {   //Heal
            value *= 1 + healModifier;  //Percent heal modifier
        }

        //Calculate new health
        health += value;
        if (health <= 0)
        {
            Kill(); //If health drops below 0, unit is dead
        }
        else if (health >= maxHealth.Value())
        {
            health = maxHealth.Value(); //If health exceeds maximum, normalize
        }

		HUDManager.i.UpdateHUD ();
    }

    public void ModifyResource(float value)
    {

        value *= 1 + resourceModifier;
        resource += value;
		if (resource >= maxResource.Value ()) {
			resource = maxResource.Value ();
		} else if (resource < 0) {
			resource = 0;
		}

		HUDManager.i.UpdateHUD ();
    }

	public abstract void Kill ();

    public void Ressurect(int healthh, int resourcee)
    {
        isDead = false;
        health = healthh;
        resource = resourcee;
    }

    public void Ressurect(int healthh)
    {
        isDead = false;
        health = healthh;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

	void Awake(){
		InitializeAwake ();
	}

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        PlayerInput();
    }


	public abstract void InitializeAwake();
    public abstract void PlayerInput();
    public abstract void Initialize();


}
