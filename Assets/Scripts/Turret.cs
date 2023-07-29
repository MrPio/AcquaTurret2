using ExtensionsFunctions;
using Managers;
using Model;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private static GameManager Game => GameManager.Instance;
    private static TurretModel Model => Game.CurrentTurretModel;
    [SerializeField] private GameObject leftSpawnPoint;
    [SerializeField] private GameObject rightSpawnPoint;
    [SerializeField] private GameObject bullet,laser;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public ReloadBar reloadBar;
    [SerializeField] private AudioClip reloadStart, reloadMiss, reloadFinish, noAmmo;
    [SerializeField] private AudioSource bulletAudioSource;
    private AmmoCounter _ammoCounter;
    private float _fireAccumulator=9999f;
    private AudioClip _fireClip;
    private bool _isLaserFiring;

    private void OnEnable()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>(Model.Sprite);
        _fireClip = Resources.Load<AudioClip>(Model.FireClip);
        _ammoCounter = GameObject.FindWithTag("ammo_counter").GetComponent<AmmoCounter>();
    }

    private void Update()
    {
        _fireAccumulator += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Game.Ammo <= 0)
            MainCamera.AudioSource.PlayOneShot(noAmmo);
        if (Input.GetMouseButton(0) && Game.Ammo > 0 && _fireAccumulator > 100f / Model.Rate && !reloadBar.IsReloading &&(Model.Name != "Laser Gun" || !_isLaserFiring))
        {
            _fireAccumulator = 0;
            if (Game.CurrentTurret == 2)
            {
                Fire(Game.Ammo % 2 == 0 ? leftSpawnPoint: rightSpawnPoint);
            }
            else
            {
                Fire(leftSpawnPoint);
                Fire(rightSpawnPoint);
            }

            bulletAudioSource.PlayOneShot(_fireClip);
        }

        if (Input.GetMouseButtonUp(0))
        {
            bulletAudioSource.Stop();
            _isLaserFiring = false;
            _fireAccumulator += 99999f;
        }

        if (!reloadBar.IsReloading && Input.GetKeyDown(KeyCode.R))
            if (Game.Ammo < Game.CurrentTurretModel.Ammo)
                Reload();
            else
                MainCamera.AudioSource.PlayOneShot(reloadMiss);
    }

    private void Fire(GameObject arm)
    {
        var armPos = arm.transform.position;
        var newBullet = Instantiate(
            original: Model.Name=="Laser Gun"?laser:bullet,
            position: armPos,
            rotation: MainCamera.MainCam.AngleToMouse(transform.position)
        );
        if (Model.Name == "Laser Gun")
        {
            var laser=newBullet.GetComponent<Laser>();
            laser.Arm = arm.transform;
            laser.Turret = transform;
            _isLaserFiring = true;   
            return;
        }
        newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.right * Model.Speed / 100f;
        --Game.Ammo;
        _ammoCounter.UpdateUI();

        // Reload if no more ammo
        if (!reloadBar.IsReloading && Game.Ammo <= 0)
            Reload();
    }

    public  void Reload()
    {
        MainCamera.AudioSource.PlayOneShot(reloadStart);
        reloadBar.Reload(
            duration: 100f / Model.Reload,
            reloadCallback: () =>
            {
                MainCamera.AudioSource.PlayOneShot(reloadFinish);
                Game.Ammo = Game.CurrentTurretModel.Ammo;
                GameObject.FindWithTag("ammo_counter").GetComponent<AmmoCounter>().UpdateUI();
            });
    }
}