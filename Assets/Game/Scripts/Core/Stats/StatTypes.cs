
namespace Core.Stats {

    public enum StatType {

        health, // Hit point pool
        mana, // Magic point pool

        strength, // Used to calculate physic damage
        wisdom, // Used to calculate magic damage and max mana
        constitution, // Used to calculate physic defense and max health
        faith, // Used to calculate magic defense
        agility, // Used to calculate movement speed, action speed
        evasion, // Used to calculate miss rate
        accuracy, // Used to calculate hit rate

        maxHealth, // Maximum health points
        maxMana, // Maximum mana points

        physicAttack, // Used to calculate physic damage
        magicAttack, // Used to calculate magic damage

        physicDefense, // Used to calculate physic damage
        magicDefense, // Used to calculate magic damage

        damage, // Final damage to the health applied to character

        movementSpeed, // Movement speed in combat
        actionSpeed, // Action speed incombat
        hitRate, // Chances of hitting the target without missing
        missRate // Chances of evading a hit

    }

}


