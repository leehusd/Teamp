using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCSV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParserCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ParserCSV()
    {

         StreamReader sr = new StreamReader("Assets/Resources/Data/test.csv",System.Text.Encoding.UTF8);

        // 스트림의 끝까지 읽기

        while (!sr.EndOfStream )
        {

            // 한 줄씩 읽어온다.

            string line = sr.ReadLine();

            // 쉼표( , )를 기준으로 데이터를 분리한다.

            string[] data = line.Split('\t');

            // 결과를 출력해본다.
            for (int i = 0; i < data.Length; i++)
            {
                Debug.Log(data[i]);
            }

            //Console.WriteLine("{0}, {1}, {2}, ... ", data[0], data[1], data[2], ... );
        }
    }
}
