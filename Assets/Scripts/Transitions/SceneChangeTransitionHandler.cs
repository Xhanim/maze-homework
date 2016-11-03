using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneChangeTransitionHandler : BaseTransitionHandler {

    public string sceneToLoad;
    public GameObject avatar;
    private AsyncOperation operation;
    private Transition transition;
    private bool loading;
    private Scene currentScene;
    private RigidbodyFirstPersonController controller;

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

    public override void OnTransitionBegin(Transition transition)
    {
        if (avatar != null)
        {
            RigidbodyFirstPersonController controller = avatar.GetComponent<RigidbodyFirstPersonController>();
            controller.movementSettings.canControl = false;
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
