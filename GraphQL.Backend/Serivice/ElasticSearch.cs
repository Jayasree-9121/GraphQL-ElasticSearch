using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Ingest;
using Elastic.Transport;
using GraphQLDemo.Contracts;
using GraphQLDemo.Models;
using GraphQLDemo.Pipeline;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GraphQLDemo.Serivice
{
    public class ElasticSearch  : IElasticSearch
    {
        private readonly ErrorPipeline _ErrorPipeline;
            public ElasticSearch(ErrorPipeline errorPipeline) {
                _ErrorPipeline = errorPipeline;
            }

            public async Task<bool> AddData()
            {
                try
                {
                    var client = await ConnectToElasticSearch();
                    var vendor = new
                    {
                        Id = 102,
                        Name = "Apex Manufacturing Co.",
                        Category = "Industrial Equipment",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Supplier of precision-engineered components and heavy machinery."
                    };

                    var response = await client.IndexAsync(vendor, x => x.Index("vendor"));
                    if (response.IsValidResponse)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        //public async Task<List<object>> GetData(string text)
        //{
        //    try
        //    {
        //        var client = await ConnectToElasticSearch();
        //        //var response = await client.SearchAsync<object>(x => x.Indices("*"));
        //        var response = await client.SearchAsync<object>(s => s.Indices("*").Query(q => q.QueryString(qs => qs.Query(text))));
        //        var vendors = response.Documents.ToList();
        //        return vendors;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new List<object>();
        //    }
        //}

        public async Task<AIResult> GetData(string text)
        {

            try
            {
                var client = await ConnectToElasticSearch();

                var response = await client.SearchAsync<JsonElement>(s => s
                    .Indices("*")
                    .Query(q => q.QueryString(qs => qs.Query(text)))
                );

                List<string> descriptions = response.Documents
                    .Select(doc => ExtractErrorDescription(doc))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                var logs = new ErrorLog
                {
                    Message = descriptions
                };

                var result = await _ErrorPipeline.Run(logs);

                return result;

            }
            catch (Exception ex)
            {
                return new AIResult();
            }
            

        }
        private string ExtractErrorDescription(JsonElement element)
        {
            try
            {
                // 1️⃣ message
                if (element.TryGetProperty("message", out var msg) &&
                    !string.IsNullOrWhiteSpace(msg.GetString()))
                {
                    return msg.GetString();
                }

                // 2️⃣ outerMessage
                if (element.TryGetProperty("outerMessage", out var outerMsg) &&
                    !string.IsNullOrWhiteSpace(outerMsg.GetString()))
                {
                    return outerMsg.GetString();
                }

                // 3️⃣ customDimensions (JSON string)
                if (element.TryGetProperty("customDimensions", out var customDimProp))
                {
                    var customDim = customDimProp.GetString();

                    if (!string.IsNullOrWhiteSpace(customDim))
                    {
                        var json = JsonDocument.Parse(customDim);

                        if (json.RootElement.TryGetProperty("message", out var innerMsg))
                        {
                            return innerMsg.GetString();
                        }
                    }
                }

                // 4️⃣ details (JSON array string)
                if (element.TryGetProperty("details", out var detailsProp))
                {
                    var details = detailsProp.GetString();

                    if (!string.IsNullOrWhiteSpace(details))
                    {
                        var json = JsonDocument.Parse(details);

                        if (json.RootElement.ValueKind == JsonValueKind.Array &&
                            json.RootElement.GetArrayLength() > 0)
                        {
                            var first = json.RootElement[0];

                            if (first.TryGetProperty("message", out var msg2))
                            {
                                return msg2.GetString();
                            }
                        }
                    }
                }

                // 5️⃣ fallback to type
                if (element.TryGetProperty("type", out var typeProp))
                {
                    return typeProp.GetString();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AppInsights>> GetData(string indexName, string text)
        {
            var client = await ConnectToElasticSearch();

            var response = await client.SearchAsync<AppInsights>(s => s
                .Indices(indexName)
                .Query(q => q.QueryString(qs => qs.Query(text)))
            );

            return response.Documents.ToList();
        }


        //public async bool AddDocuments()
        //{
        //    try
        //    {
        //        var client = await ConnectToElasticSearch();
        //        var index = await client.Indices.CreateAsync("Vendors");
        //        var 
        //        return true;
        //    }catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public async Task<ElasticsearchClient> ConnectToElasticSearch()
            {
                string cloudId = "My_Observability_project:dXMtY2VudHJhbDEuZ2NwLmVsYXN0aWMuY2xvdWQkZGU4MmU0YWQ5NTQyNGQ3ZDljMWVjNjM2NDI2ZjAxZTIuZXMkZGU4MmU0YWQ5NTQyNGQ3ZDljMWVjNjM2NDI2ZjAxZTIua2I=";
                string apiKey = "MmlkbFBwMEJLZWx3ZGtaU0Nvc3A6YTJLWmtrLUlhZ0Y1MjgtS2hoNk5jUQ==";
                var settings = new ElasticsearchClientSettings(cloudId, new ApiKey(apiKey));
                var client = new ElasticsearchClient(settings);
                //var response = await client.InfoAsync();  // to check connection
                return client;
            }
    }
}
