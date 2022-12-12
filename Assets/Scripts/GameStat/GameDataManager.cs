using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GameStat
{
    [Serializable]
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

        private void Awake()
        {
            _dataPath = Application.dataPath + "/Data/GameData.json";

            var loadedGameData = LoadProgress<GameData<int>>();
            _gameData = loadedGameData != null
                ? new GameData<int>(loadedGameData.Carrot, loadedGameData.Experience)
                : new GameData<int>(0, 0);
        }

        private void Start()
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

        private static void SaveProgress<T>(GameData<T> gameData)
        {
            var dataJson = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(_dataPath, dataJson);
        }

        private static T LoadProgress<T>()
        {
            T gameData = default;
            if (File.Exists(_dataPath))
            {
                var dataJson = File.ReadAllText(_dataPath);
                gameData = JsonConvert.DeserializeObject<T>(dataJson);
            }

            return gameData;
        }

        private void OnApplicationQuit()
        {
            SaveProgress(_gameData);
        }
    }
}