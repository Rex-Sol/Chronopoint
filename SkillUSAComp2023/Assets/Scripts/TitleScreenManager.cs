using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject creditsCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        mainCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("2.)Character Select");
    }

    public void Settings()
    {
        mainCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void Credits()
    {
        mainCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void Back()
    {
        mainCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("balls");
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
