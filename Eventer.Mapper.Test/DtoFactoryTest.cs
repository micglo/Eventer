using System;
using Eventer.Domain.Entity.RefreshToken;
using Eventer.Mapper.ModelFacotry.RefreshToken;
using Eventer.Model.Dto.RefreshToken;
using NUnit.Framework;

namespace Eventer.Mapper.Test
{
    /// <summary>
    /// Summary description for DtoFactoryTest
    /// </summary>
    [TestFixture]
    public class DtoFactoryTest
    {
        [Test]
        public void Is_Correct_ClientDto_Mapped_Object()
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
            var dtoFactory = new ClientFactory();

            //act
            var target = dtoFactory.GetModel<ClientDto>(client);

            //assert
            Assert.IsInstanceOf<ClientDto>(target);
            Assert.AreEqual(clientDto.RefreshTokenLifeTime, target.RefreshTokenLifeTime);
        }

        [Test]
        public void Is_Correct_RefreshTokenDto_Mapped_Object()
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
            var dtoFactory = new RefreshTokenFactory();

            //act
            var target = dtoFactory.GetModel<RefreshTokenDto>(refreshToken);

            //assert
            Assert.IsInstanceOf<RefreshTokenDto>(target);
            Assert.AreEqual(refreshTokenDto.ProtectedTicket, target.ProtectedTicket);
        }
    }
}
