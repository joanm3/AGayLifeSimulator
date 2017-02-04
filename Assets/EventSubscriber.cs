﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GayProject.Reflection;


[RequireComponent(typeof(Toggle))]
public class EventSubscriber : MonoBehaviour
{

    public EventSchedule Schedule = EventSchedule.Morning;
    public enum EventSchedule { Morning, Afternoon, Evening, Date };

    private Toggle toggle;

    // Use this for initialization
    void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((on) => UpdateToggle(on));
    }

    void UpdateToggle(bool on)
    {
        if (on)
        {
            SubscribeEvent();
        }
        else
        {
            UnsubscribeEvent();
        }
    }

    void SubscribeEvent()
    {
        switch (Schedule)
        {
            case EventSchedule.Morning:
                ActivitiesManager.OnMorning += PlayEvent;
                break;
            case EventSchedule.Afternoon:
                ActivitiesManager.OnAfternoon += PlayEvent;
                break;
            case EventSchedule.Evening:
                ActivitiesManager.OnNight += PlayEvent;
                break;
            case EventSchedule.Date:
                ActivitiesManager.OnDate += PlayEvent;
                break;
        }
    }

    void UnsubscribeEvent()
    {
        switch (Schedule)
        {
            case EventSchedule.Morning:
                ActivitiesManager.OnMorning -= PlayEvent;
                break;
            case EventSchedule.Afternoon:
                ActivitiesManager.OnAfternoon -= PlayEvent;
                break;
            case EventSchedule.Evening:
                ActivitiesManager.OnNight -= PlayEvent;
                break;
            case EventSchedule.Date:
                ActivitiesManager.OnDate -= PlayEvent;
                break;
        }
    }


    void PlayEvent(PlayerManager p)
    {

    }
}

[System.Serializable]
public class GEvent
{
    [Tooltip("leave it empty for no conditions")]
    public GEventCondition[] conditions;
    //conditions
    public string successTextKey;
    public GEventResults[] successResults;
    public string failureTextKey;
    public GEventResults[] failureResults;

    private bool conditionsTrue = true;


    void ComputeEvent(PlayerManager p)
    {
        //check if conditions are true
        if (conditions != null && conditions.Length > 0)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].ConditionIsTrue(p))
                {
                    conditionsTrue = false;
                    break;
                }
            }
        }
        else
        {
            conditionsTrue = true;
        }

        //compute results depending on conditions
        if (conditionsTrue)
        {
            if (successResults != null && successResults.Length > 0)
            {
                for (int i = 0; i < successResults.Length; i++)
                {
                    successResults[i].ComputeResult(p);
                }
            }
        }
        else
        {
            if (failureResults != null && failureResults.Length > 0)
            {
                for (int i = 0; i < failureResults.Length; i++)
                {
                    failureResults[i].ComputeResult(p);
                }
            }
        }
    }
}

[System.Serializable]
public class GEventCondition
{
    public enum ConditionType { Equals, Not_equals, Bigger_than, Smaller_than }
    public string param;
    public ConditionType condition;
    public int value;



    public bool ConditionIsTrue(PlayerManager p)
    {
        var actualValue = p.GetFieldValue(param);

        switch (condition)
        {
            case ConditionType.Equals:
                return (this.value == (int)actualValue);
            case ConditionType.Not_equals:
                return (this.value != (int)actualValue);
            case ConditionType.Bigger_than:
                return ((int)actualValue > this.value);
            case ConditionType.Smaller_than:
                return ((int)actualValue < this.value);
        }
        return false;
    }
}

[System.Serializable]
public class GEventResults
{
    public enum Operation { Sum, Substract, Divide, Multiply };

    public string param;
    public Operation operation;
    public int value;
    public bool biggerThanZero = true;

    private int targetValue;


    public void ComputeResult(PlayerManager p)
    {
        int actualValue = (int)p.GetFieldValue(param);

        switch (operation)
        {
            case Operation.Sum:
                targetValue = actualValue + value;
                break;
            case Operation.Substract:
                targetValue = actualValue + value;
                break;
            case Operation.Divide:
                targetValue = actualValue / value;
                break;
            case Operation.Multiply:
                targetValue = actualValue * value;
                break;
        }

        if (biggerThanZero && targetValue < 0)
            targetValue = 0;

        p.SetFieldValue(param, targetValue);
    }
}