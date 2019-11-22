﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nothke.ProtoGUI
{
    public class ToolbarGUI : WindowGUI
    {
        public override string WindowLabel { get { return "Toolbar"; } }


        public bool collectOnStart = true;
        public List<GameWindow> windows;

        public int buttonWidth = 120;
        public int toolbarOffset = 400;

        void Start()
        {
            draggable = false;
            windowRect.y = -30;
            windowRect.x = buttonWidth;
            windowRect.width = 0;

            if (collectOnStart)
            {
                windows = new List<GameWindow>(FindObjectsOfType<GameWindow>());

                windows.Remove(this);
            }
        }

        protected override void Window()
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < windows.Count; i++)
            {
                if (GUILayout.Button(windows[i].WindowLabel, GUILayout.Width(buttonWidth))) {
                    for (int j = 0; j < windows.Count; j++) {
                        if (j == i)
                            windows[i].Enabled = !windows[i].Enabled;
                        else
                            windows[j].Enabled = false;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}