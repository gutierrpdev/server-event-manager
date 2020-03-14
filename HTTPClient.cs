using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace HTTPClient
{
    /*
     * Provides a client to send simple POST requests over to a server. 
     */ 
    public class EventClient
    {
        private string url;

        // initialize with desired endpoint
        public EventClient(string url)
        {
            this.url = url;
        }

        private UnityWebRequest QueryRequest(object obj)
        {
            // convert to a valid json-formatted string that can be accepted by server.
            string json = JsonUtility.ToJson(obj);

            // encode payload as an array of bytes so that it can be easily added to the request body.
            byte[] payload = Encoding.UTF8.GetBytes(json);

            // Generate POST request.
            UnityWebRequest request = UnityWebRequest.Put(url, payload);

            // attach upload/ download handlers and set content-type header to json.
            request.uploadHandler = new UploadHandlerRaw(payload);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.method = UnityWebRequest.kHttpVerbPOST;

            return request;
        }

        public IEnumerator SendRequest(object obj)
        {
            // generate a valid request attaching obj to the request body.
            UnityWebRequest request = QueryRequest(obj);

            // send the request over the network to the server specified in constructor.
            yield return request.SendWebRequest();

            // handle and log errors.
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Request error:");
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