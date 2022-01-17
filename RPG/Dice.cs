using System;
using System.Collections.Generic;
using System.Text;
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

        private int Roll()
        {
            return _random.Next(1, _maxNumber + 1);
        }

        public int RunRollAnimation()
        {
            int index = 0;
            int number;

            DisplayTools.WriteInWindowCenter(_smallDiceModel);

            while (index < _beforeStop.Length)
            {
                DisplayTools.WriteInWindowCenter(_beforeStop[index].ToString());

                index++;
                Thread.Sleep(_animationInterval);
            }

            number = Roll();
            DisplayTools.WriteInWindowCenter(number.ToString());

            return number;
        }
    }
}
