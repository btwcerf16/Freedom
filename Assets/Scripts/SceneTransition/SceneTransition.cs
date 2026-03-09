using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private Animator _animator;
    private AsyncOperation _loadingScreenOperation;

    public Text LoadingProgressText;
    public Image LoadingProgressBar;

    public static SceneTransition Instance;
    public static bool IsLoaded = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Instance = this;
        if (IsLoaded)
        {
            _animator.SetTrigger("End");
        }
    }
    private void Update()
    {
        if(_loadingScreenOperation!= null)
        {
            LoadingProgressText.text = Mathf.RoundToInt(_loadingScreenOperation.progress * 100.0f) + "%";
            LoadingProgressBar.fillAmount = _loadingScreenOperation.progress;
        }

    }
    public static void SwitchScene(string sceneName)
    {
        Instance._animator.SetTrigger("Start");

        Instance._loadingScreenOperation = SceneManager.LoadSceneAsync(sceneName);
        Instance._loadingScreenOperation.allowSceneActivation = false;
    }
    public void OnAnimationOver()
    {
        IsLoaded = true;
        _loadingScreenOperation.allowSceneActivation = true;

    }
}
