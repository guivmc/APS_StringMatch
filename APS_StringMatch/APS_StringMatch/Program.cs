using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS_StringMatch
{
    class Program
    {

        static void Main( string[] args )
        {
            WordFinder wf = new WordFinder( "..\\Text\\file.txt" );


            DateTime fin;
            DateTime start = DateTime.Now;

            wf.SingleThreadFind();

            fin = DateTime.Now;
            wf.TimeOneCore = fin.Subtract( start ).ToString();

            Console.WriteLine( wf.PrintTotal() );

            Console.WriteLine( "Caluna A: " + wf.Lines[1].Length );

            Console.WriteLine( "Caluna B: " + wf.TimeOneCore );

            Console.WriteLine( "Caluna C: 3" );

            wf.MultiThreadFind();

            Console.WriteLine( "Caluna D: " + wf.TimeMultiCore );

            Console.ReadLine();
        }
    }
}
