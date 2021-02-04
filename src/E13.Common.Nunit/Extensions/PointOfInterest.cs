using System;
using System.Collections;
using System.Drawing;

namespace E13.Common.Nunit.Extensions
{
    public class PointOfInterest
    {
        public PointOfInterest(
            int xCoordinate,
            int yCoordinate)
        {
            PreviousXCoordinate = XCoordinate = xCoordinate;
            PreviousYCoordinate = YCoordinate = yCoordinate;
        }

        public void Adjust(int xCoordinate, int yCoordinate)
        {
            if (xCoordinate > this.XCoordinate
                && xCoordinate > this.PreviousXCoordinate)
            {
                this.PreviousXCoordinate = xCoordinate;
            }
            else if (xCoordinate < this.XCoordinate)
            {
                this.XCoordinate = xCoordinate;
            }
            if (yCoordinate > this.YCoordinate
                && yCoordinate > this.PreviousYCoordinate)
            {
                this.PreviousYCoordinate = yCoordinate;
            }
            else if (yCoordinate < this.YCoordinate)
            {
                this.YCoordinate = yCoordinate;
            }
        }

        #region Attributes

        public int Height
        {
            get { return PreviousYCoordinate - YCoordinate; }
        }

        public int PreviousXCoordinate
        {
            get { return previousXCoordinate; }
            set { previousXCoordinate = value; }
        }

        public int PreviousYCoordinate
        {
            get { return previousYCoordinate; }
            set { previousYCoordinate = value; }
        }

        public int Width
        {
            get { return PreviousXCoordinate - XCoordinate; }
        }

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        #endregion

        private int previousXCoordinate = -1;
        private int previousYCoordinate = -1;
        private int xCoordinate = -1;
        private int yCoordinate = -1;
    }
}