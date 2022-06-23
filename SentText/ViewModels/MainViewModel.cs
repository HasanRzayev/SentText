using BespokeFusion;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SentText.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private string from_text;

        public string From_text
        {
            get { return from_text; }
            set { from_text = value; OnPropertyChanged(); }
        }

        private string to_text;

        public string To_text
        {
            get { return to_text; }
            set { to_text = value; OnPropertyChanged(); }
        }



        private int value1;

        public int Value
        {
            get { return value1; }
            set { value1 = value; OnPropertyChanged(); }
        }
        public RelayCommand Fromfile_button
        {
            get => new RelayCommand(() =>
            {


                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true)
                    From_text = (openFileDialog.FileName).ToString();

            });
        }

        public RelayCommand Tofile_button
        {
            get => new RelayCommand(() =>
            {


                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true)
                    To_text= (openFileDialog.FileName).ToString();

            });
        }

        public void copy()
        {
            string srcPath = From_text;
            string destPath = To_text;




            if (!File.Exists(srcPath))
            {
                Console.WriteLine("File not exists");
                return;
            }
            Value=0;
            using (FileStream fsRead = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
            {


                using (FileStream fsWrite = new FileStream(destPath, FileMode.Open, FileAccess.Write))
                {
                    var len = 10;
                    var fileSize = fsRead.Length;
                    byte[] buffer = new byte[len];

                    do
                    {
                        len = fsRead.Read(buffer, 0, buffer.Length);
                        if (fsRead.Length!=0 &&len!=0)
                        {
                            int lazimli = (int)((int)fsRead.Length)/((100*len));
                            Value=Value+lazimli;
                        }
                       
                        fsWrite.Write(buffer, 0, len);
                        Thread.Sleep (100);
                        
                        fileSize -= len;
                      

                    } while (len != 0);
                    MaterialMessageBox.ShowError(@"Transfer tamamlandi!!!!!!!!!!!!");

                }
            }
        }
        public RelayCommand Copy
        {
            get => new RelayCommand(() =>
            {
                thread.Start();


            });
        }
        public Thread thread { get; set; }
        public MainViewModel()
        {
            thread = new Thread(copy);

        }
    }
}
