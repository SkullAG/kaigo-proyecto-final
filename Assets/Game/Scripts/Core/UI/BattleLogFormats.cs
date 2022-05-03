using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleLogFormats
{
    /// <summary> Actor name, spell name. </summary>
    public static string SKILL_CHARGE = "{0} begins casting {1}!";

    /// <summary> Actor name, item name. </summary>
    public static string ITEM_CHARGE = "{0} begins using {1}!";

    /// <summary> Actor name, skill name. </summary>
    public static string SKILL_USE = "{0} casts {1}.";
    
    /// <summary> Actor name, item name. </summary>
    public static string ITEM_USE = "{0} uses {1}.";

    /// <summary> Actor name, damage points. </summary>
    public static string DAMAGE_RECEIVED = "{0} has received {1} points of damage!";

    /// <summary> Actor name, health points. </summary>
    public static string HEALTH_RECOVERED = "{0} has recovered {1} HP!";

    /// <summary> Actor name, action points. </summary>
    public static string ACTIONP_RECOVERED = "{0} has recovered {1} AP!";

    /// <summary> Actor name. </summary>
    public static string ATTACK_EVASION = "{0} has evaded the attack!";

    /// <summary> Actor name, state name. </summary>
    public static string STATUS_AFFLICTION = "{0} has been afflicted by {1}."; 

    /// <summary> State name, action name. </summary>
    public static string STATE_DISPELLED = "{0}'s effects on {1} had been dispelled.";

    /// <summary> Actor name. </summary>
    public static string DEATH = "{0} has died.";

}
