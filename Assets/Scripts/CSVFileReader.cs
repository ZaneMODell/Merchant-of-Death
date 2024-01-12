using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVFileReader : MonoBehaviour
{
    public TextAsset textAssetData;

    public GameObject cubePrefab;

    public List<List<string>> data = new List<List<string>>();

    public int tableSize = 22;

    public bool runCSV = false;

    // Start is called before the first frame update
    void Start()
    {
        if(runCSV)
        {
            ReadCSV();
        }
    }

    void ReadCSV()
    {
        string[] input = textAssetData.text.Split(new string[] {",", "\n"}, StringSplitOptions.None);

        List<string> xRow = new List<string>();
        for (int x = 0; x < input.Length; x++)
        {
            if (x % 22 == 0 && x != 0)
            {
                List<string> xRowCopy = new List<string>();
                for (int y = 0; y < 22; y++)
                {
                    xRowCopy.Add(xRow[y]);
                }
                data.Add(xRowCopy);
                xRow.Clear();
            }
            xRow.Add(input[x]);

        }
        
        for (int i = 0; i < tableSize; i++)
        {
            for (int j = 0; j < tableSize; j++)
            {

                if (data[i][j] == "1" || data[i][j] == "1\r") 
                {
                    GameObject cubeGO = Instantiate(cubePrefab);
                    cubeGO.transform.position = new Vector3(j, 0, -i);
                }
               
            }
            
        }
    }

}
