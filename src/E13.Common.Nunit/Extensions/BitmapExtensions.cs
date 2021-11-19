using E13.Common.Nunit.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace System.Drawing
{
    public static class BitmapExtensions
    {
        [SupportedOSPlatform("windows")]
        public static bool IsIdentical(this Bitmap original, Bitmap compare)
        {
            var result = original.Compare(compare);

            // if result == null then the files are identical
            return result == null;
        }

        [SupportedOSPlatform("windows")]
        public static Bitmap Compare(this Bitmap original, Bitmap compare )
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));

            if (compare == null)
                throw new ArgumentNullException(nameof(compare));

            int xCoordinate = 0;
            int yCoordinate = 0;

            var newData =
                compare.LockBits(
                    new Rectangle(
                        xCoordinate,
                        yCoordinate,
                        compare.Width,
                        compare.Height ),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb );
            var originalData =
                original.LockBits(
                    new Rectangle(
                        xCoordinate,
                        yCoordinate,
                        original.Width,
                        original.Height ),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

            var poiCollection = new PointOfInterestCollection();

            unsafe
            {
                byte* newPointer = ( byte* )newData.Scan0;
                byte* originalPointer = ( byte* )originalData.Scan0;

                for ( yCoordinate = 0;
                        yCoordinate < originalData.Height;
                        ++yCoordinate )
                {
                    for ( xCoordinate = 0;
                            xCoordinate < originalData.Width;
                            ++xCoordinate )
                    {
                        int currentOriginalPixel = (
                                originalPointer[0] +
                                originalPointer[1] +
                                originalPointer[2] );
                        int currentNewPixel = (
                                newPointer[0] +
                                newPointer[1] +
                                newPointer[2] );
                        if ( currentNewPixel > currentOriginalPixel + 140
                            || currentNewPixel < currentOriginalPixel - 140 )
                        {
                            poiCollection.ProcessCoordinates(
                                xCoordinate, yCoordinate );
                        }

                        newPointer += 3;
                        originalPointer += 3;
                    }

                    newPointer +=
                        newData.Stride - ( newData.Width * 3 );
                    originalPointer +=
                        originalData.Stride - ( originalData.Width * 3 );
                }
            }

            original.UnlockBits( originalData );
            compare.UnlockBits( newData );

            if (poiCollection.Count == 0)
                return null;

            using ( var graphics = Graphics.FromImage(compare) )
            {
                using var pen = new Pen(Color.Red, 1);
                graphics.DrawRectangles(pen, poiCollection.GetRectangles());
            }

            return compare;
        }
    }
}
