
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    bool pause = false;
    [SerializeField] GameObject pausePanel;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !pause){
            Pause();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && pause){
            Resume();
        }

    }
    public void Pause(){
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume(){

        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Exit(){
        Application.Quit();
    }
}
