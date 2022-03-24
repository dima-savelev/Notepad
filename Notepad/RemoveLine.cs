using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notepad
{
    public static class RemoveLine
    {
        public static int Number;
        public static void Remove(this RichTextBox richTextBox, int number)
        {
            string[] lines = richTextBox.GetText().Split("\n".ToCharArray());
            int lineToDelete = number - 1;
            Trace.WriteLine(richTextBox.GetText());
            string richText = "";
            for (int i = 0; i < lines.GetLength(0)-1; i++)
            {
                if (i != lineToDelete)
                {
                    richText += lines[i] + "\n";
                }
            }
            richTextBox.SetText(richText);
        }
    }
}
