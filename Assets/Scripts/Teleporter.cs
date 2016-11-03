using UnityEngine;
using System.Collections;
using System;

public class Teleporter : MonoBehaviour, ITargetAnalyzer
{
    public Texture2D crosshair;
    public float maxDistance = 13;
    public AudioClip clip;
    public float volume = 0.8f;
    private TeleportWaypoint teleportWaypoint;
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

    void Update ()
    {
        if (justShoot)
        {
            justShoot = false;
            animator.SetBool("teleportShoot", false);
        }
        UpdateDetectedWaypoint();
        if (Input.GetMouseButtonDown(0) && teleportWaypoint != null)
        {
            teleportWaypoint.TeleportObject(gameObject);
            animator.SetBool("teleportShoot", true);
            justShoot = true;
            audioController.play(clip, volume);
        }
    }

    void UpdateDetectedWaypoint()
    {
        Transform cameraTransform = camera.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && maxDistance >= hit.distance)
        {
            GameObject hitObject = hit.collider.gameObject;
            teleportWaypoint = hitObject.GetComponent<TeleportWaypoint>();
        } else
        {
            teleportWaypoint = null;
        }
    }

    void OnEnable()
    {
        animator.SetBool("teleportPower", true);
    }

    void OnDisable()
    {
        animator.SetBool("teleportPower", false);
    }

    public bool InSight()
    {
        return teleportWaypoint != null;
    }

    public Texture2D GetInSightTexture()
    {
        return crosshair;
    }
}