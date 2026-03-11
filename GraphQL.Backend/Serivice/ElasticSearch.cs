using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using static System.Net.Mime.MediaTypeNames;
using GraphQLDemo.Contracts;

namespace GraphQLDemo.Serivice
{
    public class ElasticSearch  : IElasticSearch
    {
 
            public ElasticSearch() { }

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

            public async Task<List<object>> GetData(string text)
            {
                try
                {
                    var client = await ConnectToElasticSearch();
                    //var response = await client.SearchAsync<object>(x => x.Indices("*"));
                    var response = await client.SearchAsync<object>(s => s.Indices("*").Query(q => q.QueryString(qs => qs.Query(text))));
                    var vendors = response.Documents.ToList();
                    return vendors;
                }
                catch (Exception ex)
                {

                    return new List<object>();
                }
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
                string cloudId = "Application_Insights:dXMtY2VudHJhbDEuZ2NwLmVsYXN0aWMuY2xvdWQkYmRjZDMwOWU5ZGVhNGVmNWFmMTAyMmFkNDI0NTYxYjMuZXMkYmRjZDMwOWU5ZGVhNGVmNWFmMTAyMmFkNDI0NTYxYjMua2I=";
                string apiKey = "NXNrRzBwd0I4MElScGFnUHFScXM6UTBQSnZKaFN0U2MzQ3poYVBqVk1DQQ==";
                var settings = new ElasticsearchClientSettings(cloudId, new ApiKey(apiKey));
                var client = new ElasticsearchClient(settings);
                //var response = await client.InfoAsync();  // to check connection
                return client;
            }
    }
}
