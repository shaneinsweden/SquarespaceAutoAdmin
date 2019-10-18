using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquarespaceAuto.Test
{
    [TestClass]
    public class SquarespaceManagerTests
    {
        [TestMethod]
        public void SquarespaceManger_LoginSquarespace_Success()
        {
            //arrange
            SquarespaceManager manager = new SquarespaceManager();
            manager.InitDriver();

            //Act 
            manager.LoginSquarespace();

            //assert
            Assert.IsTrue(manager.WebSiteButtonIsVisible());
        }

        [TestMethod]
        public void SquarespaceManger_LoginSquarespaceAndLoadInventory_Success()
        {
            //arrange
            SquarespaceManager manager = new SquarespaceManager();
            manager.InitDriver();
            manager.LoginSquarespace();

            //Act 
            manager.LoadInventory();


            //assert
            Assert.IsTrue(manager.AddProductButtonIsVisible());
        }

        [TestMethod]
        public void SquarespaceManger_LoginLoadInventoryAddProducts_Success()
        {
            //arrange
            SquarespaceManager manager = new SquarespaceManager();
            manager.InitDriver();
            manager.LoginSquarespace();
            manager.LoadInventory();

            //Act 
            manager.LoadDigitalProducts("testdata.txt");


        }
    }
}
