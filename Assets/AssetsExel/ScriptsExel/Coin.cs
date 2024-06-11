
using UnityEngine;
public class Coin : MonoBehaviour{
    public int coinCount;
    public void Update(){
        if(coinCount >= 1){
            coinCount  = 1;
        }
    }
    public void increaseCoin(int amount){
        coinCount += amount;
    }
    public void decreaseCoin(int amount){
        coinCount -= amount;
    }
    
}