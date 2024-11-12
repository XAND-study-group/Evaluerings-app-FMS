namespace Module.Semester.Domain.Entities
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected Subject(string name, string description)
        {
            Name = name;
            Description = description;
        }
        protected Subject() { }

        public static Subject Create(string name, string description)
        {
            return new Subject(name, description);
        }
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
