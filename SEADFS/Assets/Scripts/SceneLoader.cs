using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public static void Load(SimScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
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
