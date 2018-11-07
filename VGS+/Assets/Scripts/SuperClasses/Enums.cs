using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour {

	
}
public enum ClassNames
{
    ShadowDancer,
    TwilightGuardian,
    CrystalSword,
    UnhingedFlame
};
public enum Elements
{
    fire,
    frost,
    poison,
    light,
    shadow,
    physical,
    none
};
public enum Enemies {
    Plate,
    Mail,
    Leather,
    Cloth,
    Jailer
};
public enum sStats {
    Particleflefx,
    Duration,
    Range,
    DmgType,
    Damage,
    CD,
    CritChance,//Abilities dont have inherent critChance, critDmg or speed, those are from the class itself
    CritDamage,
    Speed
};
