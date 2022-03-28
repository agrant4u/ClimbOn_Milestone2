using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sLevelManager : MonoBehaviour
{

    public static sLevelManager Instance;


    [SerializeField] private Slider _proggressBar;
    private float _target;

    [SerializeField]  private uLoadBar loadingScreen;

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


    public void LoadScene(eScene sceneName)
    {
     
       // scene.allowSceneActivation = false;
       
        StartCoroutine(UpdateProggres(sceneName));
       


     


        /*do
        {
            await Task.Delay(100);
           _target = scene.progress;
        } while (scene.progress < 0.9);
        */

       // scene.allowSceneActivation = true;
       // loadingScreen.gameObject.SetActive(false);

    }

   IEnumerator UpdateProggres(eScene sceneName)
    {
        _proggressBar = Instantiate(loadingScreen, FindObjectOfType<Canvas>().transform).loadBar;

        yield return new WaitForSeconds(2f);

        var scene = SceneManager.LoadSceneAsync((int)sceneName);
        _target = 0;
        _proggressBar.value = 0;

        while (!scene.isDone)
        {
            _target = scene.progress;

            _proggressBar.value = Mathf.MoveTowards(_proggressBar.value, _target, 3 * Time.deltaTime);
            Debug.Log(_target);

            yield return null;

        }

       
    }





}
