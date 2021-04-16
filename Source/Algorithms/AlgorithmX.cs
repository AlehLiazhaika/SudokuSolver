using Algorithms.Contracts;
using Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Extensions;

namespace Algorithms
{
    internal sealed class AlgorithmX : IAlgorithmX
    {
        public ICollection<ISet<T>> Solve<T>(ISet<T> setX, ICollection<ISet<T>> collectionS) =>
            GetSolution(new IncidenceMatrix<T>(setX, collectionS));

        private static ICollection<ISet<T>> GetSolution<T>(IncidenceMatrix<T> incidenceMatrix) //TODO add exceptions handling
        {
            if (incidenceMatrix.VisibleRows.Any())
            {
                var currentColumn = incidenceMatrix.VisibleColumns.OrderBy(x => x.Rows.Count(y => y.IsVisible)).First();

                foreach (var row in currentColumn.Rows.Where(x => x.IsVisible))
                {
                    var adjacentColumns = incidenceMatrix.VisibleColumns.Where(x => row.Items.Contains(x.Item)).ToList();
                    var adjacentRows = incidenceMatrix.VisibleRows.Where(x => x.Items.Intersect(row.Items).Any()).ToList();

                    adjacentColumns.Hide();
                    adjacentRows.Hide();

                    if (!incidenceMatrix.VisibleColumns.Any())
                    {
                        return new List<ISet<T>> { row.Items };
                    }

                    var solution = GetSolution(incidenceMatrix);

                    if (solution.Count > 0)
                    {
                        solution.Add(row.Items);
                        return solution;
                    }

                    adjacentColumns.Show();
                    adjacentRows.Show();
                }
            }

            return new List<ISet<T>>();
        }
    }
}