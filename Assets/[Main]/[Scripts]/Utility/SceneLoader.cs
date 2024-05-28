using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hedi.me.BoxWang
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        public void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
