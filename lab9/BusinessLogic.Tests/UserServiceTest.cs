using BusinessLogic.Services;
using Domain.Interfaces;

using Moq;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;
        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User)
                .Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }

        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] {new User() { UsersId = 1, Name = "", Role = 0, IsDeleted = false} },
                new object[] {new User() { UsersId = 2, Name = "Test", Role = 0, IsDeleted = false} },
                new object[] {new User() { UsersId = 3, Name = "Test", Role = 1, IsDeleted = false } },
            };
        }

        [Fact]
        public async Task CreateAsync_NullUser_ShouldThrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(User user)
        {
            var newUser = user;
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newUser));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async Task CreateAsyncNewUser()
        {
            var newUser = new User()
            {
                UsersId = 55,
                Name = "Testlogin",
                Role = 34,
                IsDeleted = false
            };

            await service.Create(newUser);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);

        }
    }
}
