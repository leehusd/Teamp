using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;



public class URLGetHtml
{
    public delegate void DelegateFunc(string content);
    public static DelegateFunc OnResult = null; 


    public static async Task GetHtmlString(string  url)
    {
        HttpClient httpClient = new HttpClient();
        
        var stream = await httpClient.GetStreamAsync(url);
        
        StreamReader sr = new StreamReader(stream);

        string content = sr.ReadToEnd();

        if(OnResult != null )
        {
            OnResult(content);
        }

    } 


    public static void SaveFile( string pathName, string contents)
    {
        File.WriteAllText(pathName, contents);
    }

    public static void SaveFile2(string pathName, string contents)
    {
        using (StreamWriter sw = new StreamWriter(pathName, false, Encoding.UTF8 ))
        {
            sw.Write(contents);
        }
    }

}



// 테스트로 html 텍스트 가져오기
public class TestURL
{

    public static void Initialize()
    {
        _= StartAsync();   // _= 은 경고 무시 표시임
    }

    // Start is called before the first frame update
    static async Task StartAsync()
    {
        URLGetHtml.OnResult = new URLGetHtml.DelegateFunc(OnCallback_HtmlResponse);
        await URLGetHtml.GetHtmlString("https://www.naver.com");
    }

    static void OnCallback_HtmlResponse(string content)
    {
        File.WriteAllText("htmltest.txt", content);
    }

}
