namespace SPApi.Declaration.Structs
{
    public class INI_INFO
    {
        public string Section { get; set; }
        public INI_ENTRY_INFO[] Entry { get; set; }
    }

    public class INI_ENTRY_INFO
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
