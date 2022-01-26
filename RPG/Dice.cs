using GeneralUtils;
using System;
using System.Threading;

namespace RPG
{
    public class Dice
    {
        private const string _smallDiceModel = @"╭─────╮
│  ╳  │
╰─────╯";

        private const int _animationInterval = 70;

        private readonly Random _random;
        private readonly int _maxNumber;
        private readonly int[] _beforeStop = new int[] { 3, 6, 5, 4, 2, 1, 5, 4, 1, 3, 6, 2, 1, 5, 4 };

        public Dice(int max = 6)
        {
            _random = new Random();
            _maxNumber = max;
        }

        public int Roll()
        {
            return _random.Next(1, _maxNumber + 1);
        }

        public int RollWithAnimation(int x = -1, int y = -1)
        {
            int index = 0;
            int number;
            int numberX = (x == -1 ? -1 : x + 3);
            int numberY = (y == -1 ? -1 : y + 1);

            DisplayTools.WriteInWindowCenter(_smallDiceModel, x, y);

            while (index < _beforeStop.Length)
            {
                DisplayTools.WriteInWindowCenter(_beforeStop[index].ToString(), numberX, numberY);

                index++;
                Thread.Sleep(_animationInterval);
            }

            number = Roll();
            DisplayTools.WriteInWindowCenter(number.ToString(), numberX, numberY);

            return number;
        }
    }
}
