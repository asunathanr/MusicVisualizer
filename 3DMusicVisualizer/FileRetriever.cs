using Microsoft.Win32;
using System;

namespace _3DMusicVisualizer
{
    public class FileRetriever
    {
        private Action<string> onSuccess;
        private Action onFail;

        public FileRetriever()
        {
        }

        public Action<string> OnSuccess
        {
            private get => onSuccess;
            set
            {
                onSuccess = value;
            }
        }

        public Action OnFail
        {
            private get => onFail;
            set
            {
                onFail = value;
            }
        }

        public void RetrieveFile()
        {
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Properties.Resources.openFileDialogFilter;
            openFileDialog.FilterIndex = 1;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                filePath = openFileDialog.FileName;
                OnSuccess(filePath);
            }
            else
            {
                OnFail();
            }

        }
    }
}
