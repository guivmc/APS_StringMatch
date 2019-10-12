using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APS_StringMatch
{
    public class WordFinder
    {
        public string[] Lines { get; set; }

        public Dictionary<string, int> Words { get; set; }

        public string TimeOneCore { get; set; }
        public string TimeMultiCore { get; set; }

        public WordFinder( string FilePath )
        {
            this.Lines = File.ReadAllLines( FilePath, Encoding.UTF8 );
            this.Lines[1] = this.Lines[1].ToLower();

            this.Words = new Dictionary<string, int>();
            this.Words.Add( "for", 0 );
            this.Words.Add( "double", 0 );
            this.Words.Add( "float", 0 );
            this.Words.Add( "int", 0 );
            this.Words.Add( "char", 0 );
        }

        public void SingleThreadFind(int posStart = 0, int? end = null)
        {
            try
            {
                end = end ?? Int32.Parse( this.Lines[0] );

                for( int i = posStart; i < end && !Char.IsWhiteSpace( this.Lines[1].ElementAt( i ) ); i++ )
                {

                    switch( this.Lines[1].ElementAt( i ) )
                    {
                        case 'f':
                            if( this.Lines[1].ElementAt( i + 1 ) == 'o' && this.Lines[1].ElementAt( i + 2 ) == 'r' )
                            {
                                this.Words["for"]++;
                                Console.WriteLine( "Achei um for! " + this.Words["for"] );
                                i += 2;
                                break;
                            }

                            if( this.Lines[1].ElementAt( i + 1 ) == 'l' && this.Lines[1].ElementAt( i + 2 ) == 'o' &&
                                this.Lines[1].ElementAt( i + 3 ) == 'a' && this.Lines[1].ElementAt( i + 4 ) == 't' )
                            {
                                this.Words["float"]++;
                                Console.WriteLine( "Achei um float! " + this.Words["float"] );
                                i += 4;
                                break;
                            }

                            break;
                        case 'c':
                            if( this.Lines[1].ElementAt( i + 1 ) == 'h' && this.Lines[1].ElementAt( i + 2 ) == 'a' &&
                                this.Lines[1].ElementAt( i + 3 ) == 'r' )
                            {
                                this.Words["char"]++;
                                Console.WriteLine( "Achei um char! " + this.Words["char"] );
                                i += 3;
                            }

                            break;
                        case 'd':
                            if( this.Lines[1].ElementAt( i + 1 ) == 'o' && this.Lines[1].ElementAt( i + 2 ) == 'u' &&
                                this.Lines[1].ElementAt( i + 3 ) == 'b' && this.Lines[1].ElementAt( i + 4 ) == 'l' &&
                                this.Lines[1].ElementAt( i + 5 ) == 'e' )
                            {
                                this.Words["double"]++;
                                Console.WriteLine( "Achei um double! " + this.Words["double"] );
                                i += 4;
                                break;
                            }

                            break;
                        case 'i':
                            if( this.Lines[1].ElementAt( i + 1 ) == 'n' && this.Lines[1].ElementAt( i + 2 ) == 't' )
                            {
                                this.Words["int"]++;
                                Console.WriteLine( "Achei um int! " + this.Words["int"] );
                                i += 2;
                            }
                            break;
                    }


                }
            }
            catch( Exception e )
            {

            }      
        }

        public void MultiThreadFind()
        {
            this.Words = this.Words.ToDictionary( p => p.Key, p => 0 );

            int size = Int32.Parse( this.Lines[0] ) / 3 + Int32.Parse( this.Lines[0] ) % 3;

            Thread t1 = new Thread( new ThreadStart( () => this.SingleThreadFind( 0, size)));
            Thread t2 = new Thread( new ThreadStart( () => this.SingleThreadFind( size, 2 * size )));
            Thread t3 = new Thread( new ThreadStart( () => this.SingleThreadFind( 2 * size, 3 * size )));

            t1.Name = "Thread1";
            t2.Name = "Thread2";
            t3.Name = "Thread3";

            DateTime fin;
            DateTime start = DateTime.Now;
            t1.Start();
            t2.Start();
            t3.Start();
            fin = DateTime.Now;
            this.TimeMultiCore = fin.Subtract( start ).ToString();
        }

        public string PrintTotal()
        {
            return "Int: " + this.Words["int"] + "\nFloat: " + this.Words["float"] + "\nChar: " + this.Words["char"] + "\nFor: " + this.Words["for"] + "\nDouble: " + this.Words["double"];
        }

    }
}
