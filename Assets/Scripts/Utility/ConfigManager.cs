using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System.Diagnostics;

public class ConfigManager : MonoBehaviour // Having this only work in the main menu is an explicit choice. Changes should only be looked for and applied outside of gameplay.
{
    [SerializeField] private SimpleSettingsSO settings;
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private EnemyStatsSO[] enemyStats;

    [SerializeField] private string configFileName = "config.ini";
    private FileSystemWatcher watcher;
    private FileIniDataParser iniFileParser = new FileIniDataParser();
    private string fullPath;
    private bool pendingChanges = false;

    private void Start()
    {
        fullPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, configFileName));
        string directory = Path.GetDirectoryName(fullPath);
        string file = Path.GetFileName(fullPath);

        watcher = new FileSystemWatcher(directory, file);
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        watcher.Changed += OnConfigChanged;
        watcher.Created += OnConfigChanged;
        watcher.Deleted += OnConfigChanged;
        watcher.Renamed += OnConfigChanged;
        watcher.EnableRaisingEvents = true;

        LoadConfig();
    }

    private void OnConfigChanged(object sender, FileSystemEventArgs e)
    {
        pendingChanges = true;
    }

    private void Update()
    {
        if (pendingChanges)
        {
            LoadConfig();
            pendingChanges = false;
        }
    }

    public void OpenConfig()
    {
        EnsureConfigExists();
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = fullPath,
                UseShellExecute = true
            };
            Process.Start(processInfo); // only works on Mono
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError("Failed to open config file: " + ex.Message);
        }
#elif UNITY_STANDALONE_OSX
        Process.Start("open", fullPath);
#elif UNITY_STANDALONE_LINUX
        Process.Start("xdg-open", fullPath);
#else
        UnityEngine.Debug.LogError("Opening files not possible on this platform");
#endif
    }

    private void EnsureConfigExists()
    {
        if (!File.Exists(fullPath))
        {
            GenerateConfig();
        }
    }


    private void LoadConfig()
    {
        if(!File.Exists(fullPath))
        {
            GenerateConfig();
            return;
        }

        IniData data = iniFileParser.ReadFile(fullPath);
        settings.movementUnitConversionRate = float.Parse(data["Settings"]["UnitConversionRate"]);
        settings.rowHeight = float.Parse(data["Settings"]["RowHeight"]);
        settings.downwardMovementSpeedMultiplier = float.Parse(data["Settings"]["DownwardMovementSpeedMultiplier"]);
        settings.downMovementTimeout = float.Parse(data["Settings"]["DownMovementTimeout"]);

        playerStats.movementSpeed = float.Parse(data["PlayerStats"]["MovementSpeed"]);
        playerStats.shotsPerSecond = float.Parse(data["PlayerStats"]["ShotsPerSecond"]);
        playerStats.shotDamage = int.Parse(data["PlayerStats"]["ShotDamage"]);
        playerStats.bulletSpeed = float.Parse(data["PlayerStats"]["BulletSpeed"]);

        for(int i = 0; i < enemyStats.Length; i++)
        {
            var name = enemyStats[i].displayName;
            enemyStats[i].hp = int.Parse(data[name]["HP"]);
            enemyStats[i].minimumSpeed = int.Parse(data[name]["MovementSpeed"]);
            enemyStats[i].scoreValue = int.Parse(data[name]["ScoreValue"]);
        }
    }

    public void GenerateConfig()
    {
        watcher.EnableRaisingEvents = false;
        IniData data = new IniData();
        data.Sections.AddSection("Settings");
        data["Settings"].AddKey("UnitConversionRate", settings.movementUnitConversionRate.ToString("N5"));
        data["Settings"].AddKey("RowHeight", settings.rowHeight.ToString());
        data["Settings"].AddKey("DownwardMovementSpeedMultiplier", settings.downwardMovementSpeedMultiplier.ToString());
        data["Settings"].AddKey("DownMovementTimeout", settings.downMovementTimeout.ToString());

        data.Sections.AddSection("PlayerStats");
        data["PlayerStats"].AddKey("MovementSpeed", playerStats.movementSpeed.ToString());
        data["PlayerStats"].AddKey("ShotsPerSecond", playerStats.shotsPerSecond.ToString());
        data["PlayerStats"].AddKey("ShotDamage", playerStats.shotDamage.ToString());
        data["PlayerStats"].AddKey("BulletSpeed", playerStats.bulletSpeed.ToString());

        for(int i = 0; i < enemyStats.Length; i++)
        {
            var name = enemyStats[i].displayName;
            data.Sections.AddSection(name);
            data[name].AddKey("HP", enemyStats[i].hp.ToString());
            data[name].AddKey("MovementSpeed", enemyStats[i].minimumSpeed.ToString());
            data[name].AddKey("ScoreValue", enemyStats[i].scoreValue.ToString());
        }

        iniFileParser.WriteFile(fullPath, data);
        watcher.EnableRaisingEvents = true;
    }
}
