using System;
using System.IO;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GameData
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
        private const string FileName = "GameData.json";

        private void Awake()
        {
#if UNITY_EDITOR
            _dataPath = Path.Combine(Application.dataPath, "Data", FileName);
#elif UNITY_STANDALONE
            _dataPath = Path.Combine(Application.persistentDataPath, FileName);
#endif
            var loadedGameData = Load<GameData<int>>();
            _gameData = loadedGameData != null
                ? new GameData<int>(loadedGameData.Carrot, loadedGameData.Experience)
                : new GameData<int>(0, 0);
        }

        private void Start()
        {
            FireEvents();
        }

        public static void AddCarrot()
        {
            var prevValue = _gameData.Carrot;
            _gameData.Carrot++;
            OnCarrotChange?.Invoke(prevValue, _gameData.Carrot);
        }

        private static void FireEvents()
        {
            OnCarrotChange?.Invoke(0, _gameData.Carrot);
            OnExperienceChange?.Invoke(0, _gameData.Experience);
        }

        public static void AddExperience(int value)
        {
            var prevValue = _gameData.Experience;
            _gameData.Experience += value;
            OnExperienceChange?.Invoke(prevValue, _gameData.Experience);
        }

        private static void Save<T>(GameData<T> gameData)
        {
            var dataJson = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(_dataPath, dataJson);
        }

        private static T Load<T>()
        {
            T gameData = default;
            if (File.Exists(_dataPath))
            {
                var dataJson = File.ReadAllText(_dataPath);
                gameData = JsonConvert.DeserializeObject<T>(dataJson);
            }

            return gameData;
        }

#if UNITY_EDITOR
        [MenuItem("Project Tools/Reset Game Data")]
        private static void ResetGameData()
        {
            _gameData = new GameData<int>(0, 0);
            Save(_gameData);
            FireEvents();

            Debug.LogWarning("Game Data is Reset");
        }
#endif

        private void OnApplicationQuit()
        {
            Save(_gameData);
        }
    }
}