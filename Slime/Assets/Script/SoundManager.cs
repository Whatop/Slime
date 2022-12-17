using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSound;
    public AudioMixer mixer;
    public AudioClip[] bglist;
    public AudioClip[] sfxlist;
    public AudioClip[] txtlist;

    private static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // �̷��� �ϸ� ������ ������..
    // ��������� �渶�� �ϴ� �ȵ�!
    // �׷��� ������������ ���� �Ŵ����� �������� �����ϴ�..����..
    // �Ƹ� �� ��ȣ�� ���� ��������߰���! �� ��..
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BGSoundPlay(bglist[i]);
        }
    }

    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("BGSound", Mathf.Log10(val) * 20);
    }
    public void SFXSoundVolume(float val)
    {
        mixer.SetFloat("SFXSound", Mathf.Log10(val) * 20);
    }

    // �����
    // SoundManager.instance.SFXPlay("",clip);
    // �̰Թ���
    public void SFXPlay(string sfxName, int sfxNum)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.clip = sfxlist[sfxNum];
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.volume = 0.1f;
        audioSource.Play();
        Destroy(go, sfxlist[sfxNum].length);
    }

    public void BGSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BG")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }

}
