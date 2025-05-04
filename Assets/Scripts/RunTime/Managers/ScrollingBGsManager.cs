using System;
using RunTime.Controllers;
using RunTime.Data.UnityObjects;
using RunTime.Events;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Managers
{
    public class ScrollingBGsManager : MonoBehaviour
    {
        [SerializeField] private ScrollingBackgroundController[] bgs;

        private CD_Background _data;

        private void Awake()
        {
            GetData();
        }
        
        private void GetData()
        {
            _data = Resources.Load<CD_Background>("Data/CD_Background");
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputEvents.Instance.onStartMove += OnStartMove;
            InputEvents.Instance.onStopMove += OnStopMove;
        }

        private void OnStartMove(float2 value)
        {
            SetAllBGsDir(value.x);
        }
        
        private void OnStopMove()
        {
            SetAllBGsDir(0);
        }

        private void UnSubscribeEvents()
        {
            InputEvents.Instance.onStartMove -= OnStartMove;
            InputEvents.Instance.onStopMove -= OnStopMove;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SetAllBGsSpeed();
        }

        private void SetAllBGsSpeed()
        {
            for (int i = 0; i < bgs.Length; i++)
            {
                bgs[i].SetScrollSpeed(_data.BGSpeedDatas.Speeds[i]);
            }
        }

        private void SetAllBGsDir(float x)
        {
            foreach (var item in bgs)
            {
                item.SetDir(x);
            }
        }
        
    }
}
