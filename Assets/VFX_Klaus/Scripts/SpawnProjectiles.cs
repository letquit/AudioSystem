using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine.UI;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject[] Effects;
    public RotateToMouse rotateToMouse;

    /// <summary>
    /// 音效数据对象，用于存储和管理音频相关的配置信息
    /// </summary>
    [SerializeField] 
    private SoundData soundData;
    
    int selectedPrefab = 0;

    private GameObject effectToSpawn;
    private float timeToFire = 0;
    private Text prefabName;

    void Start()
    {
        effectToSpawn = Effects[0];
        prefabName = GameObject.Find("PrefabName").GetComponent<Text>();
    }

    void Update()
    {
        // Shoot Effect
        if(Input.GetMouseButton (0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnEffects();
        }

        // Change Effect
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedPrefab++;
            if (selectedPrefab >= Effects.Length)
            {
                selectedPrefab = 0;
            }
            effectToSpawn = Effects[selectedPrefab];
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedPrefab--;
            if (selectedPrefab < 0)
            {
                selectedPrefab = Effects.Length - 1;
            }
            effectToSpawn = Effects[selectedPrefab];
        }

        // Prefab Name On Screen
        if (Effects.Length > 0 && selectedPrefab >= 0 && selectedPrefab < Effects.Length)
        {
            prefabName.text = Effects[selectedPrefab].name;
        }
    }

    void SpawnEffects()
    {
        GameObject Effects;
        
        if (firePoint != null)
        {
            Effects = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null)
            { 
                Effects.transform.localRotation = rotateToMouse.GetRotation();
            }

            // Effects.transform.parent = firePoint.transform;
            
            // 创建并播放一个带有随机音调的效果声音
            SoundManager.Instance.CreateSound()
                .WithSoundData(soundData)
                .WithRandomPitch()
                .WithPosition(Effects.transform.position)
                .Play();
        } 
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
 