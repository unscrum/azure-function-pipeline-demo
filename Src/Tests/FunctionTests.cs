using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Jay.FuncHubDemo.Functions;
using Jay.FuncHubDemo.Models;
using Jay.FuncHubDemo.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Jay.FuncHubDemo.Tests
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public async Task EventHubFunctions_AlphaTest()
        {
            //input to function
            var input = new FromHttpEvent
            {
                Date = DateTimeOffset.Now.AddDays(-1),
            };
            //expected output
            var expected = new FromAlphaConsumerPoco
            {
                Original = input,
                ProcessedOn = DateTimeOffset.Now
            };
            //mock for service that gets injected
            var mock = new Mock<IMyService>(MockBehavior.Strict);
            mock.Setup(p => p.GenerateFromAlphaConsumer(It.IsAny<FromHttpEvent>())).ReturnsAsync(expected);
            
            //create function
            var function = new EventHubFunctions(NullLoggerFactory.Instance, mock.Object);
            
            //call function
            var response = await function.RunAlphaAsync(input);
            
            //assert called service for alpha exactly 1 time with our input
            mock.Verify(p=>p.GenerateFromAlphaConsumer(input), Times.Once);
            mock.Verify(p=>p.GenerateFromAlphaConsumer(It.IsAny<FromHttpEvent>()), Times.Once);

            //assert didnt' call service for anything else
            mock.Verify(p=>p.GenerateFromHttpEvent(It.IsAny<HttpRequestData>()), Times.Never);

            Assert.AreSame(expected, response);
            
            Assert.Pass();
        }
        
        [Test]
        public async Task EventHubFunctions_BetaTest()
        {
            //input to function
            var input = new FromHttpEvent
            {
                Date = DateTimeOffset.Now.AddDays(-1),
            };
            //expected output
            var expected = new FromAlphaConsumerPoco
            {
                Original = input,
                ProcessedOn = DateTimeOffset.Now
            };
            //mock for service that gets injected
            var mock = new Mock<IMyService>(MockBehavior.Strict);
            
            //create function
            var function = new EventHubFunctions(NullLoggerFactory.Instance, mock.Object);
            
            //call function
            await function.RunBetaAsync(new []{input});
            
            //assert didnt' call service for anything
            mock.Verify(p=>p.GenerateFromAlphaConsumer(It.IsAny<FromHttpEvent>()), Times.Never);
            mock.Verify(p=>p.GenerateFromHttpEvent(It.IsAny<HttpRequestData>()), Times.Never);

            Assert.Pass();
        }
        
        
        [Test]
        public async Task TimerFunctions_Test()
        {
            //input to function
            var input = new TimerInfo()
            {
                ScheduleStatus = new ScheduleStatus
                {
                    Last = DateTime.Now.AddMinutes(-10),
                    Next = DateTime.Now.AddMinutes(10),
                    LastUpdated = DateTime.Now
                },
                IsPastDue = true
            };
            
            //create function
            var function = new TimerFunctions(NullLoggerFactory.Instance);
            
            //call function
            await function.RunAsync(input);
            
            Assert.Pass();
        }

        private class MockHttpCookies : HttpCookies
        {
            public override void Append(string name, string value)
            {
            }

            public override void Append(IHttpCookie cookie)
            {
            }

            public override IHttpCookie CreateNew()
            {
                return new HttpCookie("name", "value");
            }
        }

        private class MockHttpResponseData : HttpResponseData
        {
            public MockHttpResponseData(FunctionContext functionContext) : base(functionContext)
            {
            }

            public override HttpStatusCode StatusCode { get; set; } 
            public override HttpHeadersCollection Headers { get; set; } = new ();
            public override Stream Body { get; set; } = new MemoryStream();
            public override HttpCookies Cookies { get; } = new MockHttpCookies();
        }

        private class MockHttpRequestData : HttpRequestData
        {
            public MockHttpRequestData(FunctionContext functionContext) : base(functionContext)
            {
            }

            public override HttpResponseData CreateResponse()
            {
                return new MockHttpResponseData(FunctionContext);
            }

            public override Stream Body { get; }
            public override HttpHeadersCollection Headers { get; }
            public override IReadOnlyCollection<IHttpCookie> Cookies { get; }
            public override Uri Url { get; }
            public override IEnumerable<ClaimsIdentity> Identities { get; }
            public override string Method { get; }
        }

        private class MockHttpFunctionContext : FunctionContext
        {
            public override string InvocationId { get; }
            public override string FunctionId { get; }
            public override TraceContext TraceContext { get; }
            public override BindingContext BindingContext { get; }
            public override IServiceProvider InstanceServices { get; set; }
            public override FunctionDefinition FunctionDefinition { get; }
            public override IDictionary<object, object> Items { get; set; }
            public override IInvocationFeatures Features { get; }
            public override RetryContext RetryContext { get; }
        }
        
        [Test]
        public async Task HttpFunctions_PostTest()
        {
            //input to function

            var input = new MockHttpRequestData(new MockHttpFunctionContext());
            
            //expected output
            var expected = new FromHttpEvent
            {
                Date = DateTimeOffset.Now.AddDays(-1),
            };
           
            //mock for service that gets injected
            var mock = new Mock<IMyService>(MockBehavior.Strict);
            mock.Setup(p => p.GenerateFromHttpEvent(It.IsAny<HttpRequestData>())).ReturnsAsync(expected);

            //create function
            var function = new HttpFunctions(NullLoggerFactory.Instance, mock.Object);
            
            //call function
            var response = await function.RunPostAsync(input);
            
            //assert called service for http exactly 1 time with our input
            mock.Verify(p=>p.GenerateFromHttpEvent(input), Times.Once);
            mock.Verify(p=>p.GenerateFromHttpEvent(It.IsAny<HttpRequestData>()), Times.Once);

            //assert didnt' call service for anything else
            mock.Verify(p=>p.GenerateFromAlphaConsumer(It.IsAny<FromHttpEvent>()), Times.Never);

            Assert.IsInstanceOf<MultiOutput>(response);
            Assert.AreSame(expected, response.Event);
            

            Assert.Pass();
        }
        
        [Test]
        public async Task HttpFunctions_GetTest()
        {
            //input to function

            var input = new MockHttpRequestData(new MockHttpFunctionContext());
            
            //mock for service that gets injected
            var mock = new Mock<IMyService>(MockBehavior.Strict);
            
            //create function
            var function = new HttpFunctions(NullLoggerFactory.Instance, mock.Object);
            
            //call function
            var response = await function.RunGetAsync(input);
            
            //assert didnt' call service for anything
            mock.Verify(p=>p.GenerateFromAlphaConsumer(It.IsAny<FromHttpEvent>()), Times.Never);
            mock.Verify(p=>p.GenerateFromHttpEvent(It.IsAny<HttpRequestData>()), Times.Never);

            Assert.IsNotNull(response);
            response.Body.Position = 0;
            CollectionAssert.IsNotEmpty(response.Headers);
            Assert.IsTrue(response.Headers.Contains("Content-Type"));
            var types = response.Headers.GetValues("Content-Type").ToArray();
            CollectionAssert.IsNotEmpty(types);
            Assert.AreEqual(1, types.Length);
            CollectionAssert.AreEqual("text/plain; charset=utf-8", types[0]);
            using (var streamReader = new StreamReader(response.Body))
            {
                var body = await streamReader.ReadToEndAsync();
                Assert.AreEqual("Welcome to Azure Functions 5.0 with Event Hub Demo!", body);
            }

            Assert.Pass();
        }
    }
}