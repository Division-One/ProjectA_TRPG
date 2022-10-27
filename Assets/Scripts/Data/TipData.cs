using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipData
{
    public List<string> tipData = new List<string>();

    public void GenerateData()
    {
        List<List<object>> data = CSVReader.Parsing("Data/TipText");
        int count = data.Count;
        for (int i = 0; i < count; i++)
        {
            tipData.Add(data[i][1].ToString());
        }
    }
}
