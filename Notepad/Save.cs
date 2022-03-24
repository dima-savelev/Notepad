using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Notepad
{
    class Save
    {
        public static bool ASaveBloknot(RichTextBox fieldEdit, ref string nameFile, ref string fullName)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "rtf";
            save.Filter = "Текстовый файл (*.rtf) |*.rtf|Все файлы(*.*)|*.*";
            if (save.ShowDialog() == true)
            {
                nameFile = save.SafeFileName;
                fullName = save.FileName;
            }
            else
            {
                return false;
            }
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(save.FileName);
            doc.Save(fs, DataFormats.Rtf);
            fs.Close();
            return true;
        }
        public static bool ASave(RichTextBox fieldEdit, ref string fullName)
        {
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(fullName);
            doc.Save(fs, DataFormats.Rtf);
            fs.Close();
            return true;
        }
    }
}
