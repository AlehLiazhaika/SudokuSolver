using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Algorithms.Utils
{
    internal class IncidenceMatrix<T>
    {
        internal ICollection<Row<T>> Rows { get; } = new Collection<Row<T>>();
        internal ICollection<Row<T>> VisibleRows => Rows.Where(x => x.IsVisible).ToList();

        internal ICollection<Column<T>> Columns { get; } = new Collection<Column<T>>();
        internal ICollection<Column<T>> VisibleColumns => Columns.Where(x => x.IsVisible).ToList();

        internal IncidenceMatrix(ISet<T> xSet, ICollection<ISet<T>> collectionS)
        {
            InitColumns();
            InitRows();
            MapComponents();
            

            void InitColumns()
            {
                foreach (var item in xSet)
                {
                    Columns.Add(new Column<T>(item));
                }
            }

            void InitRows()
            {
                foreach (var set in collectionS)
                {
                    Rows.Add(new Row<T>(set));
                }
            }

            void MapComponents()
            {
                foreach (var row in Rows)
                {
                    foreach (var item in row.Items)
                    {
                        foreach (var column in Columns.Where(x => x.Item.Equals(item)))
                        {
                            row.Columns.Add(column);
                            column.Rows.Add(row);
                        }
                    }
                }
            }
        }
    }

    internal abstract class Visible
    {
        internal bool IsVisible { get; set; } = true;
        internal void Hide() => IsVisible = false;
        internal void Show() => IsVisible = true;
    }

    internal class Row<T> : Visible
    {
        internal ISet<T> Items { get; }
        internal ICollection<Column<T>> Columns { get; } = new Collection<Column<T>>();
        internal Row(ISet<T> items) => Items = items;
    }

    internal class Column<T> : Visible
    {
        internal T Item { get; }
        internal ICollection<Row<T>> Rows { get; } = new Collection<Row<T>>();
        internal Column(T item) => Item = item;
    }
}