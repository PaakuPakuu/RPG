using GeneralUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPG
{
    public class DialogCollection
    {
        public enum DialogFile
        {
            OriginSelection
        }

        private const char DIALOG_SEPARATOR = '#';

        private readonly List<string> _fileNames = new List<string>()
        {
            "d_origin_selection"
        };
        private readonly string[] _allLines;
        private int _currentDialogId;
        private readonly List<string[]> _dialogs;

        public DialogCollection(DialogFile file)
        {
            _allLines = ResourcesUtils.GetFileLines($"{ResourcesUtils.DIALOGS_PATH}/{GetPathFromDialogFile(file)}");
            _currentDialogId = 0;
            _dialogs = SplitDialogs(_allLines);
        }

        public bool RunNextDialog()
        {
            if (_currentDialogId == _allLines.Length)
            {
                return false;
            }

            foreach (string dialog in _dialogs[_currentDialogId])
            {
                DisplayTools.WriteInDialogBox(dialog);
            }

            _currentDialogId++;

            return true;
        }

        private List<string[]> SplitDialogs(string[] allLines)
        {
            List<string[]> dialogs = new List<string[]>();
            List<string> currDialog = new List<string>();

            foreach (string line in allLines)
            {
                if (line[0] == DIALOG_SEPARATOR)
                {
                    dialogs.Add(currDialog.ToArray());
                    currDialog.Clear();
                } else
                {
                    currDialog.Add(line);
                }
            }

            return dialogs;
        }

        private string GetPathFromDialogFile(DialogFile file) => $"{_fileNames[(int)file]}.txt";
    }
}
