using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public static void Load(SimScene scene)
    {
        TransitionManager.Instance.LoadScene(scene);
    }

    public static void ReloadCurrent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
public enum SimScene
{
    Home,
    Selection,
    Simulator,
    Profiles,
    ProfileList,
    ProfileMenu,
    ProfileViewer,
    Settings
}
