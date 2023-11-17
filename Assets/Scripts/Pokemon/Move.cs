using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public PokemonMoves Base { get; set; }
    public int PP { get; set; }

    public Move(PokemonMoves pBase, int pp) {
        Base = pBase;
        PP = pp;

    }

}
