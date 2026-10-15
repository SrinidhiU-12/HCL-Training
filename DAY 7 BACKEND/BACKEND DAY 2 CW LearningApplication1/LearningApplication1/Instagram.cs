namespace LearningApplication1
{
    public class Instagram
    {
        public string? UserName { get; private set; }
        public int followers { get => followers;

            set
            {
                if (value > 0) {
                    followers = value;
                    Console.WriteLine("setter inside followers");
                }
                // == --> method -->method
                // ==input validation also can be verified

                //this.followers = followers;
            } }
        public int postCount;
        public void AddStory()
        {
            Console.WriteLine("Story added");
        }
    }

  