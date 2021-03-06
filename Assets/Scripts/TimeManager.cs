﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{

    public class TimeManager : MonoBehaviour
    {
        public float timeLimitRange;
        public float timePace;
        public bool flag = false;
        private bool _startCounting = false;
        private float _lastTime;


        public EventHandler<EventArgs> TimeNextCommandEventHandler;

        public void StartCounting()
        {
            _startCounting = true;
            _lastTime = Time.time;
        }

        public void StopCounting()
        {
            _startCounting = false;
        }

        void Update()
        {
            if (_startCounting)
            {
                //Debug.Log("hello");
                flag = true;
                float presentTime = Time.time;
                float timeDelta = presentTime - _lastTime;

                if (timeDelta < timeLimitRange / 2)
                {
                    flag = false;
                }
                else if (timePace + timeLimitRange / 2 < timeDelta)
                {
                    flag = false;
                }

                if (timeDelta - timePace > -0.06 && timeDelta - timePace < 0.06)
                {
                    _lastTime = Time.time - (timeDelta - timePace);
                    if (TimeNextCommandEventHandler != null)
                    {
                        TimeNextCommandEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }


    }
}
