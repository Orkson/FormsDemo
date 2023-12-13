using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WinForms;

namespace WinFormsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ContextMock = new Mock<SalesOrderHeader>();
            var salesOrderHeaders = new List<SalesOrderHeader>
        {
            //dane testowe
        }.AsQueryable();

            var salesOrderHeadersMockSet = new Mock<DbSet<SalesOrderHeader>>();
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Provider).Returns(salesOrderHeaders.Provider);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.Expression).Returns(salesOrderHeaders.Expression);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.ElementType).Returns(salesOrderHeaders.ElementType);
            salesOrderHeadersMockSet.As<IQueryable<SalesOrderHeader>>().Setup(m => m.GetEnumerator()).Returns(() => salesOrderHeaders.GetEnumerator());

            



        }
    }
}