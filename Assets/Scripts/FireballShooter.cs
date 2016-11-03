using UnityEngine;
using System.Collections;
using System;

public class FireballShooter : MonoBehaviour, ITargetAnalyzer {
    public GameObject prefab;
    public GameObject spawn;
    public float aimCenterDistance = 10;
    public float impulse = 50;
    public Texture2D crosshair;
    public AudioClip clip;
    public float volume = 0.8f;
    private Camera camera;
    // the animator controller from the gauntlet
    private Animator animator;
    private bool justShoot;
    private AudioController audioController;

    void Awake()
    {
        // this is shitty code #shame
        animator = GetComponent<GauntletController>().gauntletModel.GetComponent<Animator>();
        audioController = GameObject.FindGameObjectWithTag("Avatar").GetComponent<AudioController>();
    }

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void Update () {
        if (justShoot)
        {
            justShoot = false;
            animator.SetBool("fireballShoot", false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            // Make spawn look at camera position
            Transform cameraTransform = camera.transform;
            Vector3 cameraPosition = cameraTransform.position;
            cameraPosition += cameraTransform.forward * aimCenterDistance;
            spawn.transform.LookAt(cameraPosition);
            GameObject ballInstance = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation) as GameObject;
            ballInstance.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * impulse, ForceMode.Impulse);
            animator.SetBool("fireballShoot", true);
            justShoot = true;
            audioController.play(clip, volume);
        }
    }

    void OnEnable()
    {
        animator.SetBool("fireballPower", true);
    }
    
    void OnDisable()
    {
        animator.SetBool("fireballPower", false);
    }

    public bool InSight()
    {
        return true;
    }

    public Texture2D GetInSightTexture()
    {
        return crosshair;
    }
}
