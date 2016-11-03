using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenTransitionHandler : BaseTransitionHandler {

    public string sceneToLoad;
    private AsyncOperation operation;
    private Transition transition;
    private bool loading;
    private Scene currentScene;

    void Update()
    {
        if (loading && operation.isDone)
        {
            transition.waitingTime = 0;
            loading = false;
            Scene newScene = SceneManager.GetSceneByName(sceneToLoad);
            SceneManager.MoveGameObjectToScene(transition.gameObject, newScene);
            SceneManager.UnloadScene(currentScene);
        }
    }

    public override void OnWaitingBegin(Transition transition)
    {
        loading = true;
        this.transition = transition;
        transition.waitingTime = 99999;
        currentScene = SceneManager.GetActiveScene();
        operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }
}
