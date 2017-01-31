using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerInfo Info;
}

[System.Serializable]
public class PlayerInfo
{

    public PlayerNature Nature;
    public PlayerStats Stats;
    public PlayerState State;

    [System.Serializable]
    public struct PlayerNature
    {
        public int PreferedRole;
        public int HIVStatus;
        public int PrettyFace;
        public int DickSize;
        public int AssEndurance;
        public int BodyGrowth;
        public int OralTalent;
    }


    [System.Serializable]
    public struct PlayerStats
    {
        public int BodyFat;
        public int BodyMuscles;
        public int BodyHair;
        public int Sympathy;
        public int Intellligence;
        public int SexStamina;
        public int Sexiness;
    }

    [System.Serializable]
    public struct PlayerState
    {
        public int SexCount;
        public int DaysLeft;
        public int Money;

        public int Salary;
        public int Fatigue;
        public int SelfConfidence;
    }

}
