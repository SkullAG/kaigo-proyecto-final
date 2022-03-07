
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

        physicAttack, // Attack of physical properties (impacts, slash, pierce, etc)
        magicAttack, // Attack of magical and/or elemental properties (curses, fire, ice, thunder, etc)

        physicDefense, // Defense towards physical attacks
        magicDefense, // Defense towards magical attacks

        movementSpeed, // Movement speed in combat
        actionSpeed // Action speed incombat

    }

}


