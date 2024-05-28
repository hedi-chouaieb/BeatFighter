using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Hedi.me.BoxWang
{
    public class AudioBrowser : MonoBehaviour
    {
        [SerializeField] private UnityEventAudioClip onUpdateAudioClip;

        private AudioClip loadedSong;

        private void Start()
        {
            SimpleFileBrowser.FileBrowser.SetFilters(true, new SimpleFileBrowser.FileBrowser.Filter("Music", ".mp3", ".wav", ".ogg"));

            SimpleFileBrowser.FileBrowser.SetDefaultFilter(".mp3");
        }
        public void BrowseAudio()
        {
            SimpleFileBrowser.FileBrowser.ShowLoadDialog(OnSuccess, OnCancel, SimpleFileBrowser.FileBrowser.PickMode.Files, false);
        }

        private void OnSuccess(string[] paths)
        {
            Debug.Log(paths[0]);
            StartCoroutine(LoadAudioCoroutine(paths[0]));
        }

        private IEnumerator LoadAudioCoroutine(string path)
        {
            var extension = Path.GetExtension(path);
            var audioType = AudioType.UNKNOWN;
            switch (extension.ToLower())
            {
                case ".mp3":
                    audioType = AudioType.MPEG;
                    break;

                case ".wav":
                    audioType = AudioType.WAV;
                    break;

                case ".ogg":
                    audioType = AudioType.OGGVORBIS;
                    break;
                default:
                    Debug.LogError($"{nameof(extension)} ({extension}) is not supported.");
                    yield break;
            }

            UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, audioType);
            yield return req.SendWebRequest();
            loadedSong = DownloadHandlerAudioClip.GetContent(req);
            while (loadedSong.loadState != AudioDataLoadState.Loaded)
            {
                yield return null;
            }
            onUpdateAudioClip?.Invoke(loadedSong);
        }

        private void OnCancel()
        {

        }

        [System.Serializable] public class UnityEventAudioClip : UnityEvent<AudioClip> { }
    }
}
