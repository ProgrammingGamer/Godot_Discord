# Godot_Discord
This is a repository for a easily accessible alteration of the Discord Unity RPC Example, made to work in Godot.

# Disclaimer

I own little of this code, and in addition this is a work in progress adapation, I will be working to clean this, and make it as functional as possible. To see the original project this code was adapted from go to : https://github.com/discordapp/discord-rpc/tree/master/examples/button-clicker

# Install
To install and use discord rich presence in Godot, you will need:

- The discord-rpc.dll for either or both x86, and x86_64 (New Versions Available From https://github.com/discordapp/discord-rpc/releases)
- The discordRPC.cs base file for importing the discord-rpc functions
- And a controller for initializing the discord calls and callbacks(I will have an example file)

and as with any Rich Presence Application

- You will need to create an application profile on Discords Developer Portal
https://discordapp.com/developers/applications/

# Instructions
To make use of:

Within your main file structure you can choose to place your dll wherever you please
(the default location is Plugins/x86_64)
To specify the location you need to place your DiscordRPC.cs in a location and change the dll imports to specify in relation to the root directory where the dll's are.
So from the root folder in my case it would be "Plugins/x86_64/discord-rpc" (No need to specify the dll extension)

After that you can use my discord controller to learn how to adapt the regular API to function in C#.

# Methods and Callbacks

The Event Handlers that are available are:

RequestRespondYes()
RequestRespondNo()
ReadyCallback(ref DiscordRpc.DiscordUser connectedUser)
DisconnectedCallback(int errorCode, string message)
ErrorCallback(int errorCode, string message)
JoinCallback(string secret)
SpectateCallback(string secret)
RequestCallback(ref DiscordRpc.DiscordUser request)

The Ready Callback is the callback that tells the API you have connected to a discord client, After that has occurred the rest of the callbacks become functional.
In Contrast the Disconnected Callback reports when the Discord Client has disconnected.
The Error Callback is self explanatory,(Contains Errorcode and the error message)
Join callback provides a callback key for authenticating connections.
as Does the spectate Request
The RequestCallback brings the information from a join request or spectate request and allows your scripts to decide how to handle adding or denying people in game.

# End Notes

*side note, you may need to manually add the compiles for the cs files to your csproj file for godot. 

This can be performed under the itemgroup as shown below

  <ItemGroup>
    <Compile Include="DiscordRPC.cs" />
    <Compile Include="DiscordController.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
