
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
   public void Retry(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

   }
   public void MainMenu(){
        SceneManager.LoadScene(0);
   }
}
