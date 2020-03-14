# server-event-manager
A set of utils to use for the intelligence assessment project in order to send game events over to the server

## Usage Notes:

To send an event over to the server, start by creating a new `ServerEventManager` object with the appropriate endpoint uri:

`eventManager = new ServerEventManager(url);`

To create a new `ServerEvent`, use the appropriate class constructor (note that the only non required field is `parameters`, which is set to null by default.)

`ServerEvent gameEvent = new ServerEvent(userId, timestamp, gameName, eventName, parameters);`

Finally, to log an event to the server, use the `logEvent` method call:

`eventManager.LogEvent(gameEvent);`

All of these definitions are included within the `ServerEvents` namespace.
