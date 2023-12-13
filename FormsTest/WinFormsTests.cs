using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WinForms.Data;


namespace FormsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dbContextMock = new Mock<SalesOrderHeader>();
            var salesOrderHeaders = new List<SalesOrderHeader>
        {
            // Dodaj przyk³adowe dane testowe
            new SalesOrderHeader { SalesPersonID = 1, OrderDate = new DateTime(2023, 1, 1), CurrencyRateID = 1 },
            new SalesOrderHeader { SalesPersonID = 1, OrderDate = new DateTime(2023, 2, 1), CurrencyRateID = 2 },
            new SalesOrderHeader { SalesPersonID = 2, OrderDate = new DateTime(2023, 1, 1), CurrencyRateID = 1 },
            new SalesOrderHeader { SalesPersonID = 2, OrderDate = new DateTime(2023, 2, 1), CurrencyRateID = null },
            // Dodaj wiêcej przyk³adowych danych
        }.AsQueryable();

            var salesOrderHeadersMockSet = new Mock<DbSet<SalesOrderHeader>>();
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Provider).Returns(salesOrderHeaders.Provider);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Expression).Returns(salesOrderHeaders.Expression);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.ElementType).Returns(salesOrderHeaders.ElementType);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.GetEnumerator()).Returns(() => salesOrderHeaders.GetEnumerator());

            dbContextMock.Setup(x => x.SalesOrderHeaders).Returns(salesOrderHeadersMockSet.Object);

            var form = new MainForm();
            form.GetType().GetProperty("YourDbContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(form, dbContextMock.Object);

            // Act
            form.LoadTopSalespersons();
        }
    }
}