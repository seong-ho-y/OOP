using UnityEngine;
using UnityEngine.SceneManagement;

// SceneManager를 사용하기 위해 필수

namespace Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Combat");
        }

        public void LoadWeapon()
        {
            SceneManager.LoadScene("Weapon");
        }
    }
}
