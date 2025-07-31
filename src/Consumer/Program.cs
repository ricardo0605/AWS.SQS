using Amazon.SQS;
using Amazon.SQS.Model;

// We don't need to provide AWS credentials
// as the machine that is running is authenticated already.
// so this client will be able to perform actions in that account.
var sqsClient = new AmazonSQSClient();
var cts = new CancellationTokenSource();

// The code might be agnostic of things like the
// region or the account number and you might only know
// the queue name. And we can use that to actually get the URL in 
// our code.
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = new List<string> { "All" },
    MessageAttributeNames = new List<string> { "All" }
};

while (!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);
    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Message Id: {message.MessageId}");
        Console.WriteLine($"Message Body: {message.Body}");

        // Just because we read the message it doesn't mean that it is deleted by the queue.
        // We actually have to explicitly delete that message to be deleted in the queue.
        // And this is done for us to acknowledge that yes we successfully process the message
        // nothing failed.
        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
    }
    await Task.Delay(3000);
}