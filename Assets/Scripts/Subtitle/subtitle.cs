using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class SubtitleBlock
{
    public int Index { get; private set; }
    public double Length { get; private set; }
    public double From { get; private set; }
    public double To { get; private set; }
    public string Text { get; private set; }

    public SubtitleBlock(int index, double from, double to, string text)
    {
        this.Index = index;
        this.From = from;
        this.To = to;
        this.Length = to - from;
        this.Text = text;
    }
    public override string ToString()
    {
        return "Index: " + Index + " From: " + From + " To: " + To + " Text: " + Text;
    }
    static public List<SubtitleBlock> ParseSubtitles(string content)
    {
        var subtitles = new List<SubtitleBlock>();
        var regex = new Regex(@"(?<index>\d*\s*)\n(?<start>\d*:\d*:\d*,\d*)\s*-->\s*(?<end>\d*:\d*:\d*,\d*)\s*\n(?<content>.*)\n(?<content2>.*)\n");
        var matches = regex.Matches(content);
        foreach (Match match in matches)
        {
            var groups = match.Groups;
            int ind = int.Parse(groups["index"].Value);
            TimeSpan fromtime, totime;
            TimeSpan.TryParse(groups["start"].Value.Replace(',', '.'), out fromtime);
            TimeSpan.TryParse(groups["end"].Value.Replace(',', '.'), out totime);
            string contenttext = groups["content"].Value;
            subtitles.Add(new SubtitleBlock(ind, fromtime.TotalSeconds, totime.TotalSeconds, contenttext));
        }
        return subtitles;
    }
}

public class subtitle : MonoBehaviour
{
    public float offset;
    List<SubtitleBlock> subt;
    public Text subtitletext;
    public VideoPlayer vp;
    string subcontent;
    public TextAsset subTitleSource;
    //IEnumerator GetRequest(string url)
    //{
    //    using (UnityWebRequest req = UnityWebRequest.Get(url))
    //    {
    //        yield return req.SendWebRequest();
    //        if (req.isNetworkError || req.isHttpError)
    //        {
    //            Debug.Log($"{req.error}: {req.downloadHandler.text}");
    //        }
    //        else
    //        {
    //            subcontent = req.downloadHandler.text;
    //        }
    //    }
    //    subt = SubtitleBlock.ParseSubtitles(subcontent);
    //    StartCoroutine(DisplaySubtitles());
    //}
    void Start()
    {
        if (subTitleSource == null) return;

        subt = SubtitleBlock.ParseSubtitles(subTitleSource.text);
        StartCoroutine(DisplaySubtitles());
        StartCoroutine(playvideo());
    }

    public float localtimer;
    void Update()
    {
        if (vp != null && vp.isPlaying)
            localtimer = (float)vp.time + offset;
    }
    IEnumerator playvideo()
    {
        if (vp != null)
            vp.Play();
        yield return null;
    }
    public IEnumerator DisplaySubtitles()
    {
        for (int j = 0; j < subt.Count; j++)
        {
            var i = subt[j];
            subtitletext.text = "";
            if (i.From <= localtimer && i.To >= localtimer)
            {
                subtitletext.text = i.Text;
                yield return new WaitForSeconds((float)i.Length);

            }
            else if (i.From > localtimer)
            {
                yield return new WaitForSeconds(Mathf.Min((float)i.From - localtimer, 0.1f));
                j--;
            }
        }
        subtitletext.text = "";
        yield return null;
    }

}
