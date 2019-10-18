using System;

namespace SquarespaceAuto
{
    public class DigitalProduct
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string[] Categories { get; set; }

        public string MainImageProductFilePath { get; set; }
        public string WatermarkedImageFilePath { get; set; }
        public string ThumbnailImageFilePath { get; set; }

        public float Price { get; set; }

        public float SalePrice { get; set; }
    }
}
