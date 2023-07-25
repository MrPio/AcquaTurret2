using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionsFunctions;
using Managers;
using Model;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ship : MonoBehaviour
{
    private static readonly int ShipDamage = Animator.StringToHash("ship_damage");
    private static readonly int ShipDestroy = Animator.StringToHash("ship_destroy");
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private List<GameObject> explosions;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject floatingTextBig, missile;
    private Sprite _missileSprite;
    private AudioClip _fireClip;
    private MoneyCounter _moneyCounter;
    private AudioClip _explodeClip;
    private ShipModel _model;
    private int _health;
    private bool _hasDelay = true;
    private float _accumulator;

    private void Awake()
    {
        _model = GameManager.Instance.CurrentWave.Spawn();
        spriteRenderer.sprite = Resources.Load<Sprite>(_model.Sprite);
        boxCollider.size = spriteRenderer.bounds.size;
        if (_model.ExplodeClip != null)
            _explodeClip = Resources.Load<AudioClip>(_model.ExplodeClip);
        _fireClip = Resources.Load<AudioClip>(_model.FireClip);
        _moneyCounter = GameObject.FindWithTag("money_counter").GetComponent<MoneyCounter>();
        GetComponent<ShipPath>().Model = _model;
        var pos = MainCamera.MainCam.RandomBoundaryPoint() * 1.1f;
        transform.SetPositionAndRotation(pos, pos.toQuaternion());
        if (_model.MissileSprite is { })
            _missileSprite = Resources.Load<Sprite>(_model.MissileSprite);

        // Custom Path for SpeedBoat
        if (_model.Name == "SpeedBoat")
            GetComponent<ShipPath>().AddPath(
                Random.Range(0, 2) == 0
                    ? new List<Vector2> { Vector2.zero }
                    : new List<Vector2>
                    {
                        Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.forward) * pos,
                        Vector2.zero
                    }
            );
    }

    void Start()
    {
        _health = _model.Health;
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_model.Rate <= 0.001f)
            return;
        _accumulator += Time.deltaTime;
        if (_hasDelay && _accumulator >= _model.Delay)
        {
            _accumulator = 0;
            _hasDelay = false;
        }
        else if (_accumulator >= 100f / _model.Rate)
        {
            _accumulator = 0;
            Fire();
        }
    }

    private void Fire()
    {
        const float range = 0.9f;
        var currentPos = (Vector2)transform.position;
        var destination = new Vector2(
            x: Random.Range(-range, range),
            y: Random.Range(-range, range)
        );
        var newMissile = Instantiate(
            original: missile,
            position: currentPos,
            rotation: (destination - currentPos).toQuaternion()
        ).GetComponent<Missile>();
        newMissile.SetMissile(_missileSprite);
        newMissile.StartPosition = currentPos;
        newMissile.Destination = destination;
        newMissile.Damage = _model.Damage;
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            animator.SetTrigger(ShipDamage);
            healthBar.SetValue(_health / (float)_model.Health);
            if (_health <= 0)
                Explode();
        }
    }

    private void Explode(bool reward = true)
    {
        if (GetComponent<ShipPath>().Dead) return;

        GetComponent<ShipPath>().Dead = true;
        MainCamera.AudioSource.PlayOneShot(_explodeClip);
        animator.SetTrigger(ShipDestroy);
        Instantiate(explosions.RandomItem(), transform);

        // Money Reward
        if (reward)
        {
            var floatingTextBig = Instantiate(this.floatingTextBig, GameObject.FindWithTag("canvas").transform);
            floatingTextBig.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"+ {_model.Money} $";
            floatingTextBig.transform.position = transform.position + Vector3.up * 0.5f;
            GameManager.Instance.Money += _model.Money;
            _moneyCounter.UpdateUI();
        }

        IEnumerator myWaitCoroutine()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        StartCoroutine(myWaitCoroutine());
    }

    public void End() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("base"))
        {
            GameObject.FindWithTag("base").GetComponent<Base>().TakeDamage(_model.Damage);
            Explode(false);
        }
    }
}