// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2020_1_OR_NEWER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evolutex.Evolunity.Collections
{
    public sealed class ListChangeEventArgs<T> : EventArgs
    {
        public readonly int Index;
        public readonly T Item;

        public ListChangeEventArgs(int index, T item)
        {
            Index = index;
            Item = item;
        }
    }

    public delegate void ListChangedEventHandler<T>(ObservableList<T> sender, ListChangeEventArgs<T> e);

    /// <summary>
    /// <para/>Reference:
    /// <see cref="UnityEngine.Rendering.ObservableList"/>
    ///
    /// <para/>Changes:
    /// <br/>- Refactored.
    /// <br/>- Made serialization via ISerializationCallbackReceiver interface.
    ///
    /// <para/>Problem:
    /// <br/>- It is not possible to use duplicate items in Inspector.
    ///   For example, we can't add a second or more item through the inspector
    ///   because Unity makes a duplicate of the previous item by default.
    ///
    /// <para/>TODO:
    /// <br/>- Remake OnAfterDeserialize to make possible use of duplicate items.
    ///   Comparison should be based on count of items instead of Contains method.
    ///   We should also execute the Add or Remove method a number of times
    ///   equal to the difference between the quantities in the serialized and main lists.
    /// <br/>- List methods without notification (optional bool argument).
    /// </summary>
    [Serializable]
    public class ObservableList<T> : IList<T>, ISerializationCallbackReceiver
    {
        // [Label("Items")]
        [SerializeField]
        private List<T> _items = new List<T>();
        [SerializeField, HideInInspector]
        private List<T> _list;

        public ObservableList()
        {
            _list = new List<T>();
        }

        public ObservableList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public ObservableList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }

        public event ListChangedEventHandler<T> ItemAdded;

        public event ListChangedEventHandler<T> ItemRemoved;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                OnEvent(ItemRemoved, index, _list[index]);
                _list[index] = value;
                OnEvent(ItemAdded, index, value);
            }
        }

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        private void OnEvent(ListChangedEventHandler<T> handler, int index, T item)
        {
            handler?.Invoke(this, new ListChangeEventArgs<T>(index, item));
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Add(T item)
        {
            _list.Add(item);

            OnEvent(ItemAdded, _list.IndexOf(item), item);
        }

        public void Add(params T[] items)
        {
            foreach (T item in items)
                Add(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);

            OnEvent(ItemAdded, index, item);
        }

        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            bool isRemoved = _list.Remove(item);

            if (isRemoved)
                OnEvent(ItemRemoved, index, item);
            return isRemoved;
        }

        public int Remove(params T[] items)
        {
            return items?.Sum(item => Remove(item) ? 1 : 0) ?? 0;
        }

        public void RemoveAt(int index)
        {
            T item = _list[index];
            _list.RemoveAt(index);

            OnEvent(ItemRemoved, index, item);
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                RemoveAt(i);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void OnBeforeSerialize()
        {
            _items.Clear();
            foreach (T item in _list)
                _items.Add(item);
        }

        public void OnAfterDeserialize()
        {
            foreach (T item in _list.Where(item => !_items.Contains(item)).ToArray())
                Remove(item);
            foreach (T item in _items.Where(item => !_list.Contains(item)))
                Add(item);
        }
    }
}

#endif