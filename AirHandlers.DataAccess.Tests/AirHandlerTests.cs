using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Entities;
using AirHandlers.DataAccess.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AirHandlers.DataAccess.Tests.Repositories
{
    [TestClass]
    public class AirHandlerTests
    {
        private AirHandlerRepository _repository;
        private SqliteConnection _connection;

        [TestInitialize]
        public void Setup()
        {
            // Crear una conexión SQLite en memoria
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            _repository = new AirHandlerRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public void Can_Add_AirHandler()
        {
            // Arrange
            var airHandler = new AirHandlerEntity("AH-001", true, DateTime.Now.AddDays(30), 22.5, 45.0);

            // Act
            _repository.AddAirHandler(airHandler);

            // Assert
            var loadedAirHandler = _repository.GetAirHandlerById<AirHandlerEntity>(airHandler.Id);
            Assert.IsNotNull(loadedAirHandler);
        }

        [TestMethod]
        public void Cannot_Get_AirHandler_By_Invalid_Id()
        {
            // Act
            var loadedAirHandler = _repository.GetAirHandlerById<AirHandlerEntity>(Guid.NewGuid());

            // Assert
            Assert.IsNull(loadedAirHandler);
        }

        [TestMethod]
        public void Can_Get_All_AirHandlers()
        {
            // Arrange
            var airHandler1 = new AirHandlerEntity("AH-002", true, DateTime.Now.AddDays(30), 21.0, 50.0);
            var airHandler2 = new AirHandlerEntity("AH-003", false, DateTime.Now.AddDays(15), 24.0, 40.0);

            _repository.AddAirHandler(airHandler1);
            _repository.AddAirHandler(airHandler2);

            // Act
            var allAirHandlers = _repository.GetAllAirHandler<AirHandlerEntity>();

            // Assert
            Assert.AreEqual(2, allAirHandlers.Count());
        }

        [TestMethod]
        public void Can_Update_AirHandler()
        {
            // Arrange
            var airHandler = new AirHandlerEntity("AH-004", true, DateTime.Now.AddDays(30), 20.0, 35.0);
            _repository.AddAirHandler(airHandler);

            airHandler.IdentifierCode = "AH-Updated";

            // Act
            _repository.UpdateAirHandler(airHandler);

            // Assert
            var updatedAirhandler = _repository.GetAirHandlerById<AirHandlerEntity>(airHandler.Id);
            Assert.AreEqual("AH-Updated", updatedAirhandler.IdentifierCode);
        }

        [TestMethod]
        public void Can_Delete_Airhandler()
        {
            // Arrange 
            var airhandler = new AirHandlerEntity("AH-005", true, DateTime.Now.AddDays(30), 22.0, 45.0);
            _repository.AddAirHandler(airhandler);

            // Act 
            _repository.DeleteAirHandler(airhandler.Id);

            // Assert 
            var loadedAirhandler = _repository.GetAirHandlerById<AirHandlerEntity>(airhandler.Id);
            Assert.IsNull(loadedAirhandler);
        }
    }
}