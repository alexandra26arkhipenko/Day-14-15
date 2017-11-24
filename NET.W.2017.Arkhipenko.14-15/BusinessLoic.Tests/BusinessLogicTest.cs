using System;
using BusinessLogic.Interfaces.Entities;
using BusinessLogic.Interfaces.Interfaces;
using BusinessLogic.ServiceImplementation;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Bll.Tests
{
    [TestClass]
    public class BusinessLogicTest
    {
        [TestMethod]
        public void ServiceTest()
        {
            string accountId = "qazwsx";
            var repositoryMock = new Mock<IRepository>();

            var accountGenerateIdNumberMock = new Mock<IAccountGenerateIdNumber>(MockBehavior.Strict);
            accountGenerateIdNumberMock.Setup(generator => generator.GenerateId()).Returns(accountId);

            var accountService = new AccountService(repositoryMock.Object, accountGenerateIdNumberMock.Object);
            var accountTest = accountService.CreateAccount(AccountType.Base, "Alex", "Losev", 500, accountGenerateIdNumberMock.Object);
            
            accountService.AddMoney(accountTest, 100);
            accountService.DivMoney(accountTest, 10);
            accountService.CloseAccout(accountTest);

           
            accountGenerateIdNumberMock.Verify(
                service => service.GenerateId(), Times.AtLeastOnce);
        }
    }
}