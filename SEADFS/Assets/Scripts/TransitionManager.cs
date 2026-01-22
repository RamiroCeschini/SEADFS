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
        StartCoroutine(Transition(scene));
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
