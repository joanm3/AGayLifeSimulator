using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GayProject.Reflection;


[RequireComponent(typeof(Toggle))]
public class EventSubscriber : MonoBehaviour
{
    public EventSchedule Schedule = EventSchedule.Morning;
    public GEvent Activity;
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


    void PlayEvent()
    {
        Activity.ComputeEvent();
    }
}

[System.Serializable]
public class GEvent
{
    [Tooltip("leave it empty for no conditions")]
    public GEventCondition[] conditions;
    //conditions
    public GEventResults[] successResults;
    public string successTextKey;
    public GEventResults[] failureResults;
    public string failureTextKey;

    private bool conditionsTrue = true;


    public void ComputeEvent()
    {
        //check if conditions are true
        if (conditions != null && conditions.Length > 0)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].ConditionIsTrue())
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
                    successResults[i].ComputeResult();
                }
            }
            Debug.Log("success");
        }
        else
        {
            if (failureResults != null && failureResults.Length > 0)
            {
                for (int i = 0; i < failureResults.Length; i++)
                {
                    failureResults[i].ComputeResult();
                }
            }
            Debug.Log("failure");
        }
    }
}

[System.Serializable]
public class GEventCondition
{
    public enum ConditionType { Equals, Not_equals, Bigger_than, Smaller_than, Random }
    public string param;
    public ConditionType condition;
    public int value;
    public bool useParamAsValue = false;
    public string paramValue;


    public bool ConditionIsTrue()
    {
        int actualValue = 0;
        if (condition != ConditionType.Random)
        {
            actualValue = (int)PlayerManager.Instance.GetFieldValue(param);
        }
        if (useParamAsValue)
        {
            value = (int)PlayerManager.Instance.GetFieldValue(paramValue);
        }


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
            case ConditionType.Random:
                Debug.Log("value: " + value + "> random:" + (int)(Random.value * 100f) + "/ condition  is: " + (value > (Random.value * 100)));
                return (value > (int)(Random.value * 100f));

        }
        return false;
    }
}

[System.Serializable]
public class GEventResults
{
    public enum Operation { Sum, Substract, Divide, Multiply, EqualToValue };

    public string param;
    public Operation operation;
    public int value;
    public bool useParamAsValue = false;
    public string paramValue;
    public bool biggerThanZero = true;
    public bool useRandomRange = false;
    public int minRange = 0;
    public int maxRange = 0;
    private int targetValue;


    public void ComputeResult()
    {
        var actualValue = (int)PlayerManager.Instance.GetFieldValue(param);
        if (useParamAsValue)
        {
            value = (int)PlayerManager.Instance.GetFieldValue(paramValue);
        }


        switch (operation)
        {
            case Operation.Sum:
                targetValue = (useRandomRange) ? actualValue + Random.Range(minRange, maxRange) : actualValue + value;
                break;
            case Operation.Substract:
                targetValue = (useRandomRange) ? actualValue - Random.Range(minRange, maxRange) : actualValue - value;
                break;
            case Operation.Divide:
                targetValue = (useRandomRange) ? actualValue / Random.Range(minRange, maxRange) : actualValue / value;
                break;
            case Operation.Multiply:
                targetValue = (useRandomRange) ? actualValue * Random.Range(minRange, maxRange) : actualValue * value;
                break;
            case Operation.EqualToValue:
                targetValue = value;
                break;
        }

        if (biggerThanZero && targetValue < 0)
            targetValue = 0;

        PlayerManager.Instance.SetFieldValue(param, targetValue);
    }
}