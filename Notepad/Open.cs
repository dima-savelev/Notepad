using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Notepad
{
    class Open
    {
        public static bool OpenFile(RichTextBox fieldEdit, ref string nameFile, ref string fullName)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "rtf";
            open.Filter = "Текстовый файл (*.rtf) |*.rtf|Все файлы(*.*)|*.*";
            if (open.ShowDialog() != true)
            {
                return false;
            }
            nameFile = open.SafeFileName;
            fullName = open.FileName;
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
            using (FileStream fs = new FileStream(fullName, FileMode.Open))
            {
                if (fullName.Remove(0, fullName.LastIndexOf('.')) == ".rtf")
                    doc.Load(fs, DataFormats.Rtf);
                if (fullName.Remove(0, fullName.LastIndexOf('.')) == ".txt")
                    doc.Load(fs, DataFormats.Text);
            }
            return true;
        }
    }
}
