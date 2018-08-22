using System;
using Godot;

public class DiscordController : Node
{
    public DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();
    public string applicationId = "345229890980937739";
    public string optionalSteamId;
    public int callbackCalls;
    public int clickCounter;
    public string pdetails = "Test Godot Discord Rich Presence";
    public DiscordRpc.DiscordUser joinRequest;

    DiscordRpc.EventHandlers handlers;

    public void Print(string s)
    {
        Console.WriteLine(s);
		GD.Print(s);
    }

    public void OnClick()
    {
        Print("Discord: on click!");
        clickCounter++;

        presence.details = string.Format(pdetails);

        DiscordRpc.UpdatePresence(presence);
    }

    public void OnDConnected()
    {
        Print("Discord: connected running special");

        pdetails = "Test Godot Discord Rich Presence";

        long epoch = DateTime.UtcNow.Second;

        Print("Epoch: "+epoch);

        long start = 0;
        start = epoch;
        long end = (epoch) + (60 * 60 * 60);

        presence.state = string.Format("Playing Solo");
        presence.details = string.Format(pdetails);
        presence.startTimestamp = start;
        presence.endTimestamp = end;

        DiscordRpc.UpdatePresence(presence);
    }

    public void RequestRespondYes()
    {
        Print("Discord: responding yes to Ask to Join request");
        DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.Yes);
    }

    public void RequestRespondNo()
    {
        Print("Discord: responding no to Ask to Join request");
        DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.No);
    }

    public void ReadyCallback(ref DiscordRpc.DiscordUser connectedUser)
    {
        ++callbackCalls;
        Print(string.Format("Discord: connected to {0}#{1}: {2}", connectedUser.username, connectedUser.discriminator, connectedUser.userId));
        OnDConnected(); 
    }

    public void DisconnectedCallback(int errorCode, string message)
    {
        ++callbackCalls;
        Print(string.Format("Discord: disconnect {0}: {1}", errorCode, message));
    }

    public void ErrorCallback(int errorCode, string message)
    {
        ++callbackCalls;
        Print(string.Format("Discord: error {0}: {1}", errorCode, message));
    }

    public void JoinCallback(string secret)
    {
        ++callbackCalls;
        Print(string.Format("Discord: join ({0})", secret));
    }

    public void SpectateCallback(string secret)
    {
        ++callbackCalls;
        Print(string.Format("Discord: spectate ({0})", secret));
    }

    public void RequestCallback(ref DiscordRpc.DiscordUser request)
    {
        ++callbackCalls;
        Print(string.Format("Discord: join request {0}#{1}: {2}", request.username, request.discriminator, request.userId));
        joinRequest = request;
    }

    public override void _Ready()
    {
		OnEnable();
    }

    public override void _Process(float delta)
    {
        DiscordRpc.RunCallbacks();
    }

    void OnEnable()
    {
        Print("Discord: init");
        callbackCalls = 0;

        handlers = new DiscordRpc.EventHandlers();
        handlers.readyCallback = ReadyCallback;
        handlers.disconnectedCallback += DisconnectedCallback;
        handlers.errorCallback += ErrorCallback;
        handlers.joinCallback += JoinCallback;
        handlers.spectateCallback += SpectateCallback;
        handlers.requestCallback += RequestCallback;
        DiscordRpc.Initialize(applicationId, ref handlers, true, optionalSteamId);
    }

    void OnDisable()
    {
        Print("Discord: shutdown");
        DiscordRpc.Shutdown();
    }
}