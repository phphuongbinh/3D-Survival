using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    #region || -------- Example ---------- ||

    // // --- MUSIC VOLUME 
    // public void SaveMusicVolume(float volume)
    // {
    //     PlayerPrefs.SetFloat("MusicVolume", volume);
    //     PlayerPrefs.Save();
    // }

    // public float LoadMusicVolume(float volume)
    // {
    //     return PlayerPrefs.GetFloat("MusicVolume");
    // }

    // // --- EFFECT VOLUME 
    // public void SaveEffectsVolume(float volume)
    // {
    //     PlayerPrefs.SetFloat("EffectsVolume", volume);
    //     PlayerPrefs.Save();
    // }

    // public float LoadEffectsVolume(float volume)
    // {
    //     return PlayerPrefs.GetFloat("EffectsVolume");
    // }

    #endregion

    #region || -------- Settings Section ---------- ||

    #region || -------- Volume Settings  ---------- ||
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
    }

    public void SaveVolumeSettings(float _music, float _effects, float _master)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
            master = _master,
        };
        PlayerPrefs.SetString("Volume", JsonUtility.ToJson(volumeSettings));
        PlayerPrefs.Save();

        print("Save To Player Pref");
    }

    public VolumeSettings LoadVolumeSettings()
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));
    }

    #endregion


    #endregion

}