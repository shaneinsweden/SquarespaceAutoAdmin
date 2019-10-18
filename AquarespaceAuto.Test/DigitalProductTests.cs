using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquarespaceAuto;
using System.Collections.Generic;
using System.Linq;

namespace AquarespaceAuto.Test
{
    [TestClass]
    public class DigitalProductTests
    {
        [TestMethod]
        public void DigitalProduct_ReadProductInfo_Success()
        {
            //arrange
            SquarespaceAuto.Data.DigitalProductRepository productRepository = new SquarespaceAuto.Data.DigitalProductRepository("testdata.txt");


            //act
            List<DigitalProduct> productList = productRepository.Products;

            //assert
            Assert.AreEqual(3, productList.Count());
        }
    } 
}
