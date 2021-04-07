﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Fibers
{
    public class Process
    {
        private static readonly Random Rng = new Random();
        private const int LongPauseBoundary = 10000;
        private const int ShortPauseBoundary = 100;
        private const int WorkBoundary = 1000;
        private const int IntervalsAmountBoundary = 10;
        private const int PriorityLevelsNumber = 10;
        private readonly List<int> _workIntervals = new List<int>();
        private readonly List<int> _pauseIntervals = new List<int>();
        
        public bool IsReady { get; private set; }
        public int Priority { get; private set; }

        public int ActiveDuration
        {
            get
            {
                return _workIntervals.Sum();
            }
        }

        public int TotalDuration
        {
            get
            {
                return ActiveDuration + _pauseIntervals.Sum();
            }
        }

        public Process()
        {
            int amount = Rng.Next(IntervalsAmountBoundary);

            for (int i = 0; i < amount; i++)
            {
                _workIntervals.Add(Rng.Next(WorkBoundary));
                _pauseIntervals.Add(Rng.Next(Rng.NextDouble() > 0.9 ? LongPauseBoundary : ShortPauseBoundary));
            }

            Priority = Rng.Next(PriorityLevelsNumber);
            IsReady = true;
        }

        public void Run()
        {
            for (int i = 0; i < _workIntervals.Count; i++)
            {
                IsReady = true;
                Thread.Sleep(_workIntervals[i]); // work emulation

                DateTime pauseBeginTime = DateTime.Now;

                do
                {
                    ProcessManager.Switch(false);
                    IsReady = false;
                }
                while ((DateTime.Now - pauseBeginTime).TotalMilliseconds < _pauseIntervals[i]); // I/O emulation
            }

            ProcessManager.Switch(true);
        }
    }
}