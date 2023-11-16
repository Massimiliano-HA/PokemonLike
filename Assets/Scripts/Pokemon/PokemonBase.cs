using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]

public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    //Base Stats 

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int specialAttack;
    [SerializeField] int specialDefense;
    [SerializeField] int speed;
    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public Sprite FrontSprite {
        get { return frontSprite; }
    }

    public Sprite BackSprite {
        get { return backSprite; }
    }

    public PokemonType Type1 {
        get { return type1; }
    }

    public PokemonType Type2 {
        get { return type2; }
    }

    public int MaxHp {
        get { return maxHp; }
    }

    public int Attack {
        get { return attack; }
    }

    public int Defense {
        get { return defense; }
    }

    public int SpecialAttack {
        get { return specialAttack; }
    }

    public int SpecialDefense {
        get { return specialDefense; }
    }

    public int Speed {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

}

[System.Serializable]
public class LearnableMove {

    [SerializeField] PokemonMoves move;
    [SerializeField] int level;

    
    public PokemonMoves Move {
        get { return move; }
    }

    public int Level {
        get { return level; }
    }
}

public enum PokemonType {
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dark,
    Dragon,
    Steel,
    Fairy

}
