namespace AiFleet.UnitTests.Fixtures;

using System;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Data.Sqlite;
using AiFleet.Core.Models.Entities;

public class TestDatabaseFixture : IDisposable
{
    private readonly SqliteConnection _sqliteConnection;

    public TestDatabaseFixture()
    {
        // Keep in-memory connection open to preserve the SQLite database
        _sqliteConnection = new SqliteConnection("Data Source=:memory:");
        _sqliteConnection.Open();
    }

    public DataConnection CreateConnection()
    {
        string connectionString = $"Data Source=InMemorySample_{Guid.NewGuid()};Mode=Memory;Cache=Shared";
        
        var sqliteConnection = new SqliteConnection(connectionString);
        sqliteConnection.Open();

        var db = LinqToDB.DataProvider.SQLite.SQLiteTools.CreateDataConnection(
            sqliteConnection,
            LinqToDB.DataProvider.SQLite.SQLiteProvider.Microsoft
        );

        db.CreateTable<AgentEntity>(tableOptions: TableOptions.CreateIfNotExists);
        return db;
    }

    public void Dispose()
    {
        _sqliteConnection.Close();
        _sqliteConnection.Dispose();
    }
}