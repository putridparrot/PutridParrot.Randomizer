﻿using System;

namespace PutridParrot.Randomizer
{
    /// <summary>
    /// Thread-safe PseudoRandomizer
    /// </summary>
    public class ConcurrentPseudoRandomizer : PseudoRandomizer
    {
        public override int NextInt(int minValue, int maxValue)
        {
            lock (_random)
            {
                return base.NextInt(minValue, maxValue);
            }
        }

        public override double NextDouble(double minValue, double maxValue)
        {
            lock (_random)
            {
                return base.NextDouble(minValue, maxValue);
            }
        }

        public override void NextBytes(Span<byte> buffer)
        {
            lock (_random)
            {
                base.NextBytes(buffer);
            }
        }
        public override void NextBytes(byte[] buffer)
        {
            lock (_random)
            {
                base.NextBytes(buffer);
            }
        }
    }
}