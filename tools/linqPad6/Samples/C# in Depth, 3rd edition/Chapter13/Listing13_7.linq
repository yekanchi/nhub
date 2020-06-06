<Query Kind="Program" />

void Main()
{
    Message message = new Message(
        from: "skeet@pobox.com",
        to: "csharp-in-depth-readers@everywhere.com",
        body: "I hope you like the third edition",
        subject: "A quick message"
    );
}

class Message
{
    public Message(string from, string to,
        string body, string subject = null,
        byte[] attachment = null)
    {
        // Normal initialization code would go here
    }
}
