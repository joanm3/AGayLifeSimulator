﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GayProject.DataManagement;
using UnityEngine.UI;
using System;
using System.Reflection;

public class PlayerManager : Singleton<PlayerManager>
{

    [Space(10)]
    public PlayerInfo Info;
    [Space(10)]
    public PlayerInfo StartingDefaultPlayerInfo;
    public bool IsReady
    {
        set
        {
            isReady = value;
            if (isReady == true)
            {
                OnIsReady();
            }
        }
        get
        {
            return isReady;
        }

    }

    private bool isReady = false;

    void OnEnable()
    {
        LoadFeatures();
        IsReady = true;
    }

    void OnDisable()
    {
        SaveFeatures();
    }

    public void OnIsReady()
    {

    }

    public void LoadFeatures()
    {
        Debug.Log("LoadFeatures called");
        FData.LoadFeature("NATURE_INITIALIZED", ref Info.NatureInitialized);
        FData.LoadFeature("PROFILE_NAME", ref Info.Profile.Name);
        FData.LoadFeature("PROFILE_AGE", ref Info.Profile.Age);
        FData.LoadFeature("PROFILE_ROLE", ref Info.Profile.Role);
        FData.LoadFeature("PROFILE_HIVSTATUS", ref Info.Profile.HIVStatus);

        FData.LoadFeature("NATURE_FACE", ref Info.Nature.PrettyFace);
        FData.LoadFeature("NATURE_DICK", ref Info.Nature.DickSize);
        FData.LoadFeature("NATURE_ASS", ref Info.Nature.AssEndurance);
        FData.LoadFeature("NATURE_ORAL", ref Info.Nature.OralTalent);

        FData.LoadFeature("STATS_BFAT", ref Info.Stats.BodyFat);
        FData.LoadFeature("STATS_BMUSCLES", ref Info.Stats.BodyMuscles);
        FData.LoadFeature("STATS_BHAIR", ref Info.Stats.BodyHair);
        FData.LoadFeature("STATS_SYMPATHY", ref Info.Stats.Sympathy);
        FData.LoadFeature("STATS_INTELLIGENCE", ref Info.Stats.Intelligence);
        FData.LoadFeature("STATS_SSTAMINA", ref Info.Stats.SexStamina);
        FData.LoadFeature("STATS_SEXINESS", ref Info.Stats.Sexiness);

        FData.LoadFeature("STATE_MONEY", ref Info.State.Money);
        FData.LoadFeature("STATE_DAYSLEFT", ref Info.State.DaysLeft);
        FData.LoadFeature("STATE_SEXCOUNT", ref Info.State.SexCount);

        FData.LoadFeature("STATE_SALARY", ref Info.State.Salary);
        FData.LoadFeature("STATE_FATIGUE", ref Info.State.Fatigue);
        FData.LoadFeature("STATE_MAXFATIGUE", ref Info.State.MaxFatigue);

        FData.LoadFeature("STATE_SELFCONFIDENCE", ref Info.State.SelfConfidence);

    }

    public void SaveFeatures()
    {
        Debug.Log("SaveFeatures called");

        FData.SaveFeature("NATURE_INITIALIZED", ref Info.NatureInitialized);
        FData.SaveFeature("PROFILE_NAME", ref Info.Profile.Name);
        FData.SaveFeature("PROFILE_AGE", ref Info.Profile.Age);
        FData.SaveFeature("PROFILE_ROLE", ref Info.Profile.Role);
        FData.SaveFeature("PROFILE_HIVSTATUS", ref Info.Profile.HIVStatus);

        FData.SaveFeature("NATURE_FACE", ref Info.Nature.PrettyFace);
        FData.SaveFeature("NATURE_DICK", ref Info.Nature.DickSize);
        FData.SaveFeature("NATURE_ASS", ref Info.Nature.AssEndurance);
        FData.SaveFeature("NATURE_ORAL", ref Info.Nature.OralTalent);

        FData.SaveFeature("STATS_BFAT", ref Info.Stats.BodyFat);
        FData.SaveFeature("STATS_BMUSCLES", ref Info.Stats.BodyMuscles);
        FData.SaveFeature("STATS_BHAIR", ref Info.Stats.BodyHair);
        FData.SaveFeature("STATS_SYMPATHY", ref Info.Stats.Sympathy);
        FData.SaveFeature("STATS_INTELLIGENCE", ref Info.Stats.Intelligence);
        FData.SaveFeature("STATS_SSTAMINA", ref Info.Stats.SexStamina);
        FData.SaveFeature("STATS_SEXINESS", ref Info.Stats.Sexiness);

        FData.SaveFeature("STATE_MONEY", ref Info.State.Money);
        FData.SaveFeature("STATE_DAYSLEFT", ref Info.State.DaysLeft);
        FData.SaveFeature("STATE_SEXCOUNT", ref Info.State.SexCount);

        FData.SaveFeature("STATE_SALARY", ref Info.State.Salary);
        FData.SaveFeature("STATE_FATIGUE", ref Info.State.Fatigue);
        FData.SaveFeature("STATE_MAXFATIGUE", ref Info.State.MaxFatigue);

        FData.SaveFeature("STATE_SELFCONFIDENCE", ref Info.State.SelfConfidence);
    }

