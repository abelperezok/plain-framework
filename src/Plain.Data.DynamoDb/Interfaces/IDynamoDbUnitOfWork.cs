using Amazon.DynamoDBv2;
using Plain.Infrastructure.Interfaces.Data;
using System.Data;

namespace Plain.Data.DynamoDb.Interfaces
{
    public interface IDynamoDbUnitOfWork : IUnitOfWork
    {
        IAmazonDynamoDB AmazonDynamoDB { get; }
    }
}
