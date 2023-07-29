﻿using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Managers
{
    public class DataManager
    {
        private static readonly int Ship4 = Animator.StringToHash("ship_4");
        private static DataManager _instance;
        public static void Reset() => _instance = new DataManager();
        private DataManager()
        {
        }

        public static DataManager Instance => _instance ??= new DataManager();


        public readonly TurretModel[] Turrets =
        {
            new(
                name: "Base Turret", sprite: "Sprites/turret_1", fireClip: "Audio/turret_fire_1",
                bulletSprite: "Sprites/bullet_1",
                baseSpeed: 380 , baseRate: 300 , baseDamage: 20, baseAmmo: 32 , baseReload: 75 ,
                speedBaseCost: 100, rateBaseCost: 180, damageBaseCost: 150, ammoBaseCost: 125, reloadBaseCost: 115,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 25 },
                    { 10, 35 },
                    { 15, 50 },
                    { 20, 70 },
                    { 25, 85 },
                    { 30, 100 },
                    { 35, 125 },
                },
                rateLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 30 },
                    { 10, 40 },
                    { 15, 50 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 80 },
                    { 35, 100 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 1 },
                    { 5, 2 },
                    { 10, 3 },
                    { 15, 3 },
                    { 20, 4 },
                    { 25, 4 },
                    { 30, 6 },
                    { 35, 6 },
                },
                ammoLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 5, 2 },
                    { 10, 2 },
                    { 15, 4 },
                    { 20, 4 },
                    { 25, 4 },
                    { 30, 6 },
                    { 35, 6 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 25 },
                    { 5, 30 },
                    { 10, 35 },
                    { 15, 40 },
                    { 20, 50 },
                    { 25, 65 },
                    { 30, 70 },
                    { 35, 80 },
                }
            ),
            new(
                name: "Gatling Gun", sprite: "Sprites/turret_2", fireClip: "Audio/turret_fire_2",
                bulletSprite: "Sprites/bullet_2",
                baseSpeed: 600, baseRate: 1200, baseDamage: 16, baseAmmo: 80, baseReload: 115,
                speedBaseCost: 200, rateBaseCost: 260, damageBaseCost: 360, ammoBaseCost: 185, reloadBaseCost: 225,
                waveUnlock: 5,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 25 },
                    { 10, 35 },
                    { 15, 50 },
                    { 20, 70 },
                    { 25, 85 },
                    { 30, 100 },
                    { 35, 125 },
                },
                rateLevelSteps: new Dictionary<int, int>
                {
                    { 0, 60 },
                    { 5, 70 },
                    { 10, 80 },
                    { 15, 100 },
                    { 20, 120 },
                    { 25, 150 },
                    { 30, 180 },
                    { 35, 220 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 1 },
                    { 3, 2 },
                    { 10, 3 },
                    { 15, 3 },
                    { 20, 4 },
                    { 25, 4 },
                    { 30, 6 },
                    { 35, 6 },
                },
                ammoLevelSteps: new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 5, 6 },
                    { 10, 6 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 8 },
                    { 30, 8 },
                    { 35, 8 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 25 },
                    { 5, 30 },
                    { 10, 35 },
                    { 15, 40 },
                    { 20, 50 },
                    { 25, 65 },
                    { 30, 70 },
                    { 35, 80 },
                }
            ),
            new(
                name: "Missile Launcher", sprite: "Sprites/turret_3", fireClip: "Audio/cannon_fire_3",
                bulletSprite: "Sprites/missile_2",
                baseSpeed: 220, baseRate: 40, baseDamage: 600, baseAmmo: 2, baseReload: 45,
                speedBaseCost: 420, rateBaseCost: 430, damageBaseCost: 550, ammoBaseCost: 780, reloadBaseCost: 475,
                waveUnlock: 10,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 25 },
                    { 10, 35 },
                    { 15, 50 },
                    { 20, 70 },
                    { 25, 85 },
                    { 30, 100 },
                    { 35, 125 },
                },
                rateLevelSteps: new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 5, 6 },
                    { 10, 8 },
                    { 15, 10 },
                    { 20, 12 },
                    { 25, 14 },
                    { 30, 16 },
                    { 35, 18 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 10 },
                    { 5, 12 },
                    { 10, 14 },
                    { 15, 16 },
                    { 20, 18 },
                    { 25, 20 },
                    { 30, 24 },
                    { 35, 28 },
                },
                ammoLevelSteps: new Dictionary<int, int>
                {
                    { 0, 1 },
                    { 5, 1 },
                    { 10, 1 },
                    { 15, 2 },
                    { 20, 2 },
                    { 25, 4 },
                    { 30, 4 },
                    { 35, 4 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 8 },
                    { 5, 12 },
                    { 10, 16 },
                    { 15, 20 },
                    { 20, 30 },
                    { 25, 35 },
                    { 30, 40 },
                    { 35, 50 },
                }
            ),
            new(
                name: "Laser Gun", sprite: "Sprites/turret_4", fireClip: "Audio/laser",
                bulletSprite: "",
                baseSpeed: 1250, baseRate: 2000, baseDamage: 22, baseAmmo: 140, baseReload: 46,
                speedBaseCost: 420, rateBaseCost: 430, damageBaseCost: 550, ammoBaseCost: 780, reloadBaseCost: 475,
                waveUnlock: 15,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 25 },
                    { 10, 35 },
                    { 15, 50 },
                    { 20, 70 },
                    { 25, 85 },
                    { 30, 100 },
                    { 35, 125 },
                },
                rateLevelSteps: new Dictionary<int, int>
                {
                    { 0, 100 },
                    { 5, 200 },
                    { 10, 300 },
                    { 15, 400 },
                    { 20, 400 },
                    { 25, 400 },
                    { 30, 400 },
                    { 35, 500 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 10 },
                    { 5, 12 },
                    { 10, 14 },
                    { 15, 16 },
                    { 20, 18 },
                    { 25, 20 },
                    { 30, 24 },
                    { 35, 28 },
                },
                ammoLevelSteps: new Dictionary<int, int>
                {
                    { 0, 8 },
                    { 5, 10 },
                    { 10, 12 },
                    { 15, 14 },
                    { 20, 16 },
                    { 25, 18 },
                    { 30, 20 },
                    { 35, 22 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 8 },
                    { 5, 12 },
                    { 10, 16 },
                    { 15, 20 },
                    { 20, 30 },
                    { 25, 35 },
                    { 30, 40 },
                    { 35, 50 },
                }
            ),
            new(
                name: "Auto-Locking Gun", sprite: "Sprites/turret_5", fireClip: "Audio/turret_fire_1",
                bulletSprite: "Sprites/bullet_2",
                baseSpeed: 395, baseRate: 580, baseDamage: 74, baseAmmo: 38, baseReload: 37,
                speedBaseCost: 420, rateBaseCost: 430, damageBaseCost: 550, ammoBaseCost: 780, reloadBaseCost: 475,
                waveUnlock: 20,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 25 },
                    { 10, 35 },
                    { 15, 50 },
                    { 20, 70 },
                    { 25, 85 },
                    { 30, 100 },
                    { 35, 125 },
                },
                rateLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 30 },
                    { 10, 40 },
                    { 15, 50 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 70 },
                    { 35, 80 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 10 },
                    { 5, 12 },
                    { 10, 14 },
                    { 15, 16 },
                    { 20, 18 },
                    { 25, 20 },
                    { 30, 24 },
                    { 35, 28 },
                },
                ammoLevelSteps: new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 5, 4 },
                    { 10, 4 },
                    { 15, 4 },
                    { 20, 6 },
                    { 25, 6 },
                    { 30, 6 },
                    { 35, 6 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 5, 3 },
                    { 10, 4 },
                    { 15, 5 },
                    { 20, 6 },
                    { 25, 7 },
                    { 30, 8 },
                    { 35, 9 },
                }
            ),
        };

        public readonly CannonModel[] Cannons =
        {
            new(
                name: "Base Cannon", sprite: "Sprites/cannon_1", fireClip: "Audio/cannon_fire_2"
                , cannonBallSprite: "Sprites/cannon_ball_1",
                baseSpeed: 40, baseDamage: 200, baseReload: 28, baseRadius: 14,
                speedBaseCost: 450, damageBaseCost: 300, reloadBaseCost: 350, radiusBaseCost: 250,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 15 },
                    { 5, 20 },
                    { 10, 25 },
                    { 15, 30 },
                    { 20, 40 },
                    { 25, 55 },
                    { 30, 60 },
                    { 35, 75 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 10 },
                    { 5, 16 },
                    { 10, 24 },
                    { 15, 32 },
                    { 20, 40 },
                    { 25, 50 },
                    { 30, 65 },
                    { 35, 75 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 5, 3 },
                    { 10, 4 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 10 },
                    { 30, 12 },
                    { 35, 15 },
                },
                radiusLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 15, 3 },
                    { 30, 4 },
                }
            ),
            new(
                name: "Artillery", sprite: "Sprites/cannon_2", fireClip: "Audio/cannon_fire_1",
                cannonBallSprite: "Sprites/cannon_ball_2",
                baseSpeed: 45, baseDamage: 500, baseReload: 24, baseRadius: 16,
                speedBaseCost: 550, damageBaseCost: 540, reloadBaseCost: 615, radiusBaseCost: 520,
                waveUnlock: 5,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 15 },
                    { 5, 20 },
                    { 10, 25 },
                    { 15, 30 },
                    { 20, 40 },
                    { 25, 55 },
                    { 30, 60 },
                    { 35, 75 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 20 },
                    { 5, 26 },
                    { 10, 34 },
                    { 15, 42 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 75 },
                    { 35, 75 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 5, 3 },
                    { 10, 4 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 10 },
                    { 30, 12 },
                    { 35, 15 },
                },
                radiusLevelSteps: new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 15, 3 },
                    { 30, 4 },
                }
            ),
            new(
                name: "EMP Cannon", sprite: "Sprites/cannon_3", fireClip: "Audio/cannon_fire_3",
                baseSpeed: 50, baseDamage: 760/34, baseReload: 18, baseRadius: 18,
                cannonBallSprite: "Sprites/cannon_ball_3",
                speedBaseCost: 572, damageBaseCost: 620, reloadBaseCost: 842, radiusBaseCost: 715,
                waveUnlock: 10,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 15 },
                    { 5, 20 },
                    { 10, 25 },
                    { 15, 30 },
                    { 20, 40 },
                    { 25, 55 },
                    { 30, 60 },
                    { 35, 75 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 40 },
                    { 5, 46 },
                    { 10, 54 },
                    { 15, 52 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 75 },
                    { 35, 75 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 3, 4 },
                    { 10, 6 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 10 },
                    { 30, 12 },
                    { 35, 15 },
                },
                radiusLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 15, 4 },
                    { 30, 5 },
                }
            ),
            new(
                name: "Blizzard Cannon", sprite: "Sprites/cannon_4", fireClip: "Audio/cannon_fire_1",
                baseSpeed: 60, baseDamage: 1175, baseReload: 18, baseRadius: 20,
                cannonBallSprite: "Sprites/cannon_ball_4_2",
                speedBaseCost: 572, damageBaseCost: 620, reloadBaseCost: 842, radiusBaseCost: 715,
                waveUnlock: 15,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 15 },
                    { 5, 20 },
                    { 10, 25 },
                    { 15, 30 },
                    { 20, 40 },
                    { 25, 55 },
                    { 30, 60 },
                    { 35, 75 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 40 },
                    { 5, 46 },
                    { 10, 54 },
                    { 15, 52 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 75 },
                    { 35, 75 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 3, 4 },
                    { 10, 6 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 10 },
                    { 30, 12 },
                    { 35, 15 },
                },
                radiusLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 15, 4 },
                    { 30, 5 },
                }
            ),
            new(
                name: "Rocket Launcher", sprite: "Sprites/cannon_5", fireClip: "Audio/missile_launch",
                baseSpeed: 65, baseDamage: 1000, baseReload: 20, baseRadius: 34,
                cannonBallSprite: "Sprites/cannon_ball_5_2",
                speedBaseCost: 572, damageBaseCost: 620, reloadBaseCost: 842, radiusBaseCost: 715,
                waveUnlock: 20,
                speedLevelSteps: new Dictionary<int, int>
                {
                    { 0, 15 },
                    { 5, 20 },
                    { 10, 25 },
                    { 15, 30 },
                    { 20, 40 },
                    { 25, 55 },
                    { 30, 60 },
                    { 35, 75 },
                },
                damageLevelSteps: new Dictionary<int, int>
                {
                    { 0, 40 },
                    { 5, 46 },
                    { 10, 54 },
                    { 15, 52 },
                    { 20, 60 },
                    { 25, 70 },
                    { 30, 75 },
                    { 35, 75 },
                },
                reloadLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 3, 4 },
                    { 10, 6 },
                    { 15, 6 },
                    { 20, 8 },
                    { 25, 10 },
                    { 30, 12 },
                    { 35, 15 },
                },
                radiusLevelSteps: new Dictionary<int, int>
                {
                    { 0, 3 },
                    { 15, 4 },
                    { 30, 5 },
                }
            ),
        };

        public readonly ShipModel[] Ships =
        {
            new(
                name: "SpeedBoat", sprite: "Sprites/ship_1", fireClip: null, explodeClip: "Audio/ship_destroy",
                baseSpeed: 100 , baseRate: 0, baseDamage: 350, baseHealth: 90, baseMoney: 103,
                hasPath: false, missileSprite: null
            ),
            new(
                name: "Vessel", sprite: "Sprites/ship_2", missileSprite: "Sprites/missile_2",
                fireClip: new[] { "Audio/cannon_fire_4", "Audio/missile_coming" }, explodeClip: "Audio/ship_destroy",
                baseSpeed: 92, baseRate: 10, baseDamage: 275, baseHealth: 220, baseMoney: 258,
                hasPath: true, explosionsCount: 2
            ),
            new(
                name: "Military", sprite: "Sprites/ship_3", missileSprite: "Sprites/missile_3",
                fireClip: new[] { "Audio/cannon_fire_4", "Audio/missile_coming" }, explodeClip: "Audio/ship_destroy",
                baseSpeed: 70, baseRate: 16, baseDamage: 385, baseHealth: 460, baseMoney: 681,
                hasPath: true, explosionsCount: 5
            ),
            new(
                name: "Submarine", sprite: "Sprites/ship_4", missileSprite: "Sprites/missile_1",
                fireClip: new[] { "Audio/cannon_fire_4", "Audio/missile_coming" }, explodeClip: "Audio/ship_destroy",
                baseSpeed: 65, baseRate: 10, baseDamage: 525, baseHealth: 782, baseMoney: 1460,
                hasPath: true, explosionsCount: 4,
                startCallback: go =>
                {
                    var renderer = go.GetComponent<Renderer>();
                    renderer.material.color = new Color(1, 1, 1, 0.5f);
                    go.GetComponent<Ship>().Invincible = true;
                    MainCamera.AudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/submarine_down"));
                },
                endPathCallback: go =>
                {
                    go.GetComponent<Ship>().Invincible = false;
                    var overlay = go.transform.Find("overlay");
                    overlay.gameObject.SetActive(true);
                    overlay.GetComponent<Animator>().SetTrigger(Ship4);
                    overlay.GetComponent<SpriteRenderer>().sprite = go.GetComponent<SpriteRenderer>().sprite;
                    MainCamera.AudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/submarine_up"));
                }
            ),
            new(
                name: "Aircraft Carrier", sprite: "Sprites/ship_5", missileSprite: "Sprites/missile_1",
                fireClip: new[] { "Audio/cannon_fire_4", "Audio/missile_coming" }, explodeClip: "Audio/ship_destroy",
                baseSpeed: 55, baseRate: 5, baseDamage: 625, baseHealth: 1820, baseMoney: 2760,delayMultiplier:1.5f,
                hasPath: true, explosionsCount: 9
                ),
        };

        public readonly WaveModel[] Waves =
        {
            new(shipsChances: new List<float> { 0.75f, 1f, 1f, 1f, 1f },shipsCount:7),
            new(shipsChances: new List<float> { 0.65f, 0.9f, 1f, 1f , 1f},shipsCount:9),
            new(shipsChances: new List<float> { 0.5f, 0.8f, 1f, 1f , 1f},shipsCount:12),
            new(shipsChances: new List<float> { 0.4f, 0.65f, 0.9f, 1f, 1f },shipsCount:13),
            new(shipsChances: new List<float> { 0.35f, 0.5f, 0.8f, 1f, 1f },shipsCount:18),
            
            new(shipsChances: new List<float> { 0.55f, 0.7f, 0.8f, 0.9f, 1f },shipsCount:12),
            new(shipsChances: new List<float> { 0.45f, 0.6f, 0.8f, 0.9f, 1f },shipsCount:15),
            new(shipsChances: new List<float> { 0.35f, 0.5f, 0.7f, 0.9f, 1f },shipsCount:17),
            new(shipsChances: new List<float> { 0.25f, 0.4f, 0.6f, 0.85f, 1f },shipsCount:14),
            new(shipsChances: new List<float> { 0.15f, 0.3f, 0.4f, 0.8f, 1f },shipsCount:22),

            new(shipsChances: new List<float> { 0.15f, 0.3f, 0.4f, 0.8f, 1f },shipsCount:14),
            new(shipsChances: new List<float> { 1f, 1f, 1f, 1f, 1f },shipsCount:11,spawnSpeedMultiply:1.8f),
            new(shipsChances: new List<float> { 0.35f, 0.4f, 0.75f, 0.9f, 1f },shipsCount:16),
            new(shipsChances: new List<float> { 0.1f, 0.2f, 0.3f, 0.75f, 1f },shipsCount:18),
            new(shipsChances: new List<float> { 0.15f, 0.25f, 0.35f, 0.85f, 1f },shipsCount:22),

            new(shipsChances: new List<float> { 0.15f, 0.3f, 0.4f, 0.8f, 1f },shipsCount:17),
            new(shipsChances: new List<float> { 0.8f, 0.8f, 0.8f, 0.8f, 1f },shipsCount:16,spawnSpeedMultiply:2.5f),
            new(shipsChances: new List<float> { 0.35f, 0.4f, 0.75f, 0.9f, 1f },shipsCount:22),
            new(shipsChances: new List<float> { 0.1f, 0.2f, 0.3f, 0.75f, 1f },shipsCount:17),
            new(shipsChances: new List<float> { 0.15f, 0.25f, 0.35f, 0.85f, 1f },shipsCount:24),

            new(shipsChances: new List<float> { 0.15f, 0.2f, 0.65f, 0.8f, 1f },shipsCount:11),
            new(shipsChances: new List<float> { 0.15f, 0.2f, 0.65f, 0.8f, 1f },shipsCount:17),
            new(shipsChances: new List<float> { 0.35f, 0.4f, 0.75f, 0.9f, 1f },shipsCount:22),
            new(shipsChances: new List<float> { 0.1f, 0.2f, 0.3f, 0.75f, 1f },shipsCount:26),
            new(shipsChances: new List<float> { 0.15f, 0.25f, 0.35f, 0.85f, 1f },shipsCount:34),
        };
    }
}