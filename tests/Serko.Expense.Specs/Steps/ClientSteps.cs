using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace Serko.Expense.Specs.Steps;

[Binding]
public class ClientSteps
{
    private readonly ApiContext context;

    public ClientSteps(ApiContext context)
    {
        this.context = context;
    }

    [Given(@"I have an api client for '(.*)'")]
    public void GivenIHaveAnApiClient(string url)
    {
        context.Client = new HttpClient
        {
            BaseAddress = new Uri(url)
        };
    }

    [When(@"I get the resource at '(.*)'")]
    public void WhenIGetTheResourceAt(string p0)
    {
        context.Response = context
            .Client
            .GetAsync(p0)
            .Result;
    }

    [Then(@"the result status code should be '(.*)'")]
    public void ThenTheResultStatusCodeShouldBe(int p0)
    {
        Assert.Equal(p0, (int)context.Response.StatusCode);
    }

    [Given(@"I post the resource at '(.*)'")]
    [When(@"I post the resource to '(.*)'")]
    public async Task WhenIPostTheResourceTo(string p0)
    {
        var json = JObject
            .FromObject(context.Reservation)
            .ToString();

        context.Response = await context
            .Client
            .PostAsync(p0, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    [Then(@"the number of results should not be '(.*)'")]
    public async Task ThenTheNumberOfResultsShouldNotBe(int p0)
    {
        var json = await context.Response.Content.ReadAsStringAsync();
        dynamic obj = JArray.Parse(json);

        Assert.NotEqual(p0, obj.Count);
    }

    [Then(@"the result should have the id '(.*)'")]
    public async Task ThenTheResultShouldHaveTheId(int p0)
    {
        var json = await context.Response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(json);

        Assert.Equal(p0, obj.GetValue("id").Value<int>());
    }

    [Given(@"I get the resource from the post at '(.*)'")]
    public async Task GivenIGetTheResourceFromThePost(string url)
    {
        var last = await context.Response.Content.ReadAsStringAsync();
        WhenIGetTheResourceAt($"{url}/{last}");
    }

    [Then(@"The result cost centre should be unknown")]
    public async Task ThenTheResultCostCentreShouldBeUnknown()
    {
        var json = await context.Response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(json);

        Assert.Equal("UNKNOWN", obj.GetValue("cost_centre").Value<string>());
    }

    [When(@"I post the email '(.*)' to '(.*)'")]
    public async Task WhenIPostTheEmailTo(string p0, string p1)
    {
        using var email = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"Serko.Expense.Specs.Resources.{p0}");

        var content = new StreamContent(email);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/email");
        context.Response = await context
            .Client
            .PostAsync(p1, content);            
    }
}
