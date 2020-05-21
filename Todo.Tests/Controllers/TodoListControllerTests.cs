using Microsoft.AspNetCore.Identity;
using Xunit;
using Todo.Controllers;
using Moq;
using Todo.Models.TodoLists;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Todo.Tests
{
    public class TodoListControllerTests 
    {
        [Fact]
        public void GravatarDataReturnsGravatarViewModel()
        {
            // Arrange
            var controller = new TodoListController(new Mock<IApplicationDbContext>().Object, new Mock<IUserStore<IdentityUser>>().Object);
            var expectedModel = new TodoListGravatarViewmodel("a@b.com");

            // Act
            var view = (PartialViewResult)controller.GravatarData(expectedModel.Email);
            var actualModel = (TodoListGravatarViewmodel)view.ViewData.Model;

            // Assert
            actualModel.Should().BeEquivalentTo(expectedModel);
        }
    }
}
