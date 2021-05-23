using System;
using Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Algorithms.Extensions;
using SuccincT.Options;

[assembly: InternalsVisibleTo("Algorithms.Tests")]
namespace Algorithms
{
    internal sealed class AlgorithmX : IAlgorithmX
    {
        public Option<ICollection<ISet<T>>> GetExactCover<T>(ISet<T> setX, ICollection<ISet<T>> collectionS)
        {
            if (collectionS.Any(x => x.Any(y => !setX.Contains(y))))
            {
                throw new ArgumentException(message: "One or more of subsets elements do not consist in the basic set X");
            }

            var exactCover = GetExactCover(incidenceMatrix: new IncidenceMatrix<T>(setX, collectionS));

            return exactCover.Count != 0 ? Option<ICollection<ISet<T>>>.Some(exactCover) : Option<ICollection<ISet<T>>>.None();
        }

        private static ICollection<ISet<T>> GetExactCover<T>(IncidenceMatrix<T> incidenceMatrix) //TODO add exceptions handling
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

                    var solution = GetExactCover(incidenceMatrix);

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