using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bag
{
    class Bag
    {
        private readonly int _maxWeigth;
        private readonly List<Item> _items = new List<Item>();

        public ReadOnlyCollection<ReadOnlyItem> Items => _items.ConvertAll(Item.ToReadOnlyItem).AsReadOnly();

        public Bag(int maxWeigth, Item[] items)
        {
            _maxWeigth = maxWeigth;

            foreach (Item item in items)
            {
                _items.Add(new Item(item.Name, item.Count));
            }
        }

        public void AddItem(string name, int count)
        {

            Item targetItem = _items.FirstOrDefault(item => item.Name == name);

            if (targetItem == null)
                throw new InvalidOperationException();

            if (GetCurrentWeigth() + count > _maxWeigth)
                throw new InvalidOperationException();

            targetItem.AddCount(count);
        }

        private int GetCurrentWeigth()
        {
            return _items.Sum(item => item.Count);
        }
    }

    public class Item
    {
        public int Count { private set; get; }
        public string Name { get; }

        public Item(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public void AddCount(int count)
        {
            Count += count;
        }

        public static ReadOnlyItem ToReadOnlyItem(Item item)
        {
            return new ReadOnlyItem(item);
        }
    }

    public class ReadOnlyItem
    {
        private readonly Item _item;
        public int Count => _item.Count;
        public string Name => _item.Name;

        public ReadOnlyItem(Item item)
        {
            _item = item;
        }
    }
}
