﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    [Serializable]
    public class GameManager
    {
        private static readonly string IOName = "GameManager.json";

        private static DataManager Data => DataManager.Instance;

        public static void Reset()
        {
            DeleteSave();
            _instance = new GameManager();
        }

        public static void DeleteSave() => IOManager.Delete(IOName);


        public void Save() => IOManager.Save(Instance, IOName);

        public static bool Load()
        {
            var instance = IOManager.Load<GameManager>(IOName);
            if (instance is not null)
                _instance = instance;
            return instance is not null;
        }

        public static bool HasSave() => IOManager.Exist(IOName);


        private static GameManager _instance;

        private GameManager()
        {
            CannonAmmo = 1;
            Health = MaxHealth;
        }

        public static GameManager Instance => _instance ??= new GameManager();

        public int Quality = InputManager.IsMobile ? 0 : 2;
        public readonly string[] QualityNames = { "Low", "High", "Ultra" };
        public int Difficulty = InputManager.IsMobile ? 0 : 1;
        public readonly string[] DifficultyNames = { "Easy", "Medium", "Hard" };

        public string[] QualityOceanMaterials =
            { "Materials/ocean_low", "Materials/ocean_high", "Materials/ocean_ultra" };

        public int Health;
        public float Armor = 0.5f;
        public int MaxHealth = 3500;
        private int _healthBaseStep = 175;
        private int _healthBaseCost = 400;
        public int HealthLevel;
        private int _repairLevel;
        public int Wave = 0;
        [NonSerialized] public int SpecialWave = -1;
        public int Ammo;
        [NonSerialized] public int CannonAmmo = 1;
        public int Money = 200;
        public int CurrentTurret = 0;
        public int CurrentCannon = 0;
        public int Score;
        public float SpecialShipChance = 0.05f;
        [NonSerialized] public bool HasOverride;

        // === CRITICAL HIT =========================================================
        private int _criticalFactorBaseCost = 325,
            _turretCriticalChanceBaseCost = 300,
            _cannonCriticalChanceBaseCost = 200;

        public float CriticalFactor = 2f, TurretCriticalChance = 0.05f, CannonCriticalChance = 0.05f;
        public int CriticalFactorLevel, TurretCriticalChanceLevel, CannonCriticalChanceLevel;
        public int CriticalMaxLevel = 9;
        public bool IsTurretCritical => new System.Random().Next(0, 1000) < TurretCriticalChance * 1000;

        public bool IsCannonCritical => new System.Random().Next(0, 1000) < CannonCriticalChance * 1000;
        // ==========================================================================


        // === BUBBLE/POWER-UP ======================================================
        [NonSerialized] private PowerUpModel? _powerUp;
        public bool HasBubbleSpawn => new System.Random().Next(0, 100) < 0.1f;

        [NonSerialized] public float PowerUpStart;

        // public float PowerUpDuration => new[] { 30, 25, 20 }[Difficulty];
        // public int MissileAssaultCount => new[] { 30, 25, 20 }[Difficulty];
        public float PowerUpProgress => _powerUp is null ? 999f : (Time.time - PowerUpStart) / _powerUp.Duration;

        public PowerUpModel? PowerUp
        {
            get
            {
                if (PowerUpProgress >= 1)
                    _powerUp = null;
                return _powerUp;
            }
            set
            {
                if (value is null)
                {
                    PowerUpStart = 0f;
                    return;
                }

                _powerUp = value;
                PowerUpStart = Time.time;
            }
        }
        // ==========================================================================

        public int HealthStep => (int)(_healthBaseStep * (1f + 0.25f * HealthLevel));

        public int HealthCost => (int)(_healthBaseCost * (1f + 0.5f * HealthLevel) *
                                       (Difficulty == 0 ? 0.9f : 1) *
                                       (Difficulty == 2 ? 1.25f : 1));

        public int CriticalFactorCost => (int)(_criticalFactorBaseCost * (1f + 0.5f * CriticalFactorLevel) *
                                               (Difficulty == 0 ? 0.9f : 1) *
                                               (Difficulty == 2 ? 1.25f : 1)) *
                                         (CriticalFactorLevel >= CriticalMaxLevel ? 0 : 1);

        public float CriticalFactorStep => 0.25f;

        public int TurretCriticalChanceCost =>
            (int)(_turretCriticalChanceBaseCost * (1f + 1f * TurretCriticalChanceLevel) *
                  (Difficulty == 0 ? 0.9f : 1) *
                  (Difficulty == 2 ? 1.25f : 1)) *
            (TurretCriticalChanceLevel >= CriticalMaxLevel ? 0 : 1);

        public float TurretCriticalChanceStep => 0.015f;

        public int CannonCriticalChanceCost =>
            (int)(_cannonCriticalChanceBaseCost * (1f + 1f * CannonCriticalChanceLevel) *
                  (Difficulty == 0 ? 0.9f : 1) *
                  (Difficulty == 2 ? 1.25f : 1)) *
            (CannonCriticalChanceLevel >= CriticalMaxLevel ? 0 : 1);

        public float CannonCriticalChanceStep => 0.015f;


        public int RepairCost => (int)(0.28f * (MaxHealth - Health) * (1f + 0.25f * _repairLevel));

        public int CurrentWaveTurretFired = 0,
            CurrentWaveTurretHit = 0,
            CurrentWaveCannonFired = 0,
            CurrentWaveCannonHit = 0;

        public float CurrentWaveTurretAccuracy => CurrentWaveTurretHit / (float)CurrentWaveTurretFired;
        public float CurrentWaveCannonAccuracy => CurrentWaveCannonHit / (float)CurrentWaveCannonFired;
        public bool HasTurretAccuracyBonus => CurrentWaveTurretAccuracy >= (InputManager.IsMobile ? 0.67f : 0.75f);
        public bool HasCannonAccuracyBonus => CurrentWaveCannonAccuracy >= 0.75;
        public bool HasAccuracyBonus => HasTurretAccuracyBonus && HasCannonAccuracyBonus;
        public bool IsSpecialWave => SpecialWave >= 0;
        public bool[] SpecialOccurInWave = new bool[999];
        public bool HasPowerUp => PowerUp is { };


        public TurretModel CurrentTurretModel => Data.Turrets[CurrentTurret];
        public CannonModel CurrentCannonModel => Data.Cannons[CurrentCannon];
        public WaveModel CurrentWave => Data.Waves[Wave];
        public float WaveFactor => Wave / (float)Data.Waves.Length;
        public List<int> SpecialsCount = new() { 1, 1, 1, 1 };
        private List<int> _specialsBought = new() { 0, 0, 0, 0 };

        private List<int> SpecialsBaseCosts => Difficulty switch
        {
            0 => new List<int> { 650, 1000, 1250, 1500 },
            1 => new List<int> { 800, 1250, 1500, 1750 },
            _ => new List<int> { 1000, 1500, 1750, 2000 }
        };

        public List<string> SpecialsName = new() { "Air Assault", "Shield", "EMP", "Health" };
        public int SpecialDamage => (int)(750 * (1f + 0.1f * Wave));

        public int SpecialCost(int index) =>
            (int)(SpecialsBaseCosts[index] * Mathf.Pow(1.06f, _specialsBought[index]));


        public void BuySpecial(int index)
        {
            if (Money >= SpecialCost(index))
            {
                Money -= SpecialCost(index);
                ++_specialsBought[index];
                ++SpecialsCount[index];
            }
        }

        public void BuyHealth()
        {
            if (Money >= HealthCost)
            {
                Money -= HealthCost;
                MaxHealth += HealthStep;
                Health += HealthStep;
                HealthLevel++;
            }
        }

        public void BuyRepair()
        {
            if (Money >= RepairCost && Health < MaxHealth)
            {
                Money -= RepairCost;
                Health = MaxHealth;
                _repairLevel++;
            }
        }

        private void BuyPerk(ref float what, ref int level, int cost, float step)
        {
            if (Money < cost || level >= CriticalMaxLevel) return;
            Money -= cost;
            what += step;
            ++level;
        }

        public void BuyCriticalFactor() =>
            BuyPerk(ref CriticalFactor, ref CriticalFactorLevel, CriticalFactorCost, CriticalFactorStep);

        public void BuyTurretCriticalChance() =>
            BuyPerk(ref TurretCriticalChance, ref TurretCriticalChanceLevel, TurretCriticalChanceCost,
                TurretCriticalChanceStep);

        public void BuyCannonCriticalChance() =>
            BuyPerk(ref CannonCriticalChance, ref CannonCriticalChanceLevel, CannonCriticalChanceCost,
                CannonCriticalChanceStep);
    }
}