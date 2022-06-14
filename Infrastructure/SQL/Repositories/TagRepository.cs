using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Core.Entities;
using Core.Models;
using Dapper;
using Infrastructure.SQL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.SQL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;
        private readonly string _connectionString;


        public TagRepository(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _context = databaseContext;
            _connectionString = configuration["connection"];
        }
        public Task<bool> Create(Tag tag)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("CreateTag",
                new { @Word = tag.Word },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public Task<Tag> Get(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<Tag>("FindTag",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());
        }
        public Task<bool> Edit(EditTweetDTO model)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("EditTag",
                new { @Word = model.Word, @id=model.Id },
                commandType: CommandType.StoredProcedure);
            return Task.FromResult(result.First());

        }
        public Task<bool> Delete(int id)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            var result = sqlConnection.Query<bool>("DeleteTag",
                new { @Id=id},
                commandType: CommandType.StoredProcedure);
            return  Task.FromResult(true);
            
        }

    }
}
