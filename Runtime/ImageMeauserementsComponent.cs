using FSS.Settings.RoomConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMeauserementsComponent : MonoBehaviour
{
    [SerializeField]
    private double width;
    [SerializeField] 
    private double height;
    [SerializeField]
    private RoomConfigManager roomConfigManager;

    private void Awake()
    {
        roomConfigManager = FindObjectOfType<RoomConfigManager>();
    }

    private void Start()
    {
        UpdateImageSize();
    }

    public void UpdateImageSize()
    {
        width = roomConfigManager.ScreenWidth_m;
        height = roomConfigManager.ScreenHeight_m;

        this.transform.localScale = new Vector3((float)width, (float)height, 1f);
        transform.position = new Vector3 (transform.position.x, 0.53f*transform.localScale.y, transform.position.z);
    }
}
