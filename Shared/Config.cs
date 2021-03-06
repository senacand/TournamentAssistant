﻿using TournamentAssistantShared.Models;
using TournamentAssistantShared.SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;

namespace TournamentAssistantShared
{
    public class Config
    {
        protected string ConfigLocation { get; set; }

        protected JSONNode CurrentConfig { get; set; }

        public Config(string filePath = null)
        {
            filePath = filePath ?? $"{Environment.CurrentDirectory}/UserData/{SharedConstructs.Name}.json";
            ConfigLocation = filePath;

            //Load config from the disk, if we can
            if (File.Exists(ConfigLocation))
            {
                CurrentConfig = JSON.Parse(File.ReadAllText(ConfigLocation));
            }
            else
            {
                CurrentConfig = new JSONObject();
            }
        }

        public void SaveString(string name, string value)
        {
            CurrentConfig[name] = value;
            File.WriteAllText(ConfigLocation, JsonHelper.FormatJson(CurrentConfig.ToString()));
        }

        public string GetString(string name)
        {
            return CurrentConfig[name].Value;
        }

        public void SaveBoolean(string name, bool value)
        {
            CurrentConfig[name] = value.ToString();
            File.WriteAllText(ConfigLocation, JsonHelper.FormatJson(CurrentConfig.ToString()));
        }

        public bool GetBoolean(string name)
        {
            return CurrentConfig[name].AsBool;
        }

        public void SaveObject(string name, JSONNode jsonObject)
        {
            CurrentConfig[name] = jsonObject;
            File.WriteAllText(ConfigLocation, JsonHelper.FormatJson(CurrentConfig.ToString()));
        }

        public JSONNode GetObject(string name)
        {
            return CurrentConfig[name].AsObject;
        }

        //Maybe remove or refactor these, they don't quite fit here,
        //it fits more in a child class of this
        public void SaveTeams(Team[] servers)
        {
            var serverListRoot = new JSONArray();

            foreach (var item in servers)
            {
                var serverItem = new JSONObject();
                serverItem["id"] = item.Id.ToString();
                serverItem["name"] = item.Name;

                serverListRoot.Add(serverItem);
            }

            SaveObject("teams", serverListRoot);
        }

        public Team[] GetTeams()
        {
            var serverList = new List<Team>();
            var serverListRoot = CurrentConfig["teams"].AsArray;

            foreach (var item in serverListRoot.Children)
            {
                serverList.Add(new Team()
                {
                    Id = Guid.Parse(item["id"]),
                    Name = item["name"],
                });
            }

            return serverList.ToArray();
        }

        public void SaveBannedMods(string[] servers)
        {
            var modListRoot = new JSONArray();

            foreach (var item in servers) modListRoot.Add(item);

            SaveObject("bannedMods", modListRoot);
        }

        public string[] GetBannedMods()
        {
            var serverList = new List<string>();
            var serverListRoot = CurrentConfig["bannedMods"].AsArray;

            foreach (var item in serverListRoot.Children) serverList.Add(item);

            return serverList.ToArray();
        }
    }
}
