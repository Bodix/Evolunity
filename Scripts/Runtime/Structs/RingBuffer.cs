// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Structs
{
	public class RingBuffer<T> where T : struct
	{
		private readonly T[] _buffer;

		public RingBuffer(int size)
		{
			if (size <= 0)
				throw new ArgumentException("Ring buffer size must be greater than zero.");

			_buffer = new T[size];
		}

		public T this[int tick]
		{
			get => Get(tick);
			set => Set(tick, value);
		}

		public int Size => _buffer.Length;

		public void Set(int tick, T data)
		{
			int index = Math.Abs(tick) % Size;

			_buffer[index] = data;
		}

		public T Get(int tick)
		{
			int index = Math.Abs(tick) % Size;

			return _buffer[index];
		}
	}
}