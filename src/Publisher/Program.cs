using Amazon.SQS;
using Amazon.SQS.Model;
using Publisher;
using System.Text.Json;

// We don't need to provide AWS credentials
// as the machine that is running is authenticated already.
// so this client will be able to perform actions in that account.
var sqsClient = new AmazonSQSClient();

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "ricardo0605@gmail.com",
    FullName = "Ricardo Pires",
    DateOfBirth = new DateTime(1900, 1, 1),
    GitHubUsername = "ricardo0605"
};

// The code might be agnostic of things like the
// region or the account number and you might only know
// the queue name. And we can use that to actually get the URL in 
// our code.
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();
