using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using VeiculosApp.Models;

namespace VeiculosApp.Testes
{
    [TestClass]
    public class TestMongo
    {
        [TestMethod]
        public void MongoConnectionTest()
        {
            try
            {
                var mongoClient = new MongoClient();

                Console.Write($"Host: {mongoClient.Settings.Server.Host} Port: {mongoClient.Settings.Server.Port}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void ListaMongoDatabasesTest()
        {
            try
            {
                var mongoClient = new MongoClient();

                var dataBases = mongoClient.ListDatabases();

                var listDataBases = dataBases.ToEnumerable();
                foreach (var item in listDataBases)
                {
                    foreach (var dataBase in item.Elements)
                    {
                        Console.WriteLine($"Name: {dataBase.Name} Value: {dataBase.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void RetornaDatabaseMongoTest()
        {
            try
            {
                var mongoClient = new MongoClient();
                var dataBase = mongoClient.GetDatabase("teste");

                Console.Write($"DataBase: {dataBase.DatabaseNamespace.DatabaseName}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void CriaObjetoMongoTest()
        {
            try
            {
                var mongoClient = new MongoClient();
                var dataBase = mongoClient.GetDatabase("teste");

                var collection = dataBase.GetCollection<Veiculo>("veiculos");

                var veiculo = new Veiculo
                {
                    Cpf = "99999999999",
                    NomeProprietario = "Teste Silva",
                    Placa = "ABC-1234",
                    Renavan = "1234567890",
                    Bloqueado = false
                };

                collection.InsertOne(veiculo);

                Console.Write($"Veículo criado: {veiculo.Id}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void RetornaObjetoMongoTest()
        {
            try
            {
                var mongoClient = new MongoClient();
                var dataBase = mongoClient.GetDatabase("teste");

                var collection = dataBase.GetCollection<Veiculo>("veiculos");

                var query = collection.AsQueryable();

                var veiculo = query.SingleOrDefault(q => q.Id == new ObjectId("5919bdda22df171be0c5c1ca"));

                Assert.IsNotNull(veiculo);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void AtualizaObjetoMongoTest()
        {
            try
            {
                var mongoClient = new MongoClient();
                var dataBase = mongoClient.GetDatabase("teste");

                var collection = dataBase.GetCollection<Veiculo>("veiculos");

                var builder = Builders<Veiculo>.Update;
                var update = builder.Set(q => q.NomeProprietario, "Teste João");

                collection.UpdateOne(q => q.Id == new ObjectId("5919bdda22df171be0c5c1ca"), update);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void RemoveObjetoMongoTest()
        {
            try
            {
                var mongoClient = new MongoClient();
                var dataBase = mongoClient.GetDatabase("teste");

                var collection = dataBase.GetCollection<Veiculo>("veiculos");

                collection.DeleteOne(q => q.Id == new ObjectId("5919bdda22df171be0c5c1ca"));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
