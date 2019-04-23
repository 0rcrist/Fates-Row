using System;
using UnityEngine;

public class Parser : MonoBehaviour
{
    private static string KILLS_SYMBOL = "[KILLS]";
    private static string LVL1_SYMBOL = "[LVL1]";
    private static string LVL2_SYMBOL = "[LVL2]";
    private static string LVL3_SYMBOL = "[LVL3]";


    public static int DataToKills(string data)
    {
        return int.Parse(DataToValue(data, KILLS_SYMBOL));
    }
    public static int DataToLVL1(string data)
    {
        return int.Parse(DataToValue(data, LVL1_SYMBOL));
    }
    public static int DataToLVL2(string data)
    {
        return int.Parse(DataToValue(data, LVL2_SYMBOL));
    }
    public static int DataToLVL3(string data)
    {
        return int.Parse(DataToValue(data, LVL3_SYMBOL));
    }


    private static string DataToValue(string data, string symbol)
    {
        string[] tokens = data.Split('/');

        foreach (string token in tokens)
        {
            if (token.StartsWith(KILLS_SYMBOL))
            {
                return token.Substring(symbol.Length);
            }
        }
       // Debug.Log(symbol + " not found in " + data);
        return "";
    }
}
