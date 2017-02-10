using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsOfTheDay : Singleton<ResultsOfTheDay>
{
    public Text morningResults;
    public Text afternoonResults;
    public Text eveningResults;
    public Text dateResults;
    public Text dateResults2;


    void Start()
    {
        gameObject.SetActive(false);
    }







}
