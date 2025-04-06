using System;
using System.Collections;
using System.Drawing;

namespace E13.Common.Nunit.Extensions
{
    // Because this is a helper for xunit tests disabling warnings is acceptable
#pragma warning disable CA1010 // Collections should implement generic interface
#pragma warning disable CA1058 // Types should not extend certain base types
    public class PointOfInterestCollection : CollectionBase
#pragma warning restore CA1058 // Types should not extend certain base types
#pragma warning restore CA1010 // Collections should implement generic interface
    {
        public PointOfInterest this[int index]
        {
            get { return List[index] as PointOfInterest ?? throw new InvalidOperationException("PointOfInterest is null"); }
        }

        public void Add(PointOfInterest pointOfInterest)
        {
            List.Add(pointOfInterest);
        }

        private int FindExistingRectangle(
            int xCoordinate,
            int yCoordinate)
        {
            int indexOfMatchingPointOfInterest = -1;

            for (int index = 0; index < List.Count; ++index)
            {
                PointOfInterest existing = this[index];

                if ((xCoordinate >= existing.XCoordinate - 40
                    && xCoordinate <= existing.PreviousXCoordinate + 40)
                    && (yCoordinate >= existing.YCoordinate - 40
                    && yCoordinate <= existing.PreviousYCoordinate + 40))
                {
                    indexOfMatchingPointOfInterest = index;
                    break;
                }
            }

            return indexOfMatchingPointOfInterest;
        }

        public Rectangle[] GetRectangles()
        {
            RemoveInvalidPointOfInterests();

            Rectangle[] rectangles =
                new Rectangle[List.Count];

            for (int i = 0; i <= (List.Count - 1); ++i)
            {
                PointOfInterest pointOfInterest = this[i];
                rectangles[i] =
                    new Rectangle(
                        pointOfInterest.XCoordinate,
                        pointOfInterest.YCoordinate,
                        pointOfInterest.Width,
                        pointOfInterest.Height);
            }

            return rectangles;
        }

        public void ProcessCoordinates(
            int xCoordinate,
            int yCoordinate)
        {
            int indexOfExistingRectangle =
                FindExistingRectangle(xCoordinate, yCoordinate);
            if (indexOfExistingRectangle == -1)
            {
                Add(new PointOfInterest(xCoordinate, yCoordinate));
            }
            else
            {
                this[indexOfExistingRectangle].Adjust(
                    xCoordinate, yCoordinate);
            }
        }

        private void RemoveInvalidPointOfInterests()
        {
            for (int index = 0; index <= (List.Count - 1); ++index)
            {
                if (this[index].Height == 0
                    || this[index].Width == 0)
                {
                    this.RemoveAt(index);
                    --index;
                }
            }
        }
    }
}