using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Filters;
using Microsoft.Win32;

namespace Client
{
    public partial class MainWindow : Window, IContractCallback
    {
        ContractClientBase client = null;
        volatile bool isCanceled = false;

        Bitmap imageUnfiltred = null;
        Bitmap imageFiltred = null;
        Bitmap imageSaved = null;
        byte[] imageToSaving = null;
        string path = null;

        public MainWindow()
        {
            InitializeComponent();

            client = new ContractClientBase(new System.ServiceModel.InstanceContext(this));

            try
            {
                List<string> filters = client.GetAvailableFilters();

                foreach (var filter in filters)
                {
                    comboBoxFilters.Items.Add(filter);
                }

                comboBoxFilters.SelectedItem = comboBoxFilters.Items[0];

                buttonSelect.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageSelection = new OpenFileDialog();

            imageSelection.Title = "Select the image";
            imageSelection.Filter = "Все файлы (*.*) | *.*";

            if ((bool)imageSelection.ShowDialog())
            {
                path = imageSelection.FileName;

                try
                {
                    imageUnfiltred = new Bitmap(path);

                    BitmapSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(imageUnfiltred.GetHbitmap(), IntPtr.Zero,
                        Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(imageUnfiltred.Width, imageUnfiltred.Height));

                    boxImage.Source = imageSource;

                    boxImage.IsEnabled = true;
                    comboBoxFilters.IsEnabled = true;
                    buttonApply.IsEnabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFilters.SelectedItem == null)
            {
                MessageBox.Show("No filter selected");
            }

            isCanceled = false;

            client = new ContractClientBase(new System.ServiceModel.InstanceContext(this));
            byte[] bytes = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageUnfiltred.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                bytes = memoryStream.GetBuffer();
            }

            try
            {
                client.ApplyFilter(comboBoxFilters.SelectedItem.ToString(), bytes);

                buttonApply.IsEnabled = false;
                comboBoxFilters.IsEnabled = false;
                buttonSelect.IsEnabled = false;
                boxImage.IsEnabled = false;
                buttonSave.IsEnabled = false;

                buttonCancel.IsEnabled = true;
                barProgress.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            barProgress.Value = 0;
            buttonCancel.IsEnabled = false;
            barProgress.IsEnabled = false;

            buttonApply.IsEnabled = true;
            buttonSelect.IsEnabled = true;
            comboBoxFilters.IsEnabled = true;
            boxImage.IsEnabled = true;

            isCanceled = true;

            client.Abort();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveSelection = new SaveFileDialog();

            saveSelection.Title = "Save the image";
            saveSelection.Filter = "Все файлы (*.*) | *.*";

            string stamp = comboBoxFilters.SelectedItem.ToString() + path.Substring(path.LastIndexOf('.'));
            path = path.Remove(path.LastIndexOf('.'));
            path += stamp;
            saveSelection.FileName = path;

            if ((bool)saveSelection.ShowDialog())
            {
                try
                {
                    using (MemoryStream memoryStream = new MemoryStream(imageToSaving))
                    {
                        imageSaved = (Bitmap)Bitmap.FromStream(memoryStream);
                        imageSaved.Save(saveSelection.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    imageToSaving = null;

                    buttonSave.IsEnabled = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void ImageCallback(byte[] bytes)
        {
            Dispatcher.BeginInvoke(new System.Threading.ThreadStart
                (delegate
                {
                    if (!isCanceled)
                    {
                        imageToSaving = bytes;

                        using (MemoryStream memoryStream = new MemoryStream(bytes))
                        {
                            imageFiltred = (Bitmap)Bitmap.FromStream(memoryStream);
                        }

                        BitmapSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(imageFiltred.GetHbitmap(), IntPtr.Zero,
                            Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(imageFiltred.Width, imageFiltred.Height));

                        boxImage.Source = imageSource;

                        boxImage.IsEnabled = true;
                        buttonApply.IsEnabled = true;
                        buttonSelect.IsEnabled = true;
                        comboBoxFilters.IsEnabled = true;
                        buttonSave.IsEnabled = true;

                        buttonCancel.IsEnabled = false;
                        barProgress.Value = 0;
                        barProgress.IsEnabled = false;
                    }
                }));
        }

        public void ProgressCallback(int progress)
        {
            Dispatcher.BeginInvoke(new System.Threading.ThreadStart
                (delegate
                {
                    if (!isCanceled)
                    {
                        barProgress.Value = progress;
                    }
                    else
                    {
                        barProgress.Value = 0;
                    }
                }));
          
        }
    }
}