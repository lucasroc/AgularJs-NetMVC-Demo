using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeiculosApp.Models;

namespace VeiculosApp.Controllers
{
    public class VeiculoController : ApiController
    {
        [HttpGet]
        public IEnumerable<Veiculo> Get()
        {
            try
            {
                var mongoClient = new MongoClient();
                var db = mongoClient.GetDatabase("veiculosdb");

                var collection = db.GetCollection<Veiculo>("veiculos");
                var query = collection.AsQueryable();

                var listaVeiculos = query.ToEnumerable();
                if (!listaVeiculos.Any())
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return listaVeiculos;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public Veiculo Get(string renavam)
        {
            try
            {
                var mongoClient = new MongoClient();
                var db = mongoClient.GetDatabase("veiculosdb");

                var collection = db.GetCollection<Veiculo>("veiculos");
                var query = collection.AsQueryable();

                var veiculo = query.SingleOrDefault(q => q.Renavan == renavam);
                if(veiculo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return veiculo;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Post(Veiculo veiculo)
        {
            try
            {
                var mongoClient = new MongoClient();
                var db = mongoClient.GetDatabase("veiculosdb");

                var collection = db.GetCollection<Veiculo>("veiculos");

                if (veiculo == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                collection.InsertOne(veiculo);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Put(Veiculo veiculo)
        {
            try
            {
                var mongoClient = new MongoClient();
                var db = mongoClient.GetDatabase("veiculosdb");

                var collection = db.GetCollection<Veiculo>("veiculos");

                if (veiculo == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                var builder = Builders<Veiculo>.Update;
                var update = builder.Set(q => q.Renavan, veiculo.Renavan)
                    .Set(q => q.NomeProprietario, veiculo.NomeProprietario)
                    .Set(q => q.Cpf, veiculo.Cpf)
                    .Set(q => q.Placa, veiculo.Placa)
                    .Set(q => q.Bloqueado, veiculo.Bloqueado);

                collection.UpdateOne(q => q.Renavan == veiculo.Renavan, update);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string renavam)
        {
            try
            {
                var mongoClient = new MongoClient();
                var db = mongoClient.GetDatabase("veiculosdb");

                var collection = db.GetCollection<Veiculo>("veiculos");

                var result = collection.DeleteOne(q => q.Renavan == renavam);
                if (result.DeletedCount == 0)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
