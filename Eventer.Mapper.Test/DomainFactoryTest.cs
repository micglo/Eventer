using System;
using Eventer.Domain.Entity.RefreshToken;
using Eventer.Mapper.ModelFacotry.RefreshToken;
using Eventer.Model.Dto.RefreshToken;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Eventer.Mapper.Test
{
    [TestFixture]
    public class DomainFactoryTest
    {
        [Test]
        public void Is_Correct_Client_Mapped_Object()
        {
            //arrange
            var client = new Client
            {
                Active = true,
                RefreshTokenLifeTime = 1000,
                ApplicationType = 0,
                ClientSecret = "1234567890",
                Id = "1234",
                Username = "username"
            };

            var clientDto = new ClientDto
            {
                Active = true,
                RefreshTokenLifeTime = 1000,
                ApplicationType = 0,
                ClientSecret = "1234567890",
                Id = "1234",
                Username = "username"
            };
            var domainFactory = new ClientFactory();

            //act
            var target = domainFactory.GetModel<Client>(clientDto);

            //assert
            Assert.IsInstanceOf<Client>(target);
            Assert.AreEqual(client.RefreshTokenLifeTime, target.RefreshTokenLifeTime);
        }

        [Test]
        public void Is_Correct_RefreshToken_Mapped_Object()
        {
            //arrange
            var refreshToken = new RefreshToken
            {
                Id = "1234",
                ProtectedTicket = "1234",
                ClientId = "1234",
                ExpiresUtc = DateTime.Today.AddDays(1),
                IssuedUtc = DateTime.Today,
                Subject = "michal"
            };

            var refreshTokenDto = new RefreshTokenDto
            {
                Id = "1234",
                ProtectedTicket = "1234",
                ClientId = "1234",
                ExpiresUtc = DateTime.Today.AddDays(1),
                IssuedUtc = DateTime.Today,
                Subject = "michal"
            };
            var domainFactory = new RefreshTokenFactory();

            //act
            var target = domainFactory.GetModel<RefreshToken>(refreshTokenDto);

            //assert
            Assert.IsInstanceOf<RefreshToken>(target);
            Assert.AreEqual(refreshToken.ProtectedTicket, target.ProtectedTicket);
        }
    }
}
