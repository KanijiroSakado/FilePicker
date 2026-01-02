using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sakado
{
    internal class Example
    {
        public static void Main()
        {
            FilePicker filePicker = new FilePicker();
            filePicker.Add(@"D:\workspace\test1", 10);
            filePicker.Add(@"D:\workspace\test2", 80);

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(filePicker.GetNextFile());
            }

        }
    }
}
