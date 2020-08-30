using System;
using System.Collections.Generic;
using System.Linq;

namespace Bag
{
    public class Bag
    {
        private readonly int _maxWeigth;
        private readonly List<IItem> _items = new List<IItem>();

        public Bag(int maxWeigth, IEnumerable<IItem> items)
        {
            _maxWeigth = maxWeigth;

            foreach (var item in items)
            {
                _items.Add(new Item(item.Name, item.Count));
            }
        }

        public IEnumerable<IItem> Items => _items;

        private int GetCurrentWeigth()
        {
            return Items.Sum(item => item.Count);
        }

        public void AddItem(string name, int count)
        {
            var targetItem = _items.FirstOrDefault(item => item.Name == name);

            if (targetItem == null)
                throw new InvalidOperationException();

            if (GetCurrentWeigth() + count > _maxWeigth)
                throw new InvalidOperationException();

            ((Item) targetItem).Count += count;
        }
    }

    public interface IItem
    {
        string Name { get; }
        int Count { get; }
    }

    public class Item: IItem
    {
        public string Name { get; }
        public int Count { get; set; }

        public Item(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
