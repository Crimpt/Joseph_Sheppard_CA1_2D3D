using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string newGameLevel = "Level1";
    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
