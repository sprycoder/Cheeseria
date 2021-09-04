using Cheeseria.API.Controllers;
using Cheeseria.API.Data;
using Cheeseria.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cheeseria.API.UnitTest
{
    public class CheeseControllerTest
    {
        private readonly DbContextOptions<CheeseContext> _dbOptions;

        public CheeseControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<CheeseContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new CheeseContext(_dbOptions))
            {
                if (dbContext.Cheeses.Count() == 0)
                {
                    dbContext.AddRange(GetFakeCheeses());
                    dbContext.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task Get_cheese_items_success()
        {
            // Arrange
            var expectedCount = 3;
            var firstCheeseName = "Gouda";

            var cheeseContext = new CheeseContext(_dbOptions);

            // Act
            var controller = new CheeseController(cheeseContext);
            var actionResult = await controller.GetAsync();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Cheese>>>(actionResult);
            var cheeses = (Assert.IsAssignableFrom<IEnumerable<Cheese>>(actionResult.Value)).ToList();
            Assert.Equal(expectedCount, cheeses.Count);
            Assert.Equal(firstCheeseName, cheeses[0].Name);
        }

        [Fact]
        public async Task Get_cheese_by_id_bad_request()
        {
            // Arrange
            var cheeseContext = new CheeseContext(_dbOptions);

            // Act
            var controller = new CheeseController(cheeseContext);
            var actionResult = await controller.GetByIdAsync(-1);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_cheese_by_id_not_found()
        {
            // Arrange
            var cheeseContext = new CheeseContext(_dbOptions);

            // Act
            var controller = new CheeseController(cheeseContext);
            var actionResult = await controller.GetByIdAsync(int.MaxValue);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_cheese_by_id_success()
        {
            // Arrange
            var expectedCheeseName = "Gouda";
            var cheeseContext = new CheeseContext(_dbOptions);

            // Act
            var controller = new CheeseController(cheeseContext);
            var actionResult = await controller.GetByIdAsync(1);

            // Assert
            Assert.IsType<ActionResult<Cheese>>(actionResult);
            Assert.Equal(expectedCheeseName, actionResult.Value.Name);
        }

        private static List<Cheese> GetFakeCheeses()
        {
            return new List<Cheese>()
            {
                new Cheese()
                {
                    Id = 1,
                    Name ="Gouda",
                    Description = "Gouda, or \"How-da\" as the locals say, is a Dutch cheese named after the city of Gouda in the Netherlands. If truth be told, it is one of the most popular cheeses in the world, accounting for 50 to 60% of the world's cheese consumption.",
                    Colour = "Yellow",
                    Price= 20.4m,
                    ImageUrl=  "https://cheese.com/media/img/cheese/GOUDA_SqYJjRh.jpg"
                },
                new Cheese() {
                    Id = 2,
                    Name="Brie",
                    Description="Brie is the best known French cheese and has a nickname \"The Queen of Cheeses\". Brie is a soft cheese named after the French region Brie, where it was originally created. Several hundred years ago, Brie was one of the tributes which had to be paid to the French kings.",
                    Colour="Cream",
                    Price=28.3m,
                    ImageUrl="https://cheese.com/media/img/cheese/Brie_PDCo3RG.jpg"
                },
               new Cheese() {
                    Id=3,
                    Name="American Cheese",
                    Description="American cheese is processed cheese made from a blend of milk, milk fats and solids, other fats and whey protein concentrate. At first, it was made from a mixture of cheeses, more often than not Colby and Cheddar.",
                    Colour="Yellow",
                    Price=18.6m,
                    ImageUrl="https://cheese.com/media/img/cheese/10-American-Cheese-shutterstock_1610208106.jpg"
                }
            };
        }
    }
}
