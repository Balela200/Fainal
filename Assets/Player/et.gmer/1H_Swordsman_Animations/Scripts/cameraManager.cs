using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace animationHelper {
        
    public class cameraManager : MonoBehaviour
    {

        public GameObject AnimationsParent;

        private List<GameObject> listGameObjectsAnimations = new List<GameObject>();
        public List<GameObject> CameraOrder;

        internal void getAllGameObjects()
        {
            listGameObjectsAnimations.Clear();
            Animator [] listAnimators =  AnimationsParent.GetComponentsInChildren<Animator>();

            foreach (Animator a in listAnimators)
            {
                listGameObjectsAnimations.Add(a.gameObject);
            }
        }
        
        public void getlistCameras()
        {
            var res = AnimationsParent.GetComponentsInChildren<CinemachineVirtualCamera>();
            foreach (var cam in res)
            {
                CameraOrder.Add(cam.gameObject);
            }
        }



    void Start()
        {
            foreach (GameObject cam in CameraOrder)
            {
                cam.SetActive(false);
            }
        }

        // Update is called once per frame

        private int index = 0;
        private double duration;
        
        void Update()
        {
            if(index < CameraOrder.Count)
            {
                CameraOrder[index].SetActive(true);
                if(duration >CameraOrder[index].GetComponent<cameraSpecificities>().duration)
                {
                    //CameraOrder[index].SetActive(false);
                    index++;
                    duration = 0;
                }

                duration+= Time.deltaTime;
                
            }
            else
            {
                Application.Quit();
            }
            
            
        }
        
    }
}