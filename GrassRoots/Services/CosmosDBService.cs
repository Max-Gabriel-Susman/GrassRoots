using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Azure.Documents.Linq;
using Xamarin.Forms;
using GrassRoots.Helpers;
using GrassRoots.Models;


namespace GrassRoots.Services
{
    public class CosmosDBService
    {
        static DocumentClient docClient = null;

        static readonly string databaseName = "Users";
        static readonly string collectionName = "Items";

        // when is initalize used?
        static async Task<bool> Initialize()
        {
            if (docClient != null)
                return true;

            try
            {
                docClient = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseName),
                    new DocumentCollection { Id = collectionName },
                    new RequestOptions { OfferThroughput = 400 }
                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient = null;

                return false;
            }

            return true;
        }

        // <GetToDoItems>
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async static Task<List<DropsUser>> GetToDoItems()
        {
            var users = new List<DropsUser>();

            if (!await Initialize())
                return users;

            var todoQuery = docClient.CreateDocumentQuery<DropsUser>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .AsDocumentQuery();

            while (todoQuery.HasMoreResults)
            {
                var queryResults = await todoQuery.ExecuteNextAsync<DropsUser>();

                users.AddRange(queryResults);
            }

            return users;
        }
        // </GetToDoItems>


        //// <GetCompletedToDoItems>
        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //public async static Task<List<User>> GetCompletedToDoItems()
        //{
        //    var todos = new List<User>();

        //    if (!await Initialize())
        //        return todos;

        //    var completedToDoQuery = docClient.CreateDocumentQuery<User>(
        //        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        //        new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
        //        .AsDocumentQuery();

        //    while (completedToDoQuery.HasMoreResults)
        //    {
        //        var queryResults = await completedToDoQuery.ExecuteNextAsync<User>();

        //        todos.AddRange(queryResults);
        //    }

        //    return todos;
        //}
        //// </GetCompletedToDoItems>


        //// <CompleteToDoItem>
        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //public async static Task CompleteToDoItem(ToDoItem item)
        //{
        //    item.Completed = true;

        //    await UpdateToDoItem(item);
        //}
        //// </CompleteToDoItem>


        //// <InsertToDoItem>
        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //public async static Task InsertToDoItem(ToDoItem item)
        //{
        //    if (!await Initialize())
        //        return;

        //    await docClient.CreateDocumentAsync(
        //        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        //        item);
        //}
        // </InsertToDoItem>

        //// <DeleteToDoItem>
        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //public async static Task DeleteToDoItem(ToDoItem item)
        //{
        //    if (!await Initialize())
        //        return;

        //    var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
        //    await docClient.DeleteDocumentAsync(docUri);
        //}
        // </DeleteToDoItem>

        //// <UpdateToDoItem>
        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //public async static Task UpdateToDoItem(ToDoItem item)
        //{
        //    if (!await Initialize())
        //        return;

        //    var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
        //    await docClient.ReplaceDocumentAsync(docUri, item);
        //}
        //// </UpdateToDoItem>
    }
}
