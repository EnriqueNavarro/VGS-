using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterSelection {
    public static ClassNames chosen;
    public static void ShadowDancer()
    {
        chosen = ClassNames.ShadowDancer;
    }
    public static void CrystalSword()
    {
        chosen = ClassNames.CrystalSword;
    }
}
