using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace GraphQL
{
    /*
     * Provides a client to send simple GraphQL requests over to a server. 
     */ 
    public class GraphQLClient
    {
        private string url;

        // initialize with desired GraphQL endpoint
        public GraphQLClient(string url)
        {
            this.url = url;
        }

        // made serializable just so that it can easily be converted into a json with the structure:
        // { "query" : "queryPayload"}, as explained in the GraphQL documentation page for creating clients.
        [Serializable]
        private class GraphQLQuery
        {
            public string query;
        }

        private UnityWebRequest QueryRequest(string query)
        {
            // encapsulate query within an adequate object.
            GraphQLQuery objQuery = new GraphQLQuery(){
                query = query
            };

            // convert to a valid json-formatted string that can be accepted by server.
            string json = JsonUtility.ToJson(objQuery);

            // Generate POST request (remember that all GraphQL queries and actions must use the POST HTTP verb).
            UnityWebRequest request = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST);

            // encode payload as an array of bytes so that it can be easily added to the request body.
            byte[] payload = Encoding.UTF8.GetBytes(json);

            // attach upload/ download handlers and set content-type header to json.
            request.uploadHandler = new UploadHandlerRaw(payload);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }

        public IEnumerator SendRequest(string query)
        {
            // generate a valid request from string-formated GraphQL query.
            UnityWebRequest request = QueryRequest(query);

            // send the request over the network to the server specified in constructor.
            yield return request.SendWebRequest();

            // handle and log errors.
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Request error:");
                foreach (KeyValuePair<string, string> entry in request.GetResponseHeaders())
                {
                    Debug.Log(entry.Value + "=" + entry.Key);
                }
                Debug.Log(request.error);
            }
            // print event information to verify that what's being sent to the server is the same object that we created 
            // within our game.
            else
            {
                Debug.Log("Request sent successfully!");
                Debug.Log(request.downloadHandler.text);
            }

            // dispose of the request to end the process.
            request.Dispose();
        }
    }
}