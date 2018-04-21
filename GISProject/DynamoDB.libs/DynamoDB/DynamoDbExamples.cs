using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.libs.DynamoDB
{
    public class DynamoDbExamples : IDynamoDBExamples
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tableName = "tempDynamoDbTable";

        public DynamoDbExamples(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public void CreateDynamoDbTable()
        {
            try
            {
                CreateTempTable();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private void CreateTempTable()
        {
            Debug.WriteLine("creating table");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id ",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH" // Partition Key
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = "Range" // Sort Key
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = tableName
            };
            var response = _dynamoDbClient.CreateTableAsync(request);

            WaitUntilTableReady(tableName);
        }

        private void WaitUntilTableReady(string tableName)
        {
            string status = null;
            do
            {
                Thread.Sleep(5000);
                try
                {
                    var res = _dynamoDbClient.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    status = res.Result.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                    
                }
            } while (status != "ACTIVE");
            Debug.WriteLine("Table Created");
        }
    }
}
