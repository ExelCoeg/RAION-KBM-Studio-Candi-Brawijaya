
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    bool pause = false;

    [SerializeField] GameObject pausePanel;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!pause)Pause();
            else Resume();
        }

    }
    public void Pause(){
        pausePanel.SetActive(true);
        pause = true;
        Time.timeScale = 0;
        
    }
    public void Resume(){

        Time.timeScale = 1;
        pause = false;
        pausePanel.SetActive(false);
    }
    public void Exit(){
        Application.Quit();
    }
}
