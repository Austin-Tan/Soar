using System;
using System.Threading;


namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(16, 42069);
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.UtcNow;

            while (isRunning)
            {
                while (_nextLoop < DateTime.UtcNow)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.UtcNow)
                    {
                        Thread.Sleep(_nextLoop - DateTime.UtcNow);
                    }
                }
            }
        }
    }
}
