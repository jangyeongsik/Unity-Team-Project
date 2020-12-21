﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace NPCReader
{
    public class NPCTalk
    {
        public int id;
        public string name;
        public List<string> talk;
    }
}

namespace CSVReaderNPC
{

    public class CSVReaderNPC
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };

        public static List<NPCReader.NPCTalk> FileRead(string file)
        {
            List<NPCReader.NPCTalk> list = new List<NPCReader.NPCTalk>();

            TextAsset data = Resources.Load(file) as TextAsset;
            
            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            if (lines.Length <= 1) return list;

            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;
                var entry = new NPCReader.NPCTalk();
                entry.talk = new List<string>();
                for (int l = 0; l < values.Length; l++)
                {
                    if (values[l] == "") continue;
                    int n;
                    switch (l)
                    {
                        case 0:
                            if (int.TryParse(values[l], out n))
                            {
                                entry.id = n;
                            }
                            break;
                        case 1:
                            entry.name = values[l];
                            break;
                        default:
                            entry.talk.Add(values[l]);
                            break;
                    }
                }
                list.Add(entry);
            }
            return list;
        }

    }
}