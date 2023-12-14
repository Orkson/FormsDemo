using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WinForms;
using WinForms.Data;


namespace WinFormsTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            var ContextMock = new Mock<INewContext>();
            var salesOrderHeaders = new List<SalesOrderHeader>
        {
            new SalesOrderHeader { SalesPersonID = 1, TotalDue = 1, CurrencyRateID = 1, OrderDate = new DateTime(2023, 1, 1) },
         
            
        }.AsQueryable();

            var salesOrderHeadersMockSet = new Mock<DbSet<SalesOrderHeader>>();
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Provider).Returns(salesOrderHeaders.Provider);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Expression).Returns(salesOrderHeaders.Expression);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.ElementType).Returns(salesOrderHeaders.ElementType);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.GetEnumerator()).Returns(() => salesOrderHeaders.GetEnumerator());

            ContextMock.Setup(x => x.SalesOrderHeaders).Returns(salesOrderHeadersMockSet.Object);


            var salesAnalyzer = new Form1(ContextMock.Object);
            var result = salesAnalyzer.ViewTopSalesPersons();
            //var form = new Form1();
            //form.ViewTopSalesPersons();

            // Assert
            Assert.IsNotNull(result, "Wyniki nie mog¹ buæ null");
            Assert.AreEqual(4, result.Count, "s¹ równe");


            //Assert.IsNotNull(result.Data, "Data source should not be null");

            //Assert.IsNotNull(dataSource, "");
            //Assert.IsTrue(dataSource.Count > 3, "");


        }
    }
}