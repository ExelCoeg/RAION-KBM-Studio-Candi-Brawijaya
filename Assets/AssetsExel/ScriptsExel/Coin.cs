
using UnityEngine;
public class Coin : MonoBehaviour{
    public int coinCount;
    
    public void increaseCoin(int amount){
        coinCount += amount;
    }
    public void decreaseCoin(int amount){
        coinCount -= amount;
    }

}