    public void ReinitializeGame()
    {
        Debug.Log("game reinitialized");

        Instance.Info = Instance.StartingDefaultPlayerInfo.Clone();
        if (UIManager.Instance != null) { UIManager.Instance.UpdateAllUI(); }
    }


}

[System.Serializable]
public class PlayerInfo
{
    [Tooltip("Zero = No / One = Yes")]
    public int NatureInitialized = 0;
    public PlayerProfile Profile;
    public PlayerNature Nature;
    public PlayerStats Stats;
    public PlayerState State;

    [System.Serializable]
    public class PlayerNature
    {
        [Range(0, 4)]
        public int PrettyFace;
        [Range(0, 4)]
        public int DickSize;
        [Range(0, 4)]
        public int AssEndurance;
        [Range(0, 4)]
        public int OralTalent;
    }


    [System.Serializable]
    public class PlayerStats
    {
        public int BodyFat;
        public int BodyMuscles;
        [Range(0, 5)]
        public int BodyHair;
        public int Sympathy;
        public int Intelligence;
        public int SexStamina;
        public int Sexiness;
    }

    [System.Serializable]
    public class PlayerState
    {
        public int SexCount;
        public int DaysLeft;
        public int Money;

        public int Salary;
        public int Fatigue;
        public int MaxFatigue;
        public int SelfConfidence;
        public int WorkBonus;
    }

    [System.Serializable]
    public class PlayerProfile
    {
        public string Name;
        public string Age;
        /// <summary>
        /// Zero = Bottom / One = Top / Two = Vers
        /// </summary>
        [Tooltip("Zero = Bottom / One = Top / Two = Vers")]
        [Range(0, 2)]
        public int Role;
        /// <summary>
        /// Zero = HIV- / One = HIV+
        /// </summary>
        [Range(0, 1)]
        [Tooltip("Zero = HIV- / One = HIV+")]
        public int HIVStatus;

    }

    public PlayerInfo()
    {

    }

    protected PlayerInfo(PlayerInfo other)
    {
        this.Profile = new PlayerProfile();
        this.Profile.Name = other.Profile.Name;
        this.Profile.Age = other.Profile.Age;
        this.Profile.Role = other.Profile.Role;
        this.Profile.HIVStatus = other.Profile.HIVStatus;

        this.Nature = new PlayerNature();
        this.Nature.PrettyFace = other.Nature.PrettyFace;
        this.Nature.DickSize = other.Nature.DickSize;
        this.Nature.AssEndurance = other.Nature.AssEndurance;
        this.Nature.OralTalent = other.Nature.OralTalent;

        this.Stats = new PlayerStats();
        this.Stats.BodyFat = other.Stats.BodyFat;
        this.Stats.BodyMuscles = other.Stats.BodyMuscles;
        this.Stats.BodyHair = other.Stats.BodyHair;
        this.Stats.Sympathy = other.Stats.Sympathy;
        this.Stats.Intelligence = other.Stats.Intelligence;
        this.Stats.SexStamina = other.Stats.SexStamina;
        this.Stats.Sexiness = other.Stats.Sexiness;

        this.State = new PlayerState();
        this.State.SexCount = other.State.SexCount;
        this.State.DaysLeft = other.State.DaysLeft;
        this.State.Money = other.State.Money;
        this.State.Salary = other.State.Salary;
        this.State.Fatigue = other.State.Fatigue;
        this.State.MaxFatigue = other.State.MaxFatigue;
        this.State.SelfConfidence = other.State.SelfConfidence;

    }


    public static PlayerInfo CopyFrom(PlayerInfo other)
    {
        return new PlayerInfo(other);
    }

    public PlayerInfo Clone()
    {
        return new PlayerInfo(this);
    }


}
