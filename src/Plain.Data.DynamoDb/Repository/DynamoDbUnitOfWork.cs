using Amazon.DynamoDBv2;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon;
using Plain.Data.DynamoDb.Interfaces;

namespace Plain.Data.DynamoDb.Repository
{
    public class DynamoDbUnitOfWork : IDynamoDbUnitOfWork
    {
        private IAmazonDynamoDB _dynamoDbClient;

        public DynamoDbUnitOfWork(string profileName)
        {
            CredentialProfile basicProfile;
            AWSCredentials awsCredentials;
            var sharedFile = new SharedCredentialsFile();
            if (sharedFile.TryGetProfile(profileName, out basicProfile) && AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, sharedFile, out awsCredentials))
            {
                _dynamoDbClient = new AmazonDynamoDBClient(awsCredentials, basicProfile.Region);
            }

            // default to Ireland with [default] profile
            _dynamoDbClient = new AmazonDynamoDBClient(RegionEndpoint.EUWest1);
        }

        public IAmazonDynamoDB AmazonDynamoDB { get => _dynamoDbClient; }

        public void Commit()
        {

        }

        public void Dispose()
        {
            _dynamoDbClient.Dispose();
        }

        public void Rollback()
        {

        }
    }
}
