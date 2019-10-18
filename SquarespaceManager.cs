using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SquarespaceAuto;
using SquarespaceAuto.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AquarespaceAuto.Test
{
    public class SquarespaceManager
    {
        IWebDriver _driver;

        internal void InitDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            _driver = new ChromeDriver(@"E:\projects\SquareSpaceAutomation\source\SquarespaceAuto\AquarespaceAuto.Test\bin\Debug\netcoreapp2.2", options);
            _driver.Manage().Window.Maximize();
        }

        internal bool WebSiteButtonIsVisible()
        {
            IWebElement element = _driver.FindElement(By.ClassName("Screenshot__Image-wm4sxs-1"));

            if (element != null)
                return true;

            return false;
        }

        internal void LoadInventory()
        {
            WaitSeconds(2);
            _driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div/div/div/div/div[2]/div[2]/a[2]/div/span")).Click();
            WaitSeconds(2);
        }

        internal bool AddProductButtonIsVisible()
        {

            IWebElement element = _driver.FindElement(By.ClassName("Button-button-3j7HJ"));

            if (element != null)
                return true;

            return false;
        }

        internal void LoadDigitalProducts(string digitalProductsFilename)
        {
            DigitalProductRepository digitalProductRepository = new DigitalProductRepository(digitalProductsFilename);
            foreach( var digitalProduct in digitalProductRepository.Products)
            {
                LoadDigitalProduct(digitalProduct);
            }

        }

        private void LoadDigitalProduct(DigitalProduct digitalProduct)
        {
            WaitSeconds(8);

            //press the add product button
            //<button data-test="add-product-button" title="Add" class="Button-button-3j7HJ undefined" 
            //ClickWebElementBy("Button-button-3j7HJ", "Add");
            //<button data-test="add-product-button" title="Add" class="Button-button-3j7HJ undefined" type="button">
            //<img class="Icon-icon-2GL_i Icon-fill-jlkAn Button-icon-31dr8" src="data:image/svg+xml,%3Csvg width='22' height='22' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M11.5 10h7.248a.75.75 0 110 1.5H11.5v7.248a.75.75 0 11-1.5 0V11.5H2.752a.75.75 0 110-1.5H10V2.752a.75.75 0 111.5 0V10z' fill='%233E3E3E' fill-rule='evenodd'/%3E%3C/svg%3E"></button>
            //ClickWebElementByXPath("/html/body/div[1]/div/div[4]/div/div/div[2]/div/div[2]/div[2]/button");
            _driver.FindElement(By.CssSelector("button[title='Add'][type='button']")).Click();

            WaitSeconds(2);

            //choose our shop
            //<span class="collection-title">SquarespaceAutomation</span>
            ClickWebElementBy("collection-title", "SquarespaceAutomation");
            WaitSeconds(2);

            //choose digital prodiuct
            //<div class="option-title">Digital</div>
            ClickWebElementBy("option-title", "Digital");
            WaitSeconds(2);

            //find title
            _driver.FindElement(By.Name("title")).SendKeys(digitalProduct.Title);
            WaitSeconds(2);
            //<input type="text" name="title" data-test="text"

            //find description
            //<div class="rte TextEditor-editor-1W6me ProseMirror" 
            _driver.FindElement(By.ClassName("rte")).SendKeys(digitalProduct.Description);
            WaitSeconds(2);

            //find tags
            //<div class="field-lhs title">Tags</div>
            ClickWebElementBy("field-lhs", "TAGS");
            WaitSeconds(2);

            //add csv tags
            //<input class="field-input" placeholder="Tags, comma separated"
            //string tagsCsvString = string.Join(",", digitalProduct.Tags);
            foreach (string tag in digitalProduct.Tags)
            {
                _driver.FindElement(By.ClassName("field-input")).SendKeys(tag);
                _driver.FindElement(By.ClassName("field-input")).SendKeys(Keys.Return);
                WaitSeconds(2);
            }

            WaitSeconds(2);
            //click on price tab
            //<div class="TabbedHeader-tab-2kGQy" data-test="tab-1" style="width: 126.25px;">Pricing &amp; Upload</div>
            ClickWebElementBy("TabbedHeader-tab-2kGQy", "Pricing & Upload");
            WaitSeconds(2);
            //add price
            //<input type="text" name="priceCents" data-test="text" tabindex="2" id="yui_3_17_2_1_1567878738747_8885">
            _driver.FindElement(By.Name("priceCents")).SendKeys(digitalProduct.Price.ToString());
            WaitSeconds(3);
            //sale price
            //<input type="text" name="salePriceCents" data-test="text" tabindex="3" id="yui_3_17_2_1_1567878738747_8980">
            _driver.FindElement(By.Name("salePriceCents")).Click();
            _driver.FindElement(By.Name("salePriceCents")).SendKeys(digitalProduct.SalePrice.ToString());
            WaitSeconds(2);

            //3 image file inputs
            //<input type="file" style="visibility:hidden; width:0px; height: 0px;" multiple="" accept="undefined">
            var fileUploadFormInputs = _driver.FindElements(By.CssSelector("input[type='file']"));

            fileUploadFormInputs[0].SendKeys(digitalProduct.WatermarkedImageFilePath);
            fileUploadFormInputs[1].SendKeys(digitalProduct.MainImageProductFilePath);
            fileUploadFormInputs[2].SendKeys(digitalProduct.ThumbnailImageFilePath);

            string imageName = Path.GetFileName(digitalProduct.MainImageProductFilePath);

            //wait until we see filname 
            //<span class="file-meta file-meta-name">myproduct.png</span>
            WaitForImageLoaded(imageName);
            WaitSeconds(2);

            //save product
            //<input class="saveAndClose" tabindex="106" type="button" data-test="dialog-saveAndClose" value="Save">
            _driver.FindElement(By.ClassName("saveAndClose")).Click();
            WaitSeconds(2);

        }

        private void WaitForImageLoaded(string imageName)
        {
            WaitForClassAndText("file-meta", imageName, 600);
        }

      
        private void ClickWebElementByXPath(string xPath)
        {
            _driver.FindElement(By.XPath(xPath)).Click();
        }

        private void ClickWebElementBy(string classname, string text)
        {
            IWebElement element = _driver.FindElements(By.ClassName(classname)).First(b => b.Text == text);

            if (element != null)
                element.Click();
        }

        internal void LoginSquarespace()
        {
            _driver.Url = "http://www.squarespace.com";
            WaitSeconds(2);

            //Login button  class="login-button" 
            _driver.FindElement(By.ClassName("login-button")).Click();
            WaitSeconds(2);

            #region password stuff
            //username class="username name="email"
            _driver.FindElement(By.Name("email")).SendKeys("john.doe@hotmail.com    ");
            WaitSeconds(2);

            //password class="password name="password" 
            _driver.FindElement(By.Name("password")).SendKeys("sommar2019");
            WaitSeconds(2);
            #endregion password stuff

            //login-button class="
            _driver.FindElement(By.ClassName("SpinnerButton-sc-1a5wq6q-0")).Click();
            WaitSeconds(2);

            //wait for authenticator code
            WaitForClass("Screenshot__Image-wm4sxs-1", 30);

            //click on the website
            _driver.FindElement(By.XPath("//*[@id=\"renderTarget\"]/div/div[4]/div[2]/div/div/div/div/div/div[1]/div/img")).Click();
        }

        private void WaitForClassAndText(string classToFind, string text,int maxWaitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(maxWaitInSeconds));

            wait.Until(drv => SafeFindWithText(By.ClassName(classToFind), text));
        }

        private IWebElement SafeFindWithText(By by, string textToFind)
        {
            try
            {
                IWebElement webElement = _driver.FindElements(by).First(elem => elem.Text == textToFind);
                return webElement;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void WaitForClass(string classToFind, int maxWaitInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(maxWaitInSeconds));

            wait.Until(drv => SafeFind(By.ClassName(classToFind)));
        }

        private IWebElement SafeFind(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void WaitSeconds(int secondsToWait)
        {
            Thread.Sleep(secondsToWait * 1000);
        }
    }
}
