namespace SPApi.Controls.Plc.Melsecs.MCP
{
    public interface iMCP
    {
        string MakeRequestData(CommObject obj);

        string MakeResponseData(CommObject obj);

        bool RecvParseRequestData(string message, out CommObject obj);

        bool RecvParseResponseData(string message, ref CommObject obj);

    }
}
