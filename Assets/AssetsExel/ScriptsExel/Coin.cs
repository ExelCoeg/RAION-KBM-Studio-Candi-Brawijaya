
using UnityEngine;
public class Coin : MonoBehaviour{
    public int coinCount;

    public void Update(){
        if(coinCount <= 0){
            coinCount  = 0;
        }
    }
    public void increaseCoin(int amount){
        coinCount += amount;
    }
    public void decreaseCoin(int amount){
        coinCount -= amount;
    }
    
}