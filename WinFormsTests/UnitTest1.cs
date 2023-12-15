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
            //dane testowe
            new SalesOrderHeader { SalesPersonID = 277, TotalDue = 123, CurrencyRateID = 4, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 279, TotalDue = 41, CurrencyRateID = 4, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 279, TotalDue = 4623, CurrencyRateID = null, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 276, TotalDue = 135, CurrencyRateID = 4, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 276, TotalDue = 131, CurrencyRateID = 4, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 282, TotalDue = 425, CurrencyRateID = null, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 276, TotalDue = 42, CurrencyRateID = 52, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 275, TotalDue = 5234, CurrencyRateID = null, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 275, TotalDue = 315, CurrencyRateID = 52, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 276, TotalDue = 537, CurrencyRateID = 52, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 277, TotalDue = 12, CurrencyRateID = null, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 234, TotalDue = 246, CurrencyRateID = 52, OrderDate = new DateTime(2023, 1, 1) },
            new SalesOrderHeader { SalesPersonID = 222, TotalDue = 537, CurrencyRateID = 60, OrderDate = new DateTime(2023, 1, 1) },

        }.AsQueryable();

            var salesOrderHeadersMockSet = new Mock<DbSet<SalesOrderHeader>>();
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Provider).Returns(salesOrderHeaders.Provider);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Expression).Returns(salesOrderHeaders.Expression);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.ElementType).Returns(salesOrderHeaders.ElementType);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.GetEnumerator()).Returns(() => salesOrderHeaders.GetEnumerator());

            ContextMock.Setup(x => x.SalesOrderHeaders).Returns(salesOrderHeadersMockSet.Object);


            var ViewTopSales = new Form1(ContextMock.Object);


            var results = ViewTopSales.ViewTopSalesPersons(2023, 4); //wyniki z 2023 roku, 4 wiersze
            

            // Assert
            
            Assert.IsNotNull(results, "Wyniki nie mog¹ buæ puste");
            Assert.AreEqual(4, results.Count, "Musz¹ byæ zwrócone 4 wyniki");

            int lastTotalOrders = int.MaxValue;

            foreach (var item in results)
            {
                //TotalOrders musi byæ posortowane od najwiêkszej iloœci
                var totalOrders = (int)item.GetType().GetProperty("TotalOrders").GetValue(item);
                Assert.IsTrue(totalOrders <= lastTotalOrders, "TotalOrders nie jest posortowane");

                //TotalDue musi byæ wiêksze od zera
                var totalDue = (decimal)item.GetType().GetProperty("TotalDue").GetValue(item);
                Assert.IsTrue(totalDue > 0, "TotalDue > 0");
            }



        }
    }
}