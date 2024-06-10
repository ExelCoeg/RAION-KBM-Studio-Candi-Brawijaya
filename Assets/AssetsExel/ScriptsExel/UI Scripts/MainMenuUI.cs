
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{   
    public Button startButton;
    public Button exitButton;

    public Sprite[] startButtonSprites;
    public Sprite[] exitButtonSprites;

    public void startMouseOn(){
        startButton.image.sprite = startButtonSprites[0];
    }
    public void startMouseOff(){
        startButton.image.sprite = startButtonSprites[1];
    }
    public void exitMouseOn(){
        exitButton.image.sprite = exitButtonSprites[0];
    }
    public void exitMouseOff(){
        exitButton.image.sprite = exitButtonSprites[1];
    }

   
    public void StartGame(){
        
        SceneManager.LoadScene(1);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
