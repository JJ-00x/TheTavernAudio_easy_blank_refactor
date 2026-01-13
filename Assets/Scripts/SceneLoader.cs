using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [Header("Scena")] 
    [SerializeField] private Object scena;

    public void LoadScene()
    {
        if (scena != null)
        { 
            SceneManager.LoadScene(scena.name);  
        }
        else
        {
            Debug.LogError("Scena nieprzypisana!");
        }        
    }

}
