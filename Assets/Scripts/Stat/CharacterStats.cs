using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stats strength;//力量
    public Stats agility;//敏捷
    public Stats intelligence;//智力
    public Stats vitality;//活力

    [Header("Offensive stats")]
    public Stats damage;
    public Stats critChance;
    public Stats critPower;



    [Header("Defensive stats")]
    public Stats maxHealth;
    public Stats armor; // 护甲
    public Stats evasion; //闪避
    public Stats magicResistance;// 魔抗

    [Header("Magic stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightningDamage;

    public bool isIgnited;//持续灼烧
    public bool isChilled;//降低20%护甲
    public bool isShocked;//降低20%攻击准确率

    private float ignitedTimer;
    private float chilledTimer;
    private float shockTimer;


    private float ignitedDamageCooldown = .3f;
    private float ignitedDamageTimer;
    private int ignitedDamage;

    public int currentHealth;
    public System.Action onHealthChanged;

    private EntityFX fx;

    private void Awake()
    {
        fx = GetComponent<EntityFX>();
    }


    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();
    }


    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;


        ignitedDamageTimer -= Time.deltaTime;

        if(ignitedTimer < 0)
        {
            isIgnited = false;
        }

        if(chilledTimer < 0)
        {
            isChilled = false;
        }
        if(shockTimer < 0)
        {
            isShocked = false;
        }

        if(ignitedDamageTimer < 0 && isIgnited)
        {
            DecreaseHealthBy(ignitedDamage);
            if(currentHealth < 0)
            {
                Die();
            }

            ignitedDamageTimer = ignitedDamageCooldown;
        }
    }







    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
        {
            return;
        }

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        DoMagicalDamage(_targetStats);

    }


    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = lightningDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();
        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);

        _targetStats.TakeDamage(totalMagicalDamage);


        if(Mathf.Max(_fireDamage,_iceDamage,_lightningDamage) <= 0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canApplyShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;
        while(!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if(Random.value < .33f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .66f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < 1f && _lightningDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        }

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

    }

    private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);

        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAilments(bool _ignite,bool _chill,bool _shock)
    {
        if(isIgnited || isChilled || isShocked)
        {
            return;
        }

        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = 2;
            fx.IgniteFxFor(2);
        }

        if (isChilled)
        {
            isChilled = _chill;
            chilledTimer = 2;
        }

        if (isShocked)
        {

            isShocked = _shock;
            shockTimer = 2;
        }
    }


    public void SetupIgniteDamage(int _damage) => ignitedDamage = _damage;


    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if(currentHealth < 0)
        {
            Die();
        }



    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;
        onHealthChanged?.Invoke();
    }



    protected virtual void Die()
    {

    }
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked) { totalEvasion += 20; }

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }
    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        }
        else
        {
            totalDamage -= _targetStats.armor.GetValue();
        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();
        if(Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        float cirtDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(cirtDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue();
    }


}
