using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SquarespaceAuto.Data
{
    public class DigitalProductRepository : IDigitalProductRepository
    {
        public DigitalProductRepository(string productDataFilePath)
        {
            List<string> fileLines = File.ReadAllLines(productDataFilePath).ToList();

            Products = new List<DigitalProduct>();
 
            foreach (string line in fileLines.Skip(1))
            {
                string[] cols = line.Split(';');
                DigitalProduct digitalProduct = new DigitalProduct()
                {
                    Title = cols[0],
                    Description = cols[1],
                    Tags = cols[2].Split(','),
                    Categories = cols[3].Split(','),
                    MainImageProductFilePath = cols[4],
                    WatermarkedImageFilePath = cols[5],
                    ThumbnailImageFilePath = cols[6],
                    Price = float.Parse(cols[7]),
                    SalePrice = float.Parse(cols[8])
                };
                Products.Add(digitalProduct);

            }
        }
        public List<DigitalProduct> Products { get; set; }
    }
}
