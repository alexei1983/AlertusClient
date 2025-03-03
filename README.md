## Alertus Client
### Web API client for Alertus in .NET 8

#### Introduction
[Alertus Technologies](https://www.alertus.com) offers an emergency management platform with panic buttons, desktop agents, 
beacons, alarms, and loudspeakers integrated into their system.

This client implements a limited number of services from the Alertus web API, including management of contacts, groups, contact group memberships, locations, 
contact methods, and system errors.

It has been tested against Alertus API version __3.19.240416__.

#### Usage

To create a new instance of the `AlertusClient` class and call the web service, you need the base URI of your Alertus instance and a username and password 
with the appropriate permission to call the API.

Then you can create the client:

```
var alertusClient = new AlertusClient("https://alertus.example.com/alertusmw/services/rest", "username", "password");
```

To create a new contact record in Alertus:

```
var newContact = new AlertusContact() 
{
  FirstName = "Jane",
  LastName = "Doe",
  JobTitle = "Emergency Coordinator",
  CountryId = "US",
  LanguageId = "en-US",
  Id = "a4bfeb42"
};

await alertusClient.Contacts.CreateOrUpdate(newContact);
```

To create a new group and add the newly created contact to the group:

```
var newGroup = new AlertusGroup() 
{
  Name = "TestGroup",
  Description = "Group for testing"
};

newGroup = await alertusClient.Groups.Create(newGroup);

if (newGroup.Id.HasValue)
  await alertusClient.ContactsGroups.Add(newGroup.Id.Value, newContact.Id)
```

To list the current Alertus system errors:

```
var currentErrors = await alertusClient.SystemErrors.List();
```

To launch an emergency activation:

```
await alertusClient.Activation.LaunchEmergency("Text of the emergency alert.");
```

To retrieve a device summary list:

```
var alertusDevices = await alertusClient.Devices.ListSummary();
```

