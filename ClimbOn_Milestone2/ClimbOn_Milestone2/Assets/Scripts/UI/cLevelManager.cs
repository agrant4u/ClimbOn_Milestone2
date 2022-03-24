using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sLevelManager : MonoBehaviour
{

    public static sLevelManager Instance;


    [SerializeField] private GameObject _LoaderCanvas;
    [SerializeField] private Image _proggressBar;
    private float _target;

    private
    void Start()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }


   
    }


    public async void LoadScene(string sceneName)
    {
        _target = 0;
        _proggressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;


        do
        {
            await Task.Delay(100);
           _target = scene.progress;
        } while (scene.progress < 0.9);


        scene.allowSceneActivation = true;
        _LoaderCanvas.SetActive(false);

    }

   void Update()
    {
        _proggressBar.fillAmount = Mathf.MoveTowards(_proggressBar.fillAmount, _target, 3 * Time.deltaTime);
    }





}
