using System;
using ExtensionsFunctions;
using Managers;
using Model;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private static readonly int Fire1 = Animator.StringToHash("fire");
    private static GameManager Game => GameManager.Instance;
    private static CannonModel Model => Game.CurrentCannonModel;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ReloadBar reloadBar;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip noAmmoClip, aimingClip;
    private float _fireAccumulator;
    private AudioClip _fireClip;
    private bool _isAiming;
    private GameObject _crosshair;
    private float _maxDistance;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Awake()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>(Model.Sprite);
        _fireClip = Resources.Load<AudioClip>(Model.FireClip);
    }

    private void Start()
    {
        _maxDistance = Mathf.Min(MainCamera.mainCam.GetHeight(), MainCamera.mainCam.GetWidth()) * 0.95f;
    }

    void Update()
    {
        _fireAccumulator += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Game.CannonAmmo <= 0)
            {
                MainCamera.AudioSource.PlayOneShot(noAmmoClip);
                return;
            }

            _isAiming = true;
            _fireAccumulator = 0;
            MainCamera.AudioSource.PlayOneShot(aimingClip);
            _crosshair = Instantiate(
                original: crosshair,
                position: spawnPoint.position,
                rotation: Quaternion.identity
            );
        }

        if (_isAiming)
        {
            var direction = ((Vector2)MainCamera.mainCam.ScreenToWorldPoint(Input.mousePosition)).normalized;
            _crosshair.transform.position =
                direction * (_maxDistance * Mathf.Min(1f, 0.12f + _fireAccumulator / (100f / Model.Speed)));

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isAiming = false;
                var pos = _crosshair.transform.position;
                Destroy(_crosshair);
                Fire(pos);
            }
        }
    }

    private void Fire(Vector2 destination)
    {
        MainCamera.AudioSource.PlayOneShot(_fireClip);
        animator.SetTrigger(Fire1);
        var spawnPos = spawnPoint.position;
        var newCannonBall = Instantiate(
            original: cannonBall,
            position: spawnPos,
            rotation: MainCamera.mainCam.AngleToMouse(from: spawnPos)
        );
        var speedMultiplier = 1f + 2f * (1f - destination.magnitude / _maxDistance);
        var script = newCannonBall.GetComponent<CannonBall>();
        script.Destination = destination;
        script.Duration /= speedMultiplier;
        newCannonBall.GetComponent<Animator>().SetFloat(Speed, speedMultiplier);
        --Game.CannonAmmo;

        // Reload if no more ammo
        if (!reloadBar.IsReloading && Game.CannonAmmo <= 0)
            reloadBar.Reload(
                duration: 100f / Model.Reload,
                reloadCallback: () => { GameManager.Instance.CannonAmmo = 1; });
    }
}