namespace Sharp3D.Core.Command
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public CommandAttribute(string name = null, string description = null)
        {
            Name = name;
            Description = description;
        }
    }
}
