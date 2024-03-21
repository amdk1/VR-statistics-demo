using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RunTrial : MonoBehaviour
{
    public float chance;
    public int trials = 5;
    public GameObject leftPos;
    public GameObject rightPos;
    public float[] outcomes;
    public int repetitions = 40;
    public float successfulDrops = 0;
    public float unsuccessfulDrops = 0;
    int initialReps;
    int initialTrials;
    Vector3 offset = new Vector3(0.001f, 0.001f, 0.001f);
    // Start is called before the first frame update

    void Start() 
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Obtained_Values/");
        initialReps = repetitions;
        initialTrials = trials;
        outcomes = new float[initialReps];

        for (int i = 0; i < (repetitions); i++)
        {
            while (trials > 0)
            {
                float randomNum = Random.Range(0f, 1f);
                if (randomNum <= chance)
                {
                    successfulDrops++;
                    GameObject item = Instantiate(leftPos) as GameObject;
                    item.transform.parent = transform;
                    item.transform.position = leftPos.transform.position + offset;
                }
                else
                {
                    unsuccessfulDrops++;
                    GameObject item = Instantiate(rightPos) as GameObject;
                    item.transform.parent = transform;
                    item.transform.position = rightPos.transform.position + offset;
                }
                trials--;               
            }
            outcomes[initialReps - repetitions] = successfulDrops;
            trials = initialTrials;
            successfulDrops = 0;
            repetitions--;            
        }
        CreateFile();
    }

    public void CreateFile()
    {
        string docName = Application.streamingAssetsPath + "/Obtained_Values/" + "Obtained" + ".txt";

        if (!File.Exists(docName))
        {
            File.WriteAllText(docName, "Values: \n\n");
        }
        
        for(int i = 0; i < initialReps; i++)
        {
            File.AppendAllText(docName, outcomes[i].ToString() + ",");
        }
    }
}
