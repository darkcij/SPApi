namespace SPApi.Declaration.Enums
{
    public enum DeviceStateType
    {
        None = 0,
        Listen = 1,
        Connecting = 2,
        Connected = 3,
        Disconnecting = 4,
        Disconnected = 5,
        Reconnecting = 6,
        Reconnected = 7,

        Success = 8,
        Failed = 9,

        Wait = 10,
        Start = 11,
        Stop = 12,

        Open = 20,
        Close = 21,
        Send = 22,
        Receive = 23
    }
}
