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
    public class RoomTests
    {
        private RoomRepository _roomRepository;
        private AirHandlerRepository _airHandlerRepository;
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

            _roomRepository = new RoomRepository(context);
            _airHandlerRepository = new AirHandlerRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public void Can_Add_Room()
        {
            // Arrange
            var airHandler = new AirHandlerEntity(
                identifierCode: "AH-001",
                isOperating: true,
                filterChangeDate: DateTime.Now.AddDays(30),
                referenceTemperature: 22.5,
                referenceHumidity: 45.0
            );

            // Agregar el Air Handler primero para cumplir con la clave foránea
            _airHandlerRepository.AddAirHandler(airHandler);

            var room = new Room(number: 101, volume: 50.0) { AssociatedHandlerId = airHandler.Id };

            // Act
            _roomRepository.CreateRoom(room);

            // Assert
            var loadedRoom = _roomRepository.GetRoomById<Room>(room.Id);
            Assert.IsNotNull(loadedRoom);
        }

        [TestMethod]
        public void Cannot_Get_Room_By_Invalid_Id()
        {
            // Act 
            var loadedRoom = _roomRepository.GetRoomById<Room>(Guid.NewGuid());

            // Assert
            Assert.IsNull(loadedRoom);
        }

        [TestMethod]
        public void Can_Get_All_Rooms()
        {
            // Arrange 
            var airHandler = new AirHandlerEntity(
                identifierCode: "AH-002",
                isOperating: true,
                filterChangeDate: DateTime.Now.AddDays(30),
                referenceTemperature: 22.5,
                referenceHumidity: 45.0
            );

            // Asegurarse de agregar el Air Handler antes de crear las habitaciones
            _airHandlerRepository.AddAirHandler(airHandler);

            var room1 = new Room(number: 101, volume: 50.0) { AssociatedHandlerId = airHandler.Id };
            var room2 = new Room(number: 102, volume: 75.0) { AssociatedHandlerId = airHandler.Id };

            _roomRepository.CreateRoom(room1);
            _roomRepository.CreateRoom(room2);

            // Act 
            var allRooms = _roomRepository.GetAllRooms<Room>();

            // Assert 
            Assert.AreEqual(2, allRooms.Count());
        }

        [TestMethod]
        public void Can_Update_Room()
        {
            // Arrange 
            var airHandler = new AirHandlerEntity("AH-003", true, DateTime.Now.AddDays(30), 22.5, 45.0);
            _airHandlerRepository.AddAirHandler(airHandler);

            var room = new Room(number: 101, volume: 50.0) { AssociatedHandlerId = airHandler.Id };
            _roomRepository.CreateRoom(room);

            room.Volume = 60.0;

            // Act 
            _roomRepository.UpdateRoom(room);

            // Assert 
            var updatedRoom = _roomRepository.GetRoomById<Room>(room.Id);
            Assert.AreEqual(60.0, updatedRoom.Volume);
        }

        [TestMethod]
        public void Can_Delete_Room()
        {
            // Arrange 
            var airHandler = new AirHandlerEntity("AH-004", true, DateTime.Now.AddDays(30), 22.5, 45.0);
            _airHandlerRepository.AddAirHandler(airHandler);

            var room = new Room(number: 101, volume: 50.0) { AssociatedHandlerId = airHandler.Id };
            _roomRepository.CreateRoom(room);

            // Act 
            _roomRepository.DeleteRoom(room);

            // Assert 
            var loadedRoom = _roomRepository.GetRoomById<Room>(room.Id);
            Assert.IsNull(loadedRoom);
        }
    }
}