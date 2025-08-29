using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class LevelTest : MonoBehaviour
{
    [SerializeField] private TextAsset _file;
    public List<FullNoteData> NotesList {get; private set;}
    public List<ColorNoteData> ColorNotesDataList {get; private set;}
    public List<ColorNote> ColorNotesList {get; private set;}
    private void Start()
    {
        var json = _file.text;
        var jsonObject = JObject.Parse(json);
        ColorNotesDataList = jsonObject["colorNotesData"]?.ToObject<List<ColorNoteData>>();
        if (ColorNotesDataList == null)
        {
            NotesList = jsonObject["colorNotes"].ToObject<List<FullNoteData>>();
            return;
        }
        ColorNotesList = jsonObject["colorNotes"].ToObject<List<ColorNote>>();
        NotesList = new List<FullNoteData>();
        foreach (var colorNote in ColorNotesList)
        {
            var fullNoteData = new FullNoteData{b = colorNote.b, x = ColorNotesDataList[colorNote.i].x, 
                y = ColorNotesDataList[colorNote.i].y, a = ColorNotesDataList[colorNote.i].a, 
                c = ColorNotesDataList[colorNote.i].c, d = ColorNotesDataList[colorNote.i].d};
            NotesList.Add(fullNoteData);
        }
        print(NotesList.Count);
    }

    [Serializable]
    public class ColorNoteData
    {
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public float a { get; set; } = 0;
        public int c { get; set; } = 0;
        public int d { get; set; } = 0;
    }

    [Serializable]
    public class ColorNote
    {
        public float b { get; set; } = 0;
        public int i { get; set; } = 0;
    }

    [Serializable]
    public class FullNoteData
    {
        public float b { get; set; } = 0;
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public float a { get; set; } = 0;
        public int c { get; set; } = 0;
        public int d { get; set; } = 0;
    }
}
