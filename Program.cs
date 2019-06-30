using System;

namespace SharpChatwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
			SharpChatwork.IChatworkClient e = null;
			e.room.message.SendAsync(0, "", true);
		}
    }
}
