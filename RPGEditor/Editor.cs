using DbService;
using Microsoft.EntityFrameworkCore;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGEditor
{
    public sealed class Editor
    {
        private static readonly Lazy<Editor> _lazy = new Lazy<Editor>(() => new Editor());

        public static Editor EditorInstance
        {
            get { return _lazy.Value; }
        }

        private bool _isRunning;

        public static Scene ActiveScene { private get; set; }

        private Editor()
        {
            _isRunning = false;
            ActiveScene = new MenusScene();
        }

        public void RunEditor()
        {
            _isRunning = true;
            DisplayTools.InitializeEditorWindow();

            while (_isRunning)
            {
                ActiveScene.ExecuteScene();
                Console.Clear();
            }


        }

        public void CloseEditor()
        {
            _isRunning = false;
        }
    }
}
