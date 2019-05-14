using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Opretning af HelloWriter objekter
            HelloWriter HW1 = new HelloWriter("J. K. Rowling");
            HelloWriter HW2 = new HelloWriter("Bo Burnham");
            HelloWriter HW3 = new HelloWriter("Hat");
            //HW1.NumberOfTimesToLoop = 500;
            //HW2.NumberOfTimesToLoop = 500;

            //Oprettelse af tråde
            Thread t1 = new Thread(HW1.SayHallo);
            Thread t2 = new Thread(HW2.SayHallo);
            Thread NeverEndingStory = new Thread(HW3.WriteForever);
            HW1.sleepTime = 1;
            HW2.sleepTime = 1;
            t1.Start(500);
            t2.Start(500);
            //NeverEndingStory.IsBackground = true;
            //Når den er background, så kan man trykke enter og lukke, pga console.readkey i main
            NeverEndingStory.Start(500);
            //Når man har en neverendingstory uden den er background, så stopper den først når man lukker programmet ned

            //Når man køre programmet kommer dele fra de to tråde i klumper, men starter altid med den der bliver kaldt først.
            //Havde den ene kørt i main, så havde den oftest kommet først, SELV hvis den blev kaldt efter at tråden blev startet.

            t1.Join();
            t2.Join();
            HW3.ShallStop = true;
            //Med shallstop, så lukker den ned når t1 og t2 har joinet.

           
            //Uden joinmetoderne, så ville main skrive Hello from Main når den kan 
            Console.WriteLine("Hello from main");
            Console.WriteLine("Hat");
            Console.ReadKey();

        }
    }



    // For lethedens skyld har jeg oprettet HelloWriter inde i main.
    public class HelloWriter
    {
        private string Name;
        //private int NumberOfTimesToLoop;
        public int sleepTime;
        public bool ShallStop;
        public HelloWriter(string name)
        {
            Name = name;
            sleepTime = 0;
            //NumberOfTimesToLoop = numberOfTimesToLoop;
        }

        public void SayHallo(object parameter)
        {
            int numberOfTimesToLoop = (int) parameter;

            for (int i = 0; i < numberOfTimesToLoop; i++)
            {
                Console.WriteLine("Hello form {0}", Name);

                Thread.Sleep(sleepTime);
            }
        }

        public void WriteForever(object parameter3)
        {
            int pause = (int) parameter3;

            while (!ShallStop)
            {
                Console.WriteLine("Never ending story");

                Thread.Sleep(pause);
            }
            
        }

    }
}
