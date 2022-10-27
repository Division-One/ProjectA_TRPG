using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties
{

   int gold = 1000;
    // Start is called before the first frame update
    public int GetGold()
    {
        return gold;
    }
   public void UseGold(int amount)
    {
        gold -= amount;
    }
    public void GainGold(int amount)
    {
        gold += amount;
    }
}
