using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GayProject.Reflection;


[RequireComponent(typeof(Toggle))]
public class EventSubscriber : MonoBehaviour
{
    public ActivitiesManager.EventSchedule Schedule = ActivitiesManager.EventSchedule.Morning;
    public GEvent Activity;


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
            case ActivitiesManager.EventSchedule.Morning:
                ActivitiesManager.OnMorning += PlayEvent;
                break;
            case ActivitiesManager.EventSchedule.Afternoon:
                ActivitiesManager.OnAfternoon += PlayEvent;
                break;
            case ActivitiesManager.EventSchedule.Evening:
                ActivitiesManager.OnNight += PlayEvent;
                break;
        }
    }

    void UnsubscribeEvent()
    {
        switch (Schedule)
        {
            case ActivitiesManager.EventSchedule.Morning:
                ActivitiesManager.OnMorning -= PlayEvent;
                break;
            case ActivitiesManager.EventSchedule.Afternoon:
                ActivitiesManager.OnAfternoon -= PlayEvent;
                break;
            case ActivitiesManager.EventSchedule.Evening:
                ActivitiesManager.OnNight -= PlayEvent;
                break;
        }
    }


    void PlayEvent()
    {
        Activity.ComputeEvent();
        Activity.ComputeResultsText(Schedule);
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
        conditionsTrue = true;
        if (conditions != null && conditions.Length > 0)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].ConditionIsTrue())
                {
                    conditionsTrue = false;
                }
            }
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

    public void ComputeResultsText(ActivitiesManager.EventSchedule schedule)
    {
        if (ResultsOfTheDay.Instance == null)
        {
            Debug.Log("Results of the day instance is null, not computing key text");
            return;
        }

        //check if we see the last iteration, otherwise call later (but then check that the text is being correctly loaded)
        if (schedule != ActivitiesManager.EventSchedule.Date)
        {
            ResultsOfTheDay.Instance.gameObject.SetActive(true);
        }
        else
        {
            BlinderManager.Instance.resultsCanvas.gameObject.SetActive(true);
        }

        if (conditionsTrue)
        {
            switch (schedule)
            {
                case ActivitiesManager.EventSchedule.Morning:
                    ResultsOfTheDay.Instance.morningResults.text = LocalizationManager.Instance.GetText(successTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Afternoon:
                    ResultsOfTheDay.Instance.afternoonResults.text = LocalizationManager.Instance.GetText(successTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Evening:
                    ResultsOfTheDay.Instance.eveningResults.text = LocalizationManager.Instance.GetText(successTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Date:
                    BlinderManager.Instance.resultsText.text = LocalizationManager.Instance.GetText(successTextKey);
                    ResultsOfTheDay.Instance.dateResults.text = LocalizationManager.Instance.GetText(successTextKey);
                    ResultsOfTheDay.Instance.dateResults2.text = LocalizationManager.Instance.GetText(successTextKey);

                    break;
            }
        }
        else
        {
            switch (schedule)
            {
                case ActivitiesManager.EventSchedule.Morning:
                    ResultsOfTheDay.Instance.morningResults.text = LocalizationManager.Instance.GetText(failureTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Afternoon:
                    ResultsOfTheDay.Instance.afternoonResults.text = LocalizationManager.Instance.GetText(failureTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Evening:
                    ResultsOfTheDay.Instance.eveningResults.text = LocalizationManager.Instance.GetText(failureTextKey);
                    break;
                case ActivitiesManager.EventSchedule.Date:
                    BlinderManager.Instance.resultsText.text = LocalizationManager.Instance.GetText(failureTextKey);
                    ResultsOfTheDay.Instance.dateResults.text = LocalizationManager.Instance.GetText(failureTextKey);
                    ResultsOfTheDay.Instance.dateResults2.text = LocalizationManager.Instance.GetText(failureTextKey);

                    break;
            }
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

        bool _condition = true;

        switch (condition)
        {
            case ConditionType.Equals:
                _condition = (this.value == (int)actualValue);
                break;
            case ConditionType.Not_equals:
                _condition = (this.value != (int)actualValue);
                break;
            case ConditionType.Bigger_than:
                _condition = ((int)actualValue > this.value);
                break;
            case ConditionType.Smaller_than:
                _condition = ((int)actualValue < this.value);
                break;
            case ConditionType.Random:
                int random = (int)(Random.value * 100f);
                Debug.Log("value: " + value + "> random:" + random + "/ condition  is: " + (value > random));
                _condition = (value > random);
                break;
        }
        return _condition;

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