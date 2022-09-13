using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class CSVReader {
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";  
    static string LINE_SPLIT_RE = @"\r\n(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; 
    static char[] TRIM_CHARS = { '\"' }; 
    public static List<Dictionary<string, object>> Read(string file) 
    { 
        var list = new List<Dictionary<string, object>>(); 
        TextAsset data = Resources.Load(file) as TextAsset; var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return list; var header = Regex.Split(lines[0], SPLIT_RE); 
        for (var i = 1; i < lines.Length; i++) 
        { 
            var values = Regex.Split(lines[i], SPLIT_RE); 
            if (values.Length == 0 || values[0] == "") continue;
            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++) 
            { 
                string value = values[j]; 
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); 
                object finalvalue = value; 
                int n; 
                float f; 
                if (int.TryParse(value, out n)) { finalvalue = n; } 
                else if (float.TryParse(value, out f)) { finalvalue = f; } 
                entry[header[j]] = finalvalue; 
            } 
            list.Add(entry); 
        } 
        return list; 
    } 
    public static List<List<object>> Parsing(string file) 
    { 
        var list = new List<List<object>>(); 
        TextAsset data = Resources.Load(file) as TextAsset; 
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        //Debug.Log("lines.Length: " + lines.Length);
        if (lines.Length <= 1) return list; 
        var header = Regex.Split(lines[0], SPLIT_RE); 
        for (var i = 1; i < lines.Length; i++) 
        { 
            var values = Regex.Split(lines[i], SPLIT_RE);
            //Debug.Log(i+"th line values.Length: " + values.Length);
            if (values.Length == 0 || values[0] == "") continue; 
            var entry = new List<object>(); 
            for (var j = 0; j < header.Length && j < values.Length; j++) 
            { 
                string value = values[j]; 
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); 
                object finalvalue = value; 
                int n; 
                float f; 
                if (int.TryParse(value, out n)) finalvalue = n; 
                else if (float.TryParse(value, out f)) finalvalue = f;
                entry.Add(finalvalue);
                //Debug.Log(j + "th value: " + finalvalue);
            } 
            list.Add(entry); 
        } 
        return list; 
    } 
}
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//public class CSVReader
//{
//    static string SPLIT_RE = @",";
//   //static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
//    //static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
//    static char[] TRIM_CHARS = { '\"','\n' };

//    public static List<Dictionary<string, object>> Read(string file)
//    {
//        var list = new List<Dictionary<string, object>>();
//        TextAsset data = Resources.Load(file) as TextAsset;

//        int headerEnd = data.text.IndexOf("\n", 0, data.text.Length);
//        var header = Regex.Split(data.text.Substring(0, headerEnd), SPLIT_RE);
//        int headerCount = header.Length;

//        var body = data.text.Substring(headerEnd);
//        var splitedBody = Regex.Split(body, SPLIT_RE);
//        int bodyCount = splitedBody.Length;
//        if (bodyCount <= 1) return list;

//        int index = 0;
//        Debug.Log("bodyCount: " + bodyCount);
//        while(index < bodyCount)
//        {
//            var entry = new Dictionary<string, object>();
//            for (int i = 0; i < headerCount; i++)
//            {
//                string value = splitedBody[index];
//                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS);
//                object finalvalue = value;
//                int n;
//                float f;
//                if (int.TryParse(value, out n))
//                {
//                    finalvalue = n;
//                }
//                else if (float.TryParse(value, out f))
//                {
//                    finalvalue = f;
//                }
//                entry[header[i]] = finalvalue;
//                Debug.Log("INDEX: " + index + ", entry["+ header[i]+"]=" + finalvalue);
//                index++;
//            }
//            list.Add(entry);
//        }
//        return list;
//    }
//}
