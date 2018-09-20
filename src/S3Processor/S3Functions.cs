using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Microsoft.Extensions.Logging;

namespace S3Processor
{
    public class S3Functions
    {
        private readonly ILogger<S3Functions> logger;
        private readonly IAmazonS3 s3Client;

        public S3Functions(IAmazonS3 s3Client, ILogger<S3Functions> logger)
        {
            this.logger = logger;
            this.s3Client = s3Client;
        }
        
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="s3Event"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> S3FunctionHandlerAsync(S3Event s3Event, ILambdaContext context)
        {
            context.Logger.LogLine("Logged from Lambda Context");
            this.logger.LogInformation($"Logged from {this.logger.GetType()}");

            return await Task.FromResult("Just Testing");

            //var firstRecord = s3Event.Records?.FirstOrDefault()?.S3;
            //if(firstRecord == null) return null;

            //try
            //{
            //    var response = await this.s3Client.GetObjectMetadataAsync(firstRecord.Bucket.Name, firstRecord.Object.Key);
            //    return response.Headers.ContentType;
            //}
            //catch(Exception e)
            //{
            //    context.Logger.LogLine($"Error getting object {firstRecord.Object.Key} from bucket {firstRecord.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
            //    context.Logger.LogLine(e.Message);
            //    context.Logger.LogLine(e.StackTrace);
            //    throw;
            //}
        }
    }
}
