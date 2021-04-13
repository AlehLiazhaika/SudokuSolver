using Algorithms.Contracts;
using Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    internal sealed class AlgorithmX : IAlgorithmX
    {
        public ICollection<ISet<T>> Solve<T>(ISet<T> setX, ICollection<ISet<T>> collectionS)
        {
            var incidenceMatrix = new IncidenceMatrix<T>(setX, collectionS);
            var solutionStack = new Stack<ISet<T>>();

            DoIteration();

            return solutionStack.ToArray();

            
            bool DoIteration() //TODO Rename, change return type, add exceptions handling
            {
                if (!incidenceMatrix.VisibleColumns.Any())
                {
                    solutionStack.ToArray();
                    return true;
                }
                else
                {
                    if (incidenceMatrix.VisibleRows.Any())
                    {
                        var currentColumn = incidenceMatrix.GetShortestVisibleColumn();

                        foreach (var row in currentColumn.Rows.Where(x => x.IsVisible))
                        {
                            solutionStack.Push(row.Items);

                            var columnsToHide =
                                incidenceMatrix.VisibleColumns.Where(x => row.Items.Contains(x.Item)).ToList();

                            var rowsToHide =
                                incidenceMatrix.VisibleRows.Where(x => x.Items.Intersect(row.Items).Any()).ToList();

                            HideAll(columnsToHide);
                            HideAll(rowsToHide);

                            if (DoIteration())
                            {
                                return true;
                            }

                            solutionStack.Pop();

                            ShowAll(columnsToHide);
                            ShowAll(rowsToHide);
                        }
                    }

                    return false;
                }
            }

            void HideAll(IEnumerable<Visible> source)
            {
                foreach (var item in source)
                {
                    item.Hide();
                }
            }

            void ShowAll(IEnumerable<Visible> source)
            {
                foreach (var item in source)
                {
                    item.Show();
                }
            }
        }
    }
}