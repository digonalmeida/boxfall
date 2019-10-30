using System;
using System.Globalization;
using UnityEngine;

public class GameModeEventData: ISerializationCallbackReceiver
{
    private const string _timeFormat = "dd/MM/yyyy HH:mm:ss";
    [SerializeField]
    private DateTime _startDate;

    [SerializeField] 
    private DateTime _endDate;

    [SerializeField] 
    private string _gameModeName;

    [HideInInspector]
    [SerializeField] 
    private string _startDateStr;
    
    [HideInInspector]
    [SerializeField] 
    private string _endDateStr;


    public void OnBeforeSerialize()
    {
        _startDateStr = dateTimeToStr(_startDate);
        _endDateStr = dateTimeToStr(_endDate);

    }

    public void OnAfterDeserialize()
    {
        _startDate = strToDateTime(_startDateStr);
        _endDate = strToDateTime(_endDateStr);
    }

    private string dateTimeToStr(DateTime dateTime)
    {
        return dateTime.ToString(_timeFormat);
    }

    private DateTime strToDateTime(string format)
    {
        return DateTime.ParseExact(format, _timeFormat, CultureInfo.InvariantCulture);
    }
}