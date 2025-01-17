﻿using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerExtensions.Environments
{
    public class LobbyAvatarPlaceLighting : MonoBehaviour
    {
        protected List<TubeBloomPrePassLight> lights = new List<TubeBloomPrePassLight>();

        protected float smoothTime = 2f;

        protected Color targetColor = Color.black;

        protected virtual void OnEnable()
        {
            foreach (TubeBloomPrePassLight light in this.GetComponentsInChildren<TubeBloomPrePassLight>())
            {
                lights.Add(light);
            }
        }

        protected virtual void OnDisable()
        {
            lights = new List<TubeBloomPrePassLight>();
        }

        protected virtual void Update()
        {
            Color current = GetColor();
            if (!IsColorVeryCloseToColor(current, targetColor))
            {
                SetColor(Color.Lerp(current, targetColor, Time.deltaTime * smoothTime));
            }
        }

        public virtual void SetColor(Color color, bool immediate)
        {
            targetColor = color;
            if (immediate)
            {
                SetColor(color);
            }
        }

        public virtual Color GetColor()
        {
            return lights[0].color;
        }

        public virtual bool IsColorVeryCloseToColor(Color color0, Color color1)
        {
            return Mathf.Abs(color0.r - color1.r) < 0.002f && Mathf.Abs(color0.g - color1.g) < 0.002f && Mathf.Abs(color0.b - color1.b) < 0.002f && Mathf.Abs(color0.a - color1.a) < 0.002f;
        }

        protected virtual void SetColor(Color color)
        {
            foreach(TubeBloomPrePassLight light in lights)
            {
                light.color = color;
                light.Refresh();
            }
        }
    }
}
