using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool saveFile = false;
        bool textChanged = false;
        string nameFile = "Безымянный";
        string fullName = "";
        private void RichBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(richBox.Document.ContentStart, richBox.Document.ContentEnd).Text;
            FormattedText ft = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(richBox.FontFamily, richBox.FontStyle, richBox.FontWeight, richBox.FontStretch), richBox.FontSize, Brushes.Black);
            richBox.Document.PageWidth = ft.Width + 12;
            Title = $"*{nameFile} - Блокнот";
            textChanged = true;
        }
        private int _deltaMouseWheel;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control))
            {
                if (_deltaMouseWheel > 0 && richBox.FontSize < 100)
                {
                    richBox.FontSize += 1;
                    string text = new TextRange(richBox.Document.ContentStart, richBox.Document.ContentEnd).Text;
                    FormattedText ft = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(richBox.FontFamily, richBox.FontStyle, richBox.FontWeight, richBox.FontStretch), richBox.FontSize, Brushes.Black);
                    richBox.Document.PageWidth = ft.Width + 12;
                }
                else if (_deltaMouseWheel < 0)
                {
                    if (richBox.FontSize < 5)
                    {
                        return;
                    }
                    richBox.FontSize -= 1;
                    string text = new TextRange(richBox.Document.ContentStart, richBox.Document.ContentEnd).Text;
                    FormattedText ft = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(richBox.FontFamily, richBox.FontStyle, richBox.FontWeight, richBox.FontStretch), richBox.FontSize, Brushes.Black);
                    if (ft.Width > richBox.Document.PageWidth)
                    {
                        richBox.Document.PageWidth = ft.Width - 12;
                    }

                }
            }
            _deltaMouseWheel = 0;
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _deltaMouseWheel = e.Delta;
        }
       
        private void FastSave_File(object sender, RoutedEventArgs e)
        {
            if (saveFile != true)
            {
                if (Save.ASaveBloknot(richBox, ref nameFile, ref fullName) == false)
                {
                    return;
                }
                saveFile = Save.ASaveBloknot(richBox, ref nameFile, ref fullName);
                Title = nameFile + " - Блокнот";
                textChanged = false;
            }
            if (saveFile == true)
            {
                Save.ASave(richBox, ref fullName);
                textChanged = false;
            }
            saveFile = true;

        }
        private void Save_File(object sender, RoutedEventArgs e)
        {
            if (Save.ASaveBloknot(richBox, ref nameFile, ref fullName) == false)
            {
                return;
            }
            saveFile = Save.ASaveBloknot(richBox, ref nameFile, ref fullName);
            Title = nameFile + " - Блокнот";
            textChanged = false;
            saveFile = true;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (textChanged == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show($"Вы хотите сохранить изменения в файле «{nameFile}»", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes && saveFile == true)
                {
                    if (Save.ASave(richBox, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Yes && saveFile != true)
                {
                    if (Save.ASaveBloknot(richBox, ref nameFile, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            richBox.Document.Blocks.Clear();
            nameFile = "Безымянный";
            Title = nameFile + " - Блокнот";
            textChanged = false;
            saveFile = false;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (textChanged == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show($"Вы хотите сохранить изменения в файле «{nameFile}»", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes && saveFile == true)
                {
                    if (Save.ASave(richBox, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Yes && saveFile != true)
                {
                    if (Save.ASaveBloknot(richBox, ref nameFile, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            if (Open.OpenFile(richBox, ref nameFile, ref fullName) == false)
            {
                return;
            }
            Title = $"{nameFile} - Блокнот";
            textChanged = false;
            saveFile = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (textChanged == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show($"Вы хотите сохранить изменения в файле «{nameFile}»", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes && saveFile == true)
                {
                    if (Save.ASave(richBox, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Yes && saveFile != true)
                {
                    if (Save.ASaveBloknot(richBox, ref nameFile, ref fullName) == false)
                    {
                        return;
                    }
                }
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            richBox.Paste();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик - Савельев Дмитрий Александрович\nТекстовый редактор для работы с файлами формата rtf", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RemoveLine_Click(object sender, RoutedEventArgs e)
        {
            DelLine delLine = new DelLine();
            delLine.ShowDialog();
            RemoveLine.Remove(richBox, RemoveLine.Number);
            ((Paragraph)richBox.Document.Blocks.FirstBlock).LineHeight = 5;
        }
    }
}
