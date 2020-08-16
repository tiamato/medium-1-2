using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bag
{
    public class Bag
    {
        private readonly int _maxWeigth;
        private readonly List<Item> _items = new List<Item>();

        public ReadOnlyCollection<ReadOnlyItem> Items => _items.ConvertAll(Item.ToReadOnlyItem).AsReadOnly();

        public Bag(int maxWeigth, IEnumerable<Item> items)
        {
            _maxWeigth = maxWeigth;

            foreach (var item in items)
            {
                _items.Add(new Item(item.Name, item.Count));
            }
        }

        public void AddItem(string name, int count)
        {
            var targetItem = _items.FirstOrDefault(item => item.Name == name);

            if (targetItem == null)
                throw new InvalidOperationException();

            if (GetCurrentWeigth() + count > _maxWeigth)
                throw new InvalidOperationException();

            targetItem.Count = count;
        }

        private int GetCurrentWeigth()
        {
            return _items.Sum(item => item.Count);
        }
    }

    public class Item
    {
        private int _count;

        public int Count
        {
            get => _count;
            set => _count += value;
        }

        public string Name { get; }

        public Item(string name, int count)
        {
            Name = name;
            Count = count;
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
