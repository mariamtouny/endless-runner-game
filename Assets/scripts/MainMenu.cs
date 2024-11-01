using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button toggleButton;
    public Sprite unmutedSprite;
    public Sprite mutedSprite;
    private bool isMuted = false;
    public AudioManager audioManager;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject controlPanel;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

    }
    private void Start()
    {
        optionsPanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);

        audioManager.PlayMusic(audioManager.mainmenu);
        toggleButton.onClick.AddListener(ToggleMute);
        UpdateButtonImage();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Play");
        Time.timeScale = 1;
    }

    public void ViewOptions()
    {
       
            optionsPanel.SetActive(true);
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackOptions()
    {
        optionsPanel.SetActive(false);
    }
    public void BackControls()
    {
        controlPanel.SetActive(false);
    }

    public void BackCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void HowTo()
    {
        controlPanel.SetActive(true);
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        toggleButton.image.sprite = isMuted ? mutedSprite : unmutedSprite;
    }
}
