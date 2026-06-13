using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    private InputActions inputActions;
    private InputAction menu;
    private bool isPaused;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject player;



    private void Awake()
    {
        inputActions = new InputActions();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        menu = inputActions.Menu.Escape;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
