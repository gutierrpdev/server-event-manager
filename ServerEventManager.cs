using UnityEngine;
using GraphQL;
using ServerManagement;

namespace ServerEvents
{
    /*
     * This class provides a simple collection of Object definitions and methods to log game Events from the actual games 
     * in a straightforward way, without having to incorporate any GraphQL or HTTP logic to the game code. 
     * To log an event to the server, create an instance of ServerEventManager with desired endpoint, generate an event using 
     * the definition specified within this class, and call the LogEvent method with that event as single parameter.
     */
    public class ServerEventManager
    {
        // client used to perform event logs to the specified server.
        private GraphQLClient graphQLClient;

        // url of the server one wishes to use to log their events. 
        // an example of this on a local machine would be: http://localhost:3000/graphql
        public ServerEventManager(string url)
        {
            graphQLClient = new GraphQLClient(url);
        }

        public void LogEvent(ServerEvent gameEvent){
            // We need to construct a valid graphQL query to send over to the server before we actually make a call to our
            // GraphQL client. All event logs are transactions of type 'mutation' following GraphQL's terminology, and use
            // the RootMutation logEvent which receives a json-like string with the gameEvent's information. 

            // JSON version of gameEvent (thus the need for it to be serializable) to use as input for logEvent RootMutation.
            string gameEventJSON = JsonUtility.ToJson(gameEvent);

            // complete mutation query containing the mutation 'header' and specifying that, upon successful completion of 
            // logging action, we wish to retrieve all data associated to the event we sent over the network in order to verify 
            // that it was correctly submited to the server.
            string mutation = 
                "mutation { logEvent (" 
                + gameEventJSON
                + ")} {name gameName timestamp userId parameters}";

            // corroutines cannot be called from an object that is not assigned to a GameObject. We wish to make this process
            // independent of other MonoBehaviour components, and so we make use of a convenience class to handle this issue by
            // generating an single-use gameObject to execute our corroutine.
            CoroutineLauncher.StartCoroutine(graphQLClient.SendRequest(mutation));
        }
    }
}
