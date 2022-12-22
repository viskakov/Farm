using System;
using System.IO;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GameData
{
    public class GameData<T>
    {
        public T Carrot { get; set; }
        public T Experience { get; set; }

        public GameData(T carrot, T experience)
        {
            Carrot = carrot;
            Experience = experience;
        }
    }

    public class GameDataManager : MonoBehaviour
    {
        public static Action<int, int> OnCarrotChange;
        public static Action<int, int> OnExperienceChange;

        private static GameData<int> _gameData;
        private static string _dataPath;
        private const string FileName = "GameData.json";

        private void Awake()
        {
            _dataPath = GetGameDataPath();
            var loadedGameData = Load<GameData<int>>(_dataPath);
            _gameData = loadedGameData != null
                ? new GameData<int>(loadedGameData.Carrot, loadedGameData.Experience)
                : new GameData<int>(0, 0);
        }

        private void Start()
        {
            FireEvents();
        }

        private static void FireEvents()
        {
            OnCarrotChange?.Invoke(0, _gameData.Carrot);
            OnExperienceChange?.Invoke(0, _gameData.Experience);
        }

        public static void AddCarrot()
        {
            var prevValue = _gameData.Carrot;
            _gameData.Carrot++;
            OnCarrotChange?.Invoke(prevValue, _gameData.Carrot);
        }

        public static void AddExperience(int value)
        {
            var prevValue = _gameData.Experience;
            _gameData.Experience += value;
            OnExperienceChange?.Invoke(prevValue, _gameData.Experience);
        }

        private static void Save<T>(GameData<T> gameData, string dataPath)
        {
            var dataJson = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(dataPath, dataJson);
        }

        private static T Load<T>(string dataPath)
        {
            T gameData = default;
            if (File.Exists(dataPath))
            {
                var dataJson = File.ReadAllText(dataPath);
                gameData = JsonConvert.DeserializeObject<T>(dataJson);
            }

            return gameData;
        }

        private string GetGameDataPath()
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, "Data", FileName);
#elif UNITY_STANDALONE
            return Path.Combine(Application.persistentDataPath, FileName);
#endif
        }

#if UNITY_EDITOR
        [MenuItem("Project Tools/Reset Game Data")]
        private static void ResetGameData()
        {
            _gameData = new GameData<int>(0, 0);
            Save(_gameData, _dataPath);
            FireEvents();

            Debug.LogWarning("Game Data is Reset");
        }
#endif

        private void OnApplicationQuit()
        {
            Save(_gameData, _dataPath);
        }
    }
}