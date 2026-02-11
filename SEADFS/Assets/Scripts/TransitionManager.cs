using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 0.5f;
    [SerializeField] private AudioClip transition;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(SimScene scene)
    {
        if (scene.ToString() == "Home" || (scene.ToString() == "Selection" && SceneManager.GetActiveScene().name == "Home") || (scene.ToString() == "ProfileMenu" && SceneManager.GetActiveScene().name == "Home")
            || (scene.ToString() == "Settings" && SceneManager.GetActiveScene().name == "Home"))
        {
            StartCoroutine(Transition(scene));
        }
        else
        {
            SceneManager.LoadScene(scene.ToString());
        }
        
    }

    private IEnumerator Transition(SimScene scene)
    {
        AudioManager.Instance.PlaySFX(transition);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene.ToString());

        yield return null;

        animator.SetTrigger("Open");
    }
}
