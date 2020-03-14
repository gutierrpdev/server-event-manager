using HTTPClient;

namespace ServerEvents
{
    /*
     * This class provides a simple collection of Object definitions and methods to log game Events from the actual games 
     * in a straightforward way, without having to incorporate any HTTP logic to the game code. 
     * To log an event to the server, create an instance of ServerEventManager with desired endpoint, generate an event using 
     * the definition specified within this class, and call the LogEvent method with that event as single parameter.
     */
    public class ServerEventManager
    {
        // client used to perform event logs to the specified server.
        private EventClient eventClient;

        // url of the server one wishes to use to log their events. 
        // an example of this on a local server would be: http://localhost:8080/event
        public ServerEventManager(string url = "http://localhost:8080/event")
        {
            eventClient = new EventClient(url);
        }

        public void LogEvent(ServerEvent gameEvent){
            // corroutines cannot be called from an object that is not assigned to a GameObject. We wish to make this process
            // independent of other MonoBehaviour components, and so we make use of a convenience class to handle this issue by
            // generating an single-use gameObject to execute our corroutine.
            CoroutineLauncher.StartCoroutine(eventClient.SendRequest(gameEvent));
        }
    }
}
