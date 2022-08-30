using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using edwinat.Portfolio.App.Api.Model;
using edwinat.Portfolio.Domain.Interfaces;
using edwinat.Portfolio.Domain.Services;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace edwinat.Portfolio.App.Api;

public class Program
{
    private IContactFormService ContactFormService;
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {

        ContactFormAlertRequest contactFormAlertRequest;
        ContactFormAlertResponse contactFormAlertResponse;

        ContactFormService = new ContactFormService();

        switch (request.HttpMethod, request.Path)
        {
            case ("POST", "/contactform"):
                var Body = JsonConvert.DeserializeObject<ContactForm>(request.Body);
                contactFormAlertRequest = new ContactFormAlertRequest
                {
                    ContactForm = new ContactForm
                    {
                        FirstName = Body.FirstName,
                        LastName = Body.LastName,
                        Email = Body.Email,
                        Phone = Body.Phone,
                        Comments = Body.Comments,
                    }
                };

                contactFormAlertResponse = await ContactFormService.ContactFormAlertAsync(contactFormAlertRequest);

                break;
            default:
                contactFormAlertResponse = new ContactFormAlertResponse
                {
                    ContactForm = null,
                    Success = false
                };
                break;
        }

        var apiGatewayProxyResponse = new APIGatewayProxyResponse();

        if (contactFormAlertResponse.Success)
        {
            apiGatewayProxyResponse.StatusCode = 200;
            apiGatewayProxyResponse.Body = JsonConvert.SerializeObject(contactFormAlertResponse);
        }
        else
        {
            apiGatewayProxyResponse.StatusCode = 500;
        }

        apiGatewayProxyResponse.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" },
            { "Access-Control-Allow-Origin", "*" },
            {"Access-Control-Allow-Methods", "HEAD, GET, POST, PUT, PATCH, DELETE" },
            {"Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token" }
    };
        return apiGatewayProxyResponse;
    }
}